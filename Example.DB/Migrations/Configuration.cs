namespace Example.DB.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Example.DB.CrmDataModel>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Example.DB.CrmDataModel context)
        {
            var customer1 = new Customer()
            {
                EdvNr = 1,
                Firma1 = "Starbyte Software",
                Firma2 = "Starbyte Software 2",
                Ort = "D�sseldorf",
                Street = "Hansaallee",
            };

            var customer2 = new Customer()
            {
                EdvNr = 2,
                Firma1 = "Bundespolizei M�hldorf",
                Firma2 = "Bundespolizei M�hldorf 2",
                Ort = "D�sseldorf",
                Street = "Friedrich-Ebert-Damm"
            };

            var customer3 = new Customer()
            {
                EdvNr = 3,
                Firma1 = "LA",
                Firma2 = "Voodafon2",
                Ort = "D�sseldorf",
                Street = "Berlineralle"
            };
            var customer4 = new Customer()
            {
                EdvNr = 4,
                Firma1 = "Akzente GmbH",
                Firma2 = "Akzente GmbH 2",
                Ort = "K�ln",
                Street = "Kyotoweg"
            };
            var customer5 = new Customer()
            {
                EdvNr = 5,
                Firma1 = "Cravatterie Hamburg GmbH",
                Firma2 = "Cravatterie Hamburg GmbH 2",
                Ort = "D�sseldorf",
                Street = "Neuenteich"
            };
            var customer6 = new Customer()
            {
                EdvNr = 6,
                Firma1 = "GroSchu GmbH",
                Firma2 = "GroSchu GmbH 2",
                Ort = "Essen",
                Street = "Monckeshofer Str."
            };
            var customer7 = new Customer()
            {
                EdvNr = 7,
                Firma1 = "BBS Akademie S�d GmbH",
                Firma2 = "BBS Akademie S�d GmbH 2",
                Ort = "D�sseldorf",
                Street = "Ludwig-Jahn-Str."
            };
            var customer8 = new Customer()
            {
                EdvNr = 8,
                Firma1 = "Eurohypo AG",
                Firma2 = "Eurohypo AG 2",
                Ort = "Essen",
                Street = "R�merstr."
            };
            context.Customers.AddOrUpdate(c => c.EdvNr, customer1, customer2, customer3, customer4, customer5, customer6, customer7, customer8);

            var contact1 = new Contact()
            {
                EdvNr = 1,
                FirstName = "Jochen",
                LastName = "Zebrowski",
                Ort = "D�sseldorf",
                Street = "R�merstr",
                Customer = customer1
            };
            var contact2 = new Contact()
            {
                EdvNr = 2,
                FirstName = "Petra",
                LastName = "Behrendt",
                Ort = "K�ln",
                Street = "Birkenstrasse",
                Customer = customer1
            };
            var contact3 = new Contact()
            {
                EdvNr =3,
                FirstName = "Otto",
                LastName = "Schr�der",
                Ort = "K�ln",
                Street = "Ludwig-Jahn-Str.",
                Customer = customer1
            };
            var contact4 = new Contact()
            {
                EdvNr = 4,
                FirstName = "Ursula",
                LastName = "K�hn",
                Ort = "K�ln",
                Street = "Birkenstrasse",
                Customer = customer3
            };
            var contact5 = new Contact()
            {
                EdvNr = 5,
                FirstName = "Angela",
                LastName = "Bachhofer",
                Ort = "K�ln",
                Street = "Jahn-Str.",
                Customer = customer3
            };
            var contact6 = new Contact()
            {
                EdvNr = 6,
                FirstName = "Birgit",
                LastName = "T�gel",
                Ort = "Essen",
                Street = "Birkenstrasse",
                Customer = customer4
            };
            var contact7 = new Contact()
            {
                EdvNr = 7,
                FirstName = "Katharina",
                LastName = "Reichelt",
                Ort = "G�ttingen",
                Street = "Weender Landstr.",
                Customer = customer5
            };
            var contact8 = new Contact()
            {
                EdvNr = 8,
                FirstName = "Olaf",
                LastName = "Firtulescu",
                Ort = "Remscheid",
                Street = "Neuenteich",
                Customer = customer6
            };
            var contact9 = new Contact()
            {
                EdvNr = 9,
                FirstName = "Otto",
                LastName = "Schr�der",
                Ort = "K�ln",
                Street = "Birkenstrasse",
                Customer = customer6
            };



            context.Contacts.AddOrUpdate(c => c.EdvNr, contact1, contact2, contact3, contact4, contact5, contact6, contact7, contact8, contact9);




        }
    }
}