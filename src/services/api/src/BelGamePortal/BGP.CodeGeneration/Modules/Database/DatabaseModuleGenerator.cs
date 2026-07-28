using System.Collections.Generic;

namespace BGP.CodeGeneration.Builder;

public class DatabaseModuleGenerator
{
    private CodeGeneratorBuilder codeGenerator = new CodeGeneratorBuilder();
    private string domainName;

    public static DatabaseModuleGenerator Create()
    {
        return new DatabaseModuleGenerator();
    }

    public DatabaseModuleGenerator WithDomainName(string domainName)
    {
        this.domainName = domainName;
        return this;
    }

    public DatabaseModuleGenerator WithSetupProject()
    {
        var appName = $"BGP.{domainName}.Database";

        codeGenerator
            .CreateProject(appName)
            .DeleteFile($"{appName}/Class1.cs")

            .PopulateWithTemplate(
                $"{appName}/{appName}.csproj",
                "BGP.CodeGeneration/Modules/Database/Templates/Project.csproj.txt",
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
                    { "LayerName", "Database" }
                })

            .CreateFileWithTemplate(
                $"{appName}/{domainName}DatabaseModule.cs",
                "BGP.CodeGeneration/Modules/Database/Templates/DatabaseModule.cs.txt",
                new Dictionary<string, object>
                {
                    { "DomainName", domainName },
                })

            .AddProjectToSln(appName)
            ;

        return this;
    }

    public DatabaseModuleGenerator WithDbContext()
    {
        var appName = $"BGP.{domainName}.Database";

        codeGenerator.CreateFileWithTemplate(
            $"{appName}/DbContext.cs",
            "BGP.CodeGeneration/Modules/Database/Templates/DbContext.cs.txt",
            new Dictionary<string, object>
            {
                { "DomainName", domainName },
            });

        return this;
    }

    public DatabaseModuleGenerator WithDesignDbContextFactory()
    {
        var appName = $"BGP.{domainName}.Database";

        codeGenerator.CreateFileWithTemplate(
            $"{appName}/DesignTimeApplicationDbContextFactory.cs",
            "BGP.CodeGeneration/Modules/Database/Templates/DesignTimeApplicationDbContextFactory.cs.txt",
            new Dictionary<string, object>
            {
                { "DomainName", domainName },
            });

        return this;
    }

    public DatabaseModuleGenerator WithScripts()
    {
        var appName = $"BGP.{domainName}.Database";

        codeGenerator
            .CreateFolder($"{appName}/Scripts");

        codeGenerator.CreateFileWithTemplate(
            $"{appName}/Scripts/AddMigrations.ps1",
            "BGP.CodeGeneration/Modules/Database/Templates/Scripts/AddMigrations.ps1.txt",
            new Dictionary<string, object>
            {
                { "DomainName", domainName },
            });

        codeGenerator.CreateFileWithTemplate(
            $"{appName}/Scripts/GetMigrationList.ps1",
            "BGP.CodeGeneration/Modules/Database/Templates/Scripts/GetMigrationList.ps1.txt",
            new Dictionary<string, object>
            {
                { "DomainName", domainName },
            });

        codeGenerator.CreateFileWithTemplate(
            $"{appName}/Scripts/RevertAll.ps1",
            "BGP.CodeGeneration/Modules/Database/Templates/Scripts/RevertAll.ps1.txt",
            new Dictionary<string, object>
            {
                { "DomainName", domainName },
            });

        codeGenerator.CreateFileWithTemplate(
            $"{appName}/Scripts/UpdateDatabase.ps1",
            "BGP.CodeGeneration/Modules/Database/Templates/Scripts/UpdateDatabase.ps1.txt",
            new Dictionary<string, object>
            {
                { "DomainName", domainName },
            });

        codeGenerator.CreateFileWithTemplate(
            $"{appName}/Scripts/Variables.ps1.ps1",
            "BGP.CodeGeneration/Modules/Database/Templates/Scripts/Variables.ps1.txt",
            new Dictionary<string, object>
            {
                { "DomainName", domainName },
            });

        return this;
    }


    public DatabaseModuleGenerator WithDbContextInitializerAndSeeder()
    {
        var appName = $"BGP.{domainName}.Database";

        codeGenerator
            .CreateFolder($"{appName}/DbContextInitializer");

        codeGenerator.CreateFileWithTemplate(
            $"{appName}/DbContextInitializer/{domainName}DbContextInitializer.cs",
            "BGP.CodeGeneration/Modules/Database/Templates/DbContextInitializer/DbContextInitializer.cs.txt",
            new Dictionary<string, object>
            {
                { "DomainName", domainName },
            });

        codeGenerator.CreateFileWithTemplate(
            $"{appName}/DbContextInitializer/{domainName}DbContextSeeder.cs",
            "BGP.CodeGeneration/Modules/Database/Templates/DbContextInitializer/DbContextSeeder.cs.txt",
            new Dictionary<string, object>
            {
                { "DomainName", domainName },
            });

        return this;
    }

    public DatabaseModuleGenerator WithEntities(List<EntityParam> entityParams)
    {
        var appName = $"BGP.{domainName}.Database";

        codeGenerator
            .CreateFolder($"{appName}/Modules");

        foreach (var entityParam in entityParams)
        {
            codeGenerator
                .CreateFolder($"{appName}/Modules/{entityParam.EntityName}s");

            codeGenerator.CreateFileWithTemplate(
                $"{appName}/Modules/{entityParam.EntityName}s/{entityParam.EntityName}EntityTypeConfiguration.cs",
                "BGP.CodeGeneration/Modules/Database/Templates/Modules/EntityTypeConfiguration.cs.txt",
                new Dictionary<string, object>
                {
                    { "DomainName", domainName },
                    { "EntityName", entityParam.EntityName },
                });

            codeGenerator.CreateFileWithTemplate(
                $"{appName}/Modules/{entityParam.EntityName}s/{entityParam.EntityName}EntityContextSeeder.cs",
                "BGP.CodeGeneration/Modules/Database/Templates/Modules/EntityContextSeeder.cs.txt",
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


