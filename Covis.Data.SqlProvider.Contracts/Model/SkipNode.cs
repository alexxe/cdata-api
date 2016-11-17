namespace Covis.Data.SqlProvider.Contracts.Model
{
   
    public class SkipNode : BNode
    {
        public SkipNode(int skip)
        {
            this.Skip = skip;
        }

       
        public int Skip { get; set; }

        public override void Accept(INodeVisitor visitor)
        {
            this.Left.Accept(visitor);
            visitor.Visit(this);
        }
    }
}
