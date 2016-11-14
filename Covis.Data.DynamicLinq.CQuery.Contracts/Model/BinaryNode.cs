// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilterAssessmentNode.cs" company="">
//   
// </copyright>
// <summary>
//   The filter assessment node.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.DynamicLinq.CQuery.Contracts.Model
{
    using System.Runtime.Serialization;

    /// <summary>
    ///     The filter assessment node.
    /// </summary>
    [DataContract]
    public class BinaryNode : BNode
    {
        public BinaryNode(BinaryOp binaryOperator)
        {
            this.BinaryOperator = binaryOperator;
        }

        #region Public Properties

        /// <summary>
        ///     Gets or sets the filter assessment.
        /// </summary>
        [DataMember]
        public BinaryOp BinaryOperator { get; set; }

        #endregion

        public override void Accept(INodeVisitor visitor)
        {
            this.Left.Accept(visitor);
            this.Right.Accept(visitor);
            visitor.Visit(this);
        }
    }
}