// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryDescriptorMapper.cs" company="">
//   
// </copyright>
// <summary>
//   The contract version resolver.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.DynamicLinq.Provider.Mapping
{
    using System.Linq.Expressions;

    using AutoMapper;

    using Covis.Data.DynamicLinq.CQuery.Contracts;
    using Covis.Data.DynamicLinq.Security;

    /// <summary>
    ///     The contract version resolver.
    /// </summary>
    /// <typeparam name="TEntity">
    /// </typeparam>
    public static class QueryDescriptorMapper
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The resolve contract property.
        /// </summary>
        /// <param name="descriptor">
        ///     The param.
        /// </param>
        /// <param name="map">
        ///     The map.
        /// </param>
        public static QueryDescriptor Map(QueryDescriptor descriptor, MapperConfiguration mapperConfiguration)
            
        {
            if (descriptor.IsMapped)
            {
                return descriptor;
            }

            

            var mapper = new NodeMapper(mapperConfiguration);
            descriptor.Root.Accept(mapper);
            descriptor.TargetType = mapper.TargetType;

            foreach (var include in descriptor.IncludeParameters)
            {
                mapper = new NodeMapper(mapperConfiguration, descriptor.EntryPointType);
                include.Accept(mapper);
            }

            descriptor.IsMapped = true;
            
            return descriptor;
        }

        public static QueryDescriptor Map(
            QueryDescriptor descriptor,
            MapperConfiguration mapperConfiguration,
            ISecurityContext securityContext)
           
        {
            if (descriptor.IsMapped)
            {
                return descriptor;
            }

            var mapper = new NodeMapper(mapperConfiguration);
            descriptor.Root.Accept(mapper);

            foreach (var include in descriptor.IncludeParameters)
            {
                mapper = new NodeMapper(mapperConfiguration);
                include.Accept(mapper);
            }

            descriptor.IsMapped = true;
            return descriptor;
        }

        #endregion
    }
}