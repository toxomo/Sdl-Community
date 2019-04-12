using System.Diagnostics;
using System.Drawing;
using Sdl.TellMe.ProviderApi;

namespace Sdl.Community.SignoffVerifySettings.TellMe
{
	public class CommunityForumAction : AbstractTellMeAction
	{
		public override bool IsAvailable => true;
		public override string Category => "SignoffVerifySettings results";
		public override Icon Icon => PluginResources.ForumIcon;

		public CommunityForumAction()
		{
			Name = "SDL Community AppStore Forum";
		}

		public override void Execute()
		{
			Process.Start("https://community.sdl.com/appsupport");
		}
	}
}