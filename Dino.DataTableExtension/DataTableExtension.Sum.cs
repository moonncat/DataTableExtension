using Dino.DataTableExtension.Converter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Dino.DataTableExtension
{
    public static partial class DataTableExtension
    {
        public static T Sum<T>(this DataTable dt, string columName)
        {
            object result = DBNull.Value;
            OperationWorker pw = new OperationWorker();
            foreach (DataRow row in dt.Select())
            {
                result = pw.Plus(result, row[columName]);
            }

            return (T)result;
        }
    }
}
