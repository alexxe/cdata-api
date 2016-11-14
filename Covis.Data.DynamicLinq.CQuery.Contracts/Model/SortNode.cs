// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CallNode.cs" company="">
//   
// </copyright>
// <summary>
//   The call node.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.DynamicLinq.CQuery.Contracts.Model
{
    using System.Runtime.Serialization;

    /// <summary>
    ///     The call node.
    /// </summary>
    [DataContract]
    public class SortNode : CallNode
    {
        #region Constructors and Destructors

        protected SortNode()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CallNode" /> class.
        /// </summary>
        /// <param name="method">
        ///     The method name.
        /// </param>
        public SortNode(bool direction)
        {
            this.Method = direction ? "OrderBy" : "OrderByDescending";
        }

        #endregion

        public override void Accept(INodeVisitor visitor)
        {
            this.Left.Accept(visitor);
            visitor.EnterContext(this);
            this.Right.Accept(visitor);
            visitor.Visit(this);
        }
    }
}