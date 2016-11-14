// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConstantNodeExtended.cs" company="">
//   
// </copyright>
// <summary>
//   The constant node extended.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.DynamicLinq.Provider.Extentions
{
    using System;

    using Covis.Data.DynamicLinq.CQuery.Contracts;
    using Covis.Data.DynamicLinq.CQuery.Contracts.Model;

    /// <summary>
    /// The constant node extended.
    /// </summary>
    public class TypedConstantNode : ConstantNode
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TypedConstantNode"/> class.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        public TypedConstantNode(object value, Type type)
            : base(value)
        {
            this.Type = type;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public Type Type { get; set; }

        #endregion
    }
}