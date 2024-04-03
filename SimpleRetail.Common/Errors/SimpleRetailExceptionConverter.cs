using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SimpleRetail.Common.Errors
{
    public class SimpleRetailExceptionConverter: JsonConverter<SimpleRetailException>
    {
        public override SimpleRetailException Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, SimpleRetailException value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("code", value.Code);
            writer.WriteString("errorMessage", value.ErrorMessage);
            writer.WriteString("errorInnerMessage", value.ErrorInnerMessage);
            writer.WriteString("errorStackTrace", value.ErrorStackTrace);
            writer.WriteNumber("statusCode", value.StatusCode);
            writer.WriteString("timeStamp", value.TimeStamp.ToUniversalTime());
            writer.WriteEndObject();
        }
    }
}
