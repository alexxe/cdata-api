// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExpressionParser.cs" company="">
//   
// </copyright>
// <summary>
//   The my expression visitor.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Qdata.Json.Contract;
using QData.Common;


namespace QData.LinqConverter
{
    /// <summary>
    ///     The my expression visitor.
    /// </summary>
    public class ExpressionConverter : ExpressionVisitor
    {
        // : ExpressionVisitor

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExpressionConverter" /> class.
        /// </summary>
        public ExpressionConverter()
        {
            this.Context = new Stack<QNode>();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the context.
        /// </summary>
        private Stack<QNode> Context { get; set; }

        #endregion

        #region Public Methods and Operators

        //public ProjectorNode Convert<TModelEntity>(Expression<Func<TModelEntity, TModelEntity>> exp) where TModelEntity : IModelEntity
        //{
        //    this.Visit(exp);
        //    return (ProjectorNode)this.Context.Peek();
        //}

        //public ProjectorNode Convert<TModelEntity>(Expression<Func<TModelEntity, IClientProjector>> exp) where TModelEntity : IModelEntity
        //{
        //    this.Visit(exp);
        //    return (ProjectorNode)this.Context.Peek();
        //}

        //public BNode Convert<TModelEntity>(Expression<Func<TModelEntity, bool>> exp) where TModelEntity : IModelEntity
        //{
        //    this.Visit(exp);
        //    return (BNode)this.Context.Peek();
        //}

        public QNode Convert(Expression exp)
        {
            this.Visit(exp);
            return this.Context.Peek();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The visit.
        /// </summary>
        /// <param name="exp">
        ///     The exp.
        /// </param>
        /// <returns>
        ///     The <see cref="Expression" />.
        /// </returns>
        public override Expression Visit(Expression exp)
        {
            if (exp == null)
            {
                return exp;
            }

            switch (exp.NodeType)
            {
                case ExpressionType.Negate:
                case ExpressionType.NegateChecked:
                case ExpressionType.Not:
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                case ExpressionType.ArrayLength:
                case ExpressionType.Quote:
                case ExpressionType.TypeAs:
                    return this.VisitUnary((UnaryExpression)exp);
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                case ExpressionType.Divide:
                case ExpressionType.Modulo:
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.Coalesce:
                case ExpressionType.ArrayIndex:
                case ExpressionType.RightShift:
                case ExpressionType.LeftShift:
                case ExpressionType.ExclusiveOr:
                    var expression = this.VisitBinary((BinaryExpression)exp);
                    return expression;
                case ExpressionType.TypeIs:
                case ExpressionType.Conditional:
                case ExpressionType.Constant:
                    return this.VisitConstant((ConstantExpression)exp);
                case ExpressionType.Parameter:
                    return this.VisitParameter((ParameterExpression)exp);
                case ExpressionType.MemberAccess:
                    return this.VisitMemberAccess((MemberExpression)exp);
                case ExpressionType.Call:
                    return this.VisitMethodCall((MethodCallExpression)exp);
                case ExpressionType.Lambda:
                    return this.VisitLambda((LambdaExpression)exp);
                case ExpressionType.New:
                    return this.VisitNew((NewExpression)exp);
                case ExpressionType.NewArrayInit:
                case ExpressionType.NewArrayBounds:

                // return this.VisitNewArray((NewArrayExpression)exp);
                case ExpressionType.Invoke:

                    // return this.VisitInvocation((InvocationExpression)exp);
                    return exp;
                case ExpressionType.MemberInit:
                    return this.VisitMemberInit((MemberInitExpression)exp);
                case ExpressionType.ListInit:

                    // return this.VisitListInit((ListInitExpression)exp);
                    return exp;
                default:
                    throw new Exception(string.Format("Unhandled expression type: '{0}'", exp.NodeType));
            }
        }

        /// <summary>
        ///     The visit binary.
        /// </summary>
        /// <param name="b">
        ///     The b.
        /// </param>
        /// <returns>
        ///     The <see cref="Expression" />.
        /// </returns>
        protected override Expression VisitBinary(BinaryExpression expression)
        {
            BinaryType op;
            if (!BinaryType.TryParse(expression.NodeType.ToString(), out op))
            {
                if (expression.NodeType == ExpressionType.OrElse)
                {
                    op = BinaryType.Or;
                }
                else if (expression.NodeType == ExpressionType.AndAlso)
                {
                    op = BinaryType.And;
                }
                else
                {
                    throw new Exception(expression.NodeType.ToString());
                }
            }

            var node = new QNode() { Type = NodeType.Binary, Value = op };
            this.Visit(expression.Left);
            node.Left = this.Context.Pop();
            this.Visit(expression.Right);
            node.Right = this.Context.Pop();

            this.Context.Push(node);

            return expression;
        }

        /// <summary>
        ///     The visit constant.
        /// </summary>
        /// <param name="c">
        ///     The c.
        /// </param>
        /// <returns>
        ///     The <see cref="Expression" />.
        /// </returns>
        protected override Expression VisitConstant(ConstantExpression expr)
        {
            if (expr.Type.IsGenericType && typeof(IModelEntity).IsAssignableFrom(expr.Type.GenericTypeArguments[0]))
            {
                this.VisitModelEntity(expr);
            }
            else
            {
                var value = this.ResolveValue(expr);
                var node = new QNode() { Type = NodeType.Constant, Value = value };
                this.Context.Push(node);
            }
            
            return expr;
        }

        private void VisitModelEntity(ConstantExpression expr)
        {
            var node = new QNode() { Type = NodeType.Querable, Value = expr.Type.GenericTypeArguments[0].Name };
            this.Context.Push(node);
        }

        /// <summary>
        ///     The visit lambda.
        /// </summary>
        /// <param name="lambda">
        ///     The lambda.
        /// </param>
        /// <returns>
        ///     The <see cref="Expression" />.
        /// </returns>
        protected virtual Expression VisitLambda(LambdaExpression lambda)
        {
            this.Visit(lambda.Body);
            return lambda;
        }

        /// <summary>
        ///     The visit member access.
        /// </summary>
        /// <param name="memberAccess">
        ///     The memberAccess.
        /// </param>
        /// <returns>
        ///     The <see cref="Expression" />.
        /// </returns>
        protected virtual Expression VisitMemberAccess(MemberExpression memberAccess)
        {
            if (memberAccess.Expression.Type.IsGenericType
                && typeof(IConstantPlaceHolder).IsAssignableFrom(memberAccess.Expression.Type))
            {
                var func = Expression.Lambda(memberAccess.Expression).Compile();
                var ph = (IConstantPlaceHolder)func.DynamicInvoke();
                var value = ph.GetValue();
                var node = new QNode() { Type = NodeType.Constant, Value = value };
                this.Context.Push(node);
                return memberAccess;
            }
            if (memberAccess.Expression.NodeType == ExpressionType.Constant)
            {
                var value = this.ResolveValue(memberAccess);
                var node = new QNode() { Type = NodeType.Constant, Value = value };
                this.Context.Push(node);
            }
            else
            {
                var builder = new MemberNodeBuilder();
                builder.Visit(memberAccess);
                var node = new QNode() { Type = NodeType.Member, Value = builder.GetPath() };
                this.Context.Push(node);
            }

            return memberAccess;
        }

        /// <summary>
        ///     The visit member init.
        /// </summary>
        /// <param name="exp">
        ///     The exp.
        /// </param>
        /// <returns>
        ///     The <see cref="Expression" />.
        /// </returns>
        protected override Expression VisitMemberInit(MemberInitExpression exp)
        {
            QNode bindingNode = null;
            for (int i = 0; i < exp.Bindings.Count; i++)
            {
                var builder = new MemberNodeBuilder();
                builder.Visit(((MemberAssignment)exp.Bindings[i]).Expression);
                var node = new QNode() { Type = NodeType.Member, Value = exp.Bindings[i].Member.Name + ":" + builder.GetPath() };
                if (bindingNode == null)
                {
                    bindingNode = node;
                }
                else
                {
                    bindingNode.Left = node;
                }
            }

            this.Context.Push(bindingNode);
            return exp;
        }

        /// <summary>
        ///     The visit method call.
        /// </summary>
        /// <param name="m">
        ///     The memberAccess.
        /// </param>
        /// <returns>
        ///     The <see cref="Expression" />.
        /// </returns>
        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            QNode node;
            MethodType method;
            BinaryType binary;
            if(MethodType.TryParse(m.Method.Name,out method))
            {
                node = new QNode() { Type = NodeType.Method, Value = method };
            }
            else if(BinaryType.TryParse(m.Method.Name,out binary))
            {
                node = new QNode() { Type = NodeType.Binary, Value = binary };
            }
            else
            {
                throw new Exception(m.Method.Name);
            }
             

            if (m.Object != null)
            {
                if (m.Arguments[0].NodeType == ExpressionType.MemberAccess)
                {
                    if (typeof(IConstantPlaceHolder).IsAssignableFrom(
                        ((MemberExpression)m.Arguments[0]).Expression.Type))
                    {
                        this.Visit(m.Object);
                        node.Left = this.Context.Pop();
                        this.Visit(m.Arguments[0]);
                        node.Right = this.Context.Pop();
                    }
                    else
                    {
                        //node = new CallNode("In");
                        //this.Visit(m.Arguments[0]);
                        //node.Left = this.Context.Pop();
                        //this.Visit(m.Object);
                        //node.Right = this.Context.Pop();
                    }
                }
                else
                {
                    this.Visit(m.Object);
                    node.Left = this.Context.Pop();
                    this.Visit(m.Arguments[0]);
                    node.Right = this.Context.Pop();
                }
            }
            else
            {
                this.Visit(m.Arguments[0]);
                node.Left = this.Context.Pop();
                if (m.Arguments.Count > 1)
                {
                    this.Visit(m.Arguments[1]);
                    node.Right = this.Context.Pop();
                }
            }

            this.Context.Push(node);
            return m;
        }

        /// <summary>
        ///     The visit new.
        /// </summary>
        /// <param name="exp">
        ///     The exp.
        /// </param>
        /// <returns>
        ///     The <see cref="Expression" />.
        /// </returns>
        protected override Expression VisitNew(NewExpression exp)
        {
            // Typisierte Select
            if (exp.Members == null)
            {
                return exp;
            }


            // anonymes Type
            QNode bindingNode = null;
            for (int i = 0; i < exp.Members.Count; i++)
            {
                var bindingProperty = exp.Members[i].Name;
                var builder = new MemberNodeBuilder();
                builder.Visit(exp.Arguments[i]);
                var node = new QNode() { Type = NodeType.Member, Value = bindingProperty + ":" + builder.GetPath() };
                if (bindingNode == null)
                {
                    bindingNode = node;
                }
                else
                {
                    bindingNode.Left = node;
                }
            }

            this.Context.Push(bindingNode);
            return exp;
        }

        /// <summary>
        ///     The visit parameter.
        /// </summary>
        /// <param name="expression">
        ///     The expression.
        /// </param>
        /// <returns>
        ///     The <see cref="Expression" />.
        /// </returns>
        protected override Expression VisitParameter(ParameterExpression expression)
        {
            return base.VisitParameter(expression);
        }

        /// <summary>
        ///     The visit select method call.
        /// </summary>
        /// <param name="m">
        ///     The memberAccess.
        /// </param>
        /// <returns>
        ///     The <see cref="Expression" />.
        /// </returns>
        protected virtual Expression VisitSelectMethodCall(MethodCallExpression m)
        {
            this.Visit(m.Arguments[0]);
            var left = this.Context.Pop();
            this.Visit(m.Arguments[1]);
            var node = this.Context.Peek();
            node.Left = left;
            return m;
        }

        /// <summary>
        ///     The visit unary.
        /// </summary>
        /// <param name="b">
        ///     The b.
        ///     The b.
        /// </param>
        /// <returns>
        ///     The <see cref="Expression" />.
        /// </returns>
        protected virtual Expression VisitUnary(UnaryExpression b)
        {
            this.Visit(b.Operand);
            return b;
        }

        /// <summary>
        ///     The is parameter expression.
        /// </summary>
        /// <param name="exp">
        ///     The exp.
        /// </param>
        /// <returns>
        ///     The <see cref="bool" />.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        private bool IsParameterExpression(Expression exp)
        {
            if (exp.NodeType == ExpressionType.Parameter)
            {
                return true;
            }

            if (exp.NodeType == ExpressionType.Constant)
            {
                return false;
            }

            if (exp.NodeType == ExpressionType.MemberAccess)
            {
                var temp = (MemberExpression)exp;
                return this.IsParameterExpression(temp.Expression);
            }

            throw new Exception("IsParameterExpression");
        }

        /// <summary>
        ///     The resolve value.
        /// </summary>
        /// <param name="expression">
        ///     The expression.
        /// </param>
        /// <returns>
        ///     The <see cref="object" />.
        /// </returns>
        private object ResolveValue(Expression expression)
        {
            if (expression.NodeType == ExpressionType.MemberAccess)
            {
                var f1 = Expression.Lambda(expression).Compile();
                return f1.DynamicInvoke();
            }

            if (expression.NodeType == ExpressionType.Constant)
            {
                var exp = (ConstantExpression)expression;
                return exp.Value;
            }

            if (expression.NodeType == ExpressionType.Call)
            {
                var exp = (MethodCallExpression)expression;
                if (this.IsParameterExpression(exp.Arguments[0]))
                {
                    return this.ResolveValue(exp.Object);
                }

                return this.ResolveValue(exp.Arguments[0]);
            }

            throw new Exception("ResolveValue");
        }

        #endregion
    }
}