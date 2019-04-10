using Sdl.TellMe.ProviderApi;

namespace Sdl.Community.SignoffVerifySettings.TellMe
{
	[TellMeProvider]
	public class SignoffVerifyettingsProvider : ITellMeProvider
	{
		public string Name => "Signoff Verify Settings";

		public AbstractTellMeAction[] ProviderActions => new AbstractTellMeAction[]
		{
			new CommunityWikiAction
			{
				Keywords = new[] {"signoff verify settings", "signoff verify settings community", "signoff verify settings support", "signoff verify settings wiki" }
			},
			new CommunityForumAction
			{
				Keywords = new[] { "signoff verify settings", "signoff verify settings community", "signoff verify settings support", "signoff verify settings forum" }
			},
			new StoreAction
			{
				Keywords = new[] { "signoff verify settings", "signoff verify settings store", "signoff verify settings download", "signoff verify settings appstore" }
			}
		};
	}
}