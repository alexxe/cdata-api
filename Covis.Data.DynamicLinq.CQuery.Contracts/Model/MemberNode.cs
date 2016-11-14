// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MemberNode.cs" company="">
//   
// </copyright>
// <summary>
//   The member node.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.DynamicLinq.CQuery.Contracts.Model
{
    using System.Runtime.Serialization;

    /// <summary>
    ///     The member node.
    /// </summary>
    [DataContract]
    public class MemberNode : LNode
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MemberNode" /> class.
        /// </summary>
        /// <param name="member">
        ///     The member.
        /// </param>
        public MemberNode(string member)
        {
            this.Member = member;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the member.
        /// </summary>
        [DataMember]
        public string Member { get; set; }

        #endregion

        public override void Accept(INodeVisitor visitor)
        {
            var members = this.Member.Split('.');
            if (members.Length == 1)
            {
                if (this.Left == null)
                {
                    this.Left = new ParameterNode();
                }
            }
            else
            {
                this.Left = new MemberNode(members[members.Length - 2]);
                this.Member = members[members.Length - 1];
            }
            this.Left.Accept(visitor);
            visitor.Visit(this);
        }
    }
}