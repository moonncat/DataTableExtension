using Dino.DataTableExtension.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dino.DataTableExtension.Test.TestModels
{
    class AliasModel
    {
        public int ID { get; set; }
        [Alias("Column_1")]
        public int? Field_1 { get; set; }
        [Alias("Column_2")]
        public decimal Field_2 { get; set; }
        [Alias("Column_3")]
        public decimal? Field_3 { get; set; }
        [Alias("Column_4")]
        public string Field_4 { get; set; }
        [Alias("Column_5")]
        public string Field_5 { get; set; }
        [Alias("Column_6")]
        public DateTime Field_6 { get; set; }
        [Alias("Column_7")]
        public DateTime? Field_7 { get; set; }
        [Alias("Column_8")]
        public Guid Field_8 { get; set; }
        [Alias("Column_9")]
        public Guid? Field_9 { get; set; }
        [Alias("Column_10")]
        public double Field_10 { get; set; }
        [Alias("Column_11")]
        public double? Field_11 { get; set; }
    }
}
