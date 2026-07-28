

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BGP.CodeGeneration.Builder;
using MediatR;

namespace BGP.CodeGeneration.Commands.Server;

public class ServerSetupProjectCommand : IRequest
{
}

public class ServerSetupProjectCommandHandler : IRequestHandler<ServerSetupProjectCommand>
{
    public async Task Handle(ServerSetupProjectCommand command, CancellationToken cancellationToken)
    {
        List<EntityParam> entityParams = [
            new EntityParam { EntityName = "Workspace", EntityKey = "Guid" },
            new EntityParam { EntityName = "Project", EntityKey = "Guid" },
            new EntityParam { EntityName = "ProjectVisibility", EntityKey = "Guid" },
            new EntityParam { EntityName = "Container", EntityKey = "Guid" },
            new EntityParam { EntityName = "Post", EntityKey = "Guid" },
            new EntityParam { EntityName = "Event", EntityKey = "Guid" },
            new EntityParam { EntityName = "ProjectContainer", EntityKey = "Guid" },
            new EntityParam { EntityName = "ParentContainerContainer", EntityKey = "Guid" },
        ];

        Console.WriteLine("Create Domain project...");
        await DomainModuleGenerator
            .Create()
            .WithDomainName("Platform")
            .WithSetupProject()
            .WithEntities(entityParams)
            .GetTaskRunner()
            .RunAllAsync();

        Console.WriteLine("Create Application project...");
        await ApplicationModuleGenerator
            .Create()
            .WithDomainName("Platform")
            .WithSetupProject()
            .GetTaskRunner()
            .RunAllAsync();

        Console.WriteLine("Create Database project...");
        await DatabaseModuleGenerator
            .Create()
            .WithDomainName("Platform")
            .WithSetupProject()
            .WithDbContext()
            .WithDbContextInitializerAndSeeder()
            .WithDesignDbContextFactory()
            .WithScripts()
            .WithEntities(entityParams)
            .GetTaskRunner()
            .RunAllAsync();

        Console.WriteLine("Create Endpoints project...");
        await ODataEndpointsModuleGenerator
            .Create()
            .WithDomainName("Platform")
            .WithControllers(entityParams)
            .WithSetupProject()
            .GetTaskRunner()
            .RunAllAsync();

        Console.WriteLine("Create Microservice project...");
        await MicroserviceModuleGenerator
            .Create()
            .WithDomainName("Platform")
            .WithSetupProject()
            .WithEnvs()
            .GetTaskRunner()
            .RunAllAsync();

        Console.WriteLine("Create Microservice Interfaces project...");
        await MicroserviceInterfacesModuleGenerator
            .Create()
            .WithDomainName("Platform")
            .WithSetupProject()
            .GetTaskRunner()
            .RunAllAsync();
    }
}
