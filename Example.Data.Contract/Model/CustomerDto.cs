namespace Example.Data.Contract.Model
{
    using System;
    using System.Collections.Generic;

    using Covis.Data.DynamicLinq.CQuery.Contracts.DEntity;

    public class CustomerDto : IModelEntity
    {
        public CustomerDto()
        {
            this.Contacts = new HashSet<ContactDto>();
        }

        public Guid Id { get; set; }

        public string EdvNr { get; set; }

        public string CustomerNr { get; set; }

        public string Firma1 { get; set; }

        public string Firma2 { get; set; }

        public string ShortName { get; set; }

        public string Street { get; set; }

        public string Ort { get; set; }

        public IEnumerable<ContactDto> Contacts { get; set; }
    }
}