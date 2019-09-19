using System;
using Sdl.Community.DtSearch4Studio.Provider.Service;
using Sdl.Desktop.IntegrationApi;
using Sdl.Desktop.IntegrationApi.Extensions;
using Sdl.TranslationStudioAutomation.IntegrationApi;
using Sdl.TranslationStudioAutomation.IntegrationApi.Presentation.DefaultLocations;

namespace Sdl.Community.DtSearch4Studio.Provider.Studio
{
	[RibbonGroup("dtSearch4StudiRibbon", Name = "dtSearch4Studio ribbon")]
	[RibbonGroupLayout(LocationByType = typeof(TranslationStudioDefaultViews.TradosStudioViewsLocation))]
	public class dtSearch4StudioProviderAction: AbstractRibbonGroup
	{
		private static EditorController GetEditorController()
		{
			return SdlTradosStudio.Application.GetController<EditorController>();
		}

		[Action("dtSearch4StudioSearchAction", Name = "Search using dtSearch", Icon = "dtSearch4Studio")]
		[ActionLayout(typeof(dtSearch4StudioProviderAction), 20, DisplayType.Large)]
		[ActionLayout(typeof(TranslationStudioDefaultContextMenus.EditorDocumentContextMenuLocation), 10, DisplayType.Large)]
		public class dtSearch4StudioAction : AbstractAction
		{
			private PersistenceService _persistenceService = new PersistenceService();

			// Search the selected word(s)/phrase using the dtSearch engine API
			protected override void Execute()
			{
				var editorController = GetEditorController();
				var activeDocument = editorController?.ActiveDocument;
				if (activeDocument != null)
				{
					var providerSettings = _persistenceService.GetProviderSettings();
					var currentSelection = activeDocument.Selection?.Current.ToString().TrimEnd() ?? string.Empty;
					var service = new SearchService();
					service.GetResults(providerSettings?.IndexPath, currentSelection);
				}
			}
		}
	}
}
