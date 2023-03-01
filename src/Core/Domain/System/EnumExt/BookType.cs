using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FSH.Learn.Domain.System.EnumExt;
public enum BookType
{
    [Display(Name = "社会科学")]
    SocialSciences = 1,
    [Display(Name = "自然科学")]
    NaturalScience = 2,
    [Display(Name = "中文")]
    Chinese = 3,
    [Display(Name = "外文")]
    OreignLanguage = 4,

}
