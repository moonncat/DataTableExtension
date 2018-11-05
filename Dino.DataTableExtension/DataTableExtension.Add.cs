using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Dino.DataTableExtension
{
    public static partial class DataTableExtension
    {
        /// <summary>
        /// add another table's DataRows which are sharing the same schema
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <param name="subTable"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static DataTable Add<T>(this DataTable dt, DataTable table)
        {
            DataTable result = dt.Copy();

            foreach (DataRow row in table.Select())
            {
                result.Rows.Add(row.ItemArray);
            }

            return result;
        }
    }
}
