namespace BGP.Shared.Libs.FormEntitySchemas.Services;

public interface IModelFormJsonSchemaConverter
{
    string Convert<T>(T model) where T : class;
}
