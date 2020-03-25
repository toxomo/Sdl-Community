﻿using System.Diagnostics;
using System.Drawing;
using Sdl.TellMe.ProviderApi;

namespace Sdl.Community.MTCloud.Provider.TellMe
{
	public class HelpAction: AbstractTellMeAction
	{
		public HelpAction()
		{
			Name = "SDL MachineTranslationCloud wiki in the SDL Community";
		}

		public override void Execute()
		{
			Process.Start("https://community.sdl.com/product-groups/translationproductivity/w/customer-experience/3356.sdl-beglobal-translation-provider");
		}

		public override bool IsAvailable => true;
		public override string Category => "SDLMachineTranslationCloud results";
		public override Icon Icon => PluginResources.Question;
	}
}
