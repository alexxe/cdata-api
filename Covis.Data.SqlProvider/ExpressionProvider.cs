// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExpressionProvider.cs" company="">
//   
// </copyright>
// <summary>
//   The repository impl.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.SqlProvider
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Data.Entity;

    using Covis.Data.SqlProvider.Contracts;
    using Covis.Data.SqlProvider.Contracts.Model;

    /// <summary>
    ///     The repository impl.
    /// </summary>
    /// <typeparam name="TEntity">
    /// </typeparam>
    public class ExpressionProvider
    {
        #region Fields

        /// <summary>
        ///     The model.
        /// </summary>
        private readonly IQueryable query;

        #endregion

        #region Constructors and Destructors

        public ExpressionProvider(IQueryable query)
        {
            this.query = query;
        }

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
        public Expression ConvertToExpression(QueryDescriptor descriptor)
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

        #endregion
    }
}