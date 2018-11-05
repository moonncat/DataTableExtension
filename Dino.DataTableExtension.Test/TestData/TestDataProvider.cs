using Dino.DataTableExtension.Test.TestModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Dino.DataTableExtension.Test.TestData
{
    class TestDataProvider
    {
        private TestDataProvider() { }
        private static TestDataProvider _this;

        public static TestDataProvider Instance
        {
            get
            {
                if (_this == null)
                {
                    _this = new TestDataProvider();
                }
                return _this;
            }
        } 
        public DataTable EntityTable {
            get
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

                return dt;
            }

        }
        public DataTable JoinTable
        {
            get
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn()
                {
                    ColumnName = "Column_4",
                    DataType = typeof(string)
                });
                dt.Columns.Add(new DataColumn()
                {
                    ColumnName = "RightTable_Col_1",
                    DataType = typeof(int)
                });
                dt.Rows.Add("string1", 123);
                dt.Rows.Add("string2", 456);
                return dt;
            }

        }

        public List<EntityModel> EntityModels
        {
            get
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

                return list;
            }
        }
    }
}
