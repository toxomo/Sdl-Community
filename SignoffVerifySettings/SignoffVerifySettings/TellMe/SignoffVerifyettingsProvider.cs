using Sdl.Community.SignoffVerifySettings.Business.Helpers;
using Sdl.TellMe.ProviderApi;

namespace Sdl.Community.SignoffVerifySettings.TellMe
{
	[TellMeProvider]
	public class SignoffVerifyettingsProvider : ITellMeProvider
	{
		public string Name => Constants.CategoryName;

		public AbstractTellMeAction[] ProviderActions => new AbstractTellMeAction[]
		{
			new CommunityWikiAction
			{
				Keywords = new[] {Constants.SignoffVerifySettings, Constants.SvsCommunity, Constants.SvsSupport, Constants.SvsWiki }
			},
			new CommunityForumAction
			{
				Keywords = new[] { Constants.SignoffVerifySettings, Constants.SvsCommunity, Constants.SvsSupport, Constants.SvsForum }
			},
			new StoreAction
			{
				Keywords = new[] { Constants.SignoffVerifySettings, Constants.SvsStore, Constants.SvsDownload, Constants.SvsAppStore }
			}
		};
	}
}