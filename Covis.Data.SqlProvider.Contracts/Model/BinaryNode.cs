// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilterAssessmentNode.cs" company="">
//   
// </copyright>
// <summary>
//   The filter assessment node.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.SqlProvider.Contracts.Model
{
    using Covis.Data.Common;

    /// <summary>
    ///     The filter assessment node.
    /// </summary>
    public class BinaryNode : BNode
    {
        public BinaryNode(BinaryType binaryOperator)
        {
            this.BinaryOperator = binaryOperator;
        }

        #region Public Properties

        /// <summary>
        ///     Gets or sets the filter assessment.
        /// </summary>
        public BinaryType BinaryOperator { get; set; }

        #endregion

        public override void Accept(INodeVisitor visitor)
        {
            this.Left.Accept(visitor);
            this.Right.Accept(visitor);
            visitor.Visit(this);
        }
    }
}