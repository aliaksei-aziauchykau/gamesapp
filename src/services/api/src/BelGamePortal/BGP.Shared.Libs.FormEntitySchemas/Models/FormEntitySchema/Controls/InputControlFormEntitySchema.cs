
using Newtonsoft.Json;

namespace BGP.Shared.Libs.FormEntitySchemas.Models.EntityFormSchema.Controls;

public class InputControlFormEntitySchema : IControlFormEntitySchema
{
    private readonly ControlFormEntitySchemaType _controlFormEntitySchemaType = ControlFormEntitySchemaType.Input;

    [JsonConverter(typeof(ControlFormEntitySchemaTypeJsonConverter))]
    public ControlFormEntitySchemaType ControlType => _controlFormEntitySchemaType;

    public string Name { get; set; }

    public int Order { get; set; }

    public string Label { get; set; }

    public string Placeholder { get; set; }
}
