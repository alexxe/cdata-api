namespace Example.Data.Contract.Model
{
    using System;
    using System.Collections.Generic;

    using Covis.Data.Common;

    public class CustomerDto : IModelEntity
    {
        public CustomerDto()
        {
            this.Contacts = new HashSet<ContactDto>();
        }

        public long Id { get; set; }

        public int EdvNr { get; set; }

        public string Firma1 { get; set; }

        public string Firma2 { get; set; }

        public string Street { get; set; }

        public string Ort { get; set; }

        public int ContactCount { get; set; }

        public IEnumerable<ContactDto> Contacts { get; set; }
    }
}