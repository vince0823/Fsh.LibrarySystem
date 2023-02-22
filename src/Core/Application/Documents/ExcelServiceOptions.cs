using FSH.Learn.Application.Documents.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.Documents;
public class ExcelServiceOptions
{
    public ISet<ICellValueFormatProvider> CellValueFormatProviders { get; set; } = new HashSet<ICellValueFormatProvider>();

    public TemplateOptions TemplateOptions { get; set; } = TemplateOptions.Default;
}
