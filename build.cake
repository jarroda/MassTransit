var target = Argument("target", "Default");

var outputDir = "./artifacts/";
var solutionPath = "./src/MassTransit.Netcore.sln";

var packages = new System.Collections.Generic.Dictionary<string, string> {
        // Containers
        {"MassTransit.StructureMapIntegration", "./src/MassTransit.StructureMapIntegration/project.json"},
        // Logging
        {"MassTransit.CoreLoggingIntegration", "./src/MassTransit.CoreLoggingIntegration/project.json"},
        {"MassTransit.Log4NetIntegration", "./src/MassTransit.Log4NetIntegration/project.json"},
        {"MassTransit.NLogIntegration", "./src/MassTransit.NLogIntegration/project.json"},
        {"MassTransit.SerilogIntegration", "./src/MassTransit.SerilogIntegration/project.json"},
        // Quartz
        {"MassTransit.QuartzIntegration", "./src/MassTransit.QuartzIntegration/project.json"},
        // Transports
        {"MassTransit.RabbitMqTransport", "./src/MassTransit.RabbitMqTransport/project.json"}
    };

Task("Clean")
    .Does(() => {
        if (DirectoryExists(outputDir))
        {
            DeleteDirectory(outputDir, recursive:true);
        }
        CreateDirectory(outputDir);
    });

Task("Restore")
    .Does(() => {
        DotNetCoreRestore("src");
    });

Task("Build")
    .IsDependentOn("Clean")
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

Task("Package")
    .IsDependentOn("Test")
    .Does(() => {
        foreach(var package in packages)
        {
            PackageProject(package.Key, package.Value);
        }
    });

Task("Default")
    .IsDependentOn("Package");

private void PackageProject(string projectName, string projectJsonPath)
{
    var settings = new DotNetCorePackSettings
        {
            OutputDirectory = outputDir,
            NoBuild = true
        };

    DotNetCorePack(projectJsonPath, settings);

    System.IO.File.WriteAllLines(outputDir + "artifacts", new[]{
        "nuget:" + projectName + "." + "1.0.0-core-1" + ".nupkg",
        "nugetSymbols:" + projectName + "." + "1.0.0-core-1" + ".symbols.nupkg"
    });
}    

RunTarget(target);