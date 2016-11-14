namespace Covis.Data.DynamicLinq.CQuery.Contracts.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    [KnownType(typeof(BNode))]
    [KnownType(typeof(ConstantNode))]
    [KnownType(typeof(LNode))]
    public class INode
    {
        public virtual void Accept(INodeVisitor visitor)
        {
            
        }
    }
}