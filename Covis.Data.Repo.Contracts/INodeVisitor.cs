namespace Covis.Data.Repo.Contracts
{
    using Covis.Data.Repo.Contracts.Model;

    public interface INodeVisitor
    {
        void Visit(BinaryNode node);

        void EnterContext(CallNode node);

        void LeaveContext(CallNode node);

        void EnterContext(ProjectorNode node);

        void LeaveContext(ProjectorNode node);

        void Visit(CallNode node);

        void Visit(TakeNode node);

        void Visit(SkipNode node);

        void Visit(MemberNode node);

        void Visit(EntryPointNode node);

        void Visit(ConstantNode node);

        void Visit(ParameterNode node);

        void Visit(ProjectorNode node);
    }
}
