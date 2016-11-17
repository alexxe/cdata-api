// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectorNode.cs" company="">
//   
// </copyright>
// <summary>
//   The selector node.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.SqlProvider.Contracts.Model
{
    using System.Collections.Generic;

    /// <summary>
    ///     The selector node.
    /// </summary>
    
    public class ProjectorNode : LNode
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ProjectorNode" /> class.
        /// </summary>
        public ProjectorNode()
        {
            this.Bindings = new Dictionary<string, INode>();
        }

        #endregion

        #region Public Properties
       

        /// <summary>
        ///     Gets the bindings.
        /// </summary>
       
        public Dictionary<string, INode> Bindings { get; private set; }

        #endregion

        public override void Accept(INodeVisitor visitor)
        {
            this.Left.Accept(visitor);
            visitor.EnterContext(this);
            visitor.Visit(this);
            visitor.LeaveContext(this);
        }
    }
}