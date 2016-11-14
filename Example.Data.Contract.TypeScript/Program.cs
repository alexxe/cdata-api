using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Data.Contract.TypeScript
{
    using System.IO;
    using System.Reflection;

    using TypeLite;
    using TypeLite.Net4;

    class Program
    {
        static void Main(string[] args)
        {
            TypeScript.Definitions().ForLoadedAssemblies();
        }

        
    }
}
