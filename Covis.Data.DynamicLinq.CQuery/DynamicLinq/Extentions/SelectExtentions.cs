// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectExtentions.cs" company="">
//   
// </copyright>
// <summary>
//   The extentions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.DynamicLinq.CQuery.DynamicLinq.Extentions
{
    using System;
    using System.Linq.Expressions;

    using Covis.Data.DynamicLinq.CQuery.Contracts;
    using Covis.Data.DynamicLinq.CQuery.Contracts.DEntity;
    using Covis.Data.DynamicLinq.CQuery.Contracts.Model;

    /// <summary>
    ///     The extentions.
    /// </summary>
    public static class Extentions1
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The select.
        /// </summary>
        /// <param name="cquery">
        ///     The cquery.
        /// </param>
        /// <param name="selectorExpression">
        ///     The selector expression.
        /// </param>
        /// <typeparam name="TModelEntity">
        /// </typeparam>
        /// <typeparam name="TResult">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="SelectResult" />.
        /// </returns>
        //public static SelectResult<TModelEntity, TEntityDescriptor> Select<TModelEntity, TEntityDescriptor>(
        //    this DQuery<TModelEntity, TEntityDescriptor> cquery,
        //    Expression<Func<TModelEntity, TModelEntity>> selectorExpression) where TModelEntity : class, IModelEntity
        //    where TEntityDescriptor : TModelEntity, ISearchableDescriptor

        //{
        //    var node = (ProjectorNode)new ExpressionConverter().Convert(selectorExpression);
        //    node.Left = cquery.Descriptor.Root;
        //    cquery.Descriptor.Root = node;
        //    return new SelectResult<TModelEntity, TEntityDescriptor>(cquery);
        //}

        #endregion
    }
}