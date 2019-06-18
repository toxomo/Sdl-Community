using System.Windows.Input;
using Microsoft.Win32;
using TmxTmDatabase;
using TmxTmDatabaseManager.Commands;

namespace TmxTmDatabaseManager.ViewModels
{
	public class MainViewModel
	{
		private ICommand _openDialogCommand;
		private TmxTmDb _tmxTmDb = new TmxTmDb();

		public MainViewModel()
		{
		}

		public ICommand OpenDialogCommand => _openDialogCommand ?? (_openDialogCommand = new CommandHandler(OpenDialog, true));

		private void OpenDialog()
		{
			var dlg = new OpenFileDialog();
			dlg.DefaultExt = ".tmx";
			dlg.Filter = "TMX Files (*.tmx)|*.tmx";
			var dialogResult = dlg.ShowDialog();

			if (dialogResult == true)
			{
				foreach (var file in dlg.FileNames)
				{
					_tmxTmDb.InsertFile(file);
				}
			}
		}
	}	
}