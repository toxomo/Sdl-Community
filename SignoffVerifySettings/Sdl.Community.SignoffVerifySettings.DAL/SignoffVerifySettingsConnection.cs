using System.Data.Common;

namespace Sdl.Community.SignoffVerifySettings.DAL
{
	public static class SignoffVerifySettingsConnection
	{
		public static string _connectionString;
		static SignoffVerifySettingsConnection()
		{
			_connectionString = GetConnectionString();
		}

		public static string DbConnectionString
		{
			get { return _connectionString; }
		}

		private static string GetConnectionString()
		{
			var builder = new DbConnectionStringBuilder();

			// Local path connection string
			builder["Data Source"] = "cjsqlserver2016.development.sheffield.sdl.corp";
			builder["integrated security"] = "True";
			builder["initial catalog"] = "SignoffVerifySettings";
			builder["MultipleActiveResultSets"] = "True";
			builder["App"] = "EntityFramework";

			return builder.ConnectionString;
		}
	}
}