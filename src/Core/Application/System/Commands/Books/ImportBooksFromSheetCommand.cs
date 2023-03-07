using Ardalis.GuardClauses;
using FSH.Learn.Application.Documents.Models;
using FSH.Learn.Application.Services.ImportSheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.System.Commands.Books;
public class ImportBooksFromSheetCommand : IRequest<ImportSheetResultDto>
{
    public RowValues Rows { get; }

    public ImportBooksFromSheetCommand(RowValues rows)
    {
        Rows = Guard.Against.Null(rows, nameof(rows));
    }

}
