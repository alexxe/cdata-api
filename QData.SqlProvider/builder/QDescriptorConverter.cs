using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;
using Qdata.Json.Contract;
using QData.Common;


namespace QData.SqlProvider.builder
{
    public class QDescriptorConverter : IQNodeVisitor
    {
        public QDescriptorConverter(MapperConfiguration conf, Expression query)
        {
            ContextExpression = new Stack<Expression>();
            ContextParameters = new Stack<ParameterExpression>();
            Mapper = new Mapping(conf);
            MemberNodeConverter = new MemberNodeConverter(Mapper);
            Query = query;
        }

        public void VisitMember(QNode node)
        {
            var member = MemberNodeConverter.ConvertToMemberExpression(ContextParameters.Peek(), node);
            ContextExpression.Push(member);
        }

        public void VisitQuerable(QNode node)
        {
            ContextExpression.Push(Query);
            Mapper.EnableMapping = true;
        }

        public void VisitMethod(QNode node)
        {
            MethodType method;
            if (node.Value is long)
            {
                method = (MethodType) Convert.ToInt16(node.Value);
            }
            else
            {
                Enum.TryParse(Convert.ToString(node.Value), out method);
            }

            var right = ContextExpression.Pop();
            var left = ContextExpression.Pop();

            var lambda = Expression.Lambda(right, ContextParameters.Peek());

            var exp = BuildMethodCallExpression(method, left, lambda);
            ContextExpression.Push(exp);
        }

        public void EnterContext(QNode node)
        {
            Type parameterType;
            var expression = ContextExpression.Peek();
            parameterType = expression.Type.IsGenericType ? expression.Type.GenericTypeArguments[0] : expression.Type.UnderlyingSystemType;

            var parameter = Expression.Parameter(
                parameterType,
                string.Format("x{0}", parameterPrefix++));
            ContextParameters.Push(parameter);
        }

        public void LeaveContext(QNode node)
        {
            ContextParameters.Pop();
        }

        public void VisitProjection(QNode node)
        {
            var left = ContextExpression.Pop();

            Type projectorType = null;
            var dynamicProperties = new List<DynamicProperty>();
            var bindings1 = MemberNodeConverter.ConvertToBindings(ContextParameters.Peek(), node);
            foreach (var binding in bindings1)
            {
                var property = new DynamicProperty(binding.Key, binding.Value.Type);
                dynamicProperties.Add(property);
            }

            projectorType = ClassFactory.Instance.GetDynamicClass(dynamicProperties);

            var resultProperties = projectorType.GetProperties();
            var bindingsSource = new List<Expression>();
            var bindingsProperties = new List<PropertyInfo>();
            foreach (var binding in bindings1)
            {
                bindingsProperties.Add(resultProperties.FirstOrDefault(x => x.Name == binding.Key));
                bindingsSource.Add(binding.Value);
            }

            var bindings = new List<MemberBinding>();
            for (var i = 0; i < bindingsSource.Count; i++)
            {
                var exp = Expression.Bind(bindingsProperties[i], bindingsSource[i]);
                bindings.Add(exp);
            }

            var lambda =
                Expression.Lambda(
                    Expression.MemberInit(Expression.New(projectorType.GetConstructor(Type.EmptyTypes)), bindings),
                    ContextParameters.Peek());

            var result = BuildMethodCallExpression(MethodType.Select, left, lambda);

            ContextExpression.Push(result);

            HasProjection = true;
            Mapper.EnableMapping = false;
        }

        //public void Visit(TakeNode node)
        //{
        //    var caller = this.ContextExpression.Pop();

        //    var types = new List<Type>() { caller.Type.IsGenericType ? caller.Type.GenericTypeArguments[0] : caller.Type };
        //    var exp = Expression.Call(typeof(Queryable), "Take", types.ToArray(), caller, Expression.Constant(node.Take));
        //    this.ContextExpression.Push(exp);

        //}

        //public void Visit(SkipNode node)
        //{
        //    var caller = this.ContextExpression.Pop();

        //    var types = new List<Type>() { caller.Type.IsGenericType ? caller.Type.GenericTypeArguments[0] : caller.Type };
        //    var exp = Expression.Call(typeof(Queryable), "Skip", types.ToArray(), caller, Expression.Constant(node.Skip));
        //    this.ContextExpression.Push(exp);

        //}

        public void VisitConstant(QNode node)
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

            var memberType = ContextExpression.Peek().Type;

