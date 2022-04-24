namespace Appegy.GitHub.UnityVersionAction;

public class UnityVersion
{
    public string Version { get; }
    public string Changeset { get; }

    public UnityVersion(string version, string changeset)
    {
        Version = version;
        Changeset = changeset;
    }
}