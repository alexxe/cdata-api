using System;
using System.Linq;
using System.Linq.Expressions;

namespace QData.SqlProvider
{
    public class Result
    {
        public Type SourceType { get; set; }

        public Type TargetType { get; set; }

        public Expression ResultExpression { get; set; }

        public IQueryable Queryable { get; set; }

        public bool HasProjection { get; set; }
    }
}
