// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPropertyRule.cs" company="">
//   
// </copyright>
// <summary>
//   The PropertyRule interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.DynamicLinq.Security
{
    using Covis.Data.DynamicLinq.CQuery.Contracts;
    using Covis.Data.DynamicLinq.CQuery.Contracts.Model;

    /// <summary>
    /// The PropertyRule interface.
    /// </summary>
    public interface IPropertyRule
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the op.
        /// </summary>
        BinaryOp Op { get; set; }

        /// <summary>
        ///     Gets or sets the property name.
        /// </summary>
        string PropertyName { get; set; }

        #endregion
    }
}