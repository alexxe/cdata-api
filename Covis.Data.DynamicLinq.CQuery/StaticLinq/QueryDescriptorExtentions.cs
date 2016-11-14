using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covis.Data.SerializeLinq.Client.Extentions
{
    using System.Linq.Expressions;
    using System.Security.Cryptography;

    using Covis.Data.SerializeLinq.Client.Contracts;
    using Covis.Data.SerializeLinq.DynamicQuery;
    using Covis.Data.SerializeLinq.DynamicQuery.Contracts;

    public static class QueryDescriptorExtentions
    {
        public static QueryParameters AsQueryParameters<TISEntity>(this QueryDescriptor<TISEntity> descriptor) where TISEntity : class, ISEntity
        {
            var param = new QueryParameters();
            param.ContractVersion = typeof(TISEntity).Assembly.GetName().Version;


            foreach (var filter in descriptor.FilterParameters)
            {
                var parser = new BaseNodeParser();
                parser.Visit(filter.Body);
                param.FilterParameters.Add(parser.RootNode);
            
            }

            foreach (var sort in descriptor.SortParameters)
            {
                var node = new SortNode() { Op = sort.Value, Member = Util.BuildMemberNode(sort.Key) };
                param.SortParameters.Add(node);
            }

            foreach (var include in descriptor.IncludeParameters)
            {
                var propertyName = Util.BuildMemberNode(include);
                param.IncludeParameters.Add(propertyName);
            }

            return param;
        }
    }
}
