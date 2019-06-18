using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmxTmDatabase
{
    public class TmxTmDb
    {
		private string _url = "mongodb://localhost:27017";

		/// <summary>
		/// Insert TMX file to MongoDb TmxTmDatabase
		/// </summary>
		public void InsertFile(string filePath)
		{
			var client = new MongoDB.Driver.MongoClient(_url);
			// If TmxTmDatabase does not exists, it is created automatically
			var db = client.GetDatabase("TmxTmDatabase");
			var mongoGridFS = new MongoGridFS(db);

			//using (var fs = new FileStream(filePath, FileMode.Open))
			//{
			//	var gridFsInfo = db.
			//}
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
