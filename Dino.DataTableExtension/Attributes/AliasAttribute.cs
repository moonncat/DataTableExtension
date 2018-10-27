using System;
using System.Collections.Generic;
using System.Text;

namespace Dino.DataTableExtension.Attributes
{
    public class AliasAttribute:Attribute
    {
        public AliasAttribute(string columnName)
        {
            this.ColumnName = columnName;
        }
        public string ColumnName { get; set; }
    }
}
