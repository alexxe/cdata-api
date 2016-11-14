// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntryPointNode.cs" company="">
//   
// </copyright>
// <summary>
//   The entry point node.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.DynamicLinq.CQuery.Contracts.Model
{
    using System;
    using System.Runtime.Serialization;

    

    /// <summary>
    ///     The entry point node.
    /// </summary>
    [DataContract]
    public class EntryPointNode : LNode
    {
        public EntryPointNode(Type entryPointType)
        {
            this.EntryPointType = entryPointType;
        }

        [DataMember]
        public Type EntryPointType { get; set; }

        public override void Accept(INodeVisitor visitor)
        {
            //this.Left.Accept(visitor);
            visitor.Visit(this);
        }
    }
}