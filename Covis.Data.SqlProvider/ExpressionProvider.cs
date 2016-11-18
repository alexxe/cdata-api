// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExpressionProvider.cs" company="">
//   
// </copyright>
// <summary>
//   The repository impl.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Data.Entity;
using AutoMapper;
using QData.Json.Contracts;
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

        public ExpressionProvider(MapperConfiguration mapConfig, DbContext ctx)
        {
            this.converter = new QDescriptorConverter(mapConfig, ctx);
        }

        #endregion

        #region Public Methods and Operators

        public Result ConvertToExpression(QDescriptor descriptor)
        {
            descriptor.Root.Accept(this.converter);
            return new Result()
                       {
                           ResultExpression = this.converter.ContextExpression.Pop(),
                           Queryable = this.converter.query,
                           SourceType = this.converter.SourceType,
                           TargetType = this.converter.TargetType,
                           HasProjection = this.converter.HasProjection
                       };
        }

        #endregion
    }
}