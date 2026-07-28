
using Newtonsoft.Json;

namespace BGP.Shared.Libs.FormEntitySchemas.Models.EntityFormSchema.Controls;
public class CheckboxControlFormEntitySchema : IControlFormEntitySchema
{
    private readonly ControlFormEntitySchemaType _controlFormEntitySchemaType = ControlFormEntitySchemaType.Checkbox;

    [JsonConverter(typeof(ControlFormEntitySchemaTypeJsonConverter))]
    public ControlFormEntitySchemaType ControlType => _controlFormEntitySchemaType;

    public string Name { get; set; }

    public int Order { get; set; }
}
