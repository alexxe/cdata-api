// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryDescriptor.cs" company="">
//   
// </copyright>
// <summary>
//   The query parameters.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.SqlProvider.Contracts
{
    using System;
    using System.Collections.Generic;

    using Covis.Data.SqlProvider.Contracts.Model;

    /// <summary>
    ///     The query parameters.
    /// </summary>
    /// <typeparam name="DTO">
    /// </typeparam>
    
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
            this.Root = new EntryPointNode(entryPointType);
        }

        #endregion

        #region Public Properties


        public Type TargetType { get; set; }

        public bool HasProjection { get; set; }
       
        public Type EntryPointType => this.GetEntryPontType(this.Root);

        private Type GetEntryPontType(LNode node)
        {
            var pointNode = node as EntryPointNode;
            return pointNode != null ? pointNode.EntryPointType : this.GetEntryPontType(node.Left);
        }

        /// <summary>
        ///     The includes.
        /// </summary>
        
        public List<MemberNode> IncludeParameters { get; private set; }

        /// <summary>
        ///     Gets the filter parameters.
        /// </summary>
       
        public LNode Root { get; set; }

        
        #endregion
    }
}