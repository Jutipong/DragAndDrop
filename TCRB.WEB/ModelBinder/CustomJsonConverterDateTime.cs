using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TCRB.WEB.ModelBinder
{
    public class CustomJsonConverterDateTime : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var result = DateTime.Parse(reader.GetString());
            return result;
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            var result = value.ToString("MM/dd/yyyy HH:mm:ss", new CultureInfo("th-TH"));
            writer.WriteStringValue(result);
        }
    }
}
