using Sdl.Community.SignoffVerifySettings.Helpers;
using Sdl.Community.SignoffVerifySettings.Model;
using Sdl.Community.SignoffVerifySettings.Report;
using Sdl.Community.SignoffVerifySettings.Service;
using Sdl.FileTypeSupport.Framework.IntegrationApi;
using Sdl.ProjectAutomation.AutomaticTasks;
using Sdl.ProjectAutomation.Core;

namespace Sdl.Community.SignoffVerifySettings
{
	/// <summary>
	/// The following class is used when the project is not finalized and user wants to run the SignoffVerifySettings batch task
	/// </summary>
	[AutomaticTask("SignoffVerifySettings", "Signoff Verify Settings", "Signoff Verify Settings", GeneratedFileType = AutomaticTaskFileType.BilingualTarget)]
	[AutomaticTaskSupportedFileType(AutomaticTaskFileType.BilingualTarget)]
	class SignoffVerifySettingsBatchTask : AbstractFileContentProcessingAutomaticTask
	{
		private ProjectInfoReportModel _projectInfoReportModel = new ProjectInfoReportModel();

		protected override void OnInitializeTask()
		{
			if (TaskFiles.Length > 0)
			{
				var projectService = new ProjectService();
				_projectInfoReportModel = projectService.GetCurrentProjectInformation(TaskFiles);
			}
		}
		protected override void ConfigureConverter(ProjectFile projectFile, IMultiFileConverter multiFileConverter)
		{
			// no implementation needed
		}

		public override void TaskComplete()
		{
			base.TaskComplete();

			// generate report only when project information exists
			if (!string.IsNullOrEmpty(_projectInfoReportModel.ProjectName))
			{
				var report = ReportGenerator.CreateReport(_projectInfoReportModel);
				CreateReport(Constants.ReportName, "Verification statistics", report);
			}
		}		
	}

	/// <summary>
	/// The following class is used in case the project was finalized and the user wants to run the Signoff Verify Settings batch task
	/// </summary>
	[AutomaticTask("SignoffVerifySettingsFinalized",
		"Signoff Verify Settings (finalized project)",
		"Signoff Verify Settings for finalized project",
		GeneratedFileType = AutomaticTaskFileType.NativeTarget)]
	[AutomaticTaskSupportedFileType(AutomaticTaskFileType.NativeTarget)]
	class SignoffVerifySettingsFinalizedBatchTask : AbstractFileContentProcessingAutomaticTask
	{
		private ProjectInfoReportModel _projectInfoReportModel = new ProjectInfoReportModel();

		protected override void OnInitializeTask()
		{
			if (TaskFiles.Length > 0)
			{
				var projectService = new ProjectService();
				_projectInfoReportModel = projectService.GetCurrentProjectInformation(TaskFiles);
			}
		}
		protected override void ConfigureConverter(ProjectFile projectFile, IMultiFileConverter multiFileConverter)
		{
			// no implementation needed
		}

		public override void TaskComplete()
		{
			base.TaskComplete();

			// generate report only when project information exists
			if (!string.IsNullOrEmpty(_projectInfoReportModel.ProjectName))
			{
				var report = ReportGenerator.CreateReport(_projectInfoReportModel);
				CreateReport(Constants.ReportName, "Verification statistics", report);
			}
		}
	}
}