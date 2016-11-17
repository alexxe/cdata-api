namespace Covis.Data.Repo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using Covis.Data.Common;
    using Covis.Data.Json.Contracts;
    using Covis.Data.SqlProvider.Contracts;
    using Covis.Data.SqlProvider.Contracts.Model;

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
            this.Descriptor.HasProjection = false;
            this.Context.Push(new EntryPointNode(type));
                
        }

        private void VisitBinary(QNode node)
        {
            this.Visit(node.Left);
            var lNode = this.Context.Pop();
            this.Visit(node.Right);
            var rNode = this.Context.Pop();

            BinaryType op;
            if (node.Value is long)
            {
                op = (BinaryType)Convert.ToInt16(node.Value);
            }
            else
            {
                Enum.TryParse(Convert.ToString(node.Value), out op);
            }


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
            MethodType method;
            if (node.Value is long)
            {
                method = (MethodType)Convert.ToInt16(node.Value);
            }
            else
            {
                Enum.TryParse(Convert.ToString(node.Value), out method);
            }

            this.Visit(node.Left);
            var lNode = this.Context.Pop();

            if (method == MethodType.Select)
            {
                var call = new ProjectorNode() { Left = (LNode)lNode };
                var root = node.Right;
                do
                {
                    this.Visit(root);
                    var rNode = this.Context.Pop();
                    call.Bindings.Add(((MemberNode)rNode).Member, rNode);
                    root = root.Left;
                }
                while (root != null);
                this.Context.Push(call);
                this.Descriptor.HasProjection = true;
            }
            else
            {
                
                this.Visit(node.Right);
                var rNode = this.Context.Pop();

                var call = new CallNode(method) { Left = (LNode)lNode, Right = rNode };
                this.Context.Push(call);
            }
            


        }
    }
}
