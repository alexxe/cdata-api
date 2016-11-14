// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntityRule.cs" company="">
//   
// </copyright>
// <summary>
//   The entity rule.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.DynamicLinq.Security
{
    using System.Collections.Generic;

    /// <summary>
    /// The entity rule.
    /// </summary>
    public class EntityRule :IEntityRule
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityRule"/> class.
        /// </summary>
        /// <param name="entityName">
        /// The entity name.
        /// </param>
        public EntityRule(string tableName)
        {
            this.PropertyRules = new List<IPropertyRule>();
            this.TableName = tableName;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the entity name.
        /// </summary>
        public string TableName { get; private set; }
        
        
        /// <summary>
        /// Gets the property rules.
        /// </summary>
        public List<IPropertyRule> PropertyRules { get; set; }

        #endregion


       
    }
}