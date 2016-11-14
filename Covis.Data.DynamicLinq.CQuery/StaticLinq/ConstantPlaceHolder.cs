using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covis.Data.DynamicLinq.CQuery.StaticLinq
{
    public class ConstantPlaceHolder<T> : IConstantPlaceHolder
    {
        public T Value { get; set; }

        public bool IsEmpty { get; set; } 
        public object GetValue()
        {
            return this.Value;
        }
    }
}
