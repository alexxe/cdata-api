// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SecurityServiceWrapper.cs" company="">
//   
// </copyright>
// <summary>
//   The security service wrapper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.DynamicLinq.Security
{
    using System.Collections.Generic;

    using Covis.Data.DynamicLinq.CQuery.Contracts;
    using Covis.Data.DynamicLinq.CQuery.Contracts.Model;

    /// <summary>
    /// The security service wrapper.
    /// </summary>
    public class SecurityServiceWrapper
    {
        
        #region Public Methods and Operators

        public List<IEntityRule> GetRules(string contextName,string userName)
        {
            var Rules = new List<IEntityRule>();
            var rule = new EntityRule("tProject");
            rule.PropertyRules.Add(new PropertyRule("ID", BinaryOp.GreaterThan, 2));
            rule.PropertyRules.Add(new QuerablePropertyRule("ID", BinaryOp.LessThan, new[] { "hierarchy", "2" }));

            var rule1 = new EntityRule("tSolution");
            rule1.PropertyRules.Add(new PropertyRule("ID", BinaryOp.GreaterThan, 2));

            var rule2 = new EntityRule("tAssembly");
            rule2.PropertyRules.Add(new PropertyRule("ID", BinaryOp.GreaterThan, 4));
            Rules.Add(rule);
            Rules.Add(rule1);
            Rules.Add(rule2);

            return Rules;
        }

        
        

        #endregion
    }
}