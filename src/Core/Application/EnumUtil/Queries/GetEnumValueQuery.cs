using FSH.Learn.Application.System.Menus;
using FSH.Learn.Domain.System.EnumExt;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.EnumUtil.Queries;
public class GetEnumValueQuery : IRequest<List<EnumNode>>
{

    public string EnumName { get; set; } = default!;
    public GetEnumValueQuery(string numName)
    {
        EnumName = numName;
    }
}
