// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Repository.cs" company="">
//   
// </copyright>
// <summary>
//   The repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.DynamicLinq.Repo
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Dynamic;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Covis.Data.DynamicLinq.CQuery.Contracts;
    using Covis.Data.DynamicLinq.CQuery.Contracts.DEntity;
    using Covis.Data.DynamicLinq.Provider;

    /// <summary>
    ///     The repository.
    /// </summary>
    /// <typeparam name="TModelEntity">
    /// </typeparam>
    /// <typeparam name="TEntity">
    /// </typeparam>
    public abstract class Repository<TModelEntity, TEntity> : IRepository<TModelEntity>
        where TModelEntity : class, IModelEntity where TEntity : class
    {
        #region Fields

        /// <summary>
        ///     The contract version.
        /// </summary>
        private readonly Version ContractVersion;

        #endregion

        protected abstract MapperConfiguration CreateMapping();

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes static members of the <see cref="Repository" /> class.
        /// </summary>
        static Repository()
        {
        }

        private readonly MapperConfiguration mapperConfiguration;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Repository{TModelEntity,TEntity}" /> class.
        /// </summary>
        protected Repository()
        {
            this.ContractVersion = typeof(TModelEntity).Assembly.GetName().Version;

            //this.SecurityConsumer = new SecurityConsumer("Project");
            //this.SecurityConsumer.UseSecurity();

            this.mapperConfiguration = this.CreateMapping();
        }

        //private SecurityConsumer SecurityConsumer { get; set; }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the instance.
        /// </summary>
        /// <summary>
        ///     The find.
        /// </summary>
        /// <param name="param">
        ///     The param.
        /// </param>
        /// <returns>
        ///     The <see cref="IEnumerable" />.
        /// </returns>
        public IEnumerable<TModelEntity> Find(QueryDescriptor descriptor)
        {
            IEnumerable<TModelEntity> result1 = null;
            //using (var ctx = this.Context)
            //{
            //    var query = ctx.Set<TEntity>().AsQueryable();
            //    var provider = new ExpressionProvider<TEntity>(query, this.mapperConfiguration);
            //    var expression = provider.Convert(descriptor);
            //    var result = query.Provider.CreateQuery<TEntity>(expression);
            //    result1 =
            //        this.mapperConfiguration.CreateMapper().Map<IEnumerable<TEntity>, IEnumerable<TModelEntity>>(result);
            //}
            return result1;
        }

        public List<object> Project(QueryDescriptor descriptor)
        {
            List<object> result = new List<object>();
            //using (var ctx = this.Context)
            //{
            //    var query = ctx.Set<TEntity>().AsQueryable();
            //    var provider = new ExpressionProvider<TEntity>(query, this.mapperConfiguration);
            //    var expression = provider.Convert(descriptor);
            //    var qResult = query.Provider.CreateQuery(expression);
                
            //    IEnumerator enumerator = qResult.GetEnumerator();
            //    while (enumerator.MoveNext())
            //    {
            //        result.Add(enumerator.Current);
            //    }
            //}

            return result;
        }

        public IEnumerable<TModelEntity> ProjectToModel(QueryDescriptor descriptor)
        {
            //using (var ctx = this.Context)
            //{
            //    var query = ctx.Set<TEntity>().AsQueryable();
            //    var provider = new ExpressionProvider<TEntity>(query, this.mapperConfiguration);
            //    var expression = provider.Convert(descriptor);
            //    var qResult = query.Provider.CreateQuery(expression);
                
            //    return this.mapperConfiguration.CreateMapper().Map<IEnumerable<TModelEntity>>(qResult);
            //}

            return null;
        }
        protected abstract DbContext Context { get; }

        #endregion

        public virtual void Test()
        {
            
        }
    }
}