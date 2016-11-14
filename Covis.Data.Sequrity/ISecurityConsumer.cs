// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISecurityConsumer.cs" company="">
//   
// </copyright>
// <summary>
//   The SecurityConsumer interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Covis.Data.DynamicLinq.Security
{
    /// <summary>
    ///     The SecurityConsumer interface.
    /// </summary>
    public interface ISecurityConsumer
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the security context name.
        /// </summary>
        string SecurityContextName { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get table name.
        /// </summary>
        /// <param name="property">
        /// The property.
        /// </param>
        /// <typeparam name="TEntity">
        /// </typeparam>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        //string GetTableName<TEntity>(string property) where TEntity : class;

        /// <summary>
        /// The get value for rule.
        /// </summary>
        /// <param name="entityName">
        /// The entity name.
        /// </param>
        /// <param name="propertyName">
        /// The property name.
        /// </param>
        /// <param name="values">
        /// The values.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        object GetValueForRule(string entityName,QuerablePropertyRule rule);

        

        #endregion
    }
}