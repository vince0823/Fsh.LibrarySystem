using FSH.Learn.Application.Common.Interfaces;
using FSH.Learn.Application.Documents.Models;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.Documents;
public interface IExcelService : ITransientService
{
    /// <summary>
    ///     Read Excel.
    /// </summary>
    /// <param name="stream">excel stream.</param>
    /// <param name="action">action.</param>
    void Read(Stream stream, Action<IWorkbook> action);

    /// <summary>
    ///     WriteCollection Excel.
    /// </summary>
    /// <param name="action">action.</param>
    /// <remarks>Create .xlsx file.</remarks>
    /// <returns></returns>
    Stream Write(Action<IWorkbook> action);

    /// <summary>
    ///     Read specific sheet values.
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    RowValues ReadValues(Stream stream, TemplateOptions options = default);

    /// <summary>
    ///     Write collection into Excel with template.
    /// </summary>
    /// <typeparam name="T">T.</typeparam>
    /// <param name="templateStream"></param>
    /// <param name="source"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    Stream WriteCollection<T>(
        Stream templateStream,
        ICollection<T> source,
        TemplateOptions options = default);

    /// <summary>
    ///     Write collection into Excel with template.
    /// </summary>
    /// <typeparam name="T">T.</typeparam>
    /// <param name="templatePath">.xlsx file</param>
    /// <param name="source"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    Stream WriteCollection<T>(
        string templatePath,
        ICollection<T> source,
        TemplateOptions options = default);
}
