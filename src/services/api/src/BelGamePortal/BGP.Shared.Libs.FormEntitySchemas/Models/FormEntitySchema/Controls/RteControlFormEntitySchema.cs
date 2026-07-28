
using Newtonsoft.Json;

namespace BGP.Shared.Libs.FormEntitySchemas.Models.EntityFormSchema.Controls;
public class RteControlFormEntitySchema : IControlFormEntitySchema
{
    private readonly ControlFormEntitySchemaType _controlFormEntitySchemaType = ControlFormEntitySchemaType.Rte;

    [JsonConverter(typeof(ControlFormEntitySchemaTypeJsonConverter))]
    public ControlFormEntitySchemaType ControlType => _controlFormEntitySchemaType;

    public string Name { get; set; }

    public int Order { get; set; }

    public string ApiKey { get; set; } = "qrnuo660vki1c05zj77v1c9lhtvdcyjf7fewvlr7kacjk5yy";
}
