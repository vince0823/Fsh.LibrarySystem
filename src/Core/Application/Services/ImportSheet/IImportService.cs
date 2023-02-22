using FSH.Learn.Application.Documents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.Services.ImportSheet;
public interface IImportService : ITransientService
{
    /// <summary>
    ///     Map CellValues to specific model
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="values"></param>
    /// <returns></returns>
    TModel MapTo<TModel>(CellValues values) where TModel : new();

    /// <summary>
    ///     Try read row value
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="rowValues"></param>
    /// <param name="resolve"></param>
    /// <param name="reject"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IList<ErrorInfo>> TryReadValuesAsync<TModel>(
        RowValues rowValues,
        Func<TModel, CellValues, CancellationToken, Task> resolve,
        Func<Exception, CellValues, CancellationToken, Task> reject = null,
        CancellationToken cancellationToken = default) where TModel : new();
}
