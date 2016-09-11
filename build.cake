var target = Argument("target", "Default");

var outputDir = "./artifacts/";
var solutionPath = "./src/MassTransit.Netcore.sln";

Task("Restore")
    .Does(() => {
        DotNetCoreRestore("src");
    });

Task("Build")
    .IsDependentOn("Restore")
    .Does(() => {
        MSBuild(solutionPath);
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(() => {
        //DotNetCoreTest("./src/MassTransit.AutomatonymousIntegration.Tests");
        //DotNetCoreTest("./src/MassTransit.AzureServiceBusTransport.Tests");
        //DotNetCoreTest("./src/MassTransit.Futures.Tests");
        DotNetCoreTest("./src/MassTransit.QuartzIntegration.Tests");
        //DotNetCoreTest("./src/MassTransit.RabbitMqTransport.Tests");
        //DotNetCoreTest("./src/MassTransit.Reactive.Tests");
        //DotNetCoreTest("./src/MassTransit.Tests");
    });

Task("Default")
    .IsDependentOn("Test");

RunTarget(target);