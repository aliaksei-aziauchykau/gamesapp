using System;
using System.Collections.Generic;

namespace BGP.CodeGeneration.Builder;

public class UiFeatureModuleGenerator
{
    private CodeGeneratorBuilder codeGenerator = new CodeGeneratorBuilder();

    private string? rootFolderPath;

    private string RootFolderPath => rootFolderPath ?? throw new ArgumentNullException(nameof(rootFolderPath));

    private string? moduleName;

    private string ModuleName => moduleName ?? throw new ArgumentNullException(nameof(moduleName));

    private string baseTemplatesPath = "BGP.CodeGeneration/Modules/UI/Feature/Templates";

    private string baseFolderPath = "features";

    public static UiFeatureModuleGenerator Create()
    {
        return new UiFeatureModuleGenerator();
    }

    public UiFeatureModuleGenerator WithRootFolderPath(string rootFolderPath = "../../../ui-apps")
    {
        this.rootFolderPath = rootFolderPath;
        return this;
    }

    public UiFeatureModuleGenerator WithBaseFolderPath(string baseFolderPath)
    {
        this.baseFolderPath = baseFolderPath;
        return this;
    }

    public UiFeatureModuleGenerator WithModuleName(string moduleName)
    {
        this.moduleName = moduleName;
        return this;
    }

    public UiFeatureModuleGenerator WithFeature()
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
               $"{baseTemplatesPath}/Feature/index.ts.txt",
               vars)

           .CreateFileWithTemplate(
               $"{basePathContentFolder}/ng-package.json",
               $"{baseTemplatesPath}/Feature/ng-package.json.txt",
               vars)

           .CreateFileWithTemplate(
               $"{basePathContentFolder}/public-api.ts",
               $"{baseTemplatesPath}/Feature/public-api.ts.txt",
               vars)

            .CreateFolder(basePathContentFolderSrc)
            .CreateFolder($"{basePathContentFolderSrc}/modules")
            .CreateFileWithTemplate(
                $"{basePathContentFolderSrc}/{ModuleName}.component.ts",
                $"{baseTemplatesPath}/Feature/feature.component.ts.txt",
                vars)

            .CreateFileWithTemplate(
                $"{basePathContentFolderSrc}/{ModuleName}.component.html",
                $"{baseTemplatesPath}/Feature/feature.component.html.txt",
                vars)

            .CreateFileWithTemplate(
                $"{basePathContentFolderSrc}/{ModuleName}.component.scss",
                $"{baseTemplatesPath}/Feature/feature.component.scss.txt",
                vars)

                ;
        return this;
    }


    public TaskRunner GetTaskRunner()
    {
        return codeGenerator.GetTaskRunner();
    }
}
