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
    using Covis.Data.SqlProvider;

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
            object result1;
            using (var ctx = this.Context)
            {
                var provider = new ExpressionProvider(this.mapperConfiguration, ctx);
                var resultExp = provider.ConvertToResultExpression(node);

                var listType = typeof(IEnumerable<>);
                var sourceType = listType.MakeGenericType(resultExp.SourceType);
                var targetType = listType.MakeGenericType(resultExp.TargetType);
                

                var result = resultExp.Queryable.Provider.CreateQuery(resultExp.ResultExpression);
                result1 = this.mapperConfiguration.CreateMapper().Map(result, sourceType, targetType);

                if (!resultExp.HasProjection)
                {
                    var listType1 = typeof(List<>);
                    var concreteType = listType1.MakeGenericType(resultExp.SourceType);
                    var valueList = Activator.CreateInstance(concreteType);
                    var methodInfo = valueList.GetType().GetMethod("Add");
                    IEnumerator enumerator = result.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        methodInfo.Invoke(valueList, new object[] { enumerator.Current });
                    }

                    
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

        public object Find1(QDescriptor node)
        {
            object result1;
            using (var ctx = this.Context)
            {
                var provider = new ExpressionProvider(this.mapperConfiguration, ctx);
                var resultExp = provider.ConvertToResultExpression(node);

                var result = resultExp.Queryable.Provider.CreateQuery(resultExp.ResultExpression);

                if (!resultExp.HasProjection)
                {
                    var listType1 = typeof(List<>);
                    var concreteType = listType1.MakeGenericType(resultExp.SourceType);
                    var valueList = Activator.CreateInstance(concreteType);
                    var methodInfo = valueList.GetType().GetMethod("Add");
                    IEnumerator enumerator = result.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        methodInfo.Invoke(valueList, new object[] { enumerator.Current });
                    }

                    var listType = typeof(IEnumerable<>);
                    var sourceType = listType.MakeGenericType(resultExp.SourceType);
                    var targetType = listType.MakeGenericType(resultExp.TargetType);
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

        protected abstract object MapProjectionToModel(
            IQueryable queryable,
            Type targetType,
            MapperConfiguration config);

        protected abstract DbContext Context { get; }

        #endregion
    }
}