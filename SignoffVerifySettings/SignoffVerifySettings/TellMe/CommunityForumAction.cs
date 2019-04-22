using System.Diagnostics;
using System.Drawing;
using Sdl.Community.SignoffVerifySettings.Business.Helpers;
using Sdl.TellMe.ProviderApi;

namespace Sdl.Community.SignoffVerifySettings.TellMe
{
	public class CommunityForumAction : AbstractTellMeAction
	{
		public override bool IsAvailable => true;
		public override string Category => Constants.CategoryName;
		public override Icon Icon => PluginResources.ForumIcon;

		public CommunityForumAction()
		{
			Name = Constants.ForumName;
		}

		public override void Execute()
		{
			Process.Start(Constants.AppSupportLink);
		}
	}
}