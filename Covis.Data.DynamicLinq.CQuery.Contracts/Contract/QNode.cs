using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covis.Data.DynamicLinq.CQuery.Contracts.Contract
{
    using System.Runtime.Serialization;

    [DataContract]
    public class QNode
    {
        [DataMember]
        public NodeType Type { get; set; }
        [DataMember]
        public QNode Left { get; set; }
        [DataMember]
        public QNode Right { get; set; }
        [DataMember]
        public object Value { get; set; }
    }
}
