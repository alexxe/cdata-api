using System.Runtime.Serialization;
using QData.Common;

namespace Qdata.Json.Contract
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
