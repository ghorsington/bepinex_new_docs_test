#addin nuget:?package=Cake.DocFx
#tool nuget:?package=docfx.console
#addin nuget:?package=Cake.DoInDirectory
#addin nuget:?package=Cake.Json
#addin nuget:?package=Newtonsoft.Json
#addin nuget:?package=Cake.FileHelpers

var target = Argument("target", "Build");
var buildVersion = Argument("build_version", RunGit("rev-parse --abbrev-ref HEAD"));

Information($"Version to build: {buildVersion}");

string RunGit(string command, string separator = "") 
{
    using(var process = StartAndReturnProcess("git", new ProcessSettings { Arguments = command, RedirectStandardOutput = true })) 
    {
        process.WaitForExit();
        return string.Join(separator, process.GetStandardOutput());
    }
}

void CleanDir(DirectoryPath path) 
{
    if(DirectoryExists(path))
        DeleteDirectory(path, new DeleteDirectorySettings { Force = true, Recursive = true });
}

Task("Cleanup")
    .Does(() => 
{
    DoInDirectory("..", () => 
    {
        CleanDir("_site");
    });
});

Task("CleanDeps")
    .Does(() =>
{
    DoInDirectory("..", () => 
    {
        CleanDir("src");
        CleanDir("common");
    });
});

Task("PullDeps")
    .Does(() =>
{
    DoInDirectory("..", () =>
    {
        if(!DirectoryExists("src"))
        {
            Information("Pulling BepInEx");
            CreateDirectory("src");
            RunGit("clone https://github.com/BepInEx/BepInEx.git src");

            DoInDirectory("src", () => 
            {
                if(buildVersion != "master")
                    RunGit($"checkout {buildVersion}");
                RunGit("submodule update --init --recursive");
                //NuGetRestore("BepInEx.sln");
            });
        }
    });
});

Task("LoadGHPages")
    .Does(() =>
{
    DoInDirectory("..", () => 
    {
        if (DirectoryExists("gh-pages"))
        {
            Information("Cleaning up previous worktree");
            RunGit("worktree remove gh-pages");
        }
        Information("Loading GH Pages as a worktree");
        RunGit("fetch");
        RunGit("worktree add gh-pages gh-pages");
    });
});

Task("GenDocs")
    .Does(() => 
{
    Information("Generating metadata");
    DocFxMetadata("./docfx.json");

    Information("Generating docs");
    DocFxBuild("./docfx.json", new DocFxBuildSettings {
        // GlobalMetadata = {
        //     ["_urlPrefix"] = urlPrefix
        // }
    });
});

Task("PublishGHPages")
    .IsDependentOn("Cleanup")
    .IsDependentOn("CleanDeps")
    .IsDependentOn("PullDeps")
    .IsDependentOn("LoadGHPages")
    .IsDependentOn("GenDocs")
    .Does(() => 
{
    var ghPages = Directory("gh-pages");
    var buildDir = ghPages + Directory(buildVersion);

    Information($"Copying docs to {buildDir}");
    CleanDir(buildDir);
    CreateDirectory(buildDir);
    CopyDirectory("_site", buildDir);

    var allTags = GetDirectories("gh-pages/*", new GlobberSettings {
        Predicate = d => !d.Hidden
    });

    Information($"Generating versions file");
    FileWriteText(ghPages + File("versions.json"),
        SerializeJsonPretty(new Dictionary<string, object> {
            ["versions"] = allTags.Select(d => new Dictionary<string, object> {
                ["name"] = d.GetDirectoryName(),
                ["tag"] = d.GetDirectoryName()
            })
        }));
});

Task("Build")
    .IsDependentOn("Cleanup")
    .IsDependentOn("PullDeps")
    .IsDependentOn("GenDocs");

Task("Serve")
    .Does(() =>
{
    DocFxServe("./_site");
});

Task("BuildServe")
    .IsDependentOn("Build")
    .IsDependentOn("Serve");

RunTarget(target);