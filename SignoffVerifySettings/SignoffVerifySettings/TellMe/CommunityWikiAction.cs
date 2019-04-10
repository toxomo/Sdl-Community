using System.Diagnostics;
using System.Drawing;
using Sdl.TellMe.ProviderApi;

namespace Sdl.Community.SignoffVerifySettings.TellMe
{
	public class CommunityWikiAction : AbstractTellMeAction
	{
		public override bool IsAvailable => true;
		public override string Category => "SignoffVerifySettings results";
		public override Icon Icon => PluginResources.ForumIcon;

		public CommunityWikiAction()
		{
			Name = "SDL Community Signoff Verify Settings plugin wiki";
		}

		public override void Execute()
		{
			Process.Start("https://community.sdl.com/product-groups/translationproductivity/w/customer-experience/4568.signoff-verify-settings");
		}
	}
}