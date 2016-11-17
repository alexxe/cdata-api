// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntryPointNode.cs" company="">
//   
// </copyright>
// <summary>
//   The entry point node.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.SqlProvider.Contracts.Model
{
    using System;

    /// <summary>
    ///     The entry point node.
    /// </summary>
    
    public class EntryPointNode : LNode
    {
        public EntryPointNode(Type entryPointType)
        {
            this.EntryPointType = entryPointType;
        }

       
        public Type EntryPointType { get; set; }

        public override void Accept(INodeVisitor visitor)
        {
            //this.Left.Accept(visitor);
            visitor.Visit(this);
        }
    }
}