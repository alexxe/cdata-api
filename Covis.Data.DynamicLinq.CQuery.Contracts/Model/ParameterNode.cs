// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParameterNode.cs" company="">
//   
// </copyright>
// <summary>
//   The parameter node.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.DynamicLinq.CQuery.Contracts.Model
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    ///     The parameter node.
    /// </summary>
    [DataContract]
    public class ParameterNode : LNode
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ParameterNode" /> class.
        /// </summary>
        /// <param name="typeName">
        ///     The parameter type.
        /// </param>
        public ParameterNode()
        {
            //this.TypeName = typeName;
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the parameter type.
        /// </summary>
        //[DataMember]
        //public string TypeName { get; set; }

        public Type Type { get; set; }
        #endregion

        public override void Accept(INodeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}