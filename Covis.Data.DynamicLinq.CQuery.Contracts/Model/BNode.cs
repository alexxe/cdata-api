// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Node.cs" company="">
//   
// </copyright>
// <summary>
//   The node.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.DynamicLinq.CQuery.Contracts.Model
{
    using System.Runtime.Serialization;

    /// <summary>
    ///     The node.
    /// </summary>
    [DataContract]
    [KnownType(typeof(CallNode))]
    [KnownType(typeof(BinaryNode))]
    public class BNode : LNode
    {
        #region Public Properties

        /// <summary>
        ///     Gets or sets the right.
        /// </summary>
        [DataMember]
        public INode Right { get; set; }


        #endregion

        public override void Accept(INodeVisitor visitor)
        {
            
        }
    }
}