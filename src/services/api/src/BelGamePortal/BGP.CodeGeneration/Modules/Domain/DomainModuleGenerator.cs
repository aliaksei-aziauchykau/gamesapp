using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BGP.CodeGeneration.Builder;

public class DomainModuleGenerator
{
    private CodeGeneratorBuilder codeGenerator = new CodeGeneratorBuilder();
    private string domainName;

    public static DomainModuleGenerator Create()
    {
        return new DomainModuleGenerator();
    }

    public DomainModuleGenerator WithDomainName(string domainName)
    {
        this.domainName = domainName;
        return this;
    }


    public DomainModuleGenerator WithSetupProject()
    {
        var appName = $"BGP.{domainName}.Domain";

        codeGenerator
            .CreateProject(appName)
            .DeleteFile($"{appName}/Class1.cs")

            .PopulateWithTemplate(
                $"{appName}/{appName}.csproj",
                "BGP.CodeGeneration/Modules/Domain/Templates/Project.csproj.txt",
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
                    { "LayerName", "Domain" }
                })

            .AddProjectToSln(appName)
            ;

        return this;
    }


    public DomainModuleGenerator WithEntities(List<EntityParam> entityParams)
    {
        var appName = $"BGP.{domainName}.Domain";

        codeGenerator
            .CreateFolder($"{domainName}/Entities");


        foreach (var entityParam in entityParams)
        {
            codeGenerator.CreateFileWithTemplate(
                $"{appName}/Entities/{entityParam.EntityName}.cs",
                "BGP.CodeGeneration/Modules/Domain/Templates/Entity.cs.txt",
                new Dictionary<string, object>
                {
                    { "DomainName", domainName },
                    { "EntityName", entityParam.EntityName },
                    { "EntityKey", entityParam.EntityKey },
                });
        }

        return this;
    }

    public TaskRunner GetTaskRunner()
    {
        return codeGenerator.GetTaskRunner();
    }
}
