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

    public abstract class BaseRepository
    {
        protected abstract MapperConfiguration CreateMapping();

        #region Constructors and Destructors

        private readonly MapperConfiguration mapperConfiguration;

        
        protected BaseRepository()
        {
            this.mapperConfiguration = this.CreateMapping();
        }

        #endregion

        #region Public Properties

        public object Find(QDescriptor node)
        {
            using (var ctx = this.Context)
            {
                var provider = new ExpressionProvider(this.mapperConfiguration, ctx);
                var epressionResult = provider.ConvertToExpression(node);
                
                var data = epressionResult.Queryable.Provider.CreateQuery(epressionResult.ResultExpression);
                
                if (epressionResult.HasProjection)
                {
                    List<object> result = new List<object>();
                    IEnumerator enumerator = data.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        result.Add(enumerator.Current);
                    }
                    return result;
                }
                var listType = typeof(IEnumerable<>);
                var sourceType = listType.MakeGenericType(epressionResult.SourceType);
                var targetType = listType.MakeGenericType(epressionResult.TargetType);
                return this.mapperConfiguration.CreateMapper().Map(data, sourceType, targetType);
            }
        }
        
        protected abstract DbContext Context { get; }

        #endregion
    }
}