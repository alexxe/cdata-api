// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SecurityContext.cs" company="">
//   
// </copyright>
// <summary>
//   The security context.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Covis.Data.DynamicLinq.Security
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The security context.
    /// </summary>
    /// <typeparam name="TEntity">
    /// </typeparam>
    public class SecurityContext : ISecurityContext
    
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityContext{TEntity}"/> class. 
        /// Initializes a new instance of the <see cref="SecurityContext"/> class.
        /// </summary>
        /// <param name="consument">
        /// The consument.
        /// </param>
        /// <param name="user">
        /// The user.
        /// </param>
        public SecurityContext(ISecurityConsumer consument, string user)
        {
            this.Consument = consument;
            this.User = user;
            this.Rules = new SecurityServiceWrapper().GetRules(consument.SecurityContextName, user);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the consument.
        /// </summary>
        public ISecurityConsumer Consument { get; set; }

        /// <summary>
        ///     Gets or sets the user.
        /// </summary>
        public string User { get; set; }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets the rules.
        /// </summary>
        internal List<IEntityRule> Rules { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get rule.
        /// </summary>
        /// <param name="entityName">
        /// The entity name.
        /// </param>
        /// <returns>
        /// The <see cref="IEntityRule"/>.
        /// </returns>
        public IEntityRule GetRule(string entityName)
        {
            return this.Rules.FirstOrDefault(x=>x.TableName == entityName);
        }

        #endregion
    }
}