// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectRepository.cs" company="">
//   
// </copyright>
// <summary>
//   The project repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Runtime.CompilerServices;

namespace Example.Repo
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using AutoMapper;

    using Covis.Data.Repo;

    using Example.Data.Contract.Model;
    using Example.DB;

    /// <summary>
    ///     The project repository.
    /// </summary>
    public class DefaultRepository : BaseRepository
    {
        private static DefaultRepository instance;

        protected DefaultRepository()
        {
        }

        protected override DbContext Context => new CustomerModel();

        public static DefaultRepository GetInstance()
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
                        cfg.CreateMap<Customer, CustomerDto>()
                        .ForMember(x => x.Firma11, op => op.MapFrom(src => src.Firma1))
                        .ForMember(x => x.Firma21, opts => opts.MapFrom(src => src.Firma2));
                        cfg.CreateMap<Contact, ContactDto>()
                            .ForMember(x => x.Customer, opts => opts.MapFrom(src => src.Customer));
                    });

            return config;
        }

       

        
    }
}