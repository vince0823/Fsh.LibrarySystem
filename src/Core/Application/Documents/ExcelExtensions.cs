using Ardalis.GuardClauses;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.Documents;
public static class ExcelExtensions
{
    public static ICell GetOrCreateCell(this IRow row, int columnIndex)
    {
        Guard.Against.Null(row, nameof(row));
        return row.GetCell(columnIndex) ?? row.CreateCell(columnIndex);
    }

    public static void RemoveAndShiftRow(this ISheet sheet, IRow row)
    {
        var rowIndex = row.RowNum;

        // sheet.RemoveRow(row);// bug:not working
        // sheet.ShiftRows(rowIndex + 1, lastRowNum, -1);

        sheet.ShiftRows(rowIndex + 1, sheet.LastRowNum + 1, -1);
    }

    public static object GetValue(this ICell cell)
    {
        Guard.Against.Null(cell, nameof(cell));
        return cell.CellType switch
        {
            CellType.Unknown => cell.ToString(),
            // DateUtil.IsValidExcelDate cannot determine correctly
            CellType.Numeric => DateUtil.IsCellDateFormatted(cell) ? DateTime.FromOADate(cell.NumericCellValue) : cell.NumericCellValue,
            CellType.String => cell.StringCellValue,
            CellType.Formula => cell.ToString(),
            CellType.Blank => null,
            CellType.Boolean => cell.BooleanCellValue,
            CellType.Error => null,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
