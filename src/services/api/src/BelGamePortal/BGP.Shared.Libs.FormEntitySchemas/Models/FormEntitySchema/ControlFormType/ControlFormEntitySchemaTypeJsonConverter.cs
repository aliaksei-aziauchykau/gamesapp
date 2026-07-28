using System;
using Newtonsoft.Json;

namespace BGP.Shared.Libs.FormEntitySchemas.Models.EntityFormSchema;

public class ControlFormEntitySchemaTypeJsonConverter : JsonConverter
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(ControlFormEntitySchemaType) == typeToConvert;
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        if (value == null)
        {
            serializer.Serialize(writer, null);
            return;
        }


        if (value is not ControlFormEntitySchemaType valuesObject) throw new Exception("Cannot convert to control form entity schema type");

        writer.WriteValue(valuesObject.Value);
    }
}
