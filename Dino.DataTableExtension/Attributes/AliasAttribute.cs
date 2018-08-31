using System;
using System.Collections.Generic;
using System.Text;

namespace Dino.DataTableExtension.Attributes
{
    public class AliasAttribute:Attribute
    {
        public string ColumnName { get; set; }
    }
}
