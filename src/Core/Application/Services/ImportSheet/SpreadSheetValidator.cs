namespace FSH.Learn.Application.Services.ImportSheet;

public class SpreadSheetValidator
{
    public static bool IsExtensionValid(string fileName)
    {
        var extension = Path.GetExtension(fileName);
        return extension == ".xlsx" || extension == ".xls";
    }

    public static void EnsureExtensionIsValid(string fileName)
    {
        if (!IsExtensionValid(fileName))
            throw new Exception("Invalid spreadsheet format.");
    }
}
