using Dino.DataTableExtension.Converter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Dino.DataTableExtension
{
    public static partial class DataTableExtension
    {
        public static DataRow LastOrDefault(this DataTable dt)
        {
            int rowCount = dt.Rows.Count;
            if (rowCount > 0)
                return dt.Rows[rowCount - 1];
            else
                return null;
        }
        public static DataRow FirstOrDefault(this DataTable dt)
        {
            int rowCount = dt.Rows.Count;
            if (rowCount > 0)
                return dt.Rows[0];
            else
                return null;
        }
        public static DataRow Max(this DataTable dt,string columnName)
        {
            int rowCount = dt.Rows.Count;
            DataRow row = dt.FirstOrDefault();
            if (row == null)
                return row;

            OperationWorker pw = new OperationWorker();
            for (int i = 1; i < rowCount; i++)
            {
                if (pw.LeftLarger(dt.Rows[i][columnName], row[columnName] ))
                {
                    row = dt.Rows[i];
                }
            }
            return row;
        }

    }
}
