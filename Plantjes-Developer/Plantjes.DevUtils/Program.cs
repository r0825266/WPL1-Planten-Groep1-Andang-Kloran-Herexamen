using Plantjes.DevUtils.Util;

Console.WriteLine("--- Plantjes - DevUtils - (c) Xander Baes, 2022 --- ");
Console.WriteLine("==> Removing excludes and resources...");
//read csproj
var lines = File.ReadAllLines(SpecialPaths.SolutionDir + "/Plantjes/Plantjes.csproj").ToList();
//remove all lines that exclude files, because this usually causes confusion
lines = lines.Where(x => !x.Contains("None Remove=")).ToList();
//find first resource include, before removing them
var resline = lines.IndexOf(lines.First(x => x.Contains("Resource Include")));
//remove all resources
lines = lines.Where(x => !x.Contains("Resource Include=")).ToList();
Console.WriteLine("==> Rebuilding resources...");
//add all images as resources
lines.InsertRange(resline, Directory.GetFiles(SpecialPaths.SolutionDir + "/Plantjes/Image").Select(x => $"<Resource Include=\"Image/{new FileInfo(x).Name}\" />"));
Console.WriteLine("==> Removing empty item groups...");
//find all empty itemgroups
List<int> indexes = new();
for (int i = 0; i < lines.Count; i++) {
    if (lines[i].Trim() == "<ItemGroup>" && lines[i + 1].Trim() == "</ItemGroup>") indexes.AddRange(new int[] { i, i + 1 });
}
//remove all found empty item groups
foreach (var i in indexes.OrderByDescending(x => x)) {
    lines.RemoveAt(i);
}

//write csproj
File.WriteAllLines(SpecialPaths.SolutionDir + "/Plantjes/Plantjes.csproj", lines);

Console.WriteLine("Done!");