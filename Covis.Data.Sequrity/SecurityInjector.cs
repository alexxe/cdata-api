// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SecurityInjector.cs" company="">
//   
// </copyright>
// <summary>
//   The where builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Covis.Data.DynamicLinq.Security
{
    using System;
    using System.Collections.Generic;

    using Covis.Data.DynamicLinq.CQuery.Contracts;
    using Covis.Data.DynamicLinq.CQuery.Contracts.Model;

    /// <summary>
    /// The where builder.
    /// </summary>
    /// <typeparam name="TEntity">
    /// </typeparam>
    public class SecurityInjector
    {
        

        #region Public Methods and Operators

        /// <summary>
        /// The append security.
        /// </summary>
        /// <param name="node">
        /// The node.
        /// </param>
        public void AppendSecurity(INode node, ISecurityContext context)
        {
            var visitor = new Visitor(context);
            this.Visit(node, visitor);
        
        }

        #endregion

        #region Methods

        /// <summary>
        /// The visit.
        /// </summary>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <exception cref="Exception">
        /// </exception>
        private void Visit(INode node,Visitor visitor)
        {
            if (node is BNode)
            {
                var binaryNode = node as BNode;
                this.Visit(binaryNode.Right, visitor);
                this.Visit(binaryNode.Left, visitor);
            }
            else if (node is ProjectorNode)
            {
                var snode = node as ProjectorNode;
                this.Visit(snode.Left, visitor);
                foreach (var binding in snode.Bindings)
                {
                    this.Visit(binding.Value, visitor);
                }

            }
            else if (node is LNode)
            {
                var lnode = node as LNode;
                this.Visit(lnode.Left, visitor);
            }

            visitor.Visit(node);

            
        }

        
        #endregion

        private class Visitor
        {
            public Visitor(ISecurityContext context)
            {
                this.SecurityContext = context;
            }
            private ISecurityContext SecurityContext { get; set; }

            private CallNode WhereNode { get; set; }

            public void Visit(INode node)
            {
                 if (node is EntryPointNode)
                {
                    // var enode = node as EntryPointNode;
                    //var entityRule = null;//this.SecurityContext.GetRule(enode.Type.Name);
                    //if (entityRule == null)
                    //{
                    //    return;
                    //}

                    //this.WhereNode = this.GetWhereClause(enode, entityRule);
                }
                
                else if(node is LNode)
                {
                    var lnode = node as LNode;
                    if (this.WhereNode != null)
                    {
                        lnode.Left = this.WhereNode;
                        this.WhereNode = null;
                    }
                }
            }

            private BinaryNode GetBinaryNode(string tableName, QuerablePropertyRule rule)
            {
                var binary = new BinaryNode(rule.Op);
                var member = new MemberNode(rule.PropertyName);
                binary.Left = member;
                object value = this.SecurityContext.Consument.GetValueForRule(
                    tableName,
                    rule);
                binary.Right = new ConstantNode(value);
                return binary;
            }

            private BinaryNode GetBinaryNode(PropertyRule rule)
            {
                var binary = new BinaryNode(rule.Op);
                var member = new MemberNode(rule.PropertyName);
                binary.Left = member;
                binary.Right = new ConstantNode(rule.Value);
                return binary;
            }

            private CallNode GetWhereClause(LNode node, IEntityRule rule)
            {
                var where = new CallNode("Where");
                where.Left = node;

                int index = 0;
                BNode temp = null;
                foreach (var prule in rule.PropertyRules)
                {
                    var propertyRule = prule as PropertyRule;
                    BinaryNode binaryNode = propertyRule != null
                                                ? this.GetBinaryNode(propertyRule)
                                                : this.GetBinaryNode(rule.TableName, (QuerablePropertyRule)prule);

                    if (index == 0)
                    {
                        temp = binaryNode;
                    }
                    else
                    {
                        var and = new BinaryNode(BinaryOp.And) { Left = temp, Right = binaryNode };
                        temp = and;
                    }

                    index++;
                }

                where.Right = temp;
                return where;
                
            }
        }

    }
}