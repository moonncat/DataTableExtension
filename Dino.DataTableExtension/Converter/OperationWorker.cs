﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Dino.DataTableExtension.Converter
{
    class OperationWorker
    {
        public Func<object, object,object> DbNullAction { get; set; }
        public object Plus(object item1, object item2)
        {
            switch (item2.GetType().Name)
            {
                case nameof(Int16):
                    item1 = (Int16)item2 + (item1 == DBNull.Value ? 0 : (Int16)item1);
                    break;
                case nameof(Int32):
                    item1 = (Int32)item2 + (item1 == DBNull.Value ? 0 : (Int32)item1);
                    break;
                case nameof(Int64):
                    item1 = (Int64)item2 + (item1 == DBNull.Value ? 0 : (Int64)item1);
                    break;
                case nameof(Single):
                    item1 = (Single)item2 + (item1 == DBNull.Value ? 0 : (Single)item1);
                    break;
                case nameof(Double):
                    item1 = (Double)item2 + (item1 == DBNull.Value ? 0 : (Double)item1);
                    break;
                case nameof(Decimal):
                    item1 = (Decimal)item2 + (item1 == DBNull.Value ? 0 : (Decimal)item1);
                    break;
                case nameof(TimeSpan):
                    item1 = (TimeSpan)item2 + (item1 == DBNull.Value ? TimeSpan.MinValue : (TimeSpan)item1);
                    break;
                default:
                    if (DbNullAction != null)
                    {
                        item1 = DbNullAction(item1, item2);
                    }
                    break;
            }
            return item1;
        }
        public bool LeftLarger(object item1, object item2)
        {
            if (item1 == DBNull.Value && item2 != DBNull.Value)
                return false;
            if (item1 != DBNull.Value && item2 == DBNull.Value)
                return true;

            switch (item1.GetType().Name)
            {
                case nameof(Int16):
                    return (Int16)item1 > (Int16)item2;
                case nameof(Int32):
                    return (Int32)item1> (Int32)item2;
                case nameof(Int64):
                    return (Int64)item1 > (Int64)item2;
                case nameof(Single):
                    return (Single)item1 > (Single)item2;
                case nameof(Double):
                    return (Double)item1 > (Double)item2;
                case nameof(Decimal):
                    return (Decimal)item1 > (Decimal)item2;
                case nameof(TimeSpan):
                    return (TimeSpan)item1 > (TimeSpan)item2;
                case nameof(DateTime):
                    return (DateTime)item1 > (DateTime)item2;
                case nameof(Boolean):
                    return (Boolean)item1 && (Boolean)item2;
                default:
                    bool result = false;

                    return result;
            }
        }
    }
}
