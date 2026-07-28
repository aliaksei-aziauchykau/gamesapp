using System;
using System.Collections.Generic;

namespace BGP.CodeGeneration.Builder;

public class UIApiModuleGenerator
{
    private readonly CodeGeneratorBuilder codeGenerator = new();

    private string? rootFolderPath;

    private string RootFolderPath => rootFolderPath ?? throw new ArgumentNullException(nameof(rootFolderPath));

    private string? moduleName;

    private string ModuleName => moduleName ?? throw new ArgumentNullException(nameof(moduleName));

    private string? entityName;

    private string EntityName => entityName ?? throw new ArgumentNullException(nameof(entityName));

    private string baseTemplatesPath = "BGP.CodeGeneration/Modules/UI/Api/Templates";

    private string baseFolderPath = "features";

    public static UIApiModuleGenerator Create()
    {
        return new UIApiModuleGenerator();
    }

    public UIApiModuleGenerator WithRootFolderPath(string rootFolderPath = "../../../ui-apps")
    {
        this.rootFolderPath = rootFolderPath;
        return this;
    }

    public UIApiModuleGenerator WithBaseFolderPath(string baseFolderPath)
    {
        this.baseFolderPath = baseFolderPath;
        return this;
    }

    public UIApiModuleGenerator WithModuleName(string moduleName)
    {
        this.moduleName = moduleName;
        return this;
    }

    public UIApiModuleGenerator WithEntityName(string entityName)
    {
        this.entityName = entityName;
        return this;
    }

    public UIApiModuleGenerator WithApi()
    {
        var basePathContent = $"{RootFolderPath}/projects/itdl/game-portal/{baseFolderPath}";
        var basePathContentFolder = $"{basePathContent}/api";
        var basePathContentFolderSrc = $"{basePathContentFolder}/src";

        var vars = new Dictionary<string, object>
        {
            { "ModuleName", ModuleName },
            { "ModuleNamePascal", ModuleName.DashToPascalCase() },

            { "EntityName", EntityName },
            { "EntityNamePascal", EntityName.DashToPascalCase() },
            { "EntityNameCamel", EntityName.DashToCamelCase() },
        };

        codeGenerator
           .CreateFolder(basePathContentFolder)
           .CreateFileWithTemplate(
               $"{basePathContentFolder}/index.ts",
               $"{baseTemplatesPath}/Api/index.ts.txt",
               vars)

           .CreateFileWithTemplate(
               $"{basePathContentFolder}/ng-package.json",
               $"{baseTemplatesPath}/Api/ng-package.json.txt",
               vars)

           .CreateFileWithTemplate(
               $"{basePathContentFolder}/public-api.ts",
               $"{baseTemplatesPath}/Api/public-api.ts.txt",
               vars)

            .CreateFolder(basePathContentFolderSrc)
            .CreateFileWithTemplate(
                $"{basePathContentFolderSrc}/{ModuleName}.api.service.ts",
                $"{baseTemplatesPath}/Api/feature.api.service.ts.txt",
                vars)

                ;
        return this;
    }


    public TaskRunner GetTaskRunner()
    {
        return codeGenerator.GetTaskRunner();
    }
}
