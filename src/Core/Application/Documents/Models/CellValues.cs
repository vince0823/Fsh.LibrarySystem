using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.Documents.Models;
public class CellValues : List<CellValues.CellValue>
{
    public int RowIndex { get; }

    public CellValues(int rowIndex, IEnumerable<CellValue> values)
        : base(values)
    {
        RowIndex = rowIndex;
    }

    public CellValue this[string columnName]
    {
        get
        {
            return this.SingleOrDefault(v =>
                string.Equals(columnName, v.ColumnName, StringComparison.OrdinalIgnoreCase));
        }
    }

    public class CellValue
    {
        public int RowIndex { get; set; }

        public int ColumnIndex { get; set; }

        public string ColumnName { get; set; }

        public object Value { get; set; }

        public CellValue(
            int rowIndex,
            int columnIndex,
            string columnName,
            object value)
        {
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
            ColumnName = columnName;
            Value = value;
        }

        public static implicit operator string(CellValue value) => value?.Value as string;
        public static implicit operator double?(CellValue value) => value?.Value as double?;
        public static implicit operator DateTime?(CellValue value) => value?.Value as DateTime?;
        public static implicit operator bool?(CellValue value) => value?.Value as bool?;
    }
}
