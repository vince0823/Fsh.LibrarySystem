using FSH.Learn.Application.System.Menus;
using FSH.Learn.Domain.System.EnumExt;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.EnumUtil.Queries;
public class GetEnumValueQueryHandler : IRequestHandler<GetEnumValueQuery, List<EnumNode>>
{
    public Task<List<EnumNode>> Handle(GetEnumValueQuery request, CancellationToken cancellationToken)
    {
        List<EnumNode> list;
        switch (request.EnumName)
        {

            case "BookType":
                 list = EnumManager.ToSelectList(BookType.SocialSciences, false);
                 break;
            case "BookRecordType":
                 list = EnumManager.ToSelectList(BookRecordType.Back, false);
                 break;
            default:
                 throw new NotFoundException("未找到该枚举类型");
        }

        return Task.FromResult(list);
    }
}
