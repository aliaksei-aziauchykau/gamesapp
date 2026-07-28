using System.Collections.Generic;

namespace BGP.CodeGeneration.Builder;

public class ApplicationModuleGenerator
{
    private CodeGeneratorBuilder codeGenerator = new CodeGeneratorBuilder();
    private string domainName;

    public static ApplicationModuleGenerator Create()
    {
        return new ApplicationModuleGenerator();
    }

    public ApplicationModuleGenerator WithDomainName(string domainName)
    {
        this.domainName = domainName;
        return this;
    }

    public ApplicationModuleGenerator WithSetupProject()
    {
        var appName = $"BGP.{domainName}.Application";

        codeGenerator
            .CreateProject(appName)
            .DeleteFile($"{appName}/Class1.cs")

            .PopulateWithTemplate(
                $"{appName}/{appName}.csproj",
                "BGP.CodeGeneration/Modules/Application/Templates/Project.csproj.txt",
                new Dictionary<string, object>
                {
                    { "DomainName", domainName }
                })

            .CreateFileWithTemplate(
                $"{appName}/AssemblyHelper.cs",
                "BGP.CodeGeneration/Modules/Common/Templates/AssemblyHelper.cs.txt",
                new Dictionary<string, object>
                {
                    { "DomainName", domainName },
                    { "LayerName", "Application" }
                })

            .CreateFileWithTemplate(
                $"{appName}/{domainName}ApplicationModule.cs",
                "BGP.CodeGeneration/Modules/Application/Templates/ApplicationModule.cs.txt",
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
