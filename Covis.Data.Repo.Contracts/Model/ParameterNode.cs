// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParameterNode.cs" company="">
//   
// </copyright>
// <summary>
//   The parameter node.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.Repo.Contracts.Model
{
    using System;

    /// <summary>
    ///     The parameter node.
    /// </summary>
    public class ParameterNode : LNode
    {
        #region Constructors and Destructors

        public ParameterNode()
        {
            
        }

        #endregion

        #region Public Properties

        

        public Type Type { get; set; }
        #endregion

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}