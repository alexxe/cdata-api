// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExpressionProvider.cs" company="">
//   
// </copyright>
// <summary>
//   The repository impl.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.DynamicLinq.Provider
{
    using System;
    using System.Collections;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    using AutoMapper;

    using Covis.Data.DynamicLinq.CQuery.Contracts;
    using Covis.Data.DynamicLinq.CQuery.Contracts.Model;
    using Covis.Data.DynamicLinq.Provider.Mapping;
    using Covis.Data.DynamicLinq.Security;

    /// <summary>
    ///     The repository impl.
    /// </summary>
    /// <typeparam name="TEntity">
    /// </typeparam>
    public class ExpressionProvider
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExpressionProvider{TEntity}" /> class.
        /// </summary>
        /// <param name="query">
        ///     The query.
        /// </param>
        public ExpressionProvider(IQueryable query, MapperConfiguration mapperConfiguration)
        {
            this.query = query;
            this.mapperConfiguration = mapperConfiguration;
        }

        #endregion

        #region Fields

        /// <summary>
        ///     The model.
        /// </summary>
        private readonly IQueryable query;

        private readonly MapperConfiguration mapperConfiguration;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The find.
        /// </summary>
        /// <param name="descriptor">
        ///     The descriptor.
        /// </param>
        /// <returns>
        ///     The <see cref="IEnumerable" />.
        /// </returns>
        public Expression Convert(QueryDescriptor descriptor)
        {
            var query = this.query;
            foreach (var include in descriptor.IncludeParameters)
            {
                string member = include.Member;
                var temp = include;
                while (temp.Left != null && temp.Left is MemberNode)
                {
                    var memberNode = temp.Left as MemberNode;
                    member = member + "." + memberNode.Member;
                    temp = memberNode;
                }

                query = query.Include(member);
            }

            var visitor = new ExpressionBuilder(query.Expression);
            descriptor.Root.Accept(visitor);

            var result = visitor.ContextExpression.Pop();
            return result;
        }

        //public Expression Convert(QueryDescriptor descriptor, ISecurityContext securityContext)
        //{
        //    descriptor = QueryDescriptorMapper.Map(descriptor, this.mapperConfiguration, securityContext);
        //    var injector = new SecurityInjector();
        //    injector.AppendSecurity(descriptor.Root, securityContext);

        //    var visitor = new ExpressionBuilder(this.query.Expression);
        //    descriptor.Root.Accept(visitor);

        //    return visitor.ContextExpression.Pop();
        //}

        #endregion
    }
}