﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropertyRule.cs" company="">
//   
// </copyright>
// <summary>
//   The property rule.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.DynamicLinq.Security
{
    using Covis.Data.DynamicLinq.CQuery.Contracts;
    using Covis.Data.DynamicLinq.CQuery.Contracts.Model;

    /// <summary>
    /// The property rule.
    /// </summary>
    public class QuerablePropertyRule : IPropertyRule
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyRule"/> class.
        /// </summary>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        /// <param name="op">
        /// The op.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public QuerablePropertyRule(string propertyName, BinaryOp op, string[] placeholder)
        {
            this.PropertyName = propertyName;
            this.Op = op;
            this.Placeholder = placeholder;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the op.
        /// </summary>
        public BinaryOp Op { get; set; }

        /// <summary>
        /// Gets or sets the property name.
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the op.
        /// </summary>
        public string[] Placeholder { get; set; }
        
        #endregion
    }
}