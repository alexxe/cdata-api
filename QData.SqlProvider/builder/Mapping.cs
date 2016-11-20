using System;
using System.Linq;
using AutoMapper;
using QData.Common;

namespace QData.SqlProvider.builder
{
    public class Mapping
    {
        private readonly MapperConfiguration mapperConfiguration;

        public Mapping(MapperConfiguration mapperConfiguration)
        {
            this.mapperConfiguration = mapperConfiguration;
            Maps = this.mapperConfiguration.GetAllTypeMaps();
        }

        private TypeMap[] Maps { get; set; }
        private TypeMap CurrentMap { get; set; }
        public bool EnableMapping { get; set; }

        public void SetCurrentMap(Type sourceType)
        {
            if (EnableMapping)
            {
                CurrentMap =
                    mapperConfiguration.GetAllTypeMaps().FirstOrDefault(x => x.SourceType == sourceType);
            }
        }

        public string GetMapNameForMember(string member)
        {
            if (!EnableMapping)
            {
                return member;
            }

            var propertyMap =
                CurrentMap.GetPropertyMaps()
                    .FirstOrDefault(
                        x => x.DestinationProperty.Name.Equals(member, StringComparison.CurrentCultureIgnoreCase));
            if (propertyMap.DestinationPropertyType.IsGenericType
                && typeof (IModelEntity).IsAssignableFrom(propertyMap.DestinationPropertyType.GenericTypeArguments[0]))
            {
                Type sourceType = propertyMap.CustomExpression != null ? propertyMap.CustomExpression.Body.Type.GenericTypeArguments[0] : propertyMap.SourceType.GenericTypeArguments[0];

                CurrentMap =
                    mapperConfiguration.GetAllTypeMaps().FirstOrDefault(x => x.SourceType == sourceType);
            }
            else if (typeof (IModelEntity).IsAssignableFrom(propertyMap.DestinationPropertyType))
            {
                Type sourceType = propertyMap.CustomExpression != null ? propertyMap.CustomExpression.Body.Type : propertyMap.SourceType;

                CurrentMap =
                    mapperConfiguration.GetAllTypeMaps().FirstOrDefault(x => x.SourceType == sourceType);
            }

            return propertyMap.SourceMember.Name;
        }
    }
}