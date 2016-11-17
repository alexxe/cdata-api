// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Node.cs" company="">
//   
// </copyright>
// <summary>
//   The node.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.SqlProvider.Contracts.Model
{
    /// <summary>
    ///     The node.
    /// </summary>
    public class BNode : LNode
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the right.
        /// </summary>
        public INode Right { get; set; }


        #endregion

        public override void Accept(INodeVisitor visitor)
        {
            
        }
    }
}