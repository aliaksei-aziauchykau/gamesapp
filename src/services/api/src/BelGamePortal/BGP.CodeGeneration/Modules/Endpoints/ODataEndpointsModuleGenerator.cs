using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BGP.CodeGeneration.Builder;

public class ODataEndpointsModuleGenerator
{
    private CodeGeneratorBuilder codeGenerator = new CodeGeneratorBuilder();
    private string domainName;

    public static ODataEndpointsModuleGenerator Create()
    {
        return new ODataEndpointsModuleGenerator();
    }

    public ODataEndpointsModuleGenerator WithDomainName(string domainName)
    {
        this.domainName = domainName;
        return this;
    }

    public ODataEndpointsModuleGenerator WithSetupProject()
    {
        var appName = $"BGP.{domainName}.OData.Endpoints";

        codeGenerator
            .CreateProject(appName)
            .DeleteFile($"{appName}/Class1.cs")

            .PopulateWithTemplate(
                $"{appName}/{appName}.csproj",
                "BGP.CodeGeneration/Modules/Endpoints/Templates/Project.csproj.txt",
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
                    { "LayerName", "ODataEndpoints" }
                })


             .CreateFileWithTemplate(
                $"{appName}/{domainName}ODataEndpointModule.cs",
                "BGP.CodeGeneration/Modules/Endpoints/Templates/ODataEndpointModule.cs.txt",
                new Dictionary<string, object>
                {
                    { "DomainName", domainName },
                })

            .AddProjectToSln(appName)
            ;

        return this;
    }


    public ODataEndpointsModuleGenerator WithControllers(List<EntityParam> entityParams)
    {
        var appName = $"BGP.{domainName}.OData.Endpoints";

        codeGenerator
            .CreateFolder($"{appName}/Modules");

        foreach (var entityParam in entityParams)
        {
            codeGenerator
                .CreateFolder($"{appName}/Modules/{entityParam.EntityName}s");

            codeGenerator.CreateFileWithTemplate(
                $"{appName}/Modules/{entityParam.EntityName}s/{entityParam.EntityName}sController.cs",
                "BGP.CodeGeneration/Modules/Endpoints/Templates/Modules/EntityController.cs.txt",
                new Dictionary<string, object>
                {
                    { "DomainName", domainName },
                    { "EntityName", entityParam.EntityName },
                    { "EntityKey", entityParam.EntityKey },
                });

            codeGenerator.CreateFileWithTemplate(
                $"{appName}/Modules/{entityParam.EntityName}s/{entityParam.EntityName}sModelConfiguration.cs",
                "BGP.CodeGeneration/Modules/Endpoints/Templates/Modules/EntityModelConfiguration.cs.txt",
                new Dictionary<string, object>
                {
                    { "DomainName", domainName },
                    { "EntityName", entityParam.EntityName },
                });

            codeGenerator.CreateFileWithTemplate(
                $"{appName}/Modules/{entityParam.EntityName}s/{entityParam.EntityName}CreateDto.cs",
                "BGP.CodeGeneration/Modules/Endpoints/Templates/Modules/EntityCreateDto.cs.txt",
                new Dictionary<string, object>
                {
                    { "DomainName", domainName },
                    { "EntityName", entityParam.EntityName },
                });

            codeGenerator.CreateFileWithTemplate(
                $"{appName}/Modules/{entityParam.EntityName}s/{entityParam.EntityName}UpdateDto.cs",
                "BGP.CodeGeneration/Modules/Endpoints/Templates/Modules/EntityUpdateDto.cs.txt",
                new Dictionary<string, object>
                {
                    { "DomainName", domainName },
                    { "EntityName", entityParam.EntityName },
                });

            codeGenerator.CreateFileWithTemplate(
                $"{appName}/Modules/{entityParam.EntityName}s/{entityParam.EntityName}PatchDto.cs",
                "BGP.CodeGeneration/Modules/Endpoints/Templates/Modules/EntityPatchDto.cs.txt",
                new Dictionary<string, object>
                {
                    { "DomainName", domainName },
                    { "EntityName", entityParam.EntityName },
                });
        }

        return this;
    }



    public TaskRunner GetTaskRunner()
    {
        return codeGenerator.GetTaskRunner();
    }
}
