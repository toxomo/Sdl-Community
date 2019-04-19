using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Sdl.Community.SignoffVerifySettings.Business.Helpers;
using Sdl.Community.SignoffVerifySettings.Business.Model;
using Sdl.Community.SignoffVerifySettings.Helpers;
using Sdl.Community.SignoffVerifySettings.Model;
using Sdl.Core.Globalization;
using Sdl.ProjectAutomation.Core;
using Sdl.ProjectAutomation.FileBased;
using Sdl.TranslationStudioAutomation.IntegrationApi;

namespace Sdl.Community.SignoffVerifySettings.Service
{
	public class ProjectService
	{
		#region Private Fields
		private List<PhaseXmlNodeModel> _phaseXmlNodeModels = new List<PhaseXmlNodeModel>();
		private List<LanguageFileXmlNodeModel> _targetFiles = new List<LanguageFileXmlNodeModel>();
		private ProjectsController _projectsController = new ProjectsController();
		private XmlDocument _document = new XmlDocument();
		private readonly Utils _utils;
		#endregion

		#region Constructors
		public ProjectService()
		{
			_utils = new Utils();
			_projectsController = GetProjectController();
			if(_projectsController != null)
			{
				if(_projectsController.CurrentProject != null)
				{
					_document = _utils.LoadXmlDocument(_projectsController.CurrentProject.FilePath);
				}
			}			
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Get current project information which will be displayed in the Signoff Verify Settings report based on the selected files
		/// </summary>
		/// <param name="taskFiles">selected files from the project based on which the batch task is running</param>
		public ProjectInfoReportModel GetCurrentProjectInformation(ProjectFile[] taskFiles)
		{			
			var projectInfoReportModel = new ProjectInfoReportModel();
			var currentProject = GetProjectController().CurrentProject;
			if (currentProject != null)
			{
				var projectInfo = currentProject.GetProjectInfo();
				if (projectInfo != null)
				{
					// Project Name
					projectInfoReportModel.ProjectName = projectInfo.Name;
					projectInfoReportModel.ProjectId = projectInfo.Id;
					// Language Pairs
					projectInfoReportModel.SourceLanguage = projectInfo.SourceLanguage;
					projectInfoReportModel.TargetLanguages = projectInfo.TargetLanguages.ToList();
				}
				_targetFiles = GetTargetFilesSettings(currentProject, taskFiles);
				GetSettingsBundleInformation(currentProject);
				var runAt = GetQAVerificationInfo(projectInfo);

				projectInfoReportModel.StudioVersion = _utils.GetStudioVersion();
				projectInfoReportModel.PhaseXmlNodeModels = _phaseXmlNodeModels;
				projectInfoReportModel.LanguageFileXmlNodeModels = _targetFiles;
				projectInfoReportModel.RunAt = runAt;
				GetMaterialsInfo(currentProject, projectInfoReportModel);
				GetVerificationSettings(projectInfoReportModel);
			}			
			return projectInfoReportModel;
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Get project controller
		/// </summary>
		/// <returns>information for the Project Controller</returns>
		private ProjectsController GetProjectController()
		{
			return SdlTradosStudio.Application.GetController<ProjectsController>();
		}

		/// <summary>
		/// Get Target Language File tag attributes from the .sdlproj xml document 
		/// </summary>
		/// <param name="currentProject">current project selected</param>
		/// <param name="taskFiles">selected project files from where the information is retrieved</param>
		/// <returns></returns>
		private List<LanguageFileXmlNodeModel> GetTargetFilesSettings(FileBasedProject currentProject, ProjectFile[] taskFiles)
		{
			var langFileXMLNodeModels = new List<LanguageFileXmlNodeModel>();
			foreach (var taskFile in taskFiles)
			{
				var languageFileNode = _document.SelectSingleNode($"//LanguageFile[@Guid='{taskFile.Id}']");
				if (languageFileNode != null)
				{
					if (languageFileNode.Attributes.Count > 0)
					{
						var languageFileXmlNodeModel = new LanguageFileXmlNodeModel
						{
							LanguageFileGuid = languageFileNode.Attributes["Guid"].Value,
							SettingsBundleGuid = languageFileNode.Attributes["SettingsBundleGuid"].Value,
							LanguageCode = languageFileNode.Attributes["LanguageCode"].Value,
							FileName = Path.GetFileName(taskFile.LocalFilePath)
						};
						langFileXMLNodeModels.Add(languageFileXmlNodeModel);
					}
				}
			}
			var sourceLangauge = currentProject.GetProjectInfo() != null ? currentProject.GetProjectInfo().SourceLanguage != null
					? currentProject.GetProjectInfo().SourceLanguage.CultureInfo != null
					? currentProject.GetProjectInfo().SourceLanguage.CultureInfo.Name
					: string.Empty : string.Empty : string.Empty;
			_targetFiles = langFileXMLNodeModels.Where(l => l.LanguageCode != sourceLangauge).ToList();

			return _targetFiles;
		}

		/// <summary>
		/// Get Settings Bundle information for the target files from the .sdlproj xml document using the Settings Bundle Guids
		/// </summary>
		/// <param name="targetFiles">target files</param>
		/// <param name="currentProject">current project selected</param>
		private void GetSettingsBundleInformation(FileBasedProject currentProject)
		{
			var settings = currentProject.GetSettings();

			//foreach target file get the phase information
			foreach (var targetFile in _targetFiles)
			{
				var settingsBundles = _document.SelectSingleNode($"//SettingsBundle[@Guid='{targetFile.SettingsBundleGuid}']");
				if (settingsBundles != null)
				{
					foreach (XmlNode settingBundle in settingsBundles)
					{
						foreach (XmlNode childNode in settingBundle.ChildNodes)
						{
							if (childNode.Attributes.Count > 0)
							{
								_utils.GetPhaseInformation(Constants.ReviewPhase, childNode, targetFile, _phaseXmlNodeModels);
								_utils.GetPhaseInformation(Constants.TranslationPhase, childNode, targetFile, _phaseXmlNodeModels);
								_utils.GetPhaseInformation(Constants.PreparationPhase, childNode, targetFile, _phaseXmlNodeModels);
								_utils.GetPhaseInformation(Constants.FinalisationPhase, childNode, targetFile, _phaseXmlNodeModels);
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Get the QA Verification last DateTime (RunAt value) based on the report which is generated after the Verify Files batch task has ran.
		/// The information for each target file is stored in _targetFiles list and "RunAt" info for the project is returned by the method as string
		/// </summary>
		/// <param name="currentProject">current project selected</param>
		private string GetQAVerificationInfo(ProjectInfo projectInfo)
		{
			var directoryInfo = new DirectoryInfo($@"{projectInfo.LocalProjectFolder}\Reports\");
			if (!directoryInfo.Exists)
			{
				return string.Empty;
			}
			//get report info for each targetFile
			GetTargetReport(directoryInfo, projectInfo.SourceLanguage?.CultureInfo?.Name);

			// get "RunAt" info from the last "Verify Files" report which is generated after running the "Verify Files" batch task on all files
			var runAt = GetProjectReport(directoryInfo);
			return runAt;
		}

		/// <summary>
		/// Get report info for each targetFile
		/// </summary>
		/// <param name="directoryInfo"></param>
		/// <param name="sourceLanguage"></param>
		private void GetTargetReport(DirectoryInfo directoryInfo, string sourceLanguage)
		{
			foreach (var targetFile in _targetFiles)
			{
				var fileInfo = directoryInfo
					.GetFiles()
					.OrderByDescending(f => f.LastWriteTime)
					.FirstOrDefault(n => n.Name.StartsWith($@"Verify Files {sourceLanguage}_{targetFile.LanguageCode}"));

				if (fileInfo != null)
				{
					var reportPath = fileInfo.FullName;
					if (File.Exists(reportPath))
					{
						var doc = _utils.LoadXmlDocument(reportPath);
						var fileNodes = doc.GetElementsByTagName("file");
						foreach (XmlNode fileNode in fileNodes)
						{
							if (fileNode.Attributes.Count > 0)
							{
								// get the info only for those target files on which the 'Verify Files' batch task has been run.
								var reportFileGuid = fileNode.Attributes["guid"].Value;
								if (targetFile.LanguageFileGuid.Equals(reportFileGuid))
								{
									targetFile.FileName = fileNode.Attributes["name"].Value;
									targetFile.RunAt = _utils.GetRunAtValue(doc);
								}
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Get "RunAt" info from the last "Verify Files" report which is generated after running the "Verify Files" batch task on all files
		/// </summary>
		/// <param name="directoryInfo"></param>
		/// <returns>runAt value</returns>
		private string GetProjectReport(DirectoryInfo directoryInfo)
		{
			// get "RunAt" info from the last "Verify Files" report which is generated after running the "Verify Files" batch task on all files
			var allReportFilesInfo = directoryInfo.GetFiles()
				.OrderByDescending(f => f.LastWriteTime)
				.FirstOrDefault(n => n.Name.StartsWith("Verify Files (") || n.Name.StartsWith("Verify Files"));
			if (allReportFilesInfo != null)
			{
				var reportPath = allReportFilesInfo.FullName;
				if (File.Exists(reportPath))
				{
					var doc = _utils.LoadXmlDocument(reportPath);
					return _utils.GetRunAtValue(doc);
				}
			}
			return string.Empty;
		}

		/// <summary>
		/// Get the Materials information: Translation Memories, Termbases and if Regex expression have been used.
		/// Get also the QACheckerRanResult value which represents the QA Checker if has been run or not. 
		/// </summary>
		/// <param name="currentProject">current selected project</param>
		/// <param name="projectInfoReportModel">projectInfoReportModel used further to display the information into report</param>
		private void GetMaterialsInfo(FileBasedProject currentProject, ProjectInfoReportModel projectInfoReportModel)
		{
			// get CheckRexEx values
			projectInfoReportModel.CheckRegexRules = _document.SelectSingleNode($"//SettingsGroup/Setting[@Id='CheckRegEx']") != null
				? _document.SelectSingleNode($"//SettingsGroup/Setting[@Id='CheckRegEx']").FirstChild != null
				? _document.SelectSingleNode($"//SettingsGroup/Setting[@Id='CheckRegEx']").FirstChild.Value
				: string.Empty : string.Empty;

			// get translation memories & termbases used
			projectInfoReportModel.TranslationMemories = currentProject.GetTranslationProviderConfiguration().Entries;
			projectInfoReportModel.Termbases = currentProject.GetTermbaseConfiguration().Termbases;

			// the below value represent the QA Checker if has been run or not (if has been run, then the 'Verification Report' exists in .sdlproj)
			projectInfoReportModel.QACheckerRanResult = _document.SelectSingleNode($"//Reports/Report[@Name='Verification Report']") != null
				? Constants.QAChekerExecuted
				: Constants.NoQAChekerExecuted;
		}
		
		/// <summary>
		/// Get the Number Verifier settings (file name and the executed date time of Number Verifier app)
		/// Get the QA Verification settings applied to Language Pair level
		/// </summary>
		private void GetVerificationSettings(ProjectInfoReportModel projectInfoReportModel)
		{
			var verificationModel = new VerificationModel
			{
				ProjectInfoReportModel = projectInfoReportModel
			};

			var languageDirections = GetLanguageDirections();
			foreach(var targetFile in _targetFiles)
			{
				var fileLanguageDirection = languageDirections.FirstOrDefault(l => l.TargetLanguageCode.Equals(targetFile.LanguageCode));
				if (fileLanguageDirection != null)
				{
					var settingsBundleNode = _document.SelectSingleNode($"//SettingsBundle[@Guid='{fileLanguageDirection.SettingsBundleGuid}']");
					if (settingsBundleNode != null)
					{
						var settingsNode = settingsBundleNode["SettingsBundle"];
						verificationModel.SettingsBundleNode = settingsNode;
						verificationModel.LanguageDirectionModel = fileLanguageDirection;

						SetNumberVerifierSettings(verificationModel);					
						SetQAVerificationSettings(verificationModel, targetFile);
					}
				}
			}
		}

		/// <summary>
		/// Set the Number Verifier Settings
		/// </summary>
		/// <param name="verificationModel"></param>
		private void SetNumberVerifierSettings(VerificationModel verificationModel)
		{
			var numberVerifierModels = new List<NumberVerifierSettingsModel>();
			var numberVerSettingsGroupNode = verificationModel.SettingsBundleNode?.SelectSingleNode("SettingsGroup[@Id='NumberVerifierSettings']");
			if (numberVerSettingsGroupNode != null)
			{
				var targetFileSettingsNode = numberVerSettingsGroupNode.SelectSingleNode("Setting[@Id='TargetFileSettings']");
				if (targetFileSettingsNode != null)
				{
					// the FirstChild("ArrayOfTargetFileSetting") is taken because int the xml structure it will always exist only one
					// "ArrayOfTargetFileSetting" child node on the TargetFileSettings node, and the child node will contain the 'TargetFileSetting' nodes
					if (targetFileSettingsNode.FirstChild != null)
					{
						// iterate each TargetFileSetting node
						foreach (XmlElement targetFileChildNode in targetFileSettingsNode.FirstChild.ChildNodes)
						{
							var numberVerifierModel = new NumberVerifierSettingsModel();

							if (targetFileChildNode.ChildNodes != null)
							{
								// take the value for each child from the TargetFileSettig node
								foreach (XmlNode child in targetFileChildNode.ChildNodes)
								{
									SetNumberVerifierApp(child, numberVerifierModel);
								}
								numberVerifierModel.TargetLanguageCode = verificationModel.LanguageDirectionModel?.TargetLanguageCode;
								numberVerifierModels.Add(numberVerifierModel);
							}
						}
						verificationModel.ProjectInfoReportModel.NumberVerifierSettingsModels = numberVerifierModels;
					}
				}
			}
		}

		/// <summary>
		/// Set the number verifier model values with the ones retrieved from settings group
		/// </summary>
		/// <param name="child"></param>
		/// <param name="numberVerifierModel"></param>
		private void SetNumberVerifierApp(XmlNode child, NumberVerifierSettingsModel numberVerifierModel)
		{
			if (child.Name.Equals(Constants.FileName))
			{
				numberVerifierModel.FileName = child.InnerXml;
			}
			if (child.Name.Equals(Constants.ApplicationVersion))
			{
				numberVerifierModel.ApplicationVersion = child.InnerXml;
			}
			if (child.Name.Equals(Constants.ExecutedDateTime))
			{
				numberVerifierModel.ExecutedDateTime = child.InnerXml;
			}
		}

		/// <summary>
		/// Set the QA Verification Settings
		/// </summary>
		/// <param name="verificationModel"></param>
		/// <param name="targetFile"></param>
		private void SetQAVerificationSettings(VerificationModel verificationModel,	LanguageFileXmlNodeModel targetFile)
		{
			var qaVerificationSettingsModels = new List<QAVerificationSettingsModel>();
			var qaVerificationSettings = verificationModel.SettingsBundleNode.SelectSingleNode("SettingsGroup[@Id='QAVerificationSettings']");
			if (qaVerificationSettings != null)
			{
				var sourceLanguage = verificationModel.ProjectInfoReportModel.SourceLanguage.DisplayName;
				var targetLanguage = new Language(verificationModel.LanguageDirectionModel.TargetLanguageCode).DisplayName;

				foreach (XmlNode qaVerificationSetting in qaVerificationSettings)
				{
					var qaVerificationSettingsModel = new QAVerificationSettingsModel
					{
						Name = qaVerificationSetting.Attributes.Count > 0 ? qaVerificationSetting.Attributes["Id"].Value : string.Empty,
						Value = qaVerificationSetting.FirstChild != null ? qaVerificationSetting.FirstChild.Value : string.Empty,
						FileName = targetFile.FileName,
						LanguagePair = $"{sourceLanguage} - {targetLanguage}"
					};
					qaVerificationSettingsModels.Add(qaVerificationSettingsModel);
				}
				verificationModel.ProjectInfoReportModel.QAVerificationSettingsModels = qaVerificationSettingsModels;
			}
		}

		/// <summary>
		/// Get the language directions used to get the neccessary information for the NumberVerifierSettings and QAVerificationSettings groups
		/// </summary>
		/// <returns>list of LanguageDirectionModel</returns>
		private List<LanguageDirectionModel> GetLanguageDirections()
		{
			var languageDirections = new List<LanguageDirectionModel>();
			var languageDirectionsNodes = _document.SelectSingleNode($"//LanguageDirections");
			if (languageDirectionsNodes != null)
			{
				foreach (XmlNode childNode in languageDirectionsNodes.ChildNodes)
				{
					var languageDirectionModel = new LanguageDirectionModel
					{
						SettingsBundleGuid = childNode.Attributes["SettingsBundleGuid"].Value,
						TargetLanguageCode = childNode.Attributes["TargetLanguageCode"].Value,
					};
					languageDirections.Add(languageDirectionModel);
				}
			}
			return languageDirections;
		}
		#endregion
	}
}