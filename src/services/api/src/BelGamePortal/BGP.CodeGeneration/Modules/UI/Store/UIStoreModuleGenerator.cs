using System;
using System.Collections.Generic;

namespace BGP.CodeGeneration.Builder;

public class UiStoreModuleGenerator
{
    private readonly CodeGeneratorBuilder codeGenerator = new();

    private string? rootFolderPath;

    private string RootFolderPath => rootFolderPath ?? throw new ArgumentNullException(nameof(rootFolderPath));

    private string? moduleName;

    private string ModuleName => moduleName ?? throw new ArgumentNullException(nameof(moduleName));

    private string baseTemplatesPath = "BGP.CodeGeneration/Modules/UI/Store/Templates";

    private string baseFolderPath = "features";

    public static UiStoreModuleGenerator Create()
    {
        return new UiStoreModuleGenerator();
    }

    public UiStoreModuleGenerator WithRootFolderPath(string rootFolderPath = "../../../ui-apps")
    {
        this.rootFolderPath = rootFolderPath;
        return this;
    }

    public UiStoreModuleGenerator WithBaseFolderPath(string baseFolderPath)
    {
        this.baseFolderPath = baseFolderPath;
        return this;
    }

    public UiStoreModuleGenerator WithModuleName(string moduleName)
    {
        this.moduleName = moduleName;
        return this;
    }

    public UiStoreModuleGenerator WithStore()
    {
        var basePathContent = $"{RootFolderPath}/projects/itdl/game-portal/{baseFolderPath}";
        var basePathContentFolder = $"{basePathContent}/ng-xs";
        var basePathContentFolderSrc = $"{basePathContentFolder}/src";

        var vars = new Dictionary<string, object>
        {
            { "ModuleName", ModuleName },
            { "ModuleNamePascal", ModuleName.DashToPascalCase() }
        };

        codeGenerator
           .CreateFolder(basePathContentFolder)
           .CreateFileWithTemplate(
               $"{basePathContentFolder}/index.ts",
               $"{baseTemplatesPath}/Store/index.ts.txt",
               vars)

           .CreateFileWithTemplate(
               $"{basePathContentFolder}/ng-package.json",
               $"{baseTemplatesPath}/Store/ng-package.json.txt",
               vars)

           .CreateFileWithTemplate(
               $"{basePathContentFolder}/public-api.ts",
               $"{baseTemplatesPath}/Store/public-api.ts.txt",
               vars)

            .CreateFolder(basePathContentFolderSrc)
            .CreateFileWithTemplate(
                $"{basePathContentFolderSrc}/{ModuleName}.store.actions.ts",
                $"{baseTemplatesPath}/Store/feature.store.actions.ts.txt",
                vars)

            .CreateFileWithTemplate(
                $"{basePathContentFolderSrc}/{ModuleName}.store.state.ts",
                $"{baseTemplatesPath}/Store/feature.store.state.ts.txt",
                vars)

                ;
        return this;
    }


    public TaskRunner GetTaskRunner()
    {
        return codeGenerator.GetTaskRunner();
    }
}
