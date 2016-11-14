﻿// --------------------------------------------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Linq;

    using AutoMapper;

    using Covis.Data.DynamicLinq.CQuery.Contracts;
    using Covis.Data.DynamicLinq.Provider;
    using Covis.Data.DynamicLinq.Provider.Mapping;

    using Example.Data.Contract.Model;

    public abstract class Repository
    {
        protected abstract MapperConfiguration CreateMapping();

        #region Constructors and Destructors

        private readonly MapperConfiguration mapperConfiguration;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Repository{TModelEntity,TEntity}" /> class.
        /// </summary>
        protected Repository()
        {
            this.mapperConfiguration = this.CreateMapping();
        }

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
        public virtual object Find(QueryDescriptor descriptor)
        {
            object result1;
            using (var ctx = this.Context)
            {
                descriptor = QueryDescriptorMapper.Map(descriptor, this.mapperConfiguration);
                var query = ctx.Set(descriptor.EntryPointType).AsQueryable();
                var provider = new ExpressionProvider(query, this.mapperConfiguration);

                var expression = provider.Convert(descriptor);
                var result = query.Provider.CreateQuery(expression);

                if (descriptor.QueryType == QueryType.Default)
                {
                    var listType = typeof(IEnumerable<>);
                    var sourceType = listType.MakeGenericType(descriptor.EntryPointType);
                    var targetType = listType.MakeGenericType(descriptor.TargetType);
                    result1 = this.mapperConfiguration.CreateMapper().Map(result, sourceType, targetType);
                }
                else if (descriptor.QueryType == QueryType.ModelProjection)
                {
                    //result1 = this.mapperConfiguration.CreateMapper().Map<IEnumerable<ProjectDto>>(result);
                    result1 = this.MapProjectionToModel(result,descriptor.TargetType,this.mapperConfiguration);
                }
                else
                {
                    List<object> result2 = new List<object>();
                    IEnumerator enumerator = result.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        result2.Add(enumerator.Current);
                    }
                    result1 = result2;
                }
            }
            return result1;
        }

        protected abstract object MapProjectionToModel(IQueryable queryable, Type targetType, MapperConfiguration config);

        protected abstract DbContext Context { get; }

        #endregion
    }
}