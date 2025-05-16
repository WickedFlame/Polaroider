using System;
using System.IO;
using System.Linq;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main () => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;
    [GitRepository] readonly GitRepository GitRepository;

    [Parameter("Version to be injected in the Build")]
    public string Version { get; set; } = $"2.4.0";

    [Parameter("The Buildnumber provided by the CI")]
    public int BuildNo = 1;

    [Parameter("Is RC Version")]
    public bool IsRc = false;

    AbsolutePath SourceDirectory => RootDirectory / "src";

    AbsolutePath TestsDirectory => SourceDirectory / "tests";

    AbsolutePath DeployPath => (AbsolutePath)"C:" / "Projects" / "NuGet Store";

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(f => f.DeleteDirectory());
            TestsDirectory.GlobDirectories("**/bin", "**/obj").ForEach(f => f.DeleteDirectory());
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetVersion($"{Version}.{BuildNo}")
                .SetAssemblyVersion($"{Version}.{BuildNo}")
                .SetFileVersion(Version)
                .SetInformationalVersion($"{Version}.{BuildNo}")
                .AddProperty("PackageVersion", PackageVersion)
                .EnableNoRestore());
        });

    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTest(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetNoBuild(true)
                .EnableNoRestore());
        });

    Target Release => _ => _
        //.DependsOn(Clean)
        //.DependsOn(Compile)
        .DependsOn(Test);

    Target Deploy => _ => _
        .DependsOn(Release)
        .Executes(() =>
            {
                // copy to local store
                foreach (var file in Directory.GetFiles(RootDirectory, $"*.{PackageVersion}.nupkg", SearchOption.AllDirectories))
                {
                    ((AbsolutePath) file).CopyToDirectory(DeployPath, ExistsPolicy.FileOverwrite);
                }

                foreach (var file in Directory.GetFiles(RootDirectory, $"*.{PackageVersion}.snupkg", SearchOption.AllDirectories))
                {
                    ((AbsolutePath) file).CopyToDirectory(DeployPath, ExistsPolicy.FileOverwrite);
                }
            }
        );

    string PackageVersion
        => IsRc ? BuildNo < 10 ? $"{Version}-RC0{BuildNo}" : $"{Version}-RC{BuildNo}" : Version;

}
