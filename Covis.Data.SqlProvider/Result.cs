using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covis.Data.SqlProvider
{
    using System.Linq.Expressions;

    public class Result
    {
        public Type SourceType { get; set; }

        public Type TargetType { get; set; }

        public Expression ResultExpression { get; set; }

        public IQueryable Queryable { get; set; }

        public bool HasProjection { get; set; }
    }
}
