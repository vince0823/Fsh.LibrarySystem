namespace FSH.Learn.Application.Services.ImportSheet;

public sealed class ErrorInfo
{
    public int RowIndex { get; set; }
    public List<string> Details { get; set; }

    public ErrorInfo(int rowIndex, List<string> details)
    {
        RowIndex = rowIndex;
        Details = details;
    }
}
