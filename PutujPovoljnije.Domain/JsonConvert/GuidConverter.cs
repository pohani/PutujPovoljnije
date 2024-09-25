using Newtonsoft.Json;

namespace PutujPovoljnije.Domain.JsonConvert
{
    public class GuidConverter : JsonConverter<Guid>
    {

        public override Guid ReadJson(JsonReader reader, Type objectType, Guid existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                string value = reader.Value.ToString();
                if (Guid.TryParse(value, out Guid guidValue))
                {
                    return guidValue;
                }
            }
            // Return Guid.Empty or handle as needed if parsing fails
            return Guid.Empty;
        }

        public override void WriteJson(JsonWriter writer, Guid value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}