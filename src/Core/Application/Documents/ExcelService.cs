using FSH.Learn.Application.Documents.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.Documents;
internal class ExcelService : IExcelService
{
    private readonly struct TemplateField
    {
        public int ColumnIndex { get; }

        public string Name { get; }

        public TemplateField(int columnIndex, string name)
        {
            Guard.Against.Negative(columnIndex, nameof(columnIndex));

            ColumnIndex = columnIndex;
            Name = name;
        }
    }

    private readonly ILogger<ExcelService> _logger;
    private readonly ExcelServiceOptions _options;

    public ExcelService(IOptions<ExcelServiceOptions> options, ILogger<ExcelService> logger)
    {
        _logger = logger;
        _options = options.Value;
    }

    #region basic method

    /// <inheritdoc />
    public void Read(Stream stream, Action<IWorkbook> action)
    {
        Guard.Against.Null(stream, nameof(stream));
        Guard.Against.Null(action, nameof(action));

        var workbook = WorkbookFactory.Create(stream);

        using (new DisposeAction(() => workbook.Close()))
        {
            action.Invoke(workbook);
        }
    }

    /// <inheritdoc />
    public Stream Write(Action<IWorkbook> action)
    {
        Guard.Against.Null(action, nameof(action));

        var workbook = new XSSFWorkbook();
        using (new DisposeAction(workbook.Close))
        {
            action.Invoke(workbook);
            var ms = new MemoryStream();
            workbook.Write(ms, true);
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }
    }

    #endregion basic method

    /// <inheritdoc />
    public RowValues ReadValues(Stream stream, TemplateOptions options = default)
    {
        options ??= _options.TemplateOptions;
        var rowValues = new RowValues();

        Read(stream, workbook =>
        {
            var sheet = workbook.GetSheetAt((int)options.Sheet);
            var fields = GetTemplateFields(sheet, options.FieldNameRow, out _);
            Guard.Against.NullOrEmpty(fields, nameof(fields));

            foreach (IRow row in sheet)
            {
                if (row == null)
                    continue;
                if (row.RowNum < options.FirstDataRow)
                    continue;

                var values = row.Cells.Select(v => new CellValues.CellValue(v.RowIndex, v.ColumnIndex, fields[v.ColumnIndex].Name, v.GetValue()));
                rowValues.Add(new CellValues(row.RowNum, values));
            }
        });
        return rowValues;
    }

    /// <inheritdoc />
    public Stream WriteCollection<T>(
        Stream templateStream,
        ICollection<T> source,
        TemplateOptions options = default)
    {
        Guard.Against.Null(templateStream, nameof(templateStream));
        Guard.Against.Null(source, nameof(source));

        options ??= _options.TemplateOptions;
        var props = typeof(T).GetProperties();

        void WriteSheet(ISheet sheet, IEnumerable<TemplateField> fields)
        {
            var rowIndex = options.FirstDataRow;
            source.ForEach(data =>
            {
                var row = sheet.CreateRow((int)rowIndex++);
                if (data == null)
                    return;

                fields.ForEach(v =>
                {
                    var prop = Array.Find(props, o =>
                        string.Equals(o.Name, v.Name, StringComparison.InvariantCultureIgnoreCase));
                    if (prop == null)
                        return;
                    var value = prop.GetValue(data);
                    var cell = row.GetOrCreateCell(v.ColumnIndex);
                    SetCellValue(cell, value);
                });
            });
        }

        var sm = new MemoryStream();
        Read(templateStream, workbook =>
        {
            var sheet = workbook.GetSheetAt((int)options.Sheet);
            Guard.Against.Null(sheet, nameof(sheet));

            // template fields

            var fieldRow = sheet.GetRow((int)options.FieldNameRow);
            var fields = fieldRow
                ?.Select(v => new TemplateField(v.ColumnIndex, v.ToString()))
                .Where(v => !string.IsNullOrEmpty(v.Name))
                .ToArray();
            Guard.Against.NullOrEmpty(fields, nameof(fields));

            WriteSheet(sheet, fields);

            // sheet.RemoveAndShiftRow(fieldRow);

            workbook.Write(sm, false);
            sm.Seek(0, SeekOrigin.Begin);
        });

        return sm;
    }

    /// <inheritdoc />
    public Stream WriteCollection<T>(
        string templatePath,
        ICollection<T> source,
        TemplateOptions options = default)
    {
        if (!File.Exists(templatePath))
        {
            _logger.LogWarning($"Template file not existed:{templatePath}.");
            throw new FileNotFoundException("Template file not existed");
        }

        using var stream = File.Open(templatePath, FileMode.Open);
        return WriteCollection(stream, source, options);
    }

    public void SetCellValue(ICell cell, object value)
    {
        Guard.Against.Null(cell, nameof(cell));

        var valueFormatProvider = _options.CellValueFormatProviders.FirstOrDefault(v => v.Type == value?.GetType());
        var format = valueFormatProvider?.Format(value) ?? value;
        cell.SetCellValue(format?.ToString());
    }

    private static TemplateField[] GetTemplateFields(ISheet sheet, uint fieldNameRow, out IRow fieldRow)
    {
        fieldRow = sheet.GetRow((int)fieldNameRow);
        var fields = fieldRow
            ?.Select(v => new TemplateField(v.ColumnIndex, v.ToString()))
            .Where(v => !string.IsNullOrEmpty(v.Name))
            .ToArray();
        return fields;
    }
}
