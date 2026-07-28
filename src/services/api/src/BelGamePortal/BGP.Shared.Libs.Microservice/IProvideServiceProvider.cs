using Microsoft.Extensions.DependencyInjection;

namespace BGP.Shared.Libs.Microservice;

public interface IProvideServiceProvider
{
    IServiceProvider ServiceProvider { get; }

    IServiceScope Scope => ServiceProvider.CreateScope();
}
