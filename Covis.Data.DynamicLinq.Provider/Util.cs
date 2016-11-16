// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Util.cs" company="">
//   
// </copyright>
// <summary>
//   The util.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Covis.Data.DynamicLinq.Provider
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    using Covis.Data.DynamicLinq.CQuery.Contracts;

    /// <summary>
    ///     The util.
    /// </summary>
    internal static class Util
    {
        #region Static Fields

        

        public static readonly MethodInfo TakeMethod = typeof(System.Linq.Queryable).GetMethod(
            "Take",
            new[] { typeof(int) });

        #endregion

        #region Public Methods and Operators



        /// <summary>
        /// The get member type.
        /// </summary>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <returns>
        /// The <see cref="Type"/>.
        /// </returns>
        public static Type GetMemberType(Expression expression)
        {
            if (expression.Type.IsGenericType)
            {
                return expression.Type.GenericTypeArguments[0];
            }

            return expression.Type.UnderlyingSystemType;
        }


        public static ParameterExpression GetParameterExpression(MemberExpression expression)
        {
            return null;
        }

        private static Expression GetParameterExpression(Expression expression)
        {
            return expression;
        }
        #endregion


    }
}