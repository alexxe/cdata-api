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
    using System.Collections.Generic;

    /// <summary>
    ///     The project solution dto.
    /// </summary>
    public class Projection 
    {
        public Projection()
        {
        }

        #region Public Properties

        /// <summary>
        ///     Gets or sets the assembly count.
        /// </summary>
        /// <summary>
        ///     Gets or sets the project name.
        /// </summary>
        public long Id { get; set; }

        public string Firma1 { get; set; }

        

        #endregion
    }
}