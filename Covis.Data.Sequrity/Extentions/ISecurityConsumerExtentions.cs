// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISecurityConsumerExtentions.cs" company="">
//   
// </copyright>
// <summary>
//   The i security consumer extentions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.DynamicLinq.Security.Extentions
{
    /// <summary>
    /// The i security consumer extentions.
    /// </summary>
    public static class ISecurityConsumerExtentions
    {
        #region Public Methods and Operators

        /// <summary>
        /// The use security.
        /// </summary>
        /// <param name="app">
        /// The app.
        /// </param>
        public static void UseSecurity(this ISecurityConsumer app)
        {
            if (Consumenten.Apps.Contains(app))
            {
                Consumenten.Apps.Add(app);
            }
        }

        public static ISecurityContext GetContext(this ISecurityConsumer app, string user)
        {
            return new SecurityContext(app,user);
        }

        #endregion
    }
}