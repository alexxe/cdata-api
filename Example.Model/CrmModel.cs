// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectRepository.cs" company="">
//   
// </copyright>
// <summary>
//   The project repository.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Runtime.CompilerServices;
using Example.Data.Contract.Model;
using Qdata.Json.Contract;
using QData.Common;
using QData.Repo;


namespace Example.Repo
{
    using System.Data.Entity;
    using System.Linq;

    using AutoMapper;
    using Example.DB;

    /// <summary>
    ///     The project repository.
    /// </summary>
    public class CrmModel

    {
        private static CrmModel instance;

        private MapperConfiguration Mapping { get; set; }

        private CrmModel()
        {
        }

        protected DbContext Context => new DB.CrmDataModel();

        public object Find<TM>(QDescriptor param)
            where TM : IModelEntity
        {
            using (var ctx = new CrmDataModel())
            {
                var typeMap =
                this.Mapping.GetAllTypeMaps()
                    .FirstOrDefault(x => x.DestinationType == typeof(TM));
                var query = ctx.Set(typeMap.SourceType).AsQueryable();
                var repo = new Model(this.Mapping);
                var result = repo.Find(param,query);
                return result;
            }
        }

        public static CrmModel GetInstance()
        {
            if (instance == null)
            {
                instance = new CrmModel();
                instance.Mapping = new MapperConfiguration(
                    cfg =>
                    {
                        cfg.CreateMissingTypeMaps = true;
                        cfg.CreateMap<Customer, CustomerDto>()
                            .ForMember(x => x.Firma11, op => op.MapFrom(src => src.Firma1))
                            .ForMember(x => x.Firma21, opts => opts.MapFrom(src => src.Firma2));
                        cfg.CreateMap<Contact, ContactDto>()
                            .ForMember(x => x.Customer, opts => opts.MapFrom(src => src.Customer));
                    });

            }

            return instance;
        }

        

    }



}