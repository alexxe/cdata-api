// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NodeConverter.cs" company="">
//   
// </copyright>
// <summary>
//   The where builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Covis.Data.SqlProvider
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    using Covis.Data.Common;
    using Covis.Data.SqlProvider.Contracts;
    using Covis.Data.SqlProvider.Contracts.Model;

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

            var lambda = Expression.Lambda(right, this.ContextParameters.Pop());
            var exp = this.BuildMethodCallExpression(node.Method, left, lambda);
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
            Type parameterType;
            var expression = this.ContextExpression.Peek();
            if (expression.Type.IsGenericType)
            {
                parameterType = expression.Type.GenericTypeArguments[0];
            }
            else
            {
                parameterType = expression.Type.UnderlyingSystemType;
            }
            
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

            var result = this.BuildMethodCallExpression(MethodType.Select, left, lambda);

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
        private Expression BuildBinaryExpression(BinaryNode filterAssessment, Expression left, Expression right)
        {
            if (filterAssessment.BinaryOperator == BinaryType.And)
            {
                return Expression.And(left, right);
            }

            if (filterAssessment.BinaryOperator == BinaryType.Or)
            {
                return Expression.Or(left, right);
            }

            if (filterAssessment.BinaryOperator == BinaryType.Equal)
            {
                return Expression.Equal(left, right);
            }

            if (filterAssessment.BinaryOperator == BinaryType.GreaterThan)
            {
                return Expression.GreaterThan(left, right);
            }

            if (filterAssessment.BinaryOperator == BinaryType.GreaterThanOrEqual)
            {
                return Expression.GreaterThanOrEqual(left, right);
            }

            if (filterAssessment.BinaryOperator == BinaryType.LessThan)
            {
                return Expression.LessThan(left, right);
            }

            if (filterAssessment.BinaryOperator == BinaryType.LessThanOrEqual)
            {
                return Expression.LessThanOrEqual(left, right);
            }

            if (filterAssessment.BinaryOperator == BinaryType.Contains)
            {
                MethodInfo containsMethod = typeof(string).GetMethod("Contains");
                return Expression.Call(left, containsMethod, (ConstantExpression)right);
            }

            if (filterAssessment.BinaryOperator == BinaryType.StartsWith)
            {
                MethodInfo startsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
                return Expression.Call(left, startsWithMethod, (ConstantExpression)right);
            }

            if (filterAssessment.BinaryOperator == BinaryType.EndsWith)
            {
                MethodInfo endsWithMethod = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });
                return Expression.Call(left, endsWithMethod, (ConstantExpression)right);
            }

            if (filterAssessment.BinaryOperator == BinaryType.In)
            {
                var method = right.Type.GetMethod("Contains");
                return Expression.Call(right, method, left);
            }

            throw new Exception(filterAssessment.ToString());
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
            MethodType method, 
            Expression caller, 
            LambdaExpression argument)
        {
            var types = new List<Type>() { caller.Type.IsGenericType ? caller.Type.GenericTypeArguments[0] : caller.Type };

            if (method == MethodType.Any)
            {
                return Expression.Call(typeof(Enumerable), "Any", types.ToArray(), caller, argument);
            }
            if (method == MethodType.Count)
            {
                return Expression.Call(typeof(Enumerable), "Count", types.ToArray(), caller, argument);
            }

            if (method == MethodType.OrderBy || method == MethodType.OrderByDescending)
            {
                var methodName = method.ToString();
                if (this.OderByCount > 0)
                {
                    methodName = methodName.Replace("OderBy", "ThenBy");
                }
                types.Add(argument.ReturnType);
                this.OderByCount++;
                return Expression.Call(typeof(Queryable), methodName,types.ToArray(), caller, Expression.Quote(argument));
            }

            if (method == MethodType.Where)
            {
                return Expression.Call(typeof(Queryable),"Where",types.ToArray(),caller,Expression.Quote(argument));
            }

            if (method == MethodType.Select)
            {
                types.Add(argument.ReturnType);
                return Expression.Call(typeof(Queryable), "Select", types.ToArray(), caller, Expression.Quote(argument));
            }
            
            throw new Exception(method.ToString());
        }

        #endregion
    }
}