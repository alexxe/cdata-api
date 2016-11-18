// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExpressionParser.cs" company="">
//   
// </copyright>
// <summary>
//   The my expression visitor.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using QData.Json.Contracts;

namespace QData.SqlProvider.builder
{
    public class MemberNodeConverter 
    {
        private Mapping Mapping { get; set; }
        #region Constructors and Destructors

        public MemberNodeConverter(Mapping mapping)
        {
            this.Mapping = mapping;
        }

        
        #endregion

        #region Properties

        
        private Expression MemberExpression { get; set; }

        #endregion

        #region Methods

        public Type[] GetMappingTypes(QNode querable)
        {
            return this.Mapping.GeTypesForQuery(querable);
        }

        public Expression ConvertToMemberExpression(ParameterExpression parameter, QNode node)
        {
            this.MemberExpression = parameter;
            this.Mapping.SetCurrentMap(parameter.Type);

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
            var mapped = this.Mapping.GetMapNameForMember(member);
            this.MemberExpression = Expression.Property(this.MemberExpression, mapped);
            
        }

        #endregion
    }
}