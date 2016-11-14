using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.HttpClient.Model
{
    using System.Linq.Expressions;
    using System.Reflection;

    using Covis.Data.DynamicLinq.CQuery.Contracts.DEntity;

    using Example.Data.Contract.Model;

    public class SolutionProjector : IClientProjector
    {
        

        public SolutionProjector()
        {
           
        }
        public string SName { get; set; }

       
    }
}
