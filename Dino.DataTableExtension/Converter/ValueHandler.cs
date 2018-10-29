﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Dino.DataTableExtension.Converter
{
    class ValueHandler
    {
        public object Convert(Type type, object source)
        {
            bool isDbNull = source == DBNull.Value;
            object val = null;
            try
            {
                switch (type.Name)
                {
                    case nameof(Int32):
                        if (isDbNull)
                            val = 0;
                        else
                            val = System.Convert.ToInt32(source);
                        break;
                    case nameof(Int64):
                        if (isDbNull)
                            val = 0;
                        else
                            val = System.Convert.ToInt64(source);
                        break;
                    case nameof(Double):
                        if (isDbNull)
                            val = 0;
                        else
                            val = System.Convert.ToDouble(source);
                        break;
                    case nameof(Decimal):
                        if (isDbNull)
                            val = 0;
                        else
                            val = System.Convert.ToDecimal(source);
                        break;
                    case nameof(Single):
                        if (isDbNull)
                            val = 0;
                        else
                            val = System.Convert.ToSingle(source);
                        break;
                    case nameof(String):
                        val = source.ToString();
                        break;
                    case nameof(DateTime):
                        if (isDbNull)
                            val = DateTime.MinValue;
                        else
                            val = System.Convert.ToDateTime(source);
                        break;
                    case nameof(TimeSpan):
                        if (isDbNull)
                            val = TimeSpan.MinValue;
                        else
                            val = TimeSpan.Parse(source.ToString());
                        break;
                    case nameof(Guid):
                        if (isDbNull)
                            val = Guid.Empty;
                        else
                            val = Guid.Parse(source.ToString());
                        break;
                    case "Nullable`1":
                        val = new NullableValueHandler().Convert(type, source);
                        break;
                    default:
                        System.Diagnostics.Debug.WriteLine($"{type.FullName} is not supported yet, pls kick a issue on https://github.com/moonncat/DataTableExtension.");
                        val = source;
                        break;
                }
            }catch(Exception ex)
            {
                throw new Exception($"{ex.Message}: \r\n{source}", ex);
            }
            return val;
        }

    }
}
