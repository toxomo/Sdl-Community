using System.Diagnostics;
using System.Drawing;
using Sdl.Community.SignoffVerifySettings.Business.Helpers;
using Sdl.TellMe.ProviderApi;

namespace Sdl.Community.SignoffVerifySettings.TellMe
{
	public class StoreAction : AbstractTellMeAction
	{
		public override bool IsAvailable => true;
		public override string Category => Constants.CategoryName;
		public override Icon Icon => PluginResources.Download;

		public StoreAction()
		{
			Name = Constants.AppStoreName;
		}

		public override void Execute()
		{
			// To Do: add the new app store link to download the SignoffVerifySettings app
			Process.Start(Constants.AppStoreLink);
		}
	}
}