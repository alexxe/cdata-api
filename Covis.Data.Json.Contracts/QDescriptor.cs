namespace Covis.Data.Json.Contracts
{
    using System.Collections.Generic;

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
