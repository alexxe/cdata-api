using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covis.Data.DynamicLinq.CQuery.DynamicLinq
{
    public enum BinaryOperator
    {
        And = 0,

        AndAlso = 1,

        Or = 2,

        OrElse = 3
    }

    public enum CompareOperator
    {
        Equal = 4,

        GreaterThan = 5,

        GreaterThanOrEqual = 6,

        LessThan = 7,

        LessThanOrEqual = 8
    }

    public enum StringMethods
    {
        Contains = 9,

        StartsWith = 10,

        EndsWith = 11
    }

    public enum Methods
    {
        Any = 12,
        In = 13
    }
}
