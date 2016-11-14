// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SQueryExtentions.cs" company="">
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

    using Covis.Data.DynamicLinq.CQuery.Contracts;
    using Covis.Data.DynamicLinq.CQuery.Contracts.DEntity;
    using Covis.Data.DynamicLinq.CQuery.Contracts.Model;

    /// <summary>
    ///     The c query extentions.
    /// </summary>
    public static class SQueryExtentions
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The include.
        /// </summary>
        /// <param name="query">
        ///     The query.
        /// </param>
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
        public static SQuery<TModelEntity> Include<TModelEntity, T>(
            this SQuery<TModelEntity> query,
            Expression<Func<TModelEntity, T>> includeExpression) where TModelEntity : class, IModelEntity where T : IModelEntity
        {
            var memberNode = (MemberNode)new ExpressionConverter().Convert((MemberExpression)includeExpression.Body);
            query.Descriptor.IncludeParameters.Add(memberNode);
            return query;
        }

        /// <summary>
        ///     The include.
        /// </summary>
        /// <param name="query">
        ///     The query.
        /// </param>
        /// <param name="includeExpression">
        ///     The include expression.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="DQuery{TISEntity}" />.
        /// </returns>
        public static SQuery<TModelEntity> Include<TModelEntity, T>(
            this SQuery<TModelEntity> query,
            Expression<Func<TModelEntity, ICollection<T>>> includeExpression) where TModelEntity : class, IModelEntity
            where T : IModelEntity
        {
            var memberNode = (MemberNode)new ExpressionConverter().Convert((MemberExpression)includeExpression.Body);
            query.Descriptor.IncludeParameters.Add(memberNode);
            return query;
        }

        /// <summary>
        ///     The where.
        /// </summary>
        /// <param name="query">
        ///     The query.
        /// </param>
        /// <param name="filterExpression">
        ///     The filter expression.
        /// </param>
        /// <typeparam name="TModelEntity">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="SQuery" />.
        /// </returns>
        

       
        #endregion
    }
}