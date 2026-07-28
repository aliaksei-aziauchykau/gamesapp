
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BGP.Shared.Libs.FormEntitySchemas.Models.EntityFormSchema.Controls;

public class SelectControlFormEntitySchema<TOption> : IControlFormEntitySchema
{
    private readonly ControlFormEntitySchemaType _controlFormEntitySchemaType = ControlFormEntitySchemaType.Select;

    [JsonConverter(typeof(ControlFormEntitySchemaTypeJsonConverter))]
    public ControlFormEntitySchemaType ControlType => _controlFormEntitySchemaType;

    public string Name { get; set; }

    public int Order { get; set; }

    public List<Option<TOption>> Options { get; set; } = new List<Option<TOption>>();

    public class Option<T>
    {
        public Option(string Label, T Value)
        {
        }

        public T Value { get; set; }

        public string Label { get; set; }
    }
}



