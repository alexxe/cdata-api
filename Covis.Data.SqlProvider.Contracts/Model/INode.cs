namespace Covis.Data.SqlProvider.Contracts.Model
{
    public class INode
    {
        public virtual void Accept(INodeVisitor visitor)
        {
            
        }
    }
}