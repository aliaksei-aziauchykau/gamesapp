using System;
using System.Collections.Generic;

namespace BGP.CodeGeneration.Builder;

public class UIEmptyModuleGenerator
{
    private readonly CodeGeneratorBuilder codeGenerator = new();

    private string? rootFolderPath;

    private string RootFolderPath => rootFolderPath ?? throw new ArgumentNullException(nameof(rootFolderPath));

    private string? moduleName;

    private string ModuleName => moduleName ?? throw new ArgumentNullException(nameof(moduleName));

    private string baseTemplatesPath = "BGP.CodeGeneration/Modules/UI/Empty/Templates";

    private string baseFolderPath = "features";

    public static UIEmptyModuleGenerator Create()
    {
        return new UIEmptyModuleGenerator();
    }

    public UIEmptyModuleGenerator WithRootFolderPath(string rootFolderPath = "../../../ui-apps")
    {
        this.rootFolderPath = rootFolderPath;
        return this;
    }

    public UIEmptyModuleGenerator WithBaseFolderPath(string baseFolderPath)
    {
        this.baseFolderPath = baseFolderPath;
        return this;
    }

    public UIEmptyModuleGenerator WithModuleName(string moduleName)
    {
        this.moduleName = moduleName;
        return this;
    }

    public UIEmptyModuleGenerator WithEmptyModule()
    {
        var basePathContent = $"{RootFolderPath}/projects/itdl/game-portal/{baseFolderPath}";
        var basePathContentFolder = $"{basePathContent}/{ModuleName}";
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
               $"{baseTemplatesPath}/Empty/index.ts.txt",
               vars)

           .CreateFileWithTemplate(
               $"{basePathContentFolder}/ng-package.json",
               $"{baseTemplatesPath}/Empty/ng-package.json.txt",
               vars)

           .CreateFileWithTemplate(
               $"{basePathContentFolder}/public-api.ts",
               $"{baseTemplatesPath}/Empty/public-api.ts.txt",
               vars)
            .CreateFolder(basePathContentFolderSrc)

                ;
        return this;
    }


    public TaskRunner GetTaskRunner()
    {
        return codeGenerator.GetTaskRunner();
    }
}
