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
            DataTable dt = TestData.TestDataProvider.Instance.EntityTable;

            var entitys = dt.ToEntity<EntityModel>();

            Assert.NotNull(entitys);
        }
        [Fact]
        public void ToEntityWithAliasTest()
        {
            DataTable dt = TestData.TestDataProvider.Instance.EntityTable;

            var entitys = dt.ToEntity<AliasModel>();

            Assert.NotNull(entitys);
        }
        [Fact]
        public void ToEntityWithTypeTest()
        {
            DataTable dt = TestData.TestDataProvider.Instance.EntityTable;

            var entitys = dt.ToEntity(typeof(AliasModel));

            Assert.NotNull(entitys);
        }
        [Fact]
        public void ToDataTableTest()
        {

            var dt = TestData.TestDataProvider.Instance.EntityModels.FromEntity<EntityModel>();

            Assert.True(dt.Rows.Count > 0);
        }
        [Fact]
        public void DataTableGroupTest()
        {
            DataTable dt = TestData.TestDataProvider.Instance.EntityTable;

            var result = dt.GroupBy(nameof(EntityModel.ID));

            Assert.True(Convert.ToInt32(result.Rows[1]["ID"]) == 1);
            Assert.True(Convert.ToInt32(result.Rows[1][1]) == 9);
            Assert.True(Convert.ToDouble(result.Rows[1][2]) == 2);

            Assert.True(result.Rows.Count == 3);
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
            DataTable dt1 = TestData.TestDataProvider.Instance.EntityTable;

            DataTable dt2 = TestData.TestDataProvider.Instance.JoinTable;


            var result = dt1.Join(dt2, "Column_4");

            Assert.True(result.Columns.Contains("RightTable_Col_1"));
            Assert.True(Convert.ToDouble(result.Rows[0]["RightTable_Col_1"]) == 123);
            Assert.True(Convert.ToDouble(result.Rows[1]["RightTable_Col_1"]) == 456);

        }
        [Fact]
        public void DataTableSumTest()
        {
            DataTable dt = TestData.TestDataProvider.Instance.EntityTable;
            Assert.True(dt.Sum<int>("Column_1") == 10);
            Assert.True(dt.Sum<decimal>("Column_2") == (decimal)109);
            Assert.True(dt.Sum<double>("Column_10")- 106.04d<0.001);

        }
        [Fact]
        public void DataTableMaxTest()
        {
            DataTable dt = TestData.TestDataProvider.Instance.EntityTable;
            Assert.True(Convert.ToDouble(dt.Max("ID")["ID"]) == 100);

            Assert.True(Convert.ToDouble(dt.Max("Column_1")["Column_1"]) == 9);

        }
    }
}
