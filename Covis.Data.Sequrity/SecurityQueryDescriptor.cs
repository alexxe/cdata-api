using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covis.Data.DynamicLinq.Security
{
    using Covis.Data.DynamicLinq.CQuery.Contracts;

    public class SecurityQueryDescriptor<TEntity> where TEntity : class
    {
        public QueryDescriptor QueryDescriptor { get; set; }
        public ISecurityContext<TEntity> SecurityContext { get; set; }
    }
}
