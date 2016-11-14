// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryDescriptor.cs" company="">
//   
// </copyright>
// <summary>
//   The query parameters.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.DynamicLinq.CQuery.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    using Covis.Data.DynamicLinq.CQuery.Contracts.Model;

    /// <summary>
    ///     The query parameters.
    /// </summary>
    /// <typeparam name="DTO">
    /// </typeparam>
    [DataContract]
    public class QueryDescriptor
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="QueryDescriptor" /> class.
        ///     Initializes a new instance of the <see cref="QueryDescriptor" /> class.
        /// </summary>
        public QueryDescriptor(Type entryPointType)
        {
            this.IncludeParameters = new List<MemberNode>();
            this.IsMapped = false;
            this.Root = new EntryPointNode(entryPointType);
            this.QueryType = QueryType.Default;
        }

        #endregion

        #region Public Properties


        public Type TargetType { get; set; }

        [DataMember]
        public QueryType QueryType { get; set; }

        public Type EntryPointType => this.GetEntryPontType(this.Root);

        private Type GetEntryPontType(LNode node)
        {
            var pointNode = node as EntryPointNode;
            return pointNode != null ? pointNode.EntryPointType : this.GetEntryPontType(node.Left);
        }

        /// <summary>
        ///     The includes.
        /// </summary>
        [DataMember]
        public List<MemberNode> IncludeParameters { get; private set; }

        /// <summary>
        ///     Gets or sets a value indicating whether is mapped.
        /// </summary>
        public bool IsMapped { get; set; }

        /// <summary>
        ///     Gets the filter parameters.
        /// </summary>
        [DataMember]
        public LNode Root { get; set; }

        
        #endregion
    }
}