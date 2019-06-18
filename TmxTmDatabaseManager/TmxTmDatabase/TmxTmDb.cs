using System.IO;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace TmxTmDatabase
{
	public class TmxTmDb
    {
		private string _url = "mongodb://localhost:27017";
		public IGridFSBucket bucket;

		/// <summary>
		/// Insert TMX file to MongoDb TmxTmDatabase
		/// </summary>
		public async Task InsertFile(string filePath)
		{
			var client = new MongoClient(_url);
			// If TmxTmDatabase does not exists, it is created automatically
			var db = client.GetDatabase("TmxTmDatabase");

			var fileBytes = File.ReadAllBytes(filePath);
			var fileName = Path.GetFileNameWithoutExtension(filePath);
			var id = await bucket.UploadFromBytesAsync(fileName, fileBytes, null);
		}

		/// <summary>
		/// Remove TMX file from the MongoDb TmxTmDatabase
		/// </summary>
		public void RemoveFile(string filePath)
		{
			var client = new MongoClient(_url);
			// If TmxTmDatabase does not exists, it is created automatically
			var db = client.GetDatabase("TmxTmDatabase");
		}
	}
}