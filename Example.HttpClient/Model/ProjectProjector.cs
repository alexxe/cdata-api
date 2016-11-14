// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectSolutionDto.cs" company="">
//   
// </copyright>
// <summary>
//   The project solution dto.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Example.HttpClient.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Covis.Data.DynamicLinq.CQuery.Contracts.DEntity;
    using Covis.Data.DynamicLinq.CQuery.DynamicLinq.Extentions;

    using Example.Data.Contract.Model;

    /// <summary>
    /// The project solution dto.
    /// </summary>
    public class ProjectProjector : IClientProjector
    {
        
        public ProjectProjector()
        {
        }
        #region Public Properties

        /// <summary>
        /// Gets or sets the assembly count.
        /// </summary>
        //public int AssemblyCount { get; set; }

        /// <summary>
        /// Gets or sets the project name.
        /// </summary>
        public string ProjectFileName { get; set; }


        public string SolutionName { get; set; }

        /// <summary>
        /// Gets or sets the solution name.
        /// </summary>
        

        /// <summary>
        /// Gets or sets the t assemblies.
        /// </summary>
        public IEnumerable<AssemblyProjector> Assemblies { get; set; }

        #endregion

       
    }
}