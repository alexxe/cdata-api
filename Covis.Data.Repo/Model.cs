// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Repository.cs" company="">
//   
// </copyright>
// <summary>
//   The repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using QData.Json.Contracts;
using QData.SqlProvider;

namespace QData.Repo
{
    public class Model
    {
        #region Constructors and Destructors

        private readonly MapperConfiguration mapperConfiguration;

        
        public Model(MapperConfiguration mapping)
        {
            this.mapperConfiguration = mapping;
        }

        #endregion

        #region Public Properties

        public object Find(QDescriptor node, IQueryable query)
        {

            var provider = new ExpressionProvider(this.mapperConfiguration,query.Expression);
            var epressionResult = provider.ConvertToExpression(node);

            var data = query.Provider.CreateQuery(epressionResult.Expression);

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
            var target = this.mapperConfiguration.GetAllTypeMaps()
                .FirstOrDefault(x => x.SourceType == query.ElementType)
                .DestinationType;
            var listType = typeof (IEnumerable<>);
            var sourceType = listType.MakeGenericType(query.ElementType);
            var targetType = listType.MakeGenericType(target);
            return this.mapperConfiguration.CreateMapper().Map(data, sourceType, targetType);

        }


        #endregion
    }
}