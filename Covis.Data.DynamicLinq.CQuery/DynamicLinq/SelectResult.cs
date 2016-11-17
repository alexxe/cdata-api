// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectResult.cs" company="">
//   
// </copyright>
// <summary>
//   The select result.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Covis.Data.DynamicLinq.CQuery.Contracts.Contract;

namespace Covis.Data.DynamicLinq.CQuery.DynamicLinq
{
    using Covis.Data.DynamicLinq.CQuery.Contracts;
    using Covis.Data.DynamicLinq.CQuery.Contracts.DEntity;

    /// <summary>
    ///     The select result.
    /// </summary>
    /// <typeparam name="TModelEntity">
    /// </typeparam>
    /// <typeparam name="TEntityDescriptor">
    /// </typeparam>
    /// <typeparam name="TResult">
    /// </typeparam>
    public class SelectResult<TModelEntity, TEntityDescriptor> : ISelectResult<TModelEntity>
        where TModelEntity : class, IModelEntity where TEntityDescriptor : TModelEntity, ISearchableDescriptor

    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SelectResult{TModelEntity,TEntityDescriptor,TResult}" /> class.
        ///     Initializes a new instance of the <see cref="WhereResult{TSEntity}" /> class.
        /// </summary>
        /// <param name="dQuery">
        ///     The c query.
        /// </param>
        public SelectResult(QDescriptorBuilder<TModelEntity, TEntityDescriptor> dQuery)
        {
            this.DQuery = dQuery;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the c query.
        /// </summary>
        internal QDescriptorBuilder<TModelEntity, TEntityDescriptor> DQuery { get; set; }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the descriptor.
        /// </summary>
        public QDescriptor Descriptor => this.DQuery.Descriptor;

        #endregion
    }
}