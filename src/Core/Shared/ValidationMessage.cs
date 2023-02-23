using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Shared;
// todo:use localization with resources
public static class ValidationMessage
{
    public const string FieldIsRequired = "字段{0}不可为空.";

    public const string FieldHasNaximumLength = "字段{0}必须是最大长度为'{1}'的字符串或数组.";

    public const string FieldIsNumberic = "字段{0}必须是在{1}到{2}范围内的数字";

    public const string FieldHasMinimumValue = "字段{0}必须是大于{1}的值";
}
