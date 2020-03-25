﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Sdl.Community.BeGlobalV4.Provider.Helpers
{
	public static class Enums
	{
		public enum LoginOptions
		{
			[Display(Name ="API Credentials")]
			APICredentials = 0,

			[Display(Name = "Studio Authentication (via SDL ID)")]
			StudioAuthentication = 1
		}

		public static string GetDisplayName(this Enum enumValue)
		{
			return enumValue.GetType()
							.GetMember(enumValue.ToString())
							.First()
							.GetCustomAttribute<DisplayAttribute>()
							.GetName();
		}
	}
}