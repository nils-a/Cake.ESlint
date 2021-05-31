#load nuget:?package=Cake.Recipe&version=2.1.0

Environment.SetVariableNames();

BuildParameters.SetParameters(
    context: Context,
    buildSystem: BuildSystem,
    sourceDirectoryPath: "./src",
    title: "Cake.ESLint",
    masterBranchName: "main",
    repositoryOwner: "cake-contrib",
    shouldRunDotNetCorePack: true,
    shouldUseDeterministicBuilds: true,
    preferredBuildProviderType: BuildProviderType.GitHubActions,
    preferredBuildAgentOperatingSystem: PlatformFamily.Windows);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(context: Context);

Build.RunDotNetCore();
