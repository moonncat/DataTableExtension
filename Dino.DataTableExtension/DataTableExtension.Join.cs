using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;

namespace Dino.DataTableExtension
{
    public static partial class DataTableExtension
    {
        /// <summary>
        /// join right table with left table with sharing the same column name
        /// </summary>
        /// <param name="leftTable"></param>
        /// <param name="rightTable"></param>
        /// <param name="columName"></param>
        /// <returns></returns>
        public static DataTable Join(this DataTable leftTable, DataTable rightTable, string columName)
        {
            DataTable midTable = leftTable.Copy();
            foreach (DataColumn col in rightTable.Columns)
            {
                if (!midTable.Columns.Contains(col.ColumnName))
                {
                    midTable.Columns.Add(col.ColumnName, col.DataType);
                }
            }
            foreach(DataRow lRow in midTable.Select())
            {
                var rRow = (rightTable.Select($"{columName}='{lRow[columName]}'")).FirstOrDefault();
                if (rRow != null)
                {
                    foreach(DataColumn col in rightTable.Columns)
                    {
                        lRow[col.ColumnName] = rRow[col.ColumnName];
                    }
                }
            }

            return midTable;
        }
    }
}
