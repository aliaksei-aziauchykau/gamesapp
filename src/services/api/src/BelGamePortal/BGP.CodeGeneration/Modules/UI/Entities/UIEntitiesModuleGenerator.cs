using System;
using System.Collections.Generic;

namespace BGP.CodeGeneration.Builder;

public class UIEntitiesModuleGenerator
{
    private readonly CodeGeneratorBuilder codeGenerator = new();

    private string? rootFolderPath;

    private string RootFolderPath => rootFolderPath ?? throw new ArgumentNullException(nameof(rootFolderPath));

    private string? entityName;

    private string EntityName => entityName ?? throw new ArgumentNullException(nameof(entityName));

    private const string SubPath = "Entities";

    private string baseTemplatesPath = $"BGP.CodeGeneration/Modules/UI/{SubPath}/Templates";

    private string baseFolderPath = "features";

    public static UIEntitiesModuleGenerator Create()
    {
        return new UIEntitiesModuleGenerator();
    }

    public UIEntitiesModuleGenerator WithRootFolderPath(string rootFolderPath = "../../../ui-apps")
    {
        this.rootFolderPath = rootFolderPath;
        return this;
    }

    public UIEntitiesModuleGenerator WithBaseFolderPath(string baseFolderPath)
    {
        this.baseFolderPath = baseFolderPath;
        return this;
    }

    public UIEntitiesModuleGenerator WithEntityName(string entityName)
    {
        this.entityName = entityName;
        return this;
    }

    public UIEntitiesModuleGenerator WithEntities()
    {
        var basePathContent = $"{RootFolderPath}/projects/itdl/game-portal/{baseFolderPath}";
        var basePathContentFolder = $"{basePathContent}/entities";
        var basePathContentFolderSrc = $"{basePathContentFolder}/src";

        var vars = new Dictionary<string, object>
        {
            { "EntityName", EntityName },
            { "EntityNamePascal", EntityName.DashToPascalCase() },
            { "EntityNameCamel", EntityName.DashToCamelCase() },
        };

        codeGenerator
           .CreateFolder(basePathContentFolder)
           .CreateFileWithTemplate(
               $"{basePathContentFolder}/index.ts",
               $"{baseTemplatesPath}/{SubPath}/index.ts.txt",
               vars)

           .CreateFileWithTemplate(
               $"{basePathContentFolder}/ng-package.json",
               $"{baseTemplatesPath}/{SubPath}/ng-package.json.txt",
               vars)

           .CreateFileWithTemplate(
               $"{basePathContentFolder}/public-api.ts",
               $"{baseTemplatesPath}/{SubPath}/public-api.ts.txt",
               vars)

            .CreateFolder(basePathContentFolderSrc)
            .CreateFolder($"{basePathContentFolderSrc}/models")
            .CreateFileWithTemplate(
                $"{basePathContentFolderSrc}/models/{EntityName}.model.ts",
                $"{baseTemplatesPath}/{SubPath}/feature.model.ts.txt",
                vars)

                ;
        return this;
    }


    public TaskRunner GetTaskRunner()
    {
        return codeGenerator.GetTaskRunner();
    }
}
