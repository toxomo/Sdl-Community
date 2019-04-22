using System.Diagnostics;
using System.Drawing;
using Sdl.Community.SignoffVerifySettings.Business.Helpers;
using Sdl.TellMe.ProviderApi;

namespace Sdl.Community.SignoffVerifySettings.TellMe
{
	public class CommunityWikiAction : AbstractTellMeAction
	{
		public override bool IsAvailable => true;
		public override string Category => Constants.CategoryName;
		public override Icon Icon => PluginResources.ForumIcon;

		public CommunityWikiAction()
		{
			Name = Constants.CommunityWikiName;
		}

		public override void Execute()
		{
			Process.Start(Constants.CommunityWikiLink);
		}
	}
}