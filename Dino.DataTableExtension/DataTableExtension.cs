using Dino.DataTableExtension.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Dino.DataTableExtension
{
    public static class DataTableExtension
    {
        public static List<T> ToEntity<T>(this DataTable dt) where T : new()
        {
            List<T> entity_list = new List<T>();
            foreach (var row in dt.Select())
            {
                var entity = new T();
                foreach (var property in typeof(T).GetProperties())
                {
                    string colName = property.Name;
                    var attrs = property.GetType().GetCustomAttributes(typeof(AliasAttribute), false);
                    if (attrs != null && attrs.Length > 0)
                    {
                        colName = attrs[0].GetType().GetProperty(nameof(AliasAttribute.ColumnName)).GetValue(attrs[0]) as string;
                    }
                    if (dt.Columns.Contains(colName))
                    {
                        object val = ValueHandler(property.PropertyType, row[property.Name]);
                        property.SetValue(entity, val);
                    }
                }
                entity_list.Add(entity);
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
    }
}
