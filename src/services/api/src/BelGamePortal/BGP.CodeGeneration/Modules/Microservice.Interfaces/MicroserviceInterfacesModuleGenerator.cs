using System.Collections.Generic;

namespace BGP.CodeGeneration.Builder;

public class MicroserviceInterfacesModuleGenerator
{
    private CodeGeneratorBuilder codeGenerator = new CodeGeneratorBuilder();
    private string domainName;

    public static MicroserviceInterfacesModuleGenerator Create()
    {
        return new MicroserviceInterfacesModuleGenerator();
    }

    public MicroserviceInterfacesModuleGenerator WithDomainName(string domainName)
    {
        this.domainName = domainName;
        return this;
    }

    public MicroserviceInterfacesModuleGenerator WithSetupProject()
    {
        var appName = $"BGP.{domainName}.Microservice.Interfaces";

        codeGenerator
            .CreateProject(appName)
            .DeleteFile($"{appName}/Class1.cs")

            .PopulateWithTemplate(
                $"{appName}/{appName}.csproj",
                "BGP.CodeGeneration/Modules/Microservice.Interfaces/Templates/Project.csproj.txt",
                new Dictionary<string, object>
                {
                    { "DomainName", domainName }
                })

            .CreateFileWithTemplate(
                $"{appName}/I{domainName}Microservice.cs",
                "BGP.CodeGeneration/Modules/Microservice.Interfaces/Templates/IMicroservice.cs.txt",
                new Dictionary<string, object>
                {
                    { "DomainName", domainName },
                })
            .AddProjectToSln(appName)
            ;

        return this;
    }

    public TaskRunner GetTaskRunner()
    {
        return codeGenerator.GetTaskRunner();
    }
}
