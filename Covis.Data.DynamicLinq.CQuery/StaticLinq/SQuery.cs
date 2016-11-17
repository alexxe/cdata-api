// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CQuery.cs" company="">
//   
// </copyright>
// <summary>
//   The c query.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Covis.Data.DynamicLinq.CQuery.Contracts.Contract;

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
    public class SQuery<TModelEntity> 
        where TModelEntity : class, IModelEntity
    {
        #region Constructors and Destructors

        
        public SQuery()
        {
            this.Descriptor = new QDescriptor();
        }

        #endregion

        #region Properties

        public Expression Root { get; set; }

        /// <summary>
        ///     Gets or sets the q descriptor.
        /// </summary>
        private QDescriptor Descriptor { get; set; }

        #endregion

        

        #region Public Methods and Operators

        public SQuery<TModelEntity> Where(
            Expression<Func<TModelEntity, bool>> lambda) 
        {
            if (this.Root == null)
            {
                this.Root = lambda;
            }
            else
            {

                this.Root = Expression.And(this.Root, lambda);
            }
            return this;
        }

        //public DQuery<TModelEntity, TEntityDescriptor> AsDQuery<TEntityDescriptor>()
        //    where TEntityDescriptor : TModelEntity, ISearchableDescriptor
        //{
        //    return new DQuery<TModelEntity, TEntityDescriptor>(this.QDescriptor);
        //}

        public void Compile()
        {
            var node = new ExpressionConverter().Convert(this.Root);

            var call = new QNode
            {
                Type = NodeType.Method,
                Value = MethodType.Where,
                Right = node,
                Left = this.Descriptor.Root
            };
            this.Descriptor.Root = call;
        }
        #endregion
    }
}