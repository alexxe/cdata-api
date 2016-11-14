// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DQuery.cs" company="">
//   
// </copyright>
// <summary>
//   The c query.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.DynamicLinq.CQuery.DynamicLinq
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Covis.Data.DynamicLinq.CQuery.Contracts;
    using Covis.Data.DynamicLinq.CQuery.Contracts.DEntity;
    using Covis.Data.DynamicLinq.CQuery.Contracts.Model;
    using Covis.Data.DynamicLinq.CQuery.DynamicLinq.Extentions;

    /// <summary>
    ///     The c query.
    /// </summary>
    /// <typeparam name="TModelEntity">
    /// </typeparam>
    /// <typeparam name="TEntityDescriptor">
    /// </typeparam>
    public class DQuery<TModelEntity, TEntityDescriptor> : IDescriptorAccsessor
        where TModelEntity : class, IModelEntity where TEntityDescriptor : TModelEntity, ISearchableDescriptor
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the q descriptor.
        /// </summary>
        private QueryDescriptor QDescriptor { get; set; }

        /// <summary>
        ///     Gets or sets the where.
        /// </summary>
        private CallNode WhereNode { get; set; }
        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the descriptor.
        /// </summary>
        public QueryDescriptor Descriptor
        {
            get
            {
                return this.QDescriptor;
            }
        }

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DQuery{TModelEntity,TEntityDescriptor}" /> class.
        /// </summary>
        public DQuery()
        {
            this.QDescriptor = new QueryDescriptor(typeof(TModelEntity));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DQuery{TModelEntity,TEntityDescriptor}" /> class.
        /// </summary>
        /// <param name="descriptor">
        ///     The descriptor.
        /// </param>
        public DQuery(QueryDescriptor descriptor)
        {
            this.QDescriptor = descriptor;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The and.
        /// </summary>
        /// <param name="right">
        ///     The right.
        /// </param>
        /// <returns>
        ///     The <see cref="DQuery" />.
        /// </returns>
        public DQuery<TModelEntity, TEntityDescriptor> And(WhereResult<TModelEntity, TEntityDescriptor> right)
        {
            var currentWhereNode = this.AsWhereResult().WhereNode;
            var rightNode = right.WhereNode.Right;
            var leftNode = (LNode)currentWhereNode.Right;
            var andNode = new BinaryNode(BinaryOp.And) { Right = rightNode, Left = leftNode };
            currentWhereNode.Right = andNode;
            return this;
        }

        /// <summary>
        ///     The as where result.
        /// </summary>
        /// <returns>
        ///     The <see cref="WhereResult" />.
        /// </returns>
        public WhereResult<TModelEntity, TEntityDescriptor> AsWhereResult()
        {
            if (this.WhereNode != null)
            {
                return new WhereResult<TModelEntity, TEntityDescriptor>(this.WhereNode);
            }
            else
            {
                return new WhereResult<TModelEntity, TEntityDescriptor>((CallNode)this.Descriptor.Root);
            }
        }

        

        /// <summary>
        ///     The include.
        /// </summary>
        /// <param name="includeExpression">
        ///     The include expression.
        /// </param>
        /// <typeparam name="DTO">
        /// </typeparam>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="QueryDescriptor" />.
        /// </returns>
        public DQuery<TModelEntity, TEntityDescriptor> Include<T>(Expression<Func<TModelEntity, T>> includeExpression)
            where T : IModelEntity
        {
            var node = new ExpressionConverter().Convert((MemberExpression)includeExpression.Body);
            this.Descriptor.IncludeParameters.Add(node);
            return this;
        }

        /// <summary>
        ///     The include.
        /// </summary>
        /// <param name="includeExpression">
        ///     The include expression.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="DQuery{TISEntity}" />.
        /// </returns>
        public DQuery<TModelEntity, TEntityDescriptor> Include<T>(
            Expression<Func<TModelEntity, ICollection<T>>> includeExpression) where T : IModelEntity
        {
            var node = new ExpressionConverter().Convert((MemberExpression)includeExpression.Body);
            this.Descriptor.IncludeParameters.Add(node);
            return this;
        }

        /// <summary>
        ///     The include.
        /// </summary>
        /// <param name="includeExpression">
        ///     The include expression.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="DQuery" />.
        /// </returns>
        public DQuery<TModelEntity, TEntityDescriptor> Include<T>(
            Expression<Func<TModelEntity, IEnumerable<T>>> includeExpression) where T : IModelEntity
        {
            var node = new ExpressionConverter().Convert((MemberExpression)includeExpression.Body);
            this.Descriptor.IncludeParameters.Add(node);
            return this;
        }

        /// <summary>
        ///     The or.
        /// </summary>
        /// <param name="right">
        ///     The right.
        /// </param>
        /// <returns>
        ///     The <see cref="DQuery" />.
        /// </returns>
        public DQuery<TModelEntity, TEntityDescriptor> Or(WhereResult<TModelEntity, TEntityDescriptor> right)
        {
            var currentWhereNode = this.AsWhereResult().WhereNode;
            var rightNode = right.WhereNode.Right;
            var leftNode = (LNode)currentWhereNode.Right;
            var orNode = new BinaryNode(BinaryOp.Or) { Right = rightNode, Left = leftNode };
            currentWhereNode.Right = orNode;
            return this;
        }

        /// <summary>
        ///     The order by.
        /// </summary>
        /// <param name="property">
        ///     The property.
        /// </param>
        /// <typeparam name="TProperty">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="DQuery" />.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public DQuery<TModelEntity, TEntityDescriptor> OrderBy<TProperty>(
            Expression<Func<TEntityDescriptor, PropertyAcsessor<TProperty>>> property)
        {
            this.AddOrderBy(false, (MemberExpression)property.Body);

            return this;
        }

        public DQuery<TModelEntity, TEntityDescriptor> OrderBy<TProperty>(
            Expression<Func<TEntityDescriptor, PropertyAcsessor<TProperty>>> property, int take)
        {
            this.AddOrderBy(false, (MemberExpression)property.Body,take);

            return this;
        }

        public DQuery<TModelEntity, TEntityDescriptor> OrderBy<TProperty>(
            Expression<Func<TEntityDescriptor, PropertyAcsessor<TProperty>>> property, int take,int skip)
        {
            this.AddOrderBy(false, (MemberExpression)property.Body, take,skip);

            return this;
        }
        /// <summary>
        ///     The order by descending.
        /// </summary>
        /// <param name="property">
        ///     The property.
        /// </param>
        /// <typeparam name="TProperty">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="DQuery" />.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public DQuery<TModelEntity, TEntityDescriptor> OrderByDescending<TProperty>(
            Expression<Func<TEntityDescriptor, PropertyAcsessor<TProperty>>> property)
        {
            this.AddOrderBy(true, (MemberExpression)property.Body);
            return this;
        }

        /// <summary>
        ///     The select.
        /// </summary>
        /// <param name="selectorExpression">
        ///     The selector expression.
        /// </param>
        /// <typeparam name="TResult">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="SelectResult" />.
        /// </returns>
        public SelectResult<TModelEntity, TEntityDescriptor> Select(Expression<Func<TModelEntity, TModelEntity>> selectorExpression)
        {
            this.Descriptor.QueryType = QueryType.ModelProjection;
            var node = new ExpressionConverter().Convert(selectorExpression);
            this.AppendNode(node);
            return new SelectResult<TModelEntity, TEntityDescriptor>(this);
        }

        public SelectResult<TModelEntity, TEntityDescriptor> Select(Expression<Func<TModelEntity, IClientProjector>> selectorExpression)
        {
            this.Descriptor.QueryType = QueryType.AnonymeProjection;
            var node = new ExpressionConverter().Convert(selectorExpression);
            this.AppendNode(node);
            return new SelectResult<TModelEntity, TEntityDescriptor>(this);
        }

        

        /// <summary>
        ///     The where.
        /// </summary>
        /// <param name="property">
        ///     The property.
        /// </param>
        /// <param name="op">
        ///     The op.
        /// </param>
        /// <param name="value">
        ///     The value.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="DQuery" />.
        /// </returns>
        public DQuery<TModelEntity, TEntityDescriptor> Where<T>(
            Expression<Func<TEntityDescriptor, PropertyAcsessor<T>>> property,
            CompareOperator op,
            T value)
        {
            this.AddWhere(BinaryOp.And, (MemberExpression)property.Body, (BinaryOp)op, value);
            return this;
        }

        public DQuery<TModelEntity, TEntityDescriptor> Where<T>(
            Expression<Func<TEntityDescriptor, PropertyAcsessor<T>>> property,
            IEnumerable<T> value)
        {
            this.AddWhere(BinaryOp.And, (MemberExpression)property.Body, "In", value);
            return this;
        }

        public DQuery<TModelEntity, TEntityDescriptor> Where<T>(
            Expression<Func<TEntityDescriptor, IEnumerable<T>>> property,
            BinaryOp op,
            T value)
        {
            this.AddWhere(BinaryOp.And, (MemberExpression)property.Body, op, value);
            return this;
        }

        /// <summary>
        ///     The where.
        /// </summary>
        /// <param name="property">
        ///     The property.
        /// </param>
        /// <param name="value">
        ///     The value.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="DQuery" />.
        /// </returns>
        public DQuery<TModelEntity, TEntityDescriptor> Where<T>(
            Expression<Func<TEntityDescriptor, PropertyAcsessor<T>>> property,
            T value)
        {
            this.AddWhere(BinaryOp.And, (MemberExpression)property.Body, BinaryOp.And, value);
            return this;
        }

        /// <summary>
        ///     The where.
        /// </summary>
        /// <param name="property">
        ///     The property.
        /// </param>
        /// <param name="op">
        ///     The op.
        /// </param>
        /// <param name="value">
        ///     The value.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="DQuery" />.
        /// </returns>
        public DQuery<TModelEntity, TEntityDescriptor> Where<T>(
            Expression<Func<TEntityDescriptor, PropertyAcsessor<T>>> property,
            StringMethods op,
            T value)
        {
            this.AddWhere(BinaryOp.And, (MemberExpression)property.Body, ((MethodEnum)op).ToString(), value);
            return this;
        }

        //public DQuery<TModelEntity, TEntityDescriptor> Where<T>(
        //    Expression<Func<TEntityDescriptor, PropertyAcsessor<T>>> property,
        //    WhereResult<TModelEntity, TEntityDescriptor> op)
        //{
        //    //this.AddWhere(BinaryOp.And, (MemberExpression)property.Body, op.ToString(), value);
        //    return this;
        //}

        /// <summary>
        ///     The where.
        /// </summary>
        /// <param name="inResult">
        ///     The in result.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="DQuery" />.
        /// </returns>
        public DQuery<TModelEntity, TEntityDescriptor> Where<T>(Expression<Func<TEntityDescriptor, InResult<T>>> inResult)
        {
            var callExpression = (MethodCallExpression)inResult.Body;
            var lambda = Expression.Lambda(callExpression.Arguments[1]).Compile();
            var value = lambda.DynamicInvoke();
            this.AddWhere(BinaryOp.And, (MemberExpression)callExpression.Arguments[0], "In", value);

            return this;
        }

        public DQuery<TModelEntity, TEntityDescriptor> Where<T,T1>(Expression<Func<TEntityDescriptor, IEnumerable<T>>> exp,WhereResult<T, T1> whereResult)
            where T : class,IModelEntity where T1 : T,ISearchableDescriptor
        {
            var memberNode = (LNode)new ExpressionConverter().Convert((MemberExpression)exp.Body);
            whereResult.WhereNode.Left = memberNode;
            whereResult.WhereNode.Method = "Any";
            //var binaryNode = new BinaryNode(compareOp) { Left = memberNode, Right = new ConstantNode(value) };

            if (this.WhereNode == null)
            {
                this.WhereNode = new CallNode("Where") { Right = whereResult.WhereNode };
                this.AppendNode(this.WhereNode);
            }
            else
            {
                var andNode = new BinaryNode(BinaryOp.And) { Left = (LNode)this.WhereNode.Right, Right = whereResult.WhereNode };
                this.WhereNode.Right = andNode;
            }

            return this;
        }
        /// <summary>
        ///     The where or.
        /// </summary>
        /// <param name="property">
        ///     The property.
        /// </param>
        /// <param name="op">
        ///     The op.
        /// </param>
        /// <param name="value">
        ///     The value.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="DQuery" />.
        /// </returns>
        public DQuery<TModelEntity, TEntityDescriptor> WhereOr<T>(
            Expression<Func<TEntityDescriptor, PropertyAcsessor<T>>> property,
            StringMethods op,
            T value)
        {
            this.AddWhere(BinaryOp.Or, (MemberExpression)property.Body, ((MethodEnum)op).ToString(), value);
            return this;
        }

        public DQuery<TModelEntity, TEntityDescriptor> WhereIn<T>(
            Expression<Func<TEntityDescriptor, PropertyAcsessor<T>>> property,
            IEnumerable<T> value)
        {
            this.AddWhere(BinaryOp.Or, (MemberExpression)property.Body, "in", value);
            return this;
        }
        /// <summary>
        ///     The where or.
        /// </summary>
        /// <param name="property">
        ///     The property.
        /// </param>
        /// <param name="op">
        ///     The op.
        /// </param>
        /// <param name="value">
        ///     The value.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="DQuery" />.
        /// </returns>
        public DQuery<TModelEntity, TEntityDescriptor> WhereOr<T>(
            Expression<Func<TEntityDescriptor, PropertyAcsessor<T>>> property,
            CompareOperator op,
            T value)
        {
            this.AddWhere(BinaryOp.Or, (MemberExpression)property.Body, (BinaryOp)op, value);
            return this;
        }

        /// <summary>
        ///     The where or.
        /// </summary>
        /// <param name="property">
        ///     The property.
        /// </param>
        /// <param name="value">
        ///     The value.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="DQuery" />.
        /// </returns>
        public DQuery<TModelEntity, TEntityDescriptor> WhereOr<T>(
            Expression<Func<TEntityDescriptor, PropertyAcsessor<T>>> property,
            T value)
        {
            this.AddWhere(BinaryOp.Or, (MemberExpression)property.Body, BinaryOp.And, value);
            return this;
        }

        /// <summary>
        ///     The where or.
        /// </summary>
        /// <param name="inResult">
        ///     The in result.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="DQuery" />.
        /// </returns>
        public DQuery<TModelEntity, TEntityDescriptor> WhereOr<T>(Expression<Func<TEntityDescriptor, InResult<T>>> inResult)
        {
            var callExpression = (MethodCallExpression)inResult.Body;
            var lambda = Expression.Lambda(callExpression.Arguments[1]).Compile();
            var value = lambda.DynamicInvoke();
            this.AddWhere(BinaryOp.Or, (MemberExpression)callExpression.Arguments[0], "In", value);

            return this;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     The add order by.
        /// </summary>
        /// <param name="isDescending">
        ///     The is descending.
        /// </param>
        /// <param name="member">
        ///     The member.
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        private void AddOrderBy(bool isDescending, MemberExpression member)
        {
            var memberNode = (LNode)new ExpressionConverter().Convert(member);
            var node = new SortNode(!isDescending) { Right = memberNode };
            this.AppendNode(node);
        }

        private void AddOrderBy(bool isDescending, MemberExpression member,int take)
        {
            this.AddOrderBy(isDescending, member);

            var takeNode = new TakeNode(take) { Right = new ConstantNode(take) };
            this.AppendNode(takeNode);

            
        }

        private void AddOrderBy(bool isDescending, MemberExpression member,int take,int skip)
        {
            this.AddOrderBy(isDescending,member,take);

            var skipNode = new SkipNode(skip) { Right = new ConstantNode(skip) };
            this.AppendNode(skipNode);

            
        }
        /// <summary>
        ///     The add where.
        /// </summary>
        /// <param name="logicOp">
        ///     The logic op.
        /// </param>
        /// <param name="member">
        ///     The member.
        /// </param>
        /// <param name="compareOp">
        ///     The compare op.
        /// </param>
        /// <param name="value">
        ///     The value.
        /// </param>
        private void AddWhere(BinaryOp logicOp, MemberExpression member, BinaryOp compareOp, object value)
        {
            var memberNode = (LNode)new ExpressionConverter().Convert(member);
            var binaryNode = new BinaryNode(compareOp) { Left = memberNode, Right = new ConstantNode(value) };

            if (this.WhereNode == null)
            {
                this.WhereNode = new CallNode("Where") { Right = binaryNode };
                this.AppendNode(this.WhereNode);
            }
            else
            {
                var andNode = new BinaryNode(logicOp) { Left = (LNode)this.WhereNode.Right, Right = binaryNode };
                this.WhereNode.Right = andNode;
            }
        }

        /// <summary>
        ///     The add where.
        /// </summary>
        /// <param name="logicOp">
        ///     The logic op.
        /// </param>
        /// <param name="member">
        ///     The member.
        /// </param>
        /// <param name="method">
        ///     The method.
        /// </param>
        /// <param name="value">
        ///     The value.
        /// </param>
        private void AddWhere(BinaryOp logicOp, MemberExpression member, string method, object value)
        {
            var memberNode = (LNode)new ExpressionConverter().Convert(member);
            var callNode = new CallNode(method) { Left = memberNode, Right = new ConstantNode(value) };

            if (this.WhereNode == null)
            {
                var call = new CallNode("Where") { Right = callNode };
                this.AppendNode(call);
            }
            else
            {
                var andNode = new BinaryNode(logicOp) { Left = (LNode)this.WhereNode.Right, Right = callNode };
                this.WhereNode.Right = andNode;
            }
        }

        /// <summary>
        ///     The append node.
        /// </summary>
        /// <param name="node">
        ///     The node.
        /// </param>
        private void AppendNode(BNode node)
        {
            node.Left = this.Descriptor.Root;
            this.Descriptor.Root = node;
        }

        /// <summary>
        ///     The append node.
        /// </summary>
        /// <param name="node">
        ///     The node.
        /// </param>
        private void AppendNode(ProjectorNode node)
        {
            node.Left = this.Descriptor.Root;
            this.Descriptor.Root = node;
        }

        

        #endregion
    }
}