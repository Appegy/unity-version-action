using System.Text.RegularExpressions;
using Appegy.GitHub.UnityVersionAction;
using CommandLine;

var input = parseArguments(args);
if (input == null)
{
    Environment.Exit(1);
    return;
}

var version = findUnityVersion(input.ProjectPath);
if (version == null)
{
    Environment.Exit(1);
    return;
}

Console.WriteLine($"{nameof(version.Version)}: {version.Version}");
Console.WriteLine($"{nameof(version.Changeset)}: {version.Changeset}");
Console.WriteLine($"::set-output name=version::{version.Version}");
Console.WriteLine($"::set-output name=changeset::{version.Changeset}");

if (!string.IsNullOrEmpty(input.VersionEnv))
{
    Console.WriteLine($"Setting environment variable {input.VersionEnv} to {version.Version}");
    Console.WriteLine($"::exportVariable name={input.VersionEnv}::{version.Version}");
}
if (!string.IsNullOrEmpty(input.ChangesetEnv))
{
    //exportVariable
    Console.WriteLine($"Setting environment variable {input.ChangesetEnv} to {version.Changeset}");
    Console.WriteLine($"::exportVariable name={input.ChangesetEnv}::{version.Changeset}");
}

static ActionInputs? parseArguments(IEnumerable<string> args)
{
    ActionInputs? inputs = null;
    Parser.Default.ParseArguments(() => new ActionInputs(), args)
        .WithNotParsed(
            errors =>
            {
                Console.Error.WriteLine(string.Join(Environment.NewLine, errors.Select(error => error.ToString())));
                inputs = null;
            })
        .WithParsed(result =>
        {
            Console.WriteLine($"Project path: {result.ProjectPath}");
            var versionEnv = string.IsNullOrEmpty(result.VersionEnv) ? "Not requested" : result.VersionEnv;
            var changesetEnv = string.IsNullOrEmpty(result.ChangesetEnv) ? "Not requested" : result.ChangesetEnv;
            Console.WriteLine($"Version Env: {versionEnv}");
            Console.WriteLine($"Changeset Env: {changesetEnv}");
            inputs = result;
        });

    return inputs;
}

static UnityVersion? findUnityVersion(string projectPath)
{
    var path = Path.Combine(projectPath, "ProjectSettings", "ProjectVersion.txt");
    if (!File.Exists(path))
    {
        Console.Error.WriteLine($"Could not find {path}");
        return null;
    }
    var file = File.ReadAllText(path);
    var match = Regex.Match(file, @"m_EditorVersionWithRevision: (.+) \((.+)\)");
    if (match.Success)
    {
        return new UnityVersion(match.Groups[1].Value, match.Groups[2].Value);
    }
    match = Regex.Match(file, @"m_EditorVersion: (.+) \((.+)\)");
    if (match.Success)
    {
        return new UnityVersion(match.Groups[1].Value, string.Empty);
    }
    Console.Error.WriteLine($"Could not find version in {path}");
    return null;
}