using System.Reflection;
using BGP.Shared.Libs.Domain.Attributes;
using BGP.Shared.Modules.CrudCommands.Commands;
using BGP.Shared.Modules.CrudCommands.DataSources;
using BGP.Shared.Modules.CrudCommands.Extensions;
using BGP.Shared.Modules.CrudCommands.Repositories;
using BGP.Shared.Modules.CrudCommands.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BGP.Shared.Modules.CrudCommands;

public class CrudCommandsModule
{
    private IServiceCollection services;

    private readonly IDictionary<DataSourceType, Action<Type, Type>> factoryDataSource
        = new Dictionary<DataSourceType, Action<Type, Type>>();

    public static CrudCommandsModule Create()
    {
        return new CrudCommandsModule();
    }

    public CrudCommandsModule AddServiceCollection(IServiceCollection services)
    {
        this.services = services;

        return this;
    }

    public CrudCommandsModule AddCrudServices()
    {
        services.AddScoped(typeof(ICrudService<,>), typeof(CrudService<,>));

        return this;
    }

    public CrudCommandsModule AddRepositories()
    {
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

        return this;
    }

    public CrudCommandsModule AddGenericServices()
    {
        services.AddScoped(typeof(ICrudService<,>), typeof(CrudService<,>));
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

        return this;
    }

    public CrudCommandsModule OverrideServices(Action<IServiceCollection> action)
    {
        action(services);

        return this;
    }

    public CrudCommandsModule AddDbContextSource(Type dbcontextType)
    {
        if (this.factoryDataSource.ContainsKey(DataSourceType.DbContext))
        {
            this.factoryDataSource.Remove(DataSourceType.DbContext);
        }

        this.factoryDataSource.Add(DataSourceType.DbContext, (entity, entityKey) =>
        {
            //services.AddScoped(interfaceType, dbcontextType);
            services.AddScoped(
                typeof(IDataSource<,>).MakeGenericType([entity, entityKey]),
                typeof(DbContextDataSource<,,>).MakeGenericType([dbcontextType, entity, entityKey]));
        });

        return this;
    }

    public CrudCommandsModule AddCrudCommands(Assembly assembly)
    {
        services.RegisterCrudCommands(assembly, (entity, entityKey) =>
        {
            var attributes = entity.GetCustomAttributes(typeof(DataSourceAttribute), false)
                .OfType<DataSourceAttribute>();

            foreach (var attribute in attributes)
            {
                factoryDataSource[attribute.DataSourceType](entity, entityKey);
            }
        });

        return this;
    }

    public CrudCommandsModule AddCrudCommandsByEntity(Type entityType)
    {
        services
            .RegisterGenericCommand(
                entityType,
                typeof(AddEntityCommand<,>),
                typeof(AddEntityCommandHandler<,>),
                entityType)

            .RegisterGenericCommand(
                entityType,
                typeof(AddOrUpdateEntityCommand<,>),
                typeof(AddOrUpdateEntityCommandHandler<,>),
                entityType)

            .RegisterGenericCommand(
                entityType,
                typeof(DeleteEntityCommand<,>),
                typeof(DeleteEntityCommandHandler<,>),
                entityType)

            .RegisterGenericCommand(
                entityType,
                typeof(GetAllEntitiesCommand<,>),
                typeof(GetAllEntitiesCommandHandler<,>),
                typeof(IList<>).MakeGenericType([entityType]))

            .RegisterGenericCommand(
                entityType,
                typeof(GetEntitiesCommand<,>),
                typeof(GetEntitiesCommandHandler<,>),
                typeof(IQueryable<>).MakeGenericType([entityType]))

            .RegisterGenericCommand(
                entityType,
                typeof(GetEntityByIdCommand<,>),
                typeof(GetEntityByIdCommandHandler<,>),
                entityType)

            .RegisterGenericCommand(
                entityType,
                typeof(PatchEntityCommand<,>),
                typeof(PatchEntityCommandHandler<,>),
                entityType)

            .RegisterGenericCommand(
                entityType,
                typeof(UpdateEntityCommand<,>),
                typeof(UpdateEntityCommandHandler<,>),
                entityType)

            .RegisterGenericCommand(
                entityType,
                typeof(GetEntityByIdQueryCommand<,>),
                typeof(GetEntityByIdQueryCommandHandler<,>),
                typeof(IQueryable<>).MakeGenericType([entityType]));

        return this;
    }
}
