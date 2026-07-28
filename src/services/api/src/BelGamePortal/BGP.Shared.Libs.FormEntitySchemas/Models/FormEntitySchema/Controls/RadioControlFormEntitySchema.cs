
using Newtonsoft.Json;

namespace BGP.Shared.Libs.FormEntitySchemas.Models.EntityFormSchema.Controls;
public class RadioControlFormEntitySchema : IControlFormEntitySchema
{
    private readonly ControlFormEntitySchemaType _controlFormEntitySchemaType = ControlFormEntitySchemaType.Radio;

    [JsonConverter(typeof(ControlFormEntitySchemaTypeJsonConverter))]
    public ControlFormEntitySchemaType ControlType => _controlFormEntitySchemaType;

    public string Name { get; set; }

    public int Order { get; set; }
}
