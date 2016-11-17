// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CQueryExtentions.cs" company="">
//   
// </copyright>
// <summary>
//   The c query extentions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.DynamicLinq.CQuery.DynamicLinq.Extentions
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Covis.Data.DynamicLinq.CQuery.Contracts.DEntity;

    /// <summary>
    ///     The c query extentions.
    /// </summary>
    public static class IEnumerableExtentions
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
        public static int Count<TModelEntity>(
            this IEnumerable<TModelEntity> collection,
            Expression<Func<TModelEntity, bool>> expression) where TModelEntity : IModelEntity
        {
            return 0;
        }

        public static int Count<TModelEntity>(
            this IEnumerable<TModelEntity> collection) where TModelEntity : IModelEntity
        {
            return 0;
        }

        public static bool Any<TModelEntity>(
            this IEnumerable<TModelEntity> collection,
            Expression<Func<TModelEntity, bool>> expression) where TModelEntity : IModelEntity
        {
            return true;
        }

        public static ICollection<TResult> Select<TIDEntity, TResult>(
            this IEnumerable<TIDEntity> coll,
            Expression<Func<TIDEntity, TResult>> selectorExpression) where TIDEntity : IModelEntity
        {
            return null;
        }

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
        //public static IEnumerable<TModelEntity> Where<TModelEntity, TEntityDescriptor>(
        //    this IEnumerable<TModelEntity> collection,
        //    WhereResult<TModelEntity, TEntityDescriptor> whereResult) where TModelEntity : class, IModelEntity
        //    where TEntityDescriptor : TModelEntity, ISearchableDescriptor
        //{
        //    return null;
        //}

        
        #endregion
    }
}