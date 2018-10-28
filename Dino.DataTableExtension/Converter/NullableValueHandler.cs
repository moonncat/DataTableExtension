using System;
using System.Collections.Generic;
using System.Text;

namespace Dino.DataTableExtension.Converter
{
    class NullableValueHandler
    {
        public object Convert(Type nullableType, object source)
        {
            if (source == DBNull.Value)
            {
                return null;
            }
            var type = nullableType.GetGenericArguments()[0];
            return new ValueHandler().Convert(type, source);

        }
    }
}
