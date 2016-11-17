namespace Covis.Data.Repo.Contracts.Model
{
    using System.Runtime.Serialization;

    public class INode
    {
        public virtual void Accept(INodeVisitor visitor)
        {
            
        }
    }
}