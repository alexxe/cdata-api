using System;
using System.Linq;
using System.Linq.Expressions;

namespace QData.SqlProvider
{
    public class Result
    {
        public Expression Expression { get; set; }

        
        public bool HasProjection { get; set; }
    }
}
