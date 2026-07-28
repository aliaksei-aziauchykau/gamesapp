using System.Collections.Generic;

namespace BGP.CodeGeneration.Builder;

public class MicroserviceModuleGenerator
{
    private CodeGeneratorBuilder codeGenerator = new CodeGeneratorBuilder();
    private string domainName;

    public static MicroserviceModuleGenerator Create()
    {
        return new MicroserviceModuleGenerator();
    }


    public MicroserviceModuleGenerator WithDomainName(string domainName)
    {
        this.domainName = domainName;
        return this;
    }

    public MicroserviceModuleGenerator WithSetupProject()
    {
        var appName = $"BGP.{domainName}.Microservice";

        codeGenerator
            .CreateProject(appName)
            .DeleteFile($"{appName}/Class1.cs")

            .PopulateWithTemplate(
                $"{appName}/{appName}.csproj",
                "BGP.CodeGeneration/Modules/Microservice/Templates/Project.csproj.txt",
                new Dictionary<string, object>
                {
                    { "DomainName", domainName }
                })

            .CreateFileWithTemplate(
                $"{appName}/{domainName}Microservice.cs",
                "BGP.CodeGeneration/Modules/Microservice/Templates/Microservice.cs.txt",
                new Dictionary<string, object>
                {
                    { "DomainName", domainName },
                })
            .CreateFileWithTemplate(
                $"{appName}/{domainName}MicroserviceBuilder.cs",
                "BGP.CodeGeneration/Modules/Microservice/Templates/MicroserviceBuilder.cs.txt",
                new Dictionary<string, object>
                {
                    { "DomainName", domainName },
                })
            .AddProjectToSln(appName)
            ;

        return this;
    }

    public MicroserviceModuleGenerator WithEnvs()
    {
        var appName = $"BGP.{domainName}.Microservice";

        codeGenerator
            .CreateFile($"{appName}/.env")
            .CreateFile($"{appName}/.Design.{domainName}.env")
            .CreateFile($"{appName}/.Local.{domainName}.env")
            .CreateFile($"{appName}/.Development.{domainName}.env");

        return this;
    }

    public TaskRunner GetTaskRunner()
    {
        return codeGenerator.GetTaskRunner();
    }
}
