#addin nuget:?package=Cake.Npm&version=1.0.0
#r "..\..\src\Cake.ESLint\bin\Debug\netstandard2.0\Cake.ESLint.dll"

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("ensure-eslint-tool")
.Does(() => 
{
   var tools = Context.Configuration.GetToolPath(Context.Environment.WorkingDirectory, Context.Environment);
   CreateDirectory(tools);
   var settings = new NpmInstallSettings 
   {
      WorkingDirectory = tools
   };
   settings.Packages.Add("eslint");

   NpmInstall(settings);
});

Task("manual-installation")
.IsDependentOn("ensure-eslint-tool")
.Does(() => {
   ESLint(x => {
      x.ContinueOnErrors = true;
      x.AddDirectory("src1");
   });
});

Task("local-project")
.Does(() => {
   NpmInstall(new NpmInstallSettings {
      WorkingDirectory = "src2"
   });

   ESLint(x => {
      x.WorkingDirectory = "src2";
      x.Output = "../output.json"; // relative to WorkingDirectory
      x.OutputFormat = ESLintOutputFormat.Json;
      x.ContinueOnErrors = true;
      x.AddDirectory("."); // relative to WorkingDirectory
   });
});

Task("Default")
   .IsDependentOn("local-project")
   .IsDependentOn("manual-installation");

RunTarget(target);
