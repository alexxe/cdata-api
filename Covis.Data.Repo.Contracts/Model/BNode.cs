// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Node.cs" company="">
//   
// </copyright>
// <summary>
//   The node.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.Repo.Contracts.Model
{
    using System.Runtime.Serialization;

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