namespace Covis.Data.Json.Contracts
{
    using System.Runtime.Serialization;

    using Covis.Data.Common;

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
