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
    public class CallNode : BNode
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the method name.
        /// </summary>
        [DataMember]
        public string Method { get; set; }

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
        public CallNode(string method)
        {
            this.Method = method;
        }

        #endregion

        private bool ChangingContext()
        {
            if (this.Method == "Where" || this.Method == "Count" || this.Method == "Any")
            {
                return true;
            }
            return false;
        }
        public override void Accept(INodeVisitor visitor)
        {
            this.Left.Accept(visitor);
            if (this.ChangingContext() && this.Right != null)
            {
                visitor.EnterContext(this);
            }

            if (this.Right != null)
            {
                this.Right.Accept(visitor);
            }
            visitor.Visit(this);
            if (this.ChangingContext() && this.Right != null)
            {
                visitor.LeaveContext(this);
            }
        }
    }
}