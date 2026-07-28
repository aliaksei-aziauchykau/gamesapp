using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BGP.CodeGeneration.Builder;

public class UiPageModuleGenerator
{
    private CodeGeneratorBuilder codeGenerator = new CodeGeneratorBuilder();

    private string rootFolderPath;

    private string moduleName;

    private string baseTemplatesPath = "BGP.CodeGeneration/Modules/UI/Page/Templates";

    public static UiPageModuleGenerator Create()
    {
        return new UiPageModuleGenerator();
    }

    public UiPageModuleGenerator WithRootFolderPath(string rootFolderPath = "../../../ui-apps")
    {
        this.rootFolderPath = rootFolderPath;
        return this;
    }

    public UiPageModuleGenerator WithModuleName(string moduleName)
    {
        this.moduleName = moduleName;
        return this;
    }

    public UiPageModuleGenerator WithPage()
    {
        var basePathPage = $"{rootFolderPath}/projects/game-portal/src/app/modules";

        var vars = new Dictionary<string, object>
        {
            { "ModuleName", moduleName },
            { "ModuleNamePascal", moduleName.DashToPascalCase() }
        };

        var folder = $"{basePathPage}/{moduleName}-page";

        codeGenerator
           .CreateFolder(folder)
           .CreateFileWithTemplate(
               $"{folder}/{moduleName}-page.module.ts",
               $"{baseTemplatesPath}/Page/page.module.ts.txt",
               vars)

           .CreateFileWithTemplate(
               $"{folder}/{moduleName}-page.component.ts",
               $"{baseTemplatesPath}/Page/page.component.ts.txt",
               vars)

           .CreateFileWithTemplate(
               $"{folder}/{moduleName}-page.component.html",
               $"{baseTemplatesPath}/Page/page.component.html.txt",
               vars)

           .CreateFileWithTemplate(
               $"{folder}/{moduleName}-page.component.scss",
               $"{baseTemplatesPath}/Page/page.component.scss.txt",
               vars)

           .CreateFileWithTemplate(
               $"{folder}/{moduleName}-page.routing.module.ts",
               $"{baseTemplatesPath}/Page/page.routing.module.ts.txt",
               vars)

            ;

        return this;
    }

    public UiPageModuleGenerator WithContent()
    {
        var basePathContent = $"{rootFolderPath}/projects/itdl/game-portal/contents";
        var basePathContentFolder = $"{basePathContent}/{moduleName}-content";
        var basePathContentFolderSrc = $"{basePathContentFolder}/src";

        var vars = new Dictionary<string, object>
        {
            { "ModuleName", moduleName },
            { "ModuleNamePascal", moduleName.DashToPascalCase() }
        };

        codeGenerator
           .CreateFolder(basePathContentFolder)
           .CreateFileWithTemplate(
               $"{basePathContentFolder}/index.ts",
               $"{baseTemplatesPath}/Content/index.ts.txt",
               vars)

           .CreateFileWithTemplate(
               $"{basePathContentFolder}/ng-package.json",
               $"{baseTemplatesPath}/Content/ng-package.json.txt",
               vars)

           .CreateFileWithTemplate(
               $"{basePathContentFolder}/public-api.ts",
               $"{baseTemplatesPath}/Content/public-api.ts.txt",
               vars)

            .CreateFolder(basePathContentFolderSrc)
            .CreateFileWithTemplate(
                $"{basePathContentFolderSrc}/{moduleName}-content.module.ts",
                $"{baseTemplatesPath}/Content/content.module.ts.txt",
                vars)

            .CreateFileWithTemplate(
                $"{basePathContentFolderSrc}/{moduleName}-content.component.ts",
                $"{baseTemplatesPath}/Content/content.component.ts.txt",
                vars)

            .CreateFileWithTemplate(
                $"{basePathContentFolderSrc}/{moduleName}-content.component.html",
                $"{baseTemplatesPath}/Content/content.component.html.txt",
                vars)

            .CreateFileWithTemplate(
                $"{basePathContentFolderSrc}/{moduleName}-content.component.scss",
                $"{baseTemplatesPath}/Content/content.component.scss.txt",
                vars)

                ;
        return this;
    }


    public TaskRunner GetTaskRunner()
    {
        return codeGenerator.GetTaskRunner();
    }
}
