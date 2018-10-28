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
        /// convert entity into datatable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static DataTable FromEntity<T>(this T entity) where T : new()
        {
            throw new NotImplementedException();
            DataTable dt = new DataTable();
            return dt;
        }
    }
}
