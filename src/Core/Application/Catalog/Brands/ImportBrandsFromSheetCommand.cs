using Ardalis.GuardClauses;
using FSH.Learn.Application.Documents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.Catalog.Brands;
public class ImportBrandsFromSheetCommand : IRequest<ImportSheetResultDto>
{
    public RowValues Rows { get; }

    public ImportBrandsFromSheetCommand(RowValues rows)
    {
        Rows = Guard.Against.Null(rows, nameof(rows));
    }

}
