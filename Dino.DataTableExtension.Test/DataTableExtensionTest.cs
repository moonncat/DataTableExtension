using System;
using System.Data;
using Dino.DataTableExtension.Test.TestModels;
using Dino.DataTableExtension;
using Xunit;

namespace Dino.DataTableExtension.Test
{
    public class DataTableExtensionTest
    {
        [Fact]
        public void ToEntityTest()
        {
            DataTable dt = new DataTable();
            foreach (var pi in typeof(EntityModel).GetProperties())
            {
                dt.Columns.Add(pi.Name);
            }
            dt.Rows.Add(1, 1, 1.0d, null, "string1", string.Empty, DateTime.Now, null, Guid.NewGuid(), null);
            dt.Rows.Add(2, null, 2.0d, null, "string2", null, DateTime.Now, null, Guid.NewGuid(), null);

            var entitys = dt.ToEntity<EntityModel>();

            Assert.NotNull(entitys);
        }
        [Fact]
        public void ToEntityWithAliasTest()
        {
            DataTable dt = new DataTable();
            foreach (var pi in typeof(EntityModel).GetProperties())
            {
                dt.Columns.Add(pi.Name);
            }
            dt.Rows.Add(1, 1, 1.0d, null, "string1", string.Empty, DateTime.Now, null, Guid.NewGuid(), null);
            dt.Rows.Add(2, null, 2.0d, null, "string2", null, DateTime.Now, null, Guid.NewGuid(), null);
            dt.Rows.Add(2, null, 2.0d, null, "string2", null, DateTime.Now, null, Guid.NewGuid(), null,2.01,null);
            dt.Rows.Add(2, null, 2.0d, null, "string2", null, DateTime.Now, null, Guid.NewGuid(), null, 2.01, 2.333);

            var entitys = dt.ToEntity<AliasModel>();

            Assert.NotNull(entitys);
        }
    }
}
