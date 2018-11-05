using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Dino.DataTableExtension
{
    public static partial class DataTableExtension
    {
        /// <summary>
        /// to exclude those DataRows in subTable from original Table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <param name="subTable"></param>
        /// <returns></returns>
        public static DataTable Exculde<T>(this DataTable dt, DataTable subTable,string columnName)
        {
            DataTable result = dt.Copy();

            foreach (DataRow row in subTable.Select())
            {
                var rows = dt.Select($"{columnName}='{row[columnName]}'");
                foreach(var r in rows)
                {
                    result.Rows.Remove(r);
                }
            }

            return result;
        }
    }
}
