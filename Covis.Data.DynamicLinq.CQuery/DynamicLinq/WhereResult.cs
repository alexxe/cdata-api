// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WhereResult.cs" company="">
//   
// </copyright>
// <summary>
//   The where result.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.DynamicLinq.CQuery.DynamicLinq
{
    using Covis.Data.DynamicLinq.CQuery.Contracts;
    using Covis.Data.DynamicLinq.CQuery.Contracts.DEntity;
    using Covis.Data.DynamicLinq.CQuery.Contracts.Model;

    /// <summary>
    ///     The where result.
    /// </summary>
    /// <typeparam name="TModelEntity">
    /// </typeparam>
    /// <typeparam name="TEntityDescriptor">
    /// </typeparam>
    public class WhereResult<TModelEntity, TEntityDescriptor>
        where TModelEntity : class, IModelEntity where TEntityDescriptor : TModelEntity, ISearchableDescriptor
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="WhereResult{TModelEntity,TEntityDescriptor}" /> class.
        ///     Initializes a new instance of the <see cref="WhereResult{TSEntity}" /> class.
        /// </summary>
        /// <param name="whereNode">
        ///     The where Node.
        /// </param>
        public WhereResult(CallNode whereNode)
        {
            this.WhereNode = whereNode;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the c query.
        /// </summary>
        public CallNode WhereNode { get; set; }

        #endregion
    }
}