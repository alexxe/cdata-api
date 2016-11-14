// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IQueryableExtentions.cs" company="">
//   
// </copyright>
// <summary>
//   The i queryable extentions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Covis.Data.DynamicLinq.Provider.Extentions
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Covis.Data.DynamicLinq.CQuery.Contracts;
    using Covis.Data.DynamicLinq.CQuery.Contracts.DEntity;
    using Covis.Data.DynamicLinq.Provider.Mapping;

    

    /// <summary>
    ///     The i queryable extentions.
    /// </summary>
    public static class IQueryableExtentions
    {
        #region Public Methods and Operators

        /// <summary>
        /// The append.
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <param name="descriptor">
        /// The descriptor.
        /// </param>
        /// <typeparam name="TEntity">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Expression"/>.
        /// </returns>
        //public static Expression Append<TEntity>(this IQueryable<TEntity> query, QueryDescriptor descriptor)
        //    where TEntity : class
        //{
        //    var provider = new ExpressionProvider<TEntity>(query);
        //    //if (descriptor.Projector == null)
        //    {
        //        return provider.ConvertProjection(descriptor.Map<TEntity>());
        //    }

        //    //return provider.ConvertProjection(descriptor.Map<TEntity>());
        //}

        /// <summary>
        /// The append.
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <param name="descriptor">
        /// The descriptor.
        /// </param>
        /// <typeparam name="TEntity">
        /// </typeparam>
        /// <typeparam name="TResult">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Expression"/>.
        /// </returns>
        //public static Expression Append<TEntity, TResult>(this IQueryable<TEntity> query, QueryDescriptor descriptor)
        //    where TResult : class where TEntity : class
        //{
        //    var provider = new ExpressionProvider<TEntity>(query);
        //    return provider.ConvertProjection<TResult>(descriptor.Map<TEntity>());
        //}

        /// <summary>
        /// The map.
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <typeparam name="TISEntity">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        //public static IEnumerable<TModelEntity> Map<TEntity, TModelEntity>(this IQueryable<TEntity> query) 
        //    where TModelEntity : class, IDEntity
        //    where TEntity : class
        //{
        //    return CMapper.Map<TModelEntity>(query);
        //}

        /// <summary>
        /// The map.
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <typeparam name="TEntity">
        /// </typeparam>
        /// <typeparam name="TISEntity">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        //public static IEnumerable<TModelEntity> Map<TEntity, TModelEntity>(this IEnumerable<TEntity> query)
        //    where TModelEntity : class,IDEntity
        //    where TEntity : class
        //{
        //    return CMapper.Map<TEntity, TModelEntity>(query);
        //}
        #endregion
    }
}