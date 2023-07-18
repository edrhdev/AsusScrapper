Console.Write("Input first BIOS version to find: ");
int firstVersion = int.Parse(Console.ReadLine() ?? throw new Exception("Input something valid"));

Console.Write("Input last BIOS version to find: ");
int lastVersion = int.Parse(Console.ReadLine() ?? throw new Exception("Input something valid"));

Console.Write("Begin at id: ");
int startIndex = int.Parse(Console.ReadLine() ?? throw new Exception("Input a valid number"));

Console.Write("End at id: ");
float endIndex = float.Parse(Console.ReadLine() ?? throw new Exception("Input a valid number"));

Console.Write("Batches: ");
int batches = int.Parse(Console.ReadLine() ?? throw new Exception("Input a valid number"));

using var httpClient = new HttpClient();

for (int currentVersion = firstVersion; currentVersion <= lastVersion; currentVersion++)
{
	int iterationsCount = 0;
	var totalIterations = endIndex - startIndex;

	var tasks = new List<Task>();
	var activeLinks = new List<string>();

	for (int i = startIndex; i <= endIndex; i++)
	{
		iterationsCount++;

		tasks.Add(TestUrl(i, activeLinks, currentVersion));

		if (i % batches == 0)
		{
			Console.WriteLine($"Currently at: #{i} ({iterationsCount / totalIterations * 100.0:0.00}%)");
			await Task.WhenAll(tasks);
			tasks.Clear();
		}
	}

	Console.WriteLine("\nMr. White, the batch is finished, results:\n");

	if (activeLinks.Count > 0)
		activeLinks.ForEach(Console.WriteLine);
	else
		Console.WriteLine($"Version {currentVersion} not found.");
}

Console.WriteLine("Press a key to exit");
Console.ReadKey();

async Task TestUrl(int i, List<string> activeLinks, int currentVersion)
{
	string url = $"https://dlcdnets.asus.com/pub/ASUS/GamingNB/Image/BIOS/{i}/G713QRAS{currentVersion}.zip";

	using var result = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, url));

	if (result.IsSuccessStatusCode)
		activeLinks.Add(url);
}