            if (valueType != memberType)
            {
                if (node.Value.GetType().IsGenericType)
                {
                    var list = (List<string>) node.Value;
                    if (memberType == typeof (long))
                    {
                        var exp = Expression.Constant(list.ConvertAll(Convert.ToInt64));
                        ContextExpression.Push(exp);
                    }
                    else if (memberType == typeof (DateTime))
                    {
                        var exp = Expression.Constant(list.ConvertAll(Convert.ToDateTime));
                        ContextExpression.Push(exp);
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
                    ContextExpression.Push(exp1);
                }
            }
            else
            {
                var exp = Expression.Constant(node.Value, ContextExpression.Peek().Type);
                ContextExpression.Push(exp);
            }
        }

        public void VisitBinary(QNode node)
        {
            var right = ContextExpression.Pop();
            var left = ContextExpression.Pop();
            BinaryType op;
            if (node.Value is long)
            {
                op = (BinaryType) Convert.ToInt16(node.Value);
            }
            else
            {
                Enum.TryParse(Convert.ToString(node.Value), out op);
            }
            var exp = BuildBinaryExpression(op, left, right);
            ContextExpression.Push(exp);
        }

        private Expression BuildBinaryExpression(BinaryType binary, Expression left, Expression right)
        {
            if (binary == BinaryType.And)
            {
                return Expression.And(left, right);
            }

            if (binary == BinaryType.Or)
            {
                return Expression.Or(left, right);
            }

            if (binary == BinaryType.Equal)
            {
                return Expression.Equal(left, right);
            }

            if (binary == BinaryType.GreaterThan)
            {
                return Expression.GreaterThan(left, right);
            }

            if (binary == BinaryType.GreaterThanOrEqual)
            {
                return Expression.GreaterThanOrEqual(left, right);
            }

            if (binary == BinaryType.LessThan)
            {
                return Expression.LessThan(left, right);
            }

            if (binary == BinaryType.LessThanOrEqual)
            {
                return Expression.LessThanOrEqual(left, right);
            }

            if (binary == BinaryType.Contains)
            {
                var containsMethod = typeof (string).GetMethod("Contains");
                return Expression.Call(left, containsMethod, (ConstantExpression) right);
            }

            if (binary == BinaryType.StartsWith)
            {
                var startsWithMethod = typeof (string).GetMethod("StartsWith", new[] {typeof (string)});
                return Expression.Call(left, startsWithMethod, (ConstantExpression) right);
            }

            if (binary == BinaryType.EndsWith)
            {
                var endsWithMethod = typeof (string).GetMethod("EndsWith", new[] {typeof (string)});
                return Expression.Call(left, endsWithMethod, (ConstantExpression) right);
            }

            if (binary == BinaryType.In)
            {
                var method = right.Type.GetMethod("Contains");
                return Expression.Call(right, method, left);
            }

            throw new Exception(binary.ToString());
        }

        private MethodCallExpression BuildMethodCallExpression(
            MethodType method,
            Expression caller,
            LambdaExpression argument)
        {
            var types = new List<Type>
            {
                caller.Type.IsGenericType ? caller.Type.GenericTypeArguments[0] : caller.Type
            };

            if (method == MethodType.Any)
            {
                return Expression.Call(typeof (Enumerable), "Any", types.ToArray(), caller, argument);
            }
            if (method == MethodType.Count)
            {
                return Expression.Call(typeof (Enumerable), "Count", types.ToArray(), caller, argument);
            }

            if (method == MethodType.OrderBy || method == MethodType.OrderByDescending)
            {
                var methodName = method.ToString();
                if (OderByCount > 0)
                {
                    methodName = methodName.Replace("OderBy", "ThenBy");
                }
                types.Add(argument.ReturnType);
                OderByCount++;
                return Expression.Call(
                    typeof (Queryable),
                    methodName,
                    types.ToArray(),
                    caller,
                    Expression.Quote(argument));
            }

            if (method == MethodType.Where)
            {
                return Expression.Call(typeof (Queryable), "Where", types.ToArray(), caller, Expression.Quote(argument));
            }

            if (method == MethodType.Select)
            {
                types.Add(argument.ReturnType);
                return Expression.Call(typeof (Queryable), "Select", types.ToArray(), caller, Expression.Quote(argument));
            }

            throw new Exception(method.ToString());
        }

        #region Fields

        private readonly MemberNodeConverter MemberNodeConverter;

        public Type SourceType { get; private set; }

        public Type TargetType { get; private set; }

        public bool HasProjection { get; private set; }

        /// <summary>
        ///     The count.
        /// </summary>
        private int parameterPrefix;

        private int OderByCount { get; set; }

        #endregion

        #region Properties

        private Mapping Mapper { get; }

        private Expression Query { get; }

        /// <summary>
        ///     Gets the current.
        /// </summary>
        public Stack<Expression> ContextExpression { get; set; }

        /// <summary>
        ///     Gets or sets the param expression.
        /// </summary>
        private Stack<ParameterExpression> ContextParameters { get; }

        #endregion
    }
}