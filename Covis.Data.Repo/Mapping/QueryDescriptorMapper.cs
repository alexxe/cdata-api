// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryDescriptorMapper.cs" company="">
//   
// </copyright>
// <summary>
//   The contract version resolver.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.Repo.Mapping
{
    using AutoMapper;

    using Covis.Data.SqlProvider.Contracts;

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
            var mapper = new NodeMapper(mapperConfiguration);
            descriptor.Root.Accept(mapper);
            descriptor.TargetType = mapper.TargetType;

            foreach (var include in descriptor.IncludeParameters)
            {
                mapper = new NodeMapper(mapperConfiguration, descriptor.EntryPointType);
                include.Accept(mapper);
            }

            return descriptor;
        }

        

        #endregion
    }
}