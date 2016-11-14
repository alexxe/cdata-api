// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NodeConverter.cs" company="">
//   
// </copyright>
// <summary>
//   The where builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Covis.Data.DynamicLinq.Provider
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    using Covis.Data.DynamicLinq.CQuery.Contracts;
    using Covis.Data.DynamicLinq.CQuery.Contracts.DEntity;
    using Covis.Data.DynamicLinq.CQuery.Contracts.Model;
    using Covis.Data.DynamicLinq.Provider.Extentions;

    /// <summary>
    ///     The where builder.
    /// </summary>
    internal class ExpressionBuilder : INodeVisitor
    {
        #region Fields

        /// <summary>
        ///     The query.
        /// </summary>
        private readonly Expression query;

        /// <summary>
        ///     The count.
        /// </summary>
        private int parameterPrefix = 0;

        #endregion

        private int OderByCount { get; set; }

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="NodeConverter" /> class.
        /// </summary>
        public ExpressionBuilder()
        {
            this.ContextExpression = new Stack<Expression>();
            this.OderByCount = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NodeConverter"/> class.
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        public ExpressionBuilder(Expression query)
        {
            this.query = query;
            this.ContextExpression = new Stack<Expression>();
            this.ContextParameters = new Stack<ParameterExpression>();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the current.
        /// </summary>
        public Stack<Expression> ContextExpression { get; set; }

        /// <summary>
        ///     Gets or sets the param expression.
        /// </summary>
        private Stack<ParameterExpression> ContextParameters { get; set; }

        #endregion

        

        #region Methods

        
        /// <summary>
        /// The visit assigment node.
        /// </summary>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Expression"/>.
        /// </returns>
        public void Visit(BinaryNode node)
        {
            var right = this.ContextExpression.Pop();
            var left = this.ContextExpression.Pop();
            var exp = this.BuildBinaryExpression(node, left, right);
            this.ContextExpression.Push(exp);
        }

        /// <summary>
        /// The visit call node.
        /// </summary>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Expression"/>.
        /// </returns>
        public void Visit(CallNode node)
        {
            var right = this.ContextExpression.Pop();
            var left = this.ContextExpression.Pop();

            if (node.Right is ConstantNode)
            {
                var exp = this.BuildMethodCallExpression(node, left, (ConstantExpression)right);
                this.ContextExpression.Push(exp);
            }
            else
            {
                var lambda = Expression.Lambda(right, this.ContextParameters.Pop());
                var exp = this.BuildMethodCallExpression(node.Method, left, lambda);
                this.ContextExpression.Push(exp);
            
            }
        }

        public void Visit(SortNode node)
        {
            var right = this.ContextExpression.Pop();
            var left = this.ContextExpression.Pop();

            var lambda = Expression.Lambda(right, this.ContextParameters.Pop());
            if (this.OderByCount > 0)
            {
                node.Method = node.Method.Replace("OderBy", "ThenBy");
            }
            var types = new List<Type>() { left.Type.IsGenericType ? left.Type.GenericTypeArguments[0] : left.Type , lambda.ReturnType };
            this.OderByCount++;
            var exp =  Expression.Call(
                    typeof(Queryable),
                    node.Method,
                    types.ToArray(),
                    left,
                    Expression.Quote(lambda));
            this.ContextExpression.Push(exp);
        
        }

        public void Visit(TakeNode node)
        {
            var caller = this.ContextExpression.Pop();
            
            var types = new List<Type>() { caller.Type.IsGenericType ? caller.Type.GenericTypeArguments[0] : caller.Type };
            var exp = Expression.Call(typeof(Queryable),"Take", types.ToArray(), caller,Expression.Constant(node.Take));
            this.ContextExpression.Push(exp);

        }

        public void Visit(SkipNode node)
        {
            var caller = this.ContextExpression.Pop();

            var types = new List<Type>() { caller.Type.IsGenericType ? caller.Type.GenericTypeArguments[0] : caller.Type };
            var exp = Expression.Call(typeof(Queryable), "Skip", types.ToArray(), caller, Expression.Constant(node.Skip));
            this.ContextExpression.Push(exp);

        }

        public void EnterContext(CallNode node)
        {
            this.PushParameter();
        }

        public void LeaveContext(CallNode node)
        {
            
        }

        public void EnterContext(ProjectorNode node)
        {
            this.PushParameter();
        }

        public void LeaveContext(ProjectorNode node)
        {
            
        }

        private void PushParameter()
        {
            Type parameterType = Util.GetMemberType(this.ContextExpression.Peek());
            ParameterExpression parameter = Expression.Parameter(
                parameterType, string.Format("x{0}", this.parameterPrefix++));
            this.ContextParameters.Push(parameter);
        }
        /// <summary>
        /// The visit constant node.
        /// </summary>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Expression"/>.
        /// </returns>
        public void Visit(ConstantNode node)
        {
            Type valueType = null;
            if (node.Value.GetType().IsGenericType)
            {
                valueType = node.Value.GetType().GetGenericArguments()[0];
            }
            else
            {
                valueType = node.Value.GetType();
            }

            Type memberType = this.ContextExpression.Peek().Type;

            if (valueType != memberType)
            {
                if (node.Value.GetType().IsGenericType)
                {
                    var list = (List<string>)node.Value;
                    if (memberType == typeof(long))
                    {
                        var exp = Expression.Constant(list.ConvertAll(Convert.ToInt64));
                        this.ContextExpression.Push(exp);
                    }
                    else if (memberType == typeof(DateTime))
                    {
                        var exp = Expression.Constant(list.ConvertAll(Convert.ToDateTime));
                        this.ContextExpression.Push(exp);

                    }
                    else
                    {
                        //var listType = typeof(List<>);
                        //var concreteType = listType.MakeGenericType(memberType);
                        //var valueList = Activator.CreateInstance(concreteType);
                        //var methodInfo = valueList.GetType().GetMethod("Add");
                        //foreach (var stringValue in (List<string>)node.Value)
                        //{
                        //    var value = ConvertConstant(stringValue, propertyMap.DestinationPropertyType, out operatorType);
                        //    methodInfo.Invoke(valueList, new object[] { value });
                        //}
                        throw new Exception(string.Format("VisitConstantNode :Type {0}", memberType));
                    }
                    
                }
                else
                {
                    var value = Convert.ChangeType(node.Value, memberType);
                    var exp1 = Expression.Constant(value);
                    this.ContextExpression.Push(exp1);
                }

                
            }
            else
            {
                var exp = Expression.Constant(node.Value, this.ContextExpression.Peek().Type);
                this.ContextExpression.Push(exp);
            }
            

            
        }

        /// <summary>
        /// The visit entry point node.
        /// </summary>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <returns>
        /// The <see cref="Expression"/>.
        /// </returns>
        public void Visit(EntryPointNode node)
        {
            this.ContextExpression.Push(this.query);
        }

        /// <summary>
        /// The visit member node.
        /// </summary>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Expression"/>.
        /// </returns>
        public void Visit(MemberNode node)
        {
            if (node.Left is ParameterNode)
            {
                MemberExpression propertyAccess = Expression.Property(this.ContextParameters.Peek(), node.Member);
                this.ContextExpression.Push(propertyAccess);
            }
            else
            {
                MemberExpression propertyAccess = Expression.Property(this.ContextExpression.Pop(), node.Member);
                this.ContextExpression.Push(propertyAccess);
            }
           
        }

        public void Visit(ParameterNode node)
        {
            
        }

        public void Visit(ProjectorNode node)
        {
            var left = this.ContextExpression.Pop();

            Type projectorType = null;
            var dynamicProperties = new List<DynamicProperty>();
            foreach (var binding in node.Bindings)
            {
                binding.Value.Accept(this);
                var exp = this.ContextExpression.Pop();
                var property = new DynamicProperty(binding.Key, exp.Type);
                dynamicProperties.Add(property);
            }

            projectorType = ClassFactory.Instance.GetDynamicClass(dynamicProperties);
            
            var resultProperties = projectorType.GetProperties();
            var bindingsSource = new List<Expression>();
            var bindingsProperties = new List<PropertyInfo>();
            foreach (var binding in node.Bindings)
            {
                bindingsProperties.Add(resultProperties.FirstOrDefault(x => x.Name == binding.Key));
                binding.Value.Accept(this);
                var exp = this.ContextExpression.Pop();

                bindingsSource.Add(exp);

            }

            var bindings = new List<MemberBinding>();
            for (int i = 0; i < bindingsSource.Count; i++)
            {
                var exp = Expression.Bind(bindingsProperties[i], bindingsSource[i]);
                bindings.Add(exp);
            }

            LambdaExpression lambda =
                Expression.Lambda(
                    Expression.MemberInit(Expression.New(projectorType.GetConstructor(Type.EmptyTypes)), bindings),
                    this.ContextParameters.Pop());

            var result = this.BuildMethodCallExpression("Select", left, lambda);

            this.ContextExpression.Push(result);
        }

        
        /// <summary>
        /// The build binary expression.
        /// </summary>
        /// <param name="filterAssessment">
        /// The filter assessment.
        /// </param>
        /// <param name="left">
        /// The left.
        /// </param>
        /// <param name="right">
        /// The right.
        /// </param>
        /// <returns>
        /// The <see cref="BinaryExpression"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        private BinaryExpression BuildBinaryExpression(BinaryNode filterAssessment, Expression left, Expression right)
        {
            if (filterAssessment.BinaryOperator == BinaryOp.AndAlso)
            {
                return Expression.AndAlso(left, right);
            }

            if (filterAssessment.BinaryOperator == BinaryOp.And)
            {
                return Expression.And(left, right);
            }

            if (filterAssessment.BinaryOperator == BinaryOp.Or)
            {
                return Expression.Or(left, right);
            }

            if (filterAssessment.BinaryOperator == BinaryOp.OrElse)
            {
                return Expression.OrElse(left, right);
            }

            if (filterAssessment.BinaryOperator == BinaryOp.Equal)
            {
                return Expression.Equal(left, right);
            }

            if (filterAssessment.BinaryOperator == BinaryOp.GreaterThan)
            {
                return Expression.GreaterThan(left, right);
            }

            if (filterAssessment.BinaryOperator == BinaryOp.GreaterThanOrEqual)
            {
                return Expression.GreaterThanOrEqual(left, right);
            }

            if (filterAssessment.BinaryOperator == BinaryOp.LessThan)
            {
                return Expression.LessThan(left, right);
            }

            if (filterAssessment.BinaryOperator == BinaryOp.LessThanOrEqual)
            {
                return Expression.LessThanOrEqual(left, right);
            }

            throw new Exception(filterAssessment.BinaryOperator.ToString());
        }

        /// <summary>
        /// The build method call expression.
        /// </summary>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <param name="caller">
        /// The caller.
        /// </param>
        /// <param name="constant">
        /// The constant.
        /// </param>
        /// <returns>
        /// The <see cref="MethodCallExpression"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        private MethodCallExpression BuildMethodCallExpression(
            CallNode node, 
            Expression caller, 
            ConstantExpression constant)
        {
            if (node.Method.Equals("In", StringComparison.OrdinalIgnoreCase))
            {
                var method = constant.Type.GetMethod("Contains");
                return Expression.Call(constant, method, caller);
            }

            if (node.Method.Equals("Contains", StringComparison.OrdinalIgnoreCase))
            {
                return Expression.Call(caller, Util.containsMethod, constant);
            }

            if (node.Method.Equals("StartsWith", StringComparison.OrdinalIgnoreCase))
            {
                return Expression.Call(caller, Util.startsWithMethod, constant);
            }

            if (node.Method.Equals("EndsWith", StringComparison.OrdinalIgnoreCase))
            {
                return Expression.Call(caller, Util.endsWithMethod, constant);
            }

            throw new Exception(node.ToString());
        }

        /// <summary>
        /// The build method call expression.
        /// </summary>
        /// <param name="methodName">
        /// The method Name.
        /// </param>
        /// <param name="caller">
        /// The member.
        /// </param>
        /// <param name="argument">
        /// The argument.
        /// </param>
        /// <returns>
        /// The <see cref="MethodCallExpression"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        private MethodCallExpression BuildMethodCallExpression(
            string methodName, 
            Expression caller, 
            LambdaExpression argument)
        {
            var types = new List<Type>() { caller.Type.IsGenericType ? caller.Type.GenericTypeArguments[0] : caller.Type };

            if (methodName.Equals("Any"))
            {
                return Expression.Call(typeof(Enumerable), methodName, types.ToArray(), caller, argument);
            }
            if (methodName.Equals("Count"))
            {
                return Expression.Call(typeof(Enumerable), methodName, types.ToArray(), caller, argument);
            }

            if (!methodName.Equals("Where"))
            {
                types.Add(argument.ReturnType);
            }

            

            if (caller.Type.GetInterface("IQueryable") != null)
            {
                return Expression.Call(
                    typeof(Queryable), 
                    methodName, 
                    types.ToArray(), 
                    caller, 
                    Expression.Quote(argument));
            }

            return Expression.Call(typeof(Enumerable), methodName, types.ToArray(), caller, argument);
        }

        #endregion
    }
}