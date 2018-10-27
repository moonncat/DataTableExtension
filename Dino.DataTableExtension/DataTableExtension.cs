using Dino.DataTableExtension.Attributes;
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
                            object val = ValueHandler(property.PropertyType, row[colName]);
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

        private static object NullableValueHandler(Type nullableType, object source)
        {
            if(source==DBNull.Value)
            {
                return null;
            }
            var type = nullableType.GetGenericArguments()[0];
            return ValueHandler(type, source);

        }
        private static object ValueHandler(Type type, object source)
        {
            object val = null;
            switch (type.Name)
            {
                case nameof(Int32):
                    val = Convert.ToInt32(source);
                    break;
                case nameof(Int64):
                    val = Convert.ToInt64(source);
                    break;
                case nameof(String):
                    val = source.ToString();
                    break;
                case nameof(Decimal):
                    val = Convert.ToDecimal(source);
                    break;
                case nameof(DateTime):
                    val = Convert.ToDateTime(source);
                    break;
                case nameof(Guid):
                    val = Guid.Parse(source.ToString());
                    break;
                case "Nullable`1":
                    val = NullableValueHandler(type, source);
                    break;
                default:
                    throw new NotImplementedException($"{type.FullName} is not supported yet, tell me and it will be supported very soon.");
            }
            return val;
        }
        /// <summary>
        /// convert entity into datatable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static DataTable FromEntity<T>(this T entity) where T : new()
        {
            DataTable dt = new DataTable();

            return dt;
        }
    }
}
