namespace Example.DB.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Example.DB.CustomerModel>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Example.DB.CustomerModel context)
        {
            var customer1 = new Customer()
            {
                Id = new Guid("54E66A85-3F87-445A-BC3E-AD866FAD0D75"),
                CustomerNr = "1234567891",
                EdvNr = "11111111",
                Firma1 = "Krupp1",
                Firma2 = "Krupp2",
                Ort = "Düsseldorf",
                ShortName = "Krupp",
                Street = "Hansaallee",
            };

            var customer2 = new Customer()
            {
                Id = new Guid("54E66A85-3F87-445A-BC3E-AD866FAD0D76"),
                CustomerNr = "1234567892",
                EdvNr = "11111112",
                Firma1 = "Voodafon1",
                Firma2 = "Voodafon2",
                Ort = "Düsseldorf",
                ShortName = "Voodafon",
                Street = "Berlineralle"
            };
            context.Customers.AddOrUpdate(c => c.Id, customer1, customer2);

            var contact1 = new Contact()
            {
                Id = new Guid("54E66A85-3F87-445A-BC3E-AD866FAD0D77"),
                EdvNr = "11111111",
                FirstName = "Alex",
                LastName = "Müller",
                Ort = "Düsseldorf",
                Street = "Hansaallee",
                Customer = customer1
            };
            var contact2 = new Contact()
            {
                Id = new Guid("54E66A85-3F87-445A-BC3E-AD866FAD0D78"),
                EdvNr = "11111112",
                FirstName = "Otto",
                LastName = "Schröder",
                Ort = "Köln",
                Street = "Birkenstrasse",
                Customer = customer1
            };



            context.Contacts.AddOrUpdate(c => c.Id, contact1, contact2);




        }
    }
}