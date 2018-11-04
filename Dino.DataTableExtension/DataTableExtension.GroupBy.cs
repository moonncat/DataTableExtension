using Dino.DataTableExtension.Converter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Dino.DataTableExtension
{
    public static partial class DataTableExtension
    {
        /// <summary>
        /// group by specific Colunm, to sum up others Column value which is type of int/double/decimal etc.
        /// </summary>
        /// <param name="sourceTable"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static DataTable GroupBy(this DataTable sourceTable, string columnName)
        {
            DataTable midTable = sourceTable.Copy();

            string marker = $"Column_{Guid.NewGuid().ToString("N")}";
            string removeMarker = Guid.NewGuid().ToString("N");
            midTable.Columns.Add(marker);
            int colIndex = midTable.Columns.IndexOf(columnName);

            foreach (var raw_row in midTable.Select())
            {
                DataRow[] resultRows = midTable.Select($"{columnName}='{raw_row[columnName]}' and {marker} is null");//
                if (resultRows.Length < 1)
                    continue;

                DataRow targetRow = midTable.Rows.Add();

                foreach (var row in resultRows)
                {
                    for (int i = 0; i < row.ItemArray.Length; i++)
                    {
                        object item = row.ItemArray[i];
                        if (colIndex == i)
                        {
                            targetRow[i] = item;
                            continue;
                        }

                        OperationWorker pw = new OperationWorker()
                        {
                            DbNullAction = (x, y) => { x = y; return x; }
                        };
                        targetRow[i] = pw.Plus(targetRow[i], item);
                        
                    }
                    row[marker] = removeMarker;
                }
                targetRow[marker] = marker;
            }
            foreach (var row in midTable.Select($"{marker}='{removeMarker}'"))
            {
                midTable.Rows.Remove(row);
            }
            midTable.Columns.Remove(marker);
            return midTable;
        }
    }
}
