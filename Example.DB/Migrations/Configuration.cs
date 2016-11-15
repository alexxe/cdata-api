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

            var customer3 = new Customer()
            {
                Id = new Guid("54E16A86-3F87-445A-BC3E-AD866FAD0D76"),
                CustomerNr = "1234567892",
                EdvNr = "11111112",
                Firma1 = "  LA",
                Firma2 = "Voodafon2",
                Ort = "Düsseldorf",
                ShortName = "Voodafon",
                Street = "Berlineralle"
            };
            var customer4 = new Customer()
            {
                Id = new Guid("54E66A87-3F87-445A-BC3E-AD866FAD0D76"),
                CustomerNr = "1234567892",
                EdvNr = "11111112",
                Firma1 = "Voodafon1",
                Firma2 = "Voodafon2",
                Ort = "Düsseldorf",
                ShortName = "Voodafon",
                Street = "Berlineralle"
            };
            var customer5 = new Customer()
            {
                Id = new Guid("54E66B88-3F87-445A-BC3E-AD866FAD0D76"),
                CustomerNr = "1234567892",
                EdvNr = "11111112",
                Firma1 = "Voodafon1",
                Firma2 = "Voodafon2",
                Ort = "Düsseldorf",
                ShortName = "Voodafon",
                Street = "Berlineralle"
            };
            var customer6 = new Customer()
            {
                Id = new Guid("54E66A89-3F97-445A-BC3E-AD866FAD0D76"),
                CustomerNr = "1234567892",
                EdvNr = "11111112",
                Firma1 = "Voodafon1",
                Firma2 = "Voodafon2",
                Ort = "Düsseldorf",
                ShortName = "Voodafon",
                Street = "Berlineralle"
            };
            var customer7 = new Customer()
            {
                Id = new Guid("55E66A85-3F87-445A-BC3E-AD868FAD0D76"),
                CustomerNr = "1234567892",
                EdvNr = "11111112",
                Firma1 = "Voodafon1",
                Firma2 = "Voodafon2",
                Ort = "Düsseldorf",
                ShortName = "Voodafon",
                Street = "Berlineralle"
            };
            var customer8 = new Customer()
            {
                Id = new Guid("56E66A85-3F87-445A-BC3E-AD856FAD0D76"),
                CustomerNr = "1234567892",
                EdvNr = "11111112",
                Firma1 = "Voodafon1",
                Firma2 = "Voodafon2",
                Ort = "Düsseldorf",
                ShortName = "Voodafon",
                Street = "Berlineralle"
            };
            context.Customers.AddOrUpdate(c => c.Id, customer1, customer2, customer3, customer4, customer5, customer6, customer7, customer8);

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
            var contact3 = new Contact()
            {
                Id = new Guid("54E66A85-4F87-445A-BC3E-AD866FAD0D78"),
                EdvNr = "11111112",
                FirstName = "Otto",
                LastName = "Schröder",
                Ort = "Köln",
                Street = "Birkenstrasse",
                Customer = customer1
            };
            var contact4 = new Contact()
            {
                Id = new Guid("54E66A85-5F87-445A-BC3E-AD866FAD0D78"),
                EdvNr = "11111112",
                FirstName = "Otto",
                LastName = "Schröder",
                Ort = "Köln",
                Street = "Birkenstrasse",
                Customer = customer3
            };
            var contact5 = new Contact()
            {
                Id = new Guid("54E66A85-6F87-445A-BC3E-AD866FAD0D78"),
                EdvNr = "11111112",
                FirstName = "Otto",
                LastName = "Schröder",
                Ort = "Köln",
                Street = "Birkenstrasse",
                Customer = customer3
            };
            var contact6 = new Contact()
            {
                Id = new Guid("54E66A85-7F87-445A-BC3E-AD866FAD0D78"),
                EdvNr = "11111112",
                FirstName = "Otto",
                LastName = "Schröder",
                Ort = "Köln",
                Street = "Birkenstrasse",
                Customer = customer4
            };
            var contact7 = new Contact()
            {
                Id = new Guid("54E66A85-8F87-445A-BC3E-AD866FAD0D78"),
                EdvNr = "11111112",
                FirstName = "Otto",
                LastName = "Schröder",
                Ort = "Köln",
                Street = "Birkenstrasse",
                Customer = customer5
            };
            var contact8 = new Contact()
            {
                Id = new Guid("54E66A85-9F87-445A-BC3E-AD866FAD0D78"),
                EdvNr = "11111112",
                FirstName = "Otto",
                LastName = "Schröder",
                Ort = "Köln",
                Street = "Birkenstrasse",
                Customer = customer6
            };
            var contact9 = new Contact()
            {
                Id = new Guid("54E66A85-9F87-445A-BB3E-AD866FAD0D78"),
                EdvNr = "11111112",
                FirstName = "Otto",
                LastName = "Schröder",
                Ort = "Köln",
                Street = "Birkenstrasse",
                Customer = customer6
            };



            context.Contacts.AddOrUpdate(c => c.Id, contact1, contact2, contact3, contact4, contact5, contact6, contact7, contact8, contact9);




        }
    }
}