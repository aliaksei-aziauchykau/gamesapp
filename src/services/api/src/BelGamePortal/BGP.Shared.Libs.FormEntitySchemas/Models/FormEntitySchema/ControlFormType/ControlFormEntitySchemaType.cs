namespace BGP.Shared.Libs.FormEntitySchemas.Models.EntityFormSchema;

public sealed class ControlFormEntitySchemaType
{
    public static readonly ControlFormEntitySchemaType Input = new ControlFormEntitySchemaType("input");
    public static readonly ControlFormEntitySchemaType Select = new ControlFormEntitySchemaType("select");
    public static readonly ControlFormEntitySchemaType Checkbox = new ControlFormEntitySchemaType("checkbox");
    public static readonly ControlFormEntitySchemaType Radio = new ControlFormEntitySchemaType("radio");
    public static readonly ControlFormEntitySchemaType Rte = new ControlFormEntitySchemaType("rte");

    private ControlFormEntitySchemaType(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }
}
