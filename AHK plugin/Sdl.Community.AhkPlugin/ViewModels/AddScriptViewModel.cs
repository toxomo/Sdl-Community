﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sdl.Community.AhkPlugin.Ui;

namespace Sdl.Community.AhkPlugin.ViewModels
{
    public class AddScriptViewModel: ViewModelBase
    {
	    private readonly MainWindowViewModel _mainWindowViewModel;
		public AddScriptViewModel(MainWindowViewModel mainWindowViewModel)
		{
			_mainWindowViewModel = mainWindowViewModel;
		}
    }
}