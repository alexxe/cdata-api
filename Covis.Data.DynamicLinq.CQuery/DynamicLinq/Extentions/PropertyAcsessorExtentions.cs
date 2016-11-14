// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropertyAcsessorExtentions.cs" company="">
//   
// </copyright>
// <summary>
//   The in result.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.DynamicLinq.CQuery.DynamicLinq.Extentions
{
    using System.Collections.Generic;

    using Covis.Data.DynamicLinq.CQuery.Contracts.DEntity;

    /// <summary>
    ///     The in result.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class InResult<T>
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="InResult{T}" /> class.
        /// </summary>
        /// <param name="property">
        ///     The property.
        /// </param>
        /// <param name="value">
        ///     The value.
        /// </param>
        public InResult(PropertyAcsessor<T> property, IEnumerable<T> value)
        {
            this.Property = property;
            this.Value = value;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the property.
        /// </summary>
        public PropertyAcsessor<T> Property { get; set; }

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        public IEnumerable<T> Value { get; set; }

        #endregion
    }

    /// <summary>
    ///     The property acsessor extentions.
    /// </summary>
    public static class PropertyAcsessorExtentions
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The in.
        /// </summary>
        /// <param name="property">
        ///     The property.
        /// </param>
        /// <param name="value">
        ///     The value.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        ///     The <see cref="InResult" />.
        /// </returns>
        public static InResult<T> In<T>(this PropertyAcsessor<T> property, IEnumerable<T> value)
        {
            return new InResult<T>(property, value);
        }

        #endregion
    }
}