// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExpressionParser.cs" company="">
//   
// </copyright>
// <summary>
//   The my expression visitor.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.LinqConverter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using AutoMapper;

    using Covis.Data.Common;
    using Covis.Data.Json.Contracts;

    public class Util
    {
        private readonly MapperConfiguration mapperConfiguration;

        #region Constructors and Destructors

        public Util(MapperConfiguration mapperConfiguration)
        {
            this.mapperConfiguration = mapperConfiguration;
        }

        #endregion

        #region Properties

        private TypeMap Map { get; set; }

        private Expression MemberExpression { get; set; }

        #endregion

        #region Methods

        public Type[] GetMappingTypes(QNode querable)
        {
            var typeMap =
                this.mapperConfiguration.GetAllTypeMaps()
                    .FirstOrDefault(x => x.DestinationType.Name.Contains(Convert.ToString(querable.Value)));
            return new Type[] { typeMap.SourceType, typeMap.DestinationType };
        }

        public Expression ConvertToMemberExpression(ParameterExpression parameter, QNode node)
        {
            this.MemberExpression = parameter;
            this.Map = this.mapperConfiguration.GetAllTypeMaps().FirstOrDefault(x => x.SourceType == parameter.Type);

            var members = Convert.ToString(node.Value).Split('.');
            foreach (var member in members)
            {
                this.VisitMember(member);
            }
            return this.MemberExpression;
        }


        public Dictionary<string,Expression> ConvertToBindings(ParameterExpression parameter, QNode node)
        {
            var result = new Dictionary<string, Expression>();
            var root = node.Right;
            do
            {
                var property = Convert.ToString(root.Value);
                var bindingPaar = property.Split(':');
                if (bindingPaar.Length == 2)
                {
                    property = bindingPaar[0];
                    root.Value = bindingPaar[1];
                }
                var member = this.ConvertToMemberExpression(parameter, root);
                result.Add(property, member);
                root = root.Left;
            }
            while (root != null);
            return result;
        }

        protected void VisitMember(string member)
        {
            var propertyMap =
                this.Map.GetPropertyMaps()
                    .FirstOrDefault(
                        x => x.DestinationProperty.Name.Equals(member, StringComparison.CurrentCultureIgnoreCase));
            this.MemberExpression = Expression.Property(this.MemberExpression, propertyMap.SourceMember.Name);

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

                this.Map = this.mapperConfiguration.GetAllTypeMaps().FirstOrDefault(x => x.SourceType == sourceType);
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

                this.Map = this.mapperConfiguration.GetAllTypeMaps().FirstOrDefault(x => x.SourceType == sourceType);
            }
        }

        #endregion
    }
}