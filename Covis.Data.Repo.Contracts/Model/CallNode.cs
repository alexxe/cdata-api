// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CallNode.cs" company="">
//   
// </copyright>
// <summary>
//   The call node.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.Repo.Contracts.Model
{
    using System.Runtime.Serialization;

    using Covis.Data.Json.Contracts;

    /// <summary>
    ///     The call node.
    /// </summary>
    
    public class CallNode : BNode
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the method name.
        /// </summary>
        
        public MethodType Method { get; set; }

        #endregion

        #region Constructors and Destructors

        protected CallNode()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="CallNode" /> class.
        /// </summary>
        /// <param name="method">
        ///     The method name.
        /// </param>
        public CallNode(MethodType method)
        {
            this.Method = method;
        }

        #endregion

        public override void Accept(INodeVisitor visitor)
        {
            this.Left.Accept(visitor);
            visitor.EnterContext(this);
            this.Right.Accept(visitor);
            visitor.Visit(this);
            visitor.LeaveContext(this);
            
        }
    }
}