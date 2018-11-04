using Dino.DataTableExtension.Attributes;
using Dino.DataTableExtension.Converter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Dino.DataTableExtension
{
    public static class DataTableExtension
    {
        /// <summary>
        /// convert datatable into entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ToEntity<T>(this DataTable dt) where T : new()
        {
            List<T> entity_list = new List<T>();
            foreach (var row in dt.Select())
            {
                try
                {
                    var entity = new T();
                    foreach (var property in typeof(T).GetProperties())
                    {
                        string colName = property.Name;
                        var aliasAttr = property.GetCustomAttributes().FirstOrDefault(a => a as AliasAttribute != null);
                        if (aliasAttr != null)
                        {
                            colName = aliasAttr.GetType().GetProperty(nameof(AliasAttribute.ColumnName)).GetValue(aliasAttr) as string;
                        }
                        if (dt.Columns.Contains(colName))
                        {
                            object val = new ValueHandler().Convert(property.PropertyType, row[colName]);
                            property.SetValue(entity, val);
                        }
                    }
                    entity_list.Add(entity);
                }catch(Exception ex)
                {
                    throw new Exception($"{ex.Message}: \r\n{string.Join(",", row.ItemArray)}", ex);
                }
            }
            return entity_list;
        }
        public static object ToEntity(this DataTable dt,Type type)
        {
            MethodInfo[] methods = typeof(DataTableExtension)
                .GetMethods(BindingFlags.Public | BindingFlags.Static);

            MethodInfo method = methods
                .FirstOrDefault(m => m.Name == nameof(DataTableExtension.ToEntity) && m.ContainsGenericParameters == true)
                .MakeGenericMethod(type);
            return method.Invoke(null, new[] { dt });
        }
        /// <summary>
        /// convert list of entities into datatable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static DataTable FromEntity<T>(this List<T> entities)
        {
            DataTable dt = new DataTable();
            foreach (var property in typeof(T).GetProperties())
            {
                string colName = property.Name;
                //if enable below lines, DataTable will add Columns with Alias name
                //var aliasAttr = property.GetCustomAttributes().FirstOrDefault(a => a as AliasAttribute != null);
                //if (aliasAttr != null)
                //{
                //    colName = aliasAttr.GetType().GetProperty(nameof(AliasAttribute.ColumnName)).GetValue(aliasAttr) as string;
                //}

                //todo: Message: System.NotSupportedException : DataSet does not support System.Nullable<>.
                dt.Columns.Add(new DataColumn() { ColumnName = colName });
            }
            foreach (var entity in entities)
            {
                DataRow row = dt.Rows.Add(
                    (from p in typeof(T).GetProperties()
                     where dt.Columns.Contains(p.Name)
                     select p.GetValue(entity)).ToArray()
                     );

            }
            return dt;
        }
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

                        switch (item.GetType().Name)
                        {
                            case nameof(Int16):
                                Int16 r1 = targetRow[i] == DBNull.Value ? (short)0 : (short)targetRow[i];
                                r1 += (Int16)item;
                                targetRow[i] = r1;
                                break;
                            case nameof(Int32):
                                Int32 r2 = targetRow[i]==DBNull.Value?0:(Int32)targetRow[i];
                                r2 += (Int32)item;
                                targetRow[i] = r2;
                                break;
                            case nameof(Int64):
                                Int64 r3 = targetRow[i] == DBNull.Value ? 0 : (Int64)targetRow[i];
                                r3 += (Int64)item;
                                targetRow[i] = r3;
                                break;
                            case nameof(Single):
                                Single r4 = targetRow[i] == DBNull.Value ? 0 : (Single)targetRow[i];
                                r4 += (Single)item;
                                targetRow[i] = r4;
                                break;
                            case nameof(Double):
                                Double r5 = targetRow[i] == DBNull.Value ? 0 : (Double)targetRow[i];
                                r5 += (Double)item;
                                targetRow[i] = r5;
                                break;
                            case nameof(Decimal):
                                Decimal r6 = targetRow[i] == DBNull.Value ? 0:(Decimal)targetRow[i];
                                r6 += (Decimal)item;
                                targetRow[i] = r6;
                                break;
                            default:
                                targetRow[i] = item;
                                break;
                        }
                    }
                    row[marker] = removeMarker;
                }
                targetRow[marker] = marker;
            }
            foreach(var row in midTable.Select($"{marker}='{removeMarker}'"))
            {
                midTable.Rows.Remove(row);
            }
            midTable.Columns.Remove(marker);
            return midTable;
        }
    }
}
