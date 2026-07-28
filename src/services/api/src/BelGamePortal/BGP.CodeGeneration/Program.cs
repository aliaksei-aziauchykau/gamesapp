using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using BGP.CodeGeneration.Commands.Ui;
using BGP.CodeGeneration.Commands.Server;
using BGP.CodeGeneration;
using BGP.CodeGeneration.Builder;
// See https://aka.ms/new-console-template for more information

var commands = new Dictionary<string, Func<IDictionary<string, string>, IRequest>> {
    { "ui:create-full-feature", (dic) => new UiCreateFullFeatureCommand {
            ModuleName = dic["bgpcg-module-name"],
            EntityName = dic["bgpcg-entity-name"],
            BaseFolderPath = dic["bgpcg-base-folder-path"],
            WithFeatureModule = dic["bgpcg-with-feature-module"].ToBool(),
            WithEntitiesModule = dic["bgpcg-with-entities-module"].ToBool(),
            WithApiModule = dic["bgpcg-with-api-module"].ToBool(),
            WithFetchersModule = dic["bgpcg-with-fetchers-module"].ToBool()
    }},
    { "ui:create-fetchers", (dic) => new UiCreateFetchersCommand {
            ModuleName = dic["bgpcg-module-name"],
            EntityName = dic["bgpcg-entity-name"],
            BaseFolderPath = dic["bgpcg-base-folder-path"]
    }},
    { "ui:create-entities", (dic) => new UiCreateEntitiesCommand {
            EntityName = dic["bgpcg-entity-name"],
            BaseFolderPath = dic["bgpcg-base-folder-path"]
    }},
    { "ui:create-api", (dic) => new UiCreateApiCommand {
            ModuleName = dic["bgpcg-module-name"],
            EntityName = dic["bgpcg-entity-name"],
            BaseFolderPath = dic["bgpcg-base-folder-path"]
    }},
    { "ui:create-store", (dic) => new UiCreateStoreCommand {
            ModuleName = dic["bgpcg-module-name"],
            BaseFolderPath = dic["bgpcg-base-folder-path"]
    }},
    { "ui:create-feature", (dic) => new UiCreateFeatureCommand {
            ModuleName = dic["bgpcg-module-name"],
            BaseFolderPath = dic["bgpcg-base-folder-path"]
    }},
    { "ui:create-page", (dic) => new UiCreatePageCommand {
            ModuleName = dic["bgpcg-module-name"],
    }},
    { "ui:create-empty-module", (dic) => new UiCreateEmptyModuleCommand {
            ModuleName = dic["bgpcg-module-name"],
            BaseFolderPath = dic["bgpcg-base-folder-path"]
    }},
    { "server:setup-project-example", (dic) => new ServerSetupProjectCommand {
    }},
};

var argsDic = ArgumentParser.ParseArguments(args);
Console.WriteLine(string.Join(" ", argsDic.Keys));

var services = new ServiceCollection();
services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly)); // Register MediatR

var provider = services.BuildServiceProvider();
var mediator = provider.GetRequiredService<IMediator>();

var projectName = argsDic["bgpcg-project"];
var commandName = argsDic["bgpcg-command"];

var commandKey = $"{projectName}:{commandName}";
var command = commands[commandKey](argsDic);

await mediator.Send(command);
