using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.Documents.Abstractions;
public interface ICellValueFormatProvider
{
    Type Type { get; }

    object Format(object value);
}
