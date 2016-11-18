// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExpressionProvider.cs" company="">
//   
// </copyright>
// <summary>
//   The repository impl.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.SqlProvider
{
    using System.Data.Entity;

    using AutoMapper;

    using Covis.Data.Json.Contracts;
    using Covis.Data.SqlProvider.builder;

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

        public Result ConvertToResultExpression(QDescriptor descriptor)
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