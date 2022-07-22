namespace Plantjes.DevUtils.Util; 

public class SpecialPaths {
    public static string SolutionDir
    {
        get
        {
            //find solution dir by going up until a sln is found
            var cd = Environment.CurrentDirectory;
            while (!Directory.GetFiles(cd).Any(x => x.EndsWith(".sln")) && cd.Length < 100) cd += "/..";
            return cd;
        }
    }
    public static string ProjectDir
    {
        get
        {
            //find project dir by going up until a csproj is found
            var cd = Environment.CurrentDirectory;
            while (!Directory.GetFiles(cd).Any(x => x.EndsWith(".csproj")) && cd.Length < 100) cd += "/..";
            return cd;
        }
    }

}