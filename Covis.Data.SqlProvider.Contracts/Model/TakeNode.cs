namespace Covis.Data.SqlProvider.Contracts.Model
{
    
    public class TakeNode : BNode
    {
        public TakeNode(int take)
        {
            this.Take = take;
        }

        
        public int Take { get; set; }

        public override void Accept(INodeVisitor visitor)
        {
            this.Left.Accept(visitor);
            visitor.Visit(this);
        }
    }
}
