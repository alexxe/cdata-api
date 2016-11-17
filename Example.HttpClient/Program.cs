// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="">
//   
// </copyright>
// <summary>
//   The program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Example.HttpClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Covis.Data.Json.Contracts;
    using Covis.Data.LinqConverter;

    using Example.Data.Contract.Model;
    using Example.HttpClient.Model;

    /// <summary>
    ///     The program.
    /// </summary>
    internal class Program
    {
        #region Static Fields

        /// <summary>
        ///     The _access token.
        /// </summary>
        private static string _accessToken;

        #endregion

        #region Methods

        /// <summary>
        ///     The main.
        /// </summary>
        /// <param name="args">
        ///     The args.
        /// </param>
        private static void Main(string[] args)
        {
            StaticQueryTest();
            Console.ReadLine();
        }

        private static void StaticQueryTest()
        {
            Console.WriteLine("StaticQueryTest");
            var client = new WebApiClient();
            var list = new List<CustomerDto>().AsQueryable();
            var id = new ConstantPlaceHolder<long>() { Value = 1 };
            var desc = new ConstantPlaceHolder<string>() { Value = "s" };

            var query =
                list
                    .Where(
                        x =>
                        x.Id > id.Value && x.Firma1.Contains(desc.Value)
                        && x.Contacts.Any(y => y.Id > id.Value && y.FirstName.Contains(desc.Value))
                        || x.Firma2.Contains("h")).Select(x => new CustomerDto() { Id = x.Id, Firma1 = x.Firma1 })
                    .Expression;
            var c = new ExpressionConverter();
            var root = c.Convert(query);

            
            var customers = client.GetTest<Projection>(new QDescriptor() { Root = root } );
            if (customers == null)
            {
                return;
            }
            foreach (var customer in customers)
            {
                Console.WriteLine("id={0} firma1={1}", customer.Id, customer.Firma1);
                

            }
        }

        #endregion
    }
}