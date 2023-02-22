namespace FSH.Learn.Application.Services.ImportSheet;

public class ImportSheetResultDto
{
    /// <summary>
    ///     Id.
    /// </summary>
    public DefaultIdType Id { get; set; }

    /// <summary>
    ///     总数.
    /// </summary>
    public int Total { get; set; }

    /// <summary>
    ///     错误信息.
    /// </summary>
    public IList<ErrorInfo> Errors { get; set; } = new List<ErrorInfo>();
}
