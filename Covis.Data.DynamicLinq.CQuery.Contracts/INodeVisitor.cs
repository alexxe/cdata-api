using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covis.Data.DynamicLinq.CQuery.Contracts
{
    using Covis.Data.DynamicLinq.CQuery.Contracts.Model;

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
