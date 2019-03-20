using System.IO;
using System.Linq;
using System.Xml.Linq;
using Sdl.Community.SignoffVerifySettings.Helpers;
using Sdl.Community.SignoffVerifySettings.Model;
using Sdl.Core.Globalization;

namespace Sdl.Community.SignoffVerifySettings.Report
{
	public class ReportBuilder
	{
		private readonly XElement _root = new XElement("Report");

		public string GetReport()
		{
			return _root.ToString();
		}

		/// <summary>
		/// Build the .xml which is used to display information in the .xsl report 
		/// </summary>
		/// <param name="projectInfoReportModel"></param>
		public void BuildTotalReport(ProjectInfoReportModel projectInfoReportModel)
		{
			var parent = new XElement(Constants.ProjectInformation);
			BuildProjectInfoPart(parent, projectInfoReportModel);
			BuildFileInfoPart(parent, projectInfoReportModel);

			_root.Add(parent);
		}

		/// <summary>
		/// Generate the xml for the .xsl report which contains the project information
		/// </summary>
		/// <param name="parent">'ProjectInformation' node</param>
		/// <param name="projectInfoReportModel">projectInfoReportModel which contains info from .sdlproj</param>
		private void BuildProjectInfoPart(XElement parent, ProjectInfoReportModel projectInfoReportModel)
		{
			parent.Add(new XElement(Constants.Project, new XAttribute(Constants.Name, projectInfoReportModel.ProjectName)));
			parent.Add(new XElement(Constants.SourceLanguage, new XAttribute(Constants.DisplayName, projectInfoReportModel.SourceLanguage.DisplayName)));

			var targetLanguages = new XElement(Constants.TargetLanguages);
			parent.Add(targetLanguages);
			foreach (var targetLanguage in projectInfoReportModel.TargetLanguages)
			{
				targetLanguages.Add(new XElement(Constants.TargetLanguage, new XElement(Constants.DisplayName, targetLanguage.DisplayName)));
			}

			var runAtValue = !string.IsNullOrEmpty(projectInfoReportModel.RunAt) ? projectInfoReportModel.RunAt : Constants.NoVerificationRun;
			parent.Add(new XElement(Constants.RunAt, runAtValue));

			var translationMemories = new XElement(Constants.TranslationMemories);
			parent.Add(translationMemories);
			if (projectInfoReportModel.TranslationMemories.Count == 0)
			{
				translationMemories.Add(new XElement(Constants.TranslationMemory, new XElement(Constants.Name, Constants.NoTranslationMemory)));
			}
			else
			{
				foreach (var translationMemory in projectInfoReportModel.TranslationMemories)
				{
					var fileName = Path.GetFileName(translationMemory.MainTranslationProvider.Uri.LocalPath);
					translationMemories.Add(new XElement(Constants.TranslationMemory, new XElement(Constants.Name, fileName)));
				}
			}

			var termbases = new XElement(Constants.Termbases);
			parent.Add(termbases);
			if (projectInfoReportModel.Termbases.Count == 0)
			{
				termbases.Add(new XElement(Constants.Termbase, new XElement(Constants.Name, Constants.NoTermbase)));
			}
			else
			{
				foreach (var termbase in projectInfoReportModel.Termbases)
				{
					translationMemories.Add(new XElement(Constants.Termbase, new XElement(Constants.Name, termbase.Name)));
				}
			}

			var regExRulesValue = !string.IsNullOrEmpty(projectInfoReportModel.RegexRules) ? Constants.RegExRulesApplied : Constants.NoRegExRules;
			parent.Add(new XElement(Constants.RegExRules, regExRulesValue));
			var checkRegExValue = !string.IsNullOrEmpty(projectInfoReportModel.CheckRegexRules) ? Constants.True : Constants.False;
			parent.Add(new XElement(Constants.CheckRegEx, checkRegExValue));

			parent.Add(new XElement(Constants.QAChecker, projectInfoReportModel.QACheckerRanResult));
		}

		/// <summary>
		/// Generate the xml for the .xsl report which contains information for each target file
		/// </summary>
		/// <param name="parent">'ProjectInformation' node</param>
		/// <param name="projectInfoReportModel">projectInfoReportModel which contains info from .sdlproj</param>
		private void BuildFileInfoPart(XElement parent, ProjectInfoReportModel projectInfoReportModel)
		{
			var languageFiles = new XElement(Constants.LanguageFiles);
			parent.Add(languageFiles);
			foreach (var file in projectInfoReportModel.LanguageFileXmlNodeModels)
			{
				var assignedPahse = Constants.NoPhaseAssigned;
				var isCurrentAssignment = Constants.False;
				var assigneesNumber = Constants.NoUserAssigned;

				// To Do: for each phase which is assigned or has info on the target file, display information in the report
				// (it might exists 2 or more phases defined on the same file, and only one can be assigned)
				var phasesInfo = projectInfoReportModel.PhaseXmlNodeModels.Where(p => p.TargetFileGuid.Equals(file.LanguageFileGUID)).ToList();
				foreach(var phaseInfo in phasesInfo)
				{
					assignedPahse = phaseInfo.PhaseName;
					isCurrentAssignment = phaseInfo.IsCurrentAssignment;
					assigneesNumber = phaseInfo.AssigneesNumber.ToString();
				}				
				
				var targetLanguage = new Language(file.LanguageCode).DisplayName;
				var runAtValue = !string.IsNullOrEmpty(file.RunAt) ? file.RunAt : Constants.NoVerificationRun;
				languageFiles.Add(new XElement(Constants.LanguageFile),
									new XAttribute(Constants.Name, file.FileName),
									new XAttribute(Constants.TargetLanguage, targetLanguage),
									new XAttribute(Constants.RunAt, runAtValue),
									new XAttribute(Constants.AssignedPhase, assignedPahse),
									new XAttribute(Constants.IsCurrentAssignment, isCurrentAssignment),
									new XAttribute(Constants.AssigneesNumber, assigneesNumber));
			}
		}
	}
}