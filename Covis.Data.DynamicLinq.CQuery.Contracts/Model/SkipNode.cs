using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covis.Data.DynamicLinq.CQuery.Contracts.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class SkipNode : BNode
    {
        public SkipNode(int skip)
        {
            this.Skip = skip;
        }

        [DataMember]
        public int Skip { get; set; }

        public override void Accept(INodeVisitor visitor)
        {
            this.Left.Accept(visitor);
            visitor.Visit(this);
        }
    }
}
