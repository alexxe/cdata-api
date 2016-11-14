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
    [KnownType(typeof(MemberNode))]
    [KnownType(typeof(ProjectorNode))]
    public class LNode : INode
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the child.
        /// </summary>
        [DataMember]
        public LNode Left { get; set; }


        public override void Accept(INodeVisitor visitor)
        {
            
        }
        #endregion
    }
}