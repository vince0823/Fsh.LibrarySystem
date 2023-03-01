using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Domain.System.EnumExt;
public class EnumNode
{
    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// 文本值
    /// </summary>
    public string Value { get; set; }
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 是否选中
    /// </summary>
    public bool Selected { get; set; }

}
