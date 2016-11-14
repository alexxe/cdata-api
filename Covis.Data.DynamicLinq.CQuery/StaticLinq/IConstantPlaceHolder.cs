using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covis.Data.DynamicLinq.CQuery.StaticLinq
{
    public interface IConstantPlaceHolder
    {
        object GetValue();

        bool IsEmpty { get; set; }
    }
}
