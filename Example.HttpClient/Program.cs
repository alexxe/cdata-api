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
    //using System.Linq;

    using Covis.Data.DynamicLinq.CQuery.Contracts;
    using Covis.Data.DynamicLinq.CQuery.Contracts.Model;
    //using Covis.Data.DynamicLinq.CQuery.Contracts.Model;
    using Covis.Data.DynamicLinq.CQuery.DynamicLinq;
    using Covis.Data.DynamicLinq.CQuery.DynamicLinq.Extentions;
    using Covis.Data.DynamicLinq.CQuery.StaticLinq;

    using Example.Data.Contract.Model;
    using Example.HttpClient.Model;

    using GraphQL.Types;

    using Enumerable = System.Linq.Enumerable;

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
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        private static void Main(string[] args)
        {
            //Test0();
            //Test();
            //Test1();
            //TestCount1();
            //WhereTest();
            FlatTest();
            //TestCount2();
            //Test3();
            //SelectorTest1();
            //SelectorTest2();
            //Join2TableTest();
            //EntryPointTest();
            //AnonymeProjectionTest1();
            //ModelProjectionTest1();
            //SelectorTest4();
            //StaticQueryTest();
            Console.ReadLine();
        }

        private static void FlatTest()
        {
            Console.WriteLine("FlatTest");
            var client = new WebApiClient();

            var param = new DQuery<CustomerDto, CustomerDtoDescriptor>();
            param.Where(x => x.Firma1, StringMethods.Contains,"K");
            param.Where(x => x.Firma2,StringMethods.Contains,"K");
            

            var customers = client.GetTest<CustomerDto>(param.Descriptor);
            foreach (var customer in customers)
            {
                Console.WriteLine("EdvNr={0} Firma1={1}", customer.EdvNr, customer.Firma1);
            }
        }

        private static void WhereTest()
        {
            //Console.WriteLine("WhereTest");
            //var client = new WebApiClient();

            //var param1 = new DQuery<ProjectDto, ProjectDtoDescriptor>();
            //param1.Where(x => x.SolutionId, CompareOperator.GreaterThan, 0);

            //var assembly = new DQuery<AssemblyDto, AssemblyDtoDescriptor>();
            //assembly.Where(x => x.Description, StringMethods.Contains, "c");

            //param1.Where(x => x.Assemblies,assembly.AsWhereResult());
            
            //var projects = client.GetTest<ProjectDto>(param1.Descriptor);
            //foreach (var project in projects)
            //{
            //    Console.WriteLine("ID={0} Name={1}", project.ID, project.Name);
            //}
        }
        private static void Test0()
        {
            //Console.WriteLine("Test0");
            //var client = new WebApiClient();

            //var assemblyFilter = new DQuery<AssemblyDto, AssemblyDtoDescriptor>();
            //var aFilter = assemblyFilter.Where(a => a.ID, CompareOperator.Equal,0).AsWhereResult();

            //var param = new DQuery<ProjectDto, ProjectDtoDescriptor>();
            //param.Where(x => x.Solution.ID, CompareOperator.Equal, 167);
            
            //var projects = client.GetTest<ProjectDto>(param.Descriptor);
            //foreach (var project in projects)
            //{
            //    Console.WriteLine("ID={0}" , project.ID);
            //}
        }

        private static void Test()
        {
            //Console.WriteLine("Test");
            //var client = new WebApiClient();

            //var param = new DQuery<ProjectDto, ProjectDtoDescriptor>();
            ////param.Where(x => x.Assemblies.Count(a => a.ID > 0) > 0);
            //param.Where(x => x.CreatedDate, CompareOperator.LessThan, DateTime.Now);
            //param.Where(x => x.ID.In(new List<long>() {1,2}));
            ////var args = new List<long>() { 1, 2, 3 };
            ////param.Where(x => x.ID ,args);
            ////param.Include(x => x.Solution);
            ////param.Include(x => x.Assemblies);
            //param.OrderBy(x => x.Description,7,6);

            //var projects = client.GetTest<ProjectDto>(param.Descriptor);
            //foreach (var project in projects)
            //{
            //    Console.WriteLine("ID={0} Name={1}", project.ID, project.Name);
            //}
        }


        private static void ModelProjectionTest1()
        {
            //Console.WriteLine("ModelProjectionTest1");
            //var client = new WebApiClient();

            //var l = new List<DateTime>();
            //l.Add(DateTime.Now);
            //l.Add(DateTime.Now.AddYears(-2));



            //var param1 = new DQuery<ProjectDto, ProjectDtoDescriptor>();
            //param1.WhereOr(x => x.SolutionId, CompareOperator.GreaterThan, 0);

            //var assembly = new DQuery<AssemblyDto, AssemblyDtoDescriptor>();
            //assembly.WhereOr(x => x.Description, StringMethods.Contains, "c");

            //var param = new DQuery<ProjectDto, ProjectDtoDescriptor>();
            //param.Include(x => x.Assemblies);
            //param.Where(x => x.CreatedDate, CompareOperator.LessThan, DateTime.Now);
            //param.WhereOr(x => x.Description, StringMethods.Contains, "c");
            //param.Where(x => x.CreatedDate.In(l));
            //param.Or(param1.AsWhereResult());
            //param.OrderBy(x => x.Name);
            ////var selector = param.Select(x => new ProjectDto() { ID = x.ID, Solution = x.Solution });
            //var selector = param.Select(x => new ProjectDto() { ID = x.ID, Solution = x.Solution, Assemblies = x.Assemblies.Where(assembly.AsWhereResult()) });


            //var projects = client.GetTest<ProjectDto>(selector.Descriptor);
            //if (projects == null)
            //{
            //    return;
            //}
            //foreach (var project in projects)
            //{
            //    Console.WriteLine("Project ID={0} ", project.ID);
            //    foreach (var assemble in project.Assemblies)
            //    {
            //        Console.WriteLine("Assemble IDe={0}", assemble.ID);
            //    }
            //}
        }
        /// <summary>
        ///     The selector test 1.
        /// </summary>
        private static void SelectorTest1()
        {
            //Console.WriteLine("SelectorTest1");
            //var client = new WebApiClient();

            //var l = new List<DateTime>();
            //l.Add(DateTime.Now);
            //l.Add(DateTime.Now.AddYears(-2));

            

            //var param1 = new DQuery<ProjectDto, ProjectDtoDescriptor>();
            //param1.WhereOr(x => x.SolutionId, CompareOperator.GreaterThan, 0);

            //var assembly = new DQuery<AssemblyDto, AssemblyDtoDescriptor>();
            //assembly.WhereOr(x => x.Description, StringMethods.Contains, "c");

            //var param = new DQuery<ProjectDto, ProjectDtoDescriptor>();
            //param.Include(x => x.Assemblies);
            //param.Where(x => x.CreatedDate, CompareOperator.LessThan, DateTime.Now);
            //param.WhereOr(x => x.Description, StringMethods.Contains, "c");
            //param.Where(x => x.CreatedDate.In(l));
            //param.Or(param1.AsWhereResult());
            //param.OrderBy(x => x.Name);
            //var selector = param.Select(x => new ProjectDto() { ID = x.ID, Assemblies = x.Assemblies.Where(assembly.AsWhereResult()).Select(y => new AssemblyDto() { ID = y.ID, Description = y.Description}) });


            //var projects = client.GetModel<ProjectDto>(selector.Descriptor);
            //foreach (var project in projects)
            //{
            //    Console.WriteLine("Project ID={0} ", project.ID);
            //    foreach (var assemble in project.Assemblies)
            //    {
            //        Console.WriteLine("Assemble IDe={0}", assemble.ID);
            //    }
            //}
        }

        /// <summary>
        /// The selector test 2.
        /// </summary>
        private static void SelectorTest2()
        {
            //Console.WriteLine("SelectorTest2");
            //var client = new WebApiClient();

            //var query =
            //    new SQuery<ProjectDto>();
            //query.Where(x => x.Name.Contains("s"));
            //query.Where(x => x.Solution.Name2.Contains("a"));
            //var dquery = query.AsDQuery<ProjectDtoDescriptor>();
            //dquery.OrderBy(x => x.ProjectFileName);
            //var selector = dquery.Select(x => new ProjectProjector() { ProjectFileName = x.ProjectFileName , Assemblies = x.Assemblies.Where(x => true) });

            //var projects = client.GetProject<ProjectProjector>(selector.Descriptor);
            //if (projects == null)
            //{
            //    return;
            //}
            //foreach (var project in projects)
            //{
            //    Console.WriteLine("ProjectName={0} SolutionName={1}", project.ProjectFileName,project.ProjectFileName);
            //}
        }


        private static void AnonymeProjectionTest1()
        {
            //Console.WriteLine("AnonymeProjectionTest1");
            //var client = new WebApiClient();

            //var query =
            //    new SQuery<ProjectDto>();
            //query.Where(x => x.Name.Contains("s"));
            //query.Where(x => x.Solution.Name2.Contains("a"));
            //var dquery = query.AsDQuery<ProjectDtoDescriptor>();
            //dquery.OrderBy(x => x.ProjectFileName);
            //var selector = dquery.Select(x => new ProjectProjector() { ProjectFileName = x.ProjectFileName, SolutionName = x.Solution.Name2 ,Solution = x.Solution } );

            //var projects = client.GetTest<ProjectProjector>(selector.Descriptor);
            //if (projects == null)
            //{
            //    return;
            //}
            //foreach (var project in projects)
            //{
            //    Console.WriteLine("ProjectName={0} SolutionName={1}", project.ProjectFileName,project.SolutionName);
                
                
            //}
        }

        private static void SelectorTest4()
        {
            //Console.WriteLine("SelectorTest4");
            //var client = new WebApiClient();

            //var query =
            //    new SQuery<ProjectDto>();
            //var dquery = query.AsDQuery<ProjectDtoDescriptor>();
            //var selector = dquery.Select(x => new ProjectProjector() { ProjectFileName = x.ProjectFileName, SolutionName = x .Solution.Name2, Assemblies = x.Assemblies.Select(a => new AssemblyProjector() { AName = a.AssemblyName }) });

            //var projects = client.GetProject<ProjectProjector>(selector.Descriptor);
            //if (projects == null)
            //{
            //    return;
            //}
            //foreach (var project in projects)
            //{
            //    foreach (var assembly in project.Assemblies)
            //    {
            //        Console.WriteLine("ProjectFileName={0} SolutionName{1} AssemblyName={2}", project.ProjectFileName, project.SolutionName, assembly.AName);
            //    }

            //}
        }

        private static void StaticQueryTest()
        {
            //Console.WriteLine("StaticQueryTest");
            //var client = new WebApiClient();

            //var id = new ConstantPlaceHolder<long>() { Value = 1 };
            //var desc = new ConstantPlaceHolder<string>() { Value = "s" };

            //var query = new SQuery<ProjectDto>().Where(x => x.Solution.ID > id.Value && x.Description.Contains(desc.Value) && x.Assemblies.Any( y => y.ID > id.Value && y.Description == desc.Value) || x.Solution.Name2 == "h");
            //query.Compile();

            //var dq = query.AsDQuery<ProjectDtoDescriptor>().OrderBy(x => x.Description,1,1).Select(x => new ProjectDto() { Name = x.Solution.Name2 });
            //var projects = client.GetTest<ProjectDto>(dq.Descriptor);
            //if (projects == null)
            //{
            //    return;
            //}
            //foreach (var project in projects)
            //{
            //    foreach (var assembly in project.Assemblies)
            //    {
            //        Console.WriteLine("ProjectFileName={0} SolutionName{1} AssemblyName={2}", project.ProjectFileName, project.Solution.Name2,assembly.AssemblyName);
            //    }

            //}
        }

        private static void EntryPointTest()
        {
            //Console.WriteLine("EntryPointTest");
            //var client = new WebApiClient();

            //var query1 = new DQuery<AssemblyDto, AssemblyDtoDescriptor>();
            //query1.Where(x => x.ID, CompareOperator.GreaterThan, 0);
            //query1.Where(x => x.tProject.ID, CompareOperator.GreaterThan, 1);


            
            //var result = client.GetTest<AssemblyDto>(query1.Descriptor);
            //if (result == null)
            //{
            //    return;
            //}
            //foreach (var assembly in result)
            //{
            //    Console.WriteLine(
            //        "AssemblyName={0} Description{1} ID={2}",
            //        assembly.AssemblyName,
            //        assembly.Description,
            //        assembly.ID);
            //}
        }

        private static void Join2TableTest()
        {
            //Console.WriteLine("Join2TableTest");
            //var client = new WebApiClient();

            //var query1 = new DQuery<AssemblyDto, AssemblyDtoDescriptor>();
            //var query2 = new DQuery<ProjectDto, ProjectDtoDescriptor>();

            //query1.Where(x => x.ID, CompareOperator.GreaterThan, (long)0);
            //query2.Where(x => x.ID, CompareOperator.GreaterThan, (long)1).Where(x => x.Assemblies,query1.AsWhereResult());


            //var selector = query2.Select(x => new ProjectProjector() { ProjectFileName = x.ProjectFileName, Assemblies = x.Assemblies.Select(a => new AssemblyProjector() { AName = a.AssemblyName }) });

            //var projects = client.GetTest<ProjectProjector>(selector.Descriptor);
            //if (projects == null)
            //{
            //    return;
            //}
            //foreach (var project in projects)
            //{
            //    foreach (var assembly in project.Assemblies)
            //    {
            //        Console.WriteLine("ProjectFileName={0} SolutionName{1} AssemblyName={2}", project.ProjectFileName, project.SolutionName, assembly.AName);
            //    }

            //}
        }
        /// <summary>
        /// The test 2.
        /// </summary>
        private static void Test1()
        {
            //Console.WriteLine("Test1");
            //var client = new WebApiClient();

            //var param1 = new DQuery<ProjectDto, ProjectDtoDescriptor>();
            //param1.WhereOr(x => x.Solution.ID, CompareOperator.GreaterThan, 0);

            //var l = new List<DateTime>();
            //l.Add(DateTime.Now);
            //l.Add(DateTime.Now.AddYears(-2));

            //var param = new DQuery<ProjectDto, ProjectDtoDescriptor>();
            //param.Include(x => x.Solution);
            //param.OrderBy(x => x.Name);
            //param.Where(x => x.CreatedDate, CompareOperator.LessThan, DateTime.Now);
            //param.WhereOr(x => x.Description, StringMethods.Contains, "c");
            //param.Where(x => x.CreatedDate.In(l));
            //param.Or(param1.AsWhereResult());
            //param.OrderBy(x => x.ProjectFileName);

            //var projects = client.Get(param.Descriptor);
            //foreach (var project in projects)
            //{
            //    Console.WriteLine("ID={0} Name={1}", project.ID, project.Name);
            //}
        }


        private static void TestCount1()
        {
            //Console.WriteLine("TestCount1");
            //var client = new WebApiClient();

            
            
            //var param1 = new SQuery<ProjectDto>();
            //param1.Where(x => x.Assemblies.Count(a => a.ID > 0) > 0);
            ////param1.Where(x => x.Solution.ID > 2 );

            //var projects = client.Get(param1.Descriptor);
            //Console.WriteLine("nnn");
            //foreach (var project in projects)
            //{
            //    Console.WriteLine("ID={0} Name={1}", project.ID, project.Name);
            //}
        }

        private static void TestCount2()
        {
            //Console.WriteLine("TestCount2");
            //var client = new WebApiClient();



            //var param1 = new SQuery<ProjectDto>();
            //param1.Where(x => x.Assemblies.Count() > 0);
            ////param1.Where(x => x.Solution.ID > 2 );

            //var projects = client.Get(param1.Descriptor);
            //Console.WriteLine("nnn");
            //foreach (var project in projects)
            //{
            //    Console.WriteLine("ID={0} Name={1}", project.ID, project.Name);
            //}
        }

        private static void Test3()
        {
            //Console.WriteLine("Test3");
            //var client = new WebApiClient();



            //var param1 = new DQuery<ProjectDto, ProjectDtoDescriptor>();
            //param1.WhereOr(x => x.Solution.ID, CompareOperator.GreaterThan, 0);

            //var projects = client.Get(param1.Descriptor);
            //Console.WriteLine("nnn");
            //foreach (var project in projects)
            //{
            //    Console.WriteLine("ID={0} Name={1}", project.ID, project.Name);
            //}
        }
        #endregion
    }

    
}