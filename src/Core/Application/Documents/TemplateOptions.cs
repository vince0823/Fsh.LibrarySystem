using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.Documents;
public class TemplateOptions
{
    public uint Sheet { get; set; }

    public uint FieldNameRow { get; set; }

    public uint FirstDataRow { get; set; }

    public TemplateOptions(
        uint sheet,
        uint fieldNameRow,
        uint firstDataRow)
    {
        Sheet = sheet;
        FieldNameRow = fieldNameRow;
        FirstDataRow = firstDataRow;
    }

    public static TemplateOptions Default = new(0, 0, 2);
}
