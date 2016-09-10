var target = Argument("target", "Default");

var outputDir = "./artifacts/";
var solutionPath = "./src/MassTransit.sln";

Task("Restore")
    .Does(() => {
        DotNetCoreRestore("src");
    });

Task("Build")
    .IsDependentOn("Restore")
    .Does(() => {
        MSBuild(solutionPath);
    });

Task("Default")
    .IsDependentOn("Build");

RunTarget(target);