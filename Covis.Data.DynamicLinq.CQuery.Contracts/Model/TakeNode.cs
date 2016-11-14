using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covis.Data.DynamicLinq.CQuery.Contracts.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class TakeNode : BNode
    {
        public TakeNode(int take)
        {
            this.Take = take;
        }

        [DataMember]
        public int Take { get; set; }

        public override void Accept(INodeVisitor visitor)
        {
            this.Left.Accept(visitor);
            visitor.Visit(this);
        }
    }
}
