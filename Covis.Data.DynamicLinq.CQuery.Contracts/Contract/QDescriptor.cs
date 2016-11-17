using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covis.Data.DynamicLinq.CQuery.Contracts.Contract
{
    public class QDescriptor
    {
        public QDescriptor()
        {
            
            this.Include = new List<QNode>();
                 
        }
        public QNode Root { get; set; }

        public List<QNode> Include { get; set; }
    }
}
