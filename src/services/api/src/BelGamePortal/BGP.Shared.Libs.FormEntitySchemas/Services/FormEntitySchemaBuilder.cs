using BGP.Shared.Libs.FormEntitySchemas.Models.EntityFormSchema;

namespace BGP.Shared.Libs.FormEntitySchemas.Services;

public class FormEntitySchemaBuilder : IFormEntitySchemaBuilder
{
    private IFormEntitySchema _formEntitySchema = new FormEntitySchema();

    public IFormEntitySchema FormEntitySchema => _formEntitySchema;

    public IFormEntitySchemaBuilder CreateFormSchema()
    {
        _formEntitySchema = new FormEntitySchema();
        return this;
    }

    public IFormEntitySchemaBuilder WithCollectionName(string collectionName)
    {
        this._formEntitySchema.CollectionName = collectionName;
        return this;
    }

    public IFormEntitySchemaBuilder WithTitle(string title)
    {
        this._formEntitySchema.Title = title;
        return this;
    }


    public IFormEntitySchemaBuilder AddControlSchema(string name, IControlFormEntitySchema controlSchema)
    {
        this._formEntitySchema.Controls.Add(name, controlSchema);
        return this;
    }

    public IFormEntitySchemaBuilder AddControlSchema(IControlFormEntitySchema controlSchema)
    {
        this._formEntitySchema.Controls.Add(controlSchema.Name, controlSchema);
        return this;
    }
}
