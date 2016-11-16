using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covis.Data.DynamicLinq.CQuery.Contracts.Contract
{
    using AutoMapper;

    using Covis.Data.DynamicLinq.CQuery.Contracts.Model;

    public class QNodeConverter
    {
        private readonly MapperConfiguration mapperConfiguration;

        public Stack<INode> Context { get; set; }
        public QNodeConverter(MapperConfiguration mapperConfiguration)
        {
            this.mapperConfiguration = mapperConfiguration;
            this.Context = new Stack<INode>();
        }
        public QueryDescriptor Descriptor { get; private set; }
        public void Visit(QNode node)
        {
            if (node.Type == NodeType.Querable)
            {
                this.VisitQuerable(node);
            }

            if (node.Type == NodeType.Binary)
            {
                this.VisitBinary(node);
            }

            if (node.Type == NodeType.Constant)
            {
                this.VisitConstant(node);
            }

            if (node.Type == NodeType.Member)
            {
                this.VisitMember(node);
            }

            if (node.Type == NodeType.Method)
            {
                this.VisitMethod(node);
            }
        }

        private void VisitQuerable(QNode node)
        {
            var type = this.mapperConfiguration.GetAllTypeMaps()
                    .FirstOrDefault(x => x.DestinationType.Name.Contains(Convert.ToString(node.Value))).DestinationType;
            this.Descriptor = new QueryDescriptor(type);
            this.Context.Push(new EntryPointNode(type));
                
        }

        private void VisitBinary(QNode node)
        {
            this.Visit(node.Left);
            var lNode = this.Context.Pop();
            this.Visit(node.Right);
            var rNode = this.Context.Pop();

            var op = (BinaryType)Convert.ToInt16(node.Value);
            var binary = new BinaryNode(op) { Left = (LNode)lNode, Right = rNode };

            this.Context.Push(binary);
        }

        private void VisitConstant(QNode node)
        {
            var constant = new ConstantNode(Convert.ToString(node.Value));
            this.Context.Push(constant);
        }

        private void VisitMember(QNode node)
        {
            var member = new MemberNode(Convert.ToString(node.Value));
            this.Context.Push(member);
        }

        private void VisitMethod(QNode node)
        {
            this.Visit(node.Left);
            var lNode = this.Context.Pop();
            this.Visit(node.Right);
            var rNode = this.Context.Pop();

            var method = (MethodType)Convert.ToInt16(node.Value);
            var call = new CallNode(method) { Left = (LNode)lNode, Right = rNode };

            this.Context.Push(call);


        }
    }
}
