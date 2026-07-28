using System;
using System.Collections.Generic;

namespace BGP.CodeGeneration.Builder;

public class UIFetchersModuleGenerator
{
    private readonly CodeGeneratorBuilder codeGenerator = new();

    private string? rootFolderPath;

    private string RootFolderPath => rootFolderPath ?? throw new ArgumentNullException(nameof(rootFolderPath));

    private string? moduleName;

    private string ModuleName => moduleName ?? throw new ArgumentNullException(nameof(moduleName));

    private string? entityName;

    private string EntityName => entityName ?? throw new ArgumentNullException(nameof(entityName));

    private const string SubPath = "Fetchers";

    private string baseTemplatesPath = $"BGP.CodeGeneration/Modules/UI/{SubPath}/Templates";

    private string baseFolderPath = "features";

    public static UIFetchersModuleGenerator Create()
    {
        return new UIFetchersModuleGenerator();
    }

    public UIFetchersModuleGenerator WithRootFolderPath(string rootFolderPath = "../../../ui-apps")
    {
        this.rootFolderPath = rootFolderPath;
        return this;
    }

    public UIFetchersModuleGenerator WithBaseFolderPath(string baseFolderPath)
    {
        this.baseFolderPath = baseFolderPath;
        return this;
    }

    public UIFetchersModuleGenerator WithModuleName(string moduleName)
    {
        this.moduleName = moduleName;
        return this;
    }

    public UIFetchersModuleGenerator WithEntityName(string entityName)
    {
        this.entityName = entityName;
        return this;
    }

    public UIFetchersModuleGenerator WithItemFetcher()
    {
        var basePathContent = $"{RootFolderPath}/projects/itdl/game-portal/{baseFolderPath}";
        var basePathContentFolder = $"{basePathContent}";
        var basePathContentFolderSrc = $"{basePathContentFolder}/item-fetcher";

        var vars = new Dictionary<string, object>
        {
            { "ModuleName", ModuleName },
            { "ModuleNamePascal", ModuleName.DashToPascalCase() },
            { "ModuleNameCamel", ModuleName.DashToCamelCase() },

            { "EntityName", EntityName },
            { "EntityNamePascal", EntityName.DashToPascalCase() },
            { "EntityNameCamel", EntityName.DashToCamelCase() },
        };

        codeGenerator
            .CreateFolder(basePathContentFolderSrc)

            .CreateFileWithTemplate(
                $"{basePathContentFolderSrc}/{ModuleName}-item-fetcher.component.html",
                $"{baseTemplatesPath}/item-fetcher/feature-item-fetcher.component.html.txt",
                vars)
            .CreateFileWithTemplate(
                $"{basePathContentFolderSrc}/{ModuleName}-item-fetcher.component.scss",
                $"{baseTemplatesPath}/item-fetcher/feature-item-fetcher.component.scss.txt",
                vars)
            .CreateFileWithTemplate(
                $"{basePathContentFolderSrc}/{ModuleName}-item-fetcher.component.ts",
                $"{baseTemplatesPath}/item-fetcher/feature-item-fetcher.component.ts.txt",
                vars)

                ;
        return this;
    }

    public UIFetchersModuleGenerator WithListFetcher()
    {
        var basePathContent = $"{RootFolderPath}/projects/itdl/game-portal/{baseFolderPath}";
        var basePathContentFolder = $"{basePathContent}";
        var basePathContentFolderSrc = $"{basePathContentFolder}/list-fetcher";

        var vars = new Dictionary<string, object>
        {
            { "ModuleName", ModuleName },
            { "ModuleNamePascal", ModuleName.DashToPascalCase() },
            { "ModuleNameCamel", ModuleName.DashToCamelCase() },

            { "EntityName", EntityName },
            { "EntityNamePascal", EntityName.DashToPascalCase() },
            { "EntityNameCamel", EntityName.DashToCamelCase() },
        };

        codeGenerator
            .CreateFolder(basePathContentFolderSrc)

            .CreateFileWithTemplate(
                $"{basePathContentFolderSrc}/{ModuleName}-list-fetcher.component.html",
                $"{baseTemplatesPath}/list-fetcher/feature-list-fetcher.component.html.txt",
                vars)
            .CreateFileWithTemplate(
                $"{basePathContentFolderSrc}/{ModuleName}-list-fetcher.component.scss",
                $"{baseTemplatesPath}/list-fetcher/feature-list-fetcher.component.scss.txt",
                vars)
            .CreateFileWithTemplate(
                $"{basePathContentFolderSrc}/{ModuleName}-list-fetcher.component.ts",
                $"{baseTemplatesPath}/list-fetcher/feature-list-fetcher.component.ts.txt",
                vars)

                ;
        return this;
    }


    public TaskRunner GetTaskRunner()
    {
        return codeGenerator.GetTaskRunner();
    }
}
