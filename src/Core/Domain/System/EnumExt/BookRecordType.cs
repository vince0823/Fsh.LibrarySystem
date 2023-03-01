using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Domain.System.EnumExt;
public enum BookRecordType
{
    [Display(Name = "借出")]
    Lend = 1,
    [Display(Name = "归还")]
    Back = 2
}
