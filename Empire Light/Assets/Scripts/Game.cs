using UnityEngine;
using System.Collections;

public static class Game
{
    private static string serverVersionLink = "";

    public const string version = "1.0.0";
    public const BuildType buildType = BuildType.Alpha;

    public static bool IsNewestVersion()
    {
        // TODO: Download newest version from server version link and compare with the current version
        return true;
    }
}

public enum BuildType
{
    Alpha,
    Beta,
    Stable
}
