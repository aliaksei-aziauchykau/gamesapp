namespace BGP.Shared.Libs.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class DataSourceAttribute : Attribute
{
    public DataSourceType DataSourceType { get; }

    public DataSourceAttribute(DataSourceType dataSourceType)
    {
        DataSourceType = dataSourceType;
    }
}

public enum DataSourceType
{
    DbContext = 1,

    MongoDb = 2,
}
