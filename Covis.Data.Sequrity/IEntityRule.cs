// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEntityRule.cs" company="">
//   
// </copyright>
// <summary>
//   The EntityRule interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.DynamicLinq.Security
{
    using System.Collections.Generic;

    /// <summary>
    /// The EntityRule interface.
    /// </summary>
    public interface IEntityRule
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the property rules.
        /// </summary>
        List<IPropertyRule> PropertyRules { get; set; }

        /// <summary>
        /// Gets the table name.
        /// </summary>
        string TableName { get; }

        #endregion
    }
}