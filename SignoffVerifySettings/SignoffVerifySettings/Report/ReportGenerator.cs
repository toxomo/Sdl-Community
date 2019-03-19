using Sdl.Community.SignoffVerifySettings.Model;

namespace Sdl.Community.SignoffVerifySettings.Report
{
	public static class ReportGenerator
	{
		public static string CreateReport(ProjectInfoReportModel projectInfoReportModel)
		{
			var builder = new ReportBuilder();
			builder.BuildTotalTable(projectInfoReportModel);

			return builder.GetReport();
		}
	}
}
