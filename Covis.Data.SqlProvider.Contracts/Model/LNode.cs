// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MemberNode.cs" company="">
//   
// </copyright>
// <summary>
//   The member node.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.SqlProvider.Contracts.Model
{
    /// <summary>
    ///     The member node.
    /// </summary>
    public class LNode : INode
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the child.
        /// </summary>
        public LNode Left { get; set; }


        public override void Accept(INodeVisitor visitor)
        {
            
        }
        #endregion
    }
}