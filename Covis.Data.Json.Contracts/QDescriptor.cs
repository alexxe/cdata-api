using System.Collections.Generic;

namespace QData.Json.Contracts
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
