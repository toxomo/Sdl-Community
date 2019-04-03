using System.Linq;
using Sdl.Community.SignoffVerifySettings.Model;

namespace Sdl.Community.SignoffVerifySettings.DAL
{
	public class SignoffVerifySettingsRepository
	{
		public void SaveProjectInformation(ProjectInfoReportModel projectInfoReportModel)
		{
			using (var dbContext = new SignoffVerifySettingsEntities())
			{
				var project = dbContext.Projects.Where(p => p.ProjectId.Equals(projectInfoReportModel.ProjectId)).FirstOrDefault();
				if(project != null)
				{
					// update exiting project 
				}
				else
				{
					// create new project
					var newProject = new Project();
					newProject.ProjectId = projectInfoReportModel.ProjectId;
					newProject.ProjectName = projectInfoReportModel.ProjectName;
					newProject.QACheckerRanResult = projectInfoReportModel.QACheckerRanResult;
					newProject.StudioVersion = projectInfoReportModel.StudioVersion;
					newProject.RunAt = project.RunAt;
					newProject.CheckRegExRules = projectInfoReportModel.CheckRegexRules;
					// other properties values to be set
					
				
				//dbContext.SaveChanges();
				}
			}
		}
	}
}