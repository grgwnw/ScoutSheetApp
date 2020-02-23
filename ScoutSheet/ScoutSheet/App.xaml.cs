using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ScoutSheet
{
	public partial class App : Application
	{
		public static string DatabaseLocation = string.Empty;
		public static string folderPathSave = string.Empty;
		public App(string folderPath, string databaseStorage)
		{
			InitializeComponent();
			MainPage = new NavigationPage(new MainPage());
			folderPathSave = folderPath;
			DatabaseLocation = databaseStorage;
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
