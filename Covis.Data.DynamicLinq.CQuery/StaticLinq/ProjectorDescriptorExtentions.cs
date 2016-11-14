// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CQueryExtentions.cs" company="">
//   
// </copyright>
// <summary>
//   The c query extentions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Covis.Data.SerializeLinq.Client.Extentions
{
    using System;
    using System.Linq.Expressions;

    using Covis.Data.SerializeLinq.Client.Contracts;
    using Covis.Data.SerializeLinq.DynamicQuery;
    using Covis.Data.SerializeLinq.DynamicQuery.Contracts;

    /// <summary>
    ///     The c query extentions.
    /// </summary>
    public static class ProjectorDescriptorExtentions
    {
        #region Public Methods and Operators


        public static QueryParameters AsQueryParameters<TISEntity, TResult>(this ProjectorDescriptor<TISEntity, TResult> descriptor) where TISEntity : class, ISEntity where TResult : class
        {
            var param = descriptor.QueryDescriptor.AsQueryParameters();

            if (descriptor.Selector != null)
            {
                var parser = new ProjectorParser();
                parser.Visit(descriptor.Selector);

                param.SelectorParameters.AddRange(parser.SelectorParameters);
            }

            return param;
        }

        
        
        

        #endregion
    }
}