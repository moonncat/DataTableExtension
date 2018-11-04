using System;
using System.Data;
using Dino.DataTableExtension.Test.TestModels;
using Dino.DataTableExtension;
using Xunit;
using System.Collections.Generic;

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
            dt.Rows.Add(2, null, 2.0d, null, "string2", null, DateTime.Now, null, Guid.NewGuid(), null, 2.01, 2.333, 2.0f, null);
            dt.Rows.Add(2, null, 2.0d, null, "string2", null, DateTime.Now, null, Guid.NewGuid(), null, 2.01, 2.333, 2.0f, 2.333f);

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
        [Fact]
        public void ToEntityWithTypeTest()
        {
            DataTable dt = new DataTable();
            foreach (var pi in typeof(EntityModel).GetProperties())
            {
                dt.Columns.Add(pi.Name);
            }
            dt.Rows.Add(1, 1, 1.0d, null, "string1", string.Empty, DateTime.Now, null, Guid.NewGuid(), null);
            dt.Rows.Add(2, null, 2.0d, null, "string2", null, DateTime.Now, null, Guid.NewGuid(), null);
            dt.Rows.Add(2, null, 2.0d, null, "string2", null, DateTime.Now, null, Guid.NewGuid(), null, 2.01, null);


            var entitys = dt.ToEntity(typeof(AliasModel));

            Assert.NotNull(entitys);
        }
        [Fact]
        public void ToDataTableTest()
        {
            List<EntityModel> list = new List<EntityModel>();
            list.Add(new EntityModel()
            {
                ID = 1,
                Column_1 = 1,
                Column_2 = 1,
                Column_3 = null,
                Column_4 = "string2",
                Column_5 = null,
                Column_6 = DateTime.Now,
                Column_7 = null,
                Column_8 = Guid.NewGuid(),
                Column_9 = null,
                Column_10 = 2.3333,
                Column_11 = null,
                Column_12 = 2.3333f,
                Column_13 = null,
                Column_14 = TimeSpan.MinValue,
            });

            var dt = list.FromEntity<EntityModel>();

            Assert.True(dt.Rows.Count > 0);
        }
        [Fact]
        public void DataTableGroupTest()
        {
            DataTable dt = new DataTable();
            foreach (var pi in typeof(EntityModel).GetProperties())
            {
                dt.Columns.Add(new DataColumn()
                {
                    ColumnName = pi.Name,
                    DataType = pi.PropertyType.Name.Contains(nameof(Nullable)) ? pi.PropertyType.GetGenericArguments()[0] : pi.PropertyType
                });
            }
            dt.Rows.Add(2, 1, 1.0d, null, "string1", string.Empty, DateTime.Now, null, Guid.NewGuid(), null);
            dt.Rows.Add(2, null, 2.0d, null, "string2", null, DateTime.Now, null, Guid.NewGuid(), null);
            dt.Rows.Add(2, null, 2.0d, null, "string2", null, DateTime.Now, null, Guid.NewGuid(), null, 2.01, null);
            dt.Rows.Add(2, null, 2.0d, null, "string2", null, DateTime.Now, null, Guid.NewGuid(), null, 2.01, null);

            dt.Rows.Add(1, null, 2.0d, null, "string2", null, DateTime.Now, null, Guid.NewGuid(), null, 2.01, null);


            var result = dt.GroupBy(nameof(EntityModel.ID));

            Assert.True(Convert.ToInt32(result.Rows[1]["ID"]) == 1);
            Assert.True(result.Rows[1][1] == DBNull.Value);
            Assert.True(Convert.ToDouble(result.Rows[1][2]) == 2);

            Assert.True(result.Rows.Count == 2);
            Assert.True(Convert.ToInt32(result.Rows[0]["ID"]) == 2);
            Assert.True(result.Rows[0][1] == DBNull.Value);
            Assert.True(Convert.ToDouble(result.Rows[0][2]) == 7);
            Assert.True(result.Rows[0][3] == DBNull.Value);
            Assert.True(result.Rows[0][4].ToString() == "string2");
            Assert.True(result.Rows[0][5] == DBNull.Value);
            Assert.NotNull(result.Rows[0][6]);
            Assert.True(result.Rows[0][7] == DBNull.Value);
            Assert.NotNull(result.Rows[0][8]);
            Assert.True(result.Rows[0][9] == DBNull.Value);
            Assert.True(Convert.ToDouble(result.Rows[0][10]) == 4.02);
            Assert.True(result.Rows[0][11] == DBNull.Value);
        }
        [Fact]
        public void DataTableJoinTest()
        {
            DataTable dt = new DataTable();
            foreach (var pi in typeof(EntityModel).GetProperties())
            {
                dt.Columns.Add(new DataColumn()
                {
                    ColumnName = pi.Name,
                    DataType = pi.PropertyType.Name.Contains(nameof(Nullable)) ? pi.PropertyType.GetGenericArguments()[0] : pi.PropertyType
                });
            }
            dt.Rows.Add(2, 1, 1.0d, null, "string1", string.Empty, DateTime.Now, null, Guid.NewGuid(), null);
            dt.Rows.Add(2, null, 2.0d, null, "string2", null, DateTime.Now, null, Guid.NewGuid(), null);
            dt.Rows.Add(2, null, 2.0d, null, "string2", null, DateTime.Now, null, Guid.NewGuid(), null, 2.01, null);
            dt.Rows.Add(2, null, 2.0d, null, "string2", null, DateTime.Now, null, Guid.NewGuid(), null, 2.01, null);

            dt.Rows.Add(1, null, 2.0d, null, "string2", null, DateTime.Now, null, Guid.NewGuid(), null, 2.01, null);

            DataTable dtRight = new DataTable();
            dtRight.Columns.Add(new DataColumn()
            {
                ColumnName = "Column_4",
                DataType = typeof(string)
            });
            dtRight.Columns.Add(new DataColumn()
            {
                ColumnName = "RightTable_Col_1",
                DataType = typeof(int)
            });
            dtRight.Rows.Add("string1", 123);
            dtRight.Rows.Add("string2", 456);


            var result = dt.Join(dtRight, "Column_4");

            Assert.True(result.Columns.Contains("RightTable_Col_1"));
            Assert.True(Convert.ToDouble(result.Rows[0]["RightTable_Col_1"]) == 123);
            Assert.True(Convert.ToDouble(result.Rows[1]["RightTable_Col_1"]) == 456);

        }
        [Fact]
        public void DataTableSumTest()
        {
            DataTable dt = new DataTable();
            foreach (var pi in typeof(EntityModel).GetProperties())
            {
                dt.Columns.Add(new DataColumn()
                {
                    ColumnName = pi.Name,
                    DataType = pi.PropertyType.Name.Contains(nameof(Nullable)) ? pi.PropertyType.GetGenericArguments()[0] : pi.PropertyType
                });
            }
            dt.Rows.Add(2, 1, 1.0d, null, "string1", string.Empty, DateTime.Now, null, Guid.NewGuid(), null);
            dt.Rows.Add(2, null, 2.0d, null, "string2", null, DateTime.Now, null, Guid.NewGuid(), null);
            dt.Rows.Add(2, null, 2.0d, null, "string2", null, DateTime.Now, null, Guid.NewGuid(), null, 2.01, null);
            dt.Rows.Add(2, null, 2.0d, null, "string2", null, DateTime.Now, null, Guid.NewGuid(), null, 2.01, null);

            dt.Rows.Add(1, null, 2.0d, null, "string2", null, DateTime.Now, null, Guid.NewGuid(), null, 2.01, null);
            
            Assert.True(dt.Sum<int>("Column_1") == 1);
            Assert.True(dt.Sum<decimal>("Column_2") == (decimal)9);
            Assert.True(dt.Sum<double>("Column_10")- 6.03d<0.001);

        }
    }
}
