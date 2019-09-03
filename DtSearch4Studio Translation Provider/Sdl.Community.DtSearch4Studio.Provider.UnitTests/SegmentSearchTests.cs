using Sdl.Community.DtSearch4Studio.Provider.Service;
using Xunit;

namespace Sdl.Community.DtSearch4Studio.Provider.UnitTests
{
	public class SegmentSearchTests
	{
		private readonly SearchService _searchService = new SearchService();

		[Theory]
		[InlineData("This is a test file")]
		public void GetWordsResults(string segment)
		{
			var indexPath = GetTestIndex();
			var results = _searchService.GetResults(indexPath, segment);
		    Assert.True(results.Count > 0);

			// check if it is possible based on the results to make also an assert.Equal("","");
			//Assert.Equal("","")

			// to do: remove the index file from the local machine
		}

		// write the IndexTestDoc4.zip to local machine and unarhive it so the tests can make use of it
		private string GetTestIndex()
		{
			var indexPath = string.Empty;
			//// add the localZipPath where the .zip will be written locally
			//var localZipPath = 
			//var _assemblies = new Dictionary<string, Assembly>();
			//var executingAssembly = Assembly.GetExecutingAssembly();
			//var resourceNames = executingAssembly.GetManifestResourceNames();
			//foreach (var resourceName in resourceNames)
			//{
			//	if (resourceName.EndsWith(".zip", StringComparison.InvariantCultureIgnoreCase))
			//	{
			//		using (var stream = executingAssembly.GetManifestResourceStream(resourceName))
			//		{
			//			var assemblyData = new byte[(int)stream.Length];
			//			stream.Read(assemblyData, 0, assemblyData.Length);
			//			using (var sw = new BinaryWriter(File.Open(localZipPath, FileMode.Create)))
			//			{
			//				sw.Write(assemblyData);
			//			}
			//		}
			//	}
			//}
			return indexPath;
		}
	}
}
