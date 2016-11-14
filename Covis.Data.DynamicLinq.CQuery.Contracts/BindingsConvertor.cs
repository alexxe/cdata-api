namespace Covis.Data.DynamicLinq.CQuery.Contracts
{
    using System;
    using System.Collections.Generic;

    using Covis.Data.DynamicLinq.CQuery.Contracts.Model;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class BindingsConvertor : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is Dictionary<string, INode>)
            {
                Dictionary<string, INode> components = (Dictionary<string, INode>)value;
                writer.WriteStartArray();
                foreach (KeyValuePair<string, INode> entry in components)
                {
                    serializer.Serialize(writer, entry);
                }
                writer.WriteEndArray();
            }
            else
            {
                writer.WriteStartObject();
                writer.WriteEndObject();
            }
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartArray)
            {
                // Load JArray from stream
                JArray jArray = JArray.Load(reader);

                // Create target object based on JObject
                List<KeyValuePair<string, INode>> target = new List<KeyValuePair<string, INode>>();

                // Populate the object properties
                serializer.Populate(jArray.CreateReader(), target);
                Dictionary<string, INode> dictionary = new Dictionary<string, INode>();

                foreach (KeyValuePair<string, INode> comp in target) if (!dictionary.ContainsKey(comp.Key)) dictionary.Add(comp.Key, comp.Value);
                return dictionary;
            }
            return new Dictionary<string, INode>();
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}