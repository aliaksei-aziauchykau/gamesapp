using Microsoft.Extensions.DependencyInjection;

namespace BGP.Shared.Libs.Common;

public class DIModule<T> where T : DIModule<T>, new()
{
    private IServiceCollection? service;

    public IServiceCollection Services => service ?? throw new InvalidOperationException("Service collection not set");

    public static T Create()
    {
        return new T();
    }

    public static T Create(IServiceCollection services)
    {
        return new T().AddServiceCollection(services);
    }

    public T AddServiceCollection(IServiceCollection services)
    {
        this.service = services;

        return (T)this;
    }
}
