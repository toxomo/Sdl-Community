using System.Xml.Linq;
using Sdl.Community.SignoffVerifySettings.Model;

namespace Sdl.Community.SignoffVerifySettings.Report
{
	public class ReportBuilder
	{
		private readonly XElement root = new XElement("Report");

		public string GetReport()
		{
			return root.ToString();
		}

		public void BuildTotalTable(ProjectInfoReportModel projectInfoReportModel)
		{
			var parent = new XElement("ProjectInformation");
			BuildTable(parent, projectInfoReportModel);

			root.Add(parent);
		}

		private void BuildTable(XElement parent, ProjectInfoReportModel projectInfoReportModel)
		{
			parent.Add(new XElement("Project", projectInfoReportModel.ProjectName));
		}
	}
}