// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectRepository.cs" company="">
//   
// </copyright>
// <summary>
//   The project repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Example.Repo
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using AutoMapper;

    using Covis.Data.DynamicLinq.Repo;

    using Example.Data.Contract.Model;
    using Example.DB;

    /// <summary>
    ///     The project repository.
    /// </summary>
    public class DefaultRepository : Repository
    {
        private static Repository instance;

        protected DefaultRepository()
        {
        }

        protected override DbContext Context => new CustomerModel();

        public static Repository GetInstance()
        {
            if (instance == null)
            {
                instance = new DefaultRepository();
            }

            return instance;
        }

        protected override MapperConfiguration CreateMapping()
        {
            var config = new MapperConfiguration(
                cfg =>
                    {
                        cfg.CreateMissingTypeMaps = true;
                        cfg.CreateMap<Customer, CustomerDto>();
                        cfg.CreateMap<Contact, ContactDto>()
                            .ForMember(x => x.Customer, opts => opts.MapFrom(src => src.Customer));
                    });

            return config;
        }

        protected override object MapProjectionToModel(
            IQueryable queryable,
            Type targetType,
            MapperConfiguration config)
        {
            if (targetType == typeof(CustomerDto))
            {
                return config.CreateMapper().Map<IEnumerable<CustomerDto>>(queryable);
            }
            if (targetType == typeof(ContactDto))
            {
                return config.CreateMapper().Map<IEnumerable<ContactDto>>(queryable);
            }

            return null;
        }

        public object Test()
        {
            using (var ctx = new CustomerModel())
            {
                return ctx.Customers.ToList();

            }
        }
    }
}