// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Repository.cs" company="">
//   
// </copyright>
// <summary>
//   The repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Covis.Data.Repo
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using AutoMapper;

    using Covis.Data.Json.Contracts;
    using Covis.Data.Repo.Mapping;
    using Covis.Data.SqlProvider;
    using Covis.Data.SqlProvider.Contracts;
    using Covis.Data.SqlProvider.Contracts.Model;

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

        public object Find(QDescriptor node)
        {
            var converter = new QNodeConverter(this.mapperConfiguration);
            converter.Visit(node.Root);
            converter.Descriptor.Root = (LNode)converter.Context.Pop(); 
            return this.Find(converter.Descriptor);
        }

        private object Find(QueryDescriptor descriptor)
        {
            object result1;
            using (var ctx = this.Context)
            {
                descriptor = QueryDescriptorMapper.Map(descriptor, this.mapperConfiguration);
                var query = ctx.Set(descriptor.EntryPointType).AsQueryable();
                var provider = new ExpressionProvider(query);

                var expression = provider.ConvertToExpression(descriptor);
                var result = query.Provider.CreateQuery(expression);
                

                if (!descriptor.HasProjection)
                {

                    var listType1 = typeof(List<>);
                    var concreteType = listType1.MakeGenericType(descriptor.EntryPointType);
                    var valueList = Activator.CreateInstance(concreteType);
                    var methodInfo = valueList.GetType().GetMethod("Add");
                    IEnumerator enumerator = result.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        methodInfo.Invoke(valueList, new object[] { enumerator.Current });
                    }

                    
                    var listType = typeof(IEnumerable<>);
                    var sourceType = listType.MakeGenericType(descriptor.EntryPointType);
                    var targetType = listType.MakeGenericType(descriptor.TargetType);
                    result1 = this.mapperConfiguration.CreateMapper().Map(valueList, sourceType, targetType);
                }
                //else if (descriptor.QueryType == QueryType.ModelProjection)
                //{
                //    //result1 = this.mapperConfiguration.CreateMapper().Map<IEnumerable<ProjectDto>>(result);
                //    result1 = this.MapProjectionToModel(result,descriptor.TargetType,this.mapperConfiguration);
                //}
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