using Microsoft.Extensions.DependencyInjection;

namespace GAA.Shared.Modules.Common.Interfaces;

public class DIModule<T> where T : DIModule<T>, new()
{
    private IServiceCollection? _services;

    public IServiceCollection Services => _services ?? throw new InvalidOperationException("Service collection not set");

    public static T Create() => new();

    public static T Create(IServiceCollection services) => new T().AddServiceCollection(services);

    public T AddServiceCollection(IServiceCollection services)
    {
        _services = services;
        return (T)this;
    }
}
