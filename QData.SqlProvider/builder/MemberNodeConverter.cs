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
using Qdata.Json.Contract;


namespace QData.SqlProvider.builder
{
    public class MemberNodeConverter
    {
        #region Constructors and Destructors

        public MemberNodeConverter(Mapping mapping)
        {
            Mapping = mapping;
        }

        #endregion

        private Mapping Mapping { get; }

        #region Properties

        private Expression MemberExpression { get; set; }

        #endregion

        #region Methods

        public Expression ConvertToMemberExpression(ParameterExpression parameter, QNode node)
        {
            MemberExpression = parameter;
            Mapping.SetCurrentMap(parameter.Type);

            var members = Convert.ToString(node.Value).Split('.');
            foreach (var member in members)
            {
                VisitMember(member);
            }
            return MemberExpression;
        }


        public Dictionary<string, Expression> ConvertToBindings(ParameterExpression parameter, QNode node)
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
                var member = ConvertToMemberExpression(parameter, root);
                result.Add(property, member);
                root = root.Left;
            } while (root != null);
            return result;
        }

        protected void VisitMember(string member)
        {
            var mapped = Mapping.GetMapNameForMember(member);
            MemberExpression = Expression.Property(MemberExpression, mapped);
        }

        #endregion
    }
}