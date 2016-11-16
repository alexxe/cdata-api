// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryDescriptorMap.cs" company="">
//   
// </copyright>
// <summary>
//   The map.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.DynamicLinq.Provider.Mapping
{
    using System.Linq;

    using AutoMapper;

    /// <summary>
    ///     The map.
    /// </summary>
    public static class QueryDescriptorMap
    {
        #region Static Fields

        /// <summary>
        ///     The mappings.
        /// </summary>
        /// <summary>
        ///     The map.
        /// </summary>
        /// <param name="source">
        ///     The source.
        /// </param>
        /// <returns>
        ///     The <see cref="object" />.
        /// </returns>
        public static object Map(object source)
        {
            return null;
            //var mapItem = Mappings.FirstOrDefault(x => x.TargetType == source.GetType());
            var mapItem = Mapper.Configuration.GetAllTypeMaps().FirstOrDefault(x => x.SourceType == source.GetType());
            var result = Mapper.Map(source, source.GetType(), mapItem.DestinationType);
            return result;
        }

        #endregion
    }
}