namespace Example.Data.Contract.Model
{
    using System;

    using Covis.Data.DynamicLinq.CQuery.Contracts.DEntity;

    public class ContactDto : IModelEntity
    {
        public ContactDto()
        {
        }

        public Guid Id { get; set; }

        public string EdvNr { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Street { get; set; }

        public string Ort { get; set; }

        public CustomerDto Customer { get; set; }
    }
}