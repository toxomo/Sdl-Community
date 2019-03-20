using System.IO;
using System.Xml.Linq;
using Sdl.Community.SignoffVerifySettings.Helpers;
using Sdl.Community.SignoffVerifySettings.Model;

namespace Sdl.Community.SignoffVerifySettings.Report
{
	public class ReportBuilder
	{
		private readonly XElement _root = new XElement("Report");

		public string GetReport()
		{
			return _root.ToString();
		}

		public void BuildTotalTable(ProjectInfoReportModel projectInfoReportModel)
		{
			var parent = new XElement("ProjectInformation");
			BuildTable(parent, projectInfoReportModel);

			_root.Add(parent);
		}

		private void BuildTable(XElement parent, ProjectInfoReportModel projectInfoReportModel)
		{
			parent.Add(new XElement("Project", new XAttribute("Name", projectInfoReportModel.ProjectName)));
			parent.Add(new XElement("SourceLanguage", new XAttribute("DisplayName", projectInfoReportModel.SourceLanguage.DisplayName)));

			var targetLanguages = new XElement("TargetLanguages");
			parent.Add(targetLanguages);
			foreach(var targetLanguage in projectInfoReportModel.TargetLanguages)
			{
				targetLanguages.Add(new XElement("TargetLanguage", new XElement("DisplayName", targetLanguage.DisplayName)));
			}

			var runAtValue = !string.IsNullOrEmpty(projectInfoReportModel.RunAt) ? projectInfoReportModel.RunAt : Constants.NoVerificationRun;			
			parent.Add(new XElement("RunAt", runAtValue));
			
			var translationMemories = new XElement("TranslationMemories");
			parent.Add(translationMemories);
			if (projectInfoReportModel.TranslationMemories.Count == 0)
			{
				translationMemories.Add(new XElement("TranslationMemory", new XElement("Name", Constants.NoTranslationMemory)));
			}
			else
			{
				foreach (var translationMemory in projectInfoReportModel.TranslationMemories)
				{
					var fileName = Path.GetFileName(translationMemory.MainTranslationProvider.Uri.LocalPath);
					translationMemories.Add(new XElement("TranslationMemory", new XElement("Name", fileName)));
				}
			}

			var termbases = new XElement("Termbases");
			parent.Add(termbases);
			if (projectInfoReportModel.Termbases.Count == 0)
			{
				termbases.Add(new XElement("Termbase", new XElement("Name", Constants.NoTermbase)));
			}
			else
			{
				foreach (var termbase in projectInfoReportModel.Termbases)
				{
					translationMemories.Add(new XElement("Termbase", new XElement("Name", termbase.Name)));
				}
			}

			var regExRulesValue = !string.IsNullOrEmpty(projectInfoReportModel.RegexRules) ? Constants.RegExRulesApplied : Constants.NoRegExRules;
			parent.Add(new XElement("RegExRules", regExRulesValue));
			var checkRegExValue = !string.IsNullOrEmpty(projectInfoReportModel.CheckRegexRules) ? "true" : "false";
			parent.Add(new XElement("CheckRegEx", checkRegExValue));
			
			parent.Add(new XElement("QAChecker", projectInfoReportModel.QACheckerRanResult));
		}
	}
}