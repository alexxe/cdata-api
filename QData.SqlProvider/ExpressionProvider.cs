// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExpressionProvider.cs" company="">
//   
// </copyright>
// <summary>
//   The repository impl.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq.Expressions;
using AutoMapper;
using Qdata.Json.Contract;
using QData.SqlProvider.builder;

namespace QData.SqlProvider
{
    /// <summary>
    ///     The repository impl.
    /// </summary>
    /// <typeparam name="TEntity">
    /// </typeparam>
    public class ExpressionProvider
    {
        #region Fields

        private readonly QDescriptorConverter converter;

        #endregion

        #region Constructors and Destructors

        public ExpressionProvider(MapperConfiguration mapConfig,Expression query)
        {
            this.converter = new QDescriptorConverter(mapConfig, query);
        }

        #endregion

        #region Public Methods and Operators

        public Result ConvertToExpression(QDescriptor descriptor)
        {
            descriptor.Root.Accept(this.converter);
            return new Result()
                       {
                           Expression = this.converter.ContextExpression.Pop(),
                           HasProjection = this.converter.HasProjection
                       };
        }

        #endregion
    }
}