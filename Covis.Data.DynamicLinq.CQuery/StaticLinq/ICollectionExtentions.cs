// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CQueryExtentions.cs" company="">
//   
// </copyright>
// <summary>
//   The c query extentions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.DynamicLinq.CQuery.StaticLinq
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Covis.Data.DynamicLinq.CQuery.Contracts.DEntity;
    using Covis.Data.DynamicLinq.CQuery.DynamicLinq;

    /// <summary>
    ///     The c query extentions.
    /// </summary>
    public static class ICollectionExtentions
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The count.
        /// </summary>
        /// <param name="collection">
        ///     The collection.
        /// </param>
        /// <param name="expression">
        ///     The expression.
        /// </param>
        /// <typeparam name="TISEntity">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="int" />.
        /// </returns>
        public static int Count<TISEntity>(
            this ICollection<TISEntity> collection,
            Expression<Func<TISEntity, bool>> expression) where TISEntity : IModelEntity
        {
            return 0;
        }

        /// <summary>
        ///     The select.
        /// </summary>
        /// <param name="coll">
        ///     The coll.
        /// </param>
        /// <param name="selectorExpression">
        ///     The selector expression.
        /// </param>
        /// <typeparam name="TISEntity">
        /// </typeparam>
        /// <typeparam name="TResult">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="IEnumerable" />.
        /// </returns>
        public static ICollection<TResult> Select<TISEntity, TResult>(
            this ICollection<TISEntity> coll,
            Expression<Func<TISEntity, TResult>> selectorExpression) where TISEntity : IModelEntity
        {
            return null;
        }

        //public static ICollection<TResult> Select<TISEntity, TResult>(
        //    this IEnumerable<TISEntity> coll,
        //    Expression<Func<TISEntity, TResult>> selectorExpression) where TISEntity : IDEntity
        //{
        //    return null;
        //}
        /// <summary>
        ///     The where.
        /// </summary>
        /// <param name="collection">
        ///     The collection.
        /// </param>
        /// <param name="expression">
        ///     The expression.
        /// </param>
        /// <typeparam name="TModelEntity">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="ICollection" />.
        /// </returns>
        //public static ICollection<TModelEntity> Where<TModelEntity, TEntityDescriptor>(
        //    this ICollection<TModelEntity> collection,
        //    WhereResult<TModelEntity, TEntityDescriptor> whereResult) where TModelEntity : class, IModelEntity
        //    where TEntityDescriptor : TModelEntity, ISearchableDescriptor
        //{
        //    return null;
        //}

        #endregion
    }
}