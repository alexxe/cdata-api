using System;
using System.Linq;
using AutoMapper;
using QData.Common;
using QData.Json.Contracts;

namespace QData.SqlProvider.builder
{
    public class Mapping
    {
        private readonly MapperConfiguration mapperConfiguration;

        private TypeMap[] Maps { get; set; }

        private TypeMap CurrentMap { get; set; }

        public bool EnableMapping { get; set; }

        public Mapping(MapperConfiguration mapperConfiguration)
        {
            this.mapperConfiguration = mapperConfiguration;
            this.Maps = this.mapperConfiguration.GetAllTypeMaps();
        }

        public Type[] GeTypesForQuery(QNode query)
        {
            var typeMap =
                this.mapperConfiguration.GetAllTypeMaps()
                    .FirstOrDefault(x => x.DestinationType.Name.Contains(Convert.ToString(query.Value)));
            return new Type[] { typeMap.SourceType, typeMap.DestinationType };
        }

        public void SetCurrentMap(Type sourceType)
        {
            if (this.EnableMapping)
            {
                this.CurrentMap =
                    this.mapperConfiguration.GetAllTypeMaps().FirstOrDefault(x => x.SourceType == sourceType);
            }
        }

        public string GetMapNameForMember(string member)
        {
            if (!this.EnableMapping)
            {
                return member;
            }

            var propertyMap =
                this.CurrentMap.GetPropertyMaps()
                    .FirstOrDefault(
                        x => x.DestinationProperty.Name.Equals(member, StringComparison.CurrentCultureIgnoreCase));
            if (propertyMap.DestinationPropertyType.IsGenericType
                && typeof(IModelEntity).IsAssignableFrom(propertyMap.DestinationPropertyType.GenericTypeArguments[0]))
            {
                Type sourceType;
                if (propertyMap.CustomExpression != null)
                {
                    sourceType = propertyMap.CustomExpression.Body.Type.GenericTypeArguments[0];
                }
                else
                {
                    sourceType = propertyMap.SourceType.GenericTypeArguments[0];
                }

                this.CurrentMap = this.mapperConfiguration.GetAllTypeMaps().FirstOrDefault(x => x.SourceType == sourceType);
            }
            else if (typeof(IModelEntity).IsAssignableFrom(propertyMap.DestinationPropertyType))
            {
                Type sourceType;
                if (propertyMap.CustomExpression != null)
                {
                    sourceType = propertyMap.CustomExpression.Body.Type;
                }
                else
                {
                    sourceType = propertyMap.SourceType;
                }

                this.CurrentMap = this.mapperConfiguration.GetAllTypeMaps().FirstOrDefault(x => x.SourceType == sourceType);
            }

            return propertyMap.SourceMember.Name;
        }
    }
}
