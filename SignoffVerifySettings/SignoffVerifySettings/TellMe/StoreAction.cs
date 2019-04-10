using System.Diagnostics;
using System.Drawing;
using Sdl.TellMe.ProviderApi;

namespace Sdl.Community.SignoffVerifySettings.TellMe
{
	public class StoreAction : AbstractTellMeAction
	{
		public override bool IsAvailable => true;
		public override string Category => "SignoffVerifySettings results";
		public override Icon Icon => PluginResources.Download;

		public StoreAction()
		{
			Name = "Download Signoff Verify Settings from AppStore";
		}

		public override void Execute()
		{
			// To Do: add the new app store link to download the SignoffVerifySettings app
			// Process.Start("");
		}
	}
}