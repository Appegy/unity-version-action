using CommandLine;

namespace Appegy.GitHub.UnityVersionAction;

public class ActionInputs
{
    [Option("project_path",
        Required = true,
        HelpText = "Path to the project.")]
    public string ProjectPath { get; set; } = null!;

    [Option("version_env",
        Required = false,
        HelpText = "The name of environment variable to store the version. Keep empty if you don't want to store it.")]
    public string VersionEnv { get; set; } = "";

    [Option("changeset_env",
        Required = false,
        HelpText = "The name of environment variable to store the changeset. Keep empty if you don't want to store it.")]
    public string ChangesetEnv { get; set; } = "";
}