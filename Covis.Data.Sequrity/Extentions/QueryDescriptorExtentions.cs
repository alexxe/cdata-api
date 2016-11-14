// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryDescriptorExtentions.cs" company="">
//   
// </copyright>
// <summary>
//   The i queryable extentions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Covis.Data.DynamicLinq.Security.Extentions
{
    using Covis.Data.DynamicLinq.CQuery.Contracts;

    /// <summary>
    ///     The i queryable extentions.
    /// </summary>
    public static class QueryDescriptorExtentions
    {
        #region Public Methods and Operators

        /// <summary>
        /// The as security descriptor.
        /// </summary>
        /// <param name="descriptor">
        /// The descriptor.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <typeparam name="TEntity">
        /// </typeparam>
        /// <returns>
        /// The <see cref="QueryDescriptor"/>.
        /// </returns>
        //public static QueryDescriptor AsSecurityDescriptor<TEntity>(
        //    this QueryDescriptor descriptor,
        //    ISecurityContext<TEntity> context) where TEntity : class
        //{
        //    if (!descriptor.IsMapped)
        //    {
        //        descriptor.Map<TEntity>();
        //    }

        //    var injector = new SecurityInjector<TEntity>(context);
        //    injector.AppendSecurity(descriptor.Root);

        //    return descriptor;
        //}

        #endregion
    }
}