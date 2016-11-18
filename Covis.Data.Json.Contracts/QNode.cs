using System.Runtime.Serialization;
using QData.Common;

namespace QData.Json.Contracts
{
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
