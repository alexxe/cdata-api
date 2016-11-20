// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="">
//   
// </copyright>
// <summary>
//   The program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------


using Example.Data.Contract.Model;
using Example.HttpClient.Model;
using Qdata.Json.Contract;
using QData.LinqConverter;

namespace Example.HttpClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    

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
            for (int i = 0; i < 1; i++)
            {

                StaticQueryTest();
                StaticQueryTest1();
             
            }
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
                        x.Id > id.Value && x.Firma11.Contains(desc.Value)
                        && x.Contacts.Any(y => y.Id > id.Value && y.FirstName.Contains(desc.Value))
                        || x.Firma21.Contains("h") ).Select(x => new Projection() { Id = x.Id, Firma1 = x.Firma11 })
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

        private static void StaticQueryTest1()
        {
            Console.WriteLine("StaticQueryTest");
            var client = new WebApiClient();
            var list = new List<CustomerDto>().AsQueryable();
            var id = new ConstantPlaceHolder<long>() { Value = 1 };
            var desc = new ConstantPlaceHolder<string>() { Value = "s" };

            var query =
                list
                    .Select(x => new { Id1 = x.Id, Firma4 = x.Firma11 }).Where(
                        x =>
                        x.Id1 > id.Value && x.Firma4.Contains(desc.Value))
                    .Expression;
            var c = new ExpressionConverter();
            var root = c.Convert(query);


            var customers = client.GetTest<Projection1>(new QDescriptor() { Root = root });
            if (customers == null)
            {
                return;
            }
            foreach (var customer in customers)
            {
                Console.WriteLine("id={0} firma4={1}", customer.Id, customer.Firma4);


            }
        }

        #endregion
    }
}