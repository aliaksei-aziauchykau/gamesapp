
namespace BGP.Shared.Libs.FormEntitySchemas.Models.EntityFormSchema;

public interface IControlFormEntitySchema
{
    public ControlFormEntitySchemaType ControlType { get; }

    public string Name { get; set; }

    public int Order { get; set; }
}
