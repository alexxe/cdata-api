// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CQuery.cs" company="">
//   
// </copyright>
// <summary>
//   The c query.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.DynamicLinq.CQuery.StaticLinq
{
    using System;
    using System.Linq.Expressions;

    using Covis.Data.DynamicLinq.CQuery.Contracts;
    using Covis.Data.DynamicLinq.CQuery.Contracts.DEntity;
    using Covis.Data.DynamicLinq.CQuery.Contracts.Model;
    using Covis.Data.DynamicLinq.CQuery.DynamicLinq;

    /// <summary>
    ///     The c query.
    /// </summary>
    /// <typeparam name="TModelEntity">
    /// </typeparam>
    public class SQuery<TModelEntity> : IDescriptorAccsessor
        where TModelEntity : class, IModelEntity
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="DQuery{TISEntity}" /> class.
        /// </summary>
        public SQuery()
        {
            this.QDescriptor = new QueryDescriptor(typeof(TModelEntity));
        }

        #endregion

        #region Properties

        public Expression<Func<TModelEntity, bool>> FilterExpression { get; set; }

        /// <summary>
        ///     Gets or sets the q descriptor.
        /// </summary>
        private QueryDescriptor QDescriptor { get; set; }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the descriptor.
        /// </summary>
        public QueryDescriptor Descriptor
        {
            get
            {
                return this.QDescriptor;
            }
        }

        #endregion

        #region Public Methods and Operators

        public SQuery<TModelEntity> Where(
            Expression<Func<TModelEntity, bool>> filterExpression) 
        {
            this.FilterExpression = filterExpression;
            return this;
        }

        public DQuery<TModelEntity, TEntityDescriptor> AsDQuery<TEntityDescriptor>()
            where TEntityDescriptor : TModelEntity, ISearchableDescriptor
        {
            return new DQuery<TModelEntity, TEntityDescriptor>(this.QDescriptor);
        }

        public void Compile()
        {
            BNode node = new ExpressionConverter().Convert(this.FilterExpression);

            var call = new CallNode("Where") { Right = node };
            call.Left = this.Descriptor.Root;
            this.Descriptor.Root = call;
        }
        #endregion
    }
}