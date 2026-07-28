using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using HandlebarsDotNet;

namespace BGP.CodeGeneration.Builder;

public class CodeGeneratorBuilder
{
    private TaskBuilder taskBuilder = new TaskBuilder();

    public static CodeGeneratorBuilder Create()
    {
        return new CodeGeneratorBuilder();
    }

    public CodeGeneratorBuilder CreateProject(string projectName, string outputDirectory = ".")
    {
        taskBuilder.AddAction(() => PowerShellExecutor.ExecuteScriptAsync("Scripts/create-project.ps1", new Dictionary<string, object>
        {
            { "ProjectName", projectName},
            { "Directory", outputDirectory }
        }));


        return this;
    }

    public CodeGeneratorBuilder AddProjectToSln(string projectName, string outputDirectory = ".")
    {
        taskBuilder.AddAction(() => PowerShellExecutor.ExecuteScriptAsync("Scripts/add-project-to-sln.ps1", new Dictionary<string, object>
        {
            { "ProjectName", projectName},
            { "Directory", outputDirectory }
        }));


        return this;
    }

    public CodeGeneratorBuilder CreateFolder(string path)
    {
        taskBuilder.AddAction(() => PowerShellExecutor.ExecuteScriptAsync("./Scripts/create-folder.ps1", new Dictionary<string, object>
        {
            { "Path", path }
        }));

        return this;
    }

    public CodeGeneratorBuilder CreateFile(string path)
    {
        taskBuilder.AddAction(() => PowerShellExecutor.ExecuteScriptAsync("./Scripts/create-file.ps1", new Dictionary<string, object>
        {
            { "Path", path }
        }));

        return this;
    }

    public CodeGeneratorBuilder PopulateWithTemplate(string pathToCreate, string pathToTemplate, Dictionary<string, object> templateContext)
    {
        taskBuilder.AddAction(async () =>
        {
            var content = await FileService.ReadFileAsync(pathToTemplate);
            var template = Handlebars.Compile(content);
            var result = template(templateContext);
            await FileService.WriteFileAsync(pathToCreate, result);
        });

        return this;
    }

    public CodeGeneratorBuilder CreateFileWithTemplate(string pathToCreate, string pathToTemplate, Dictionary<string, object> templateContext)
    {

        this.CreateFile(pathToCreate)
            .PopulateWithTemplate(pathToCreate, pathToTemplate, templateContext);

        return this;
    }

    public CodeGeneratorBuilder DeleteFile(string path)
    {
        taskBuilder.AddAction(() => PowerShellExecutor.ExecuteScriptAsync("./Scripts/delete-file.ps1", new Dictionary<string, object>
        {
            { "Path", path }
        }));

        return this;
    }

    public TaskRunner GetTaskRunner()
    {
        return taskBuilder.GetTaskRunner();
    }
}
