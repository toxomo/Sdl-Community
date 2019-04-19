using System.Xml;
using Sdl.Community.SignoffVerifySettings.Model;

namespace Sdl.Community.SignoffVerifySettings.Business.Model
{
	public class VerificationModel
	{
		public XmlElement SettingsBundleNode { get; set; }
		public LanguageDirectionModel LanguageDirectionModel { get; set; }
		public ProjectInfoReportModel ProjectInfoReportModel { get; set; }
	}
}