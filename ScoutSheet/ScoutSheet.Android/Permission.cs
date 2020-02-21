using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace ScoutSheet.Droid
{
	public class Permission
	{
		public bool requestPermission(string permission)
		{
			if(ContextCompat.CheckSelfPermission(Application.Context, permission) == (int)Android.Content.PM.Permission.Granted)
			{
				return true;
			}
			else
			{
				if(ActivityCompat.ShouldShowRequestPermissionRationale((Activity)Android.App.Application.Context, permission))
				{
					Log.Info("", "To save your excel/CSV/JSON file");
					var requiredPermissions = new String[] { permission };
					Snackbar.Make(View.FocusableAuto, Resource.String.,)
				}
			}
		}
	}
}