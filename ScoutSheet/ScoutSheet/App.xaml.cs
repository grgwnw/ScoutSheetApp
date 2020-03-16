using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ScoutSheet
{
	public partial class App : Application
	{
		public static string DatabaseLocation = string.Empty;
		public static string folderPathSave = string.Empty;
		public static int PressLength = 400;
		public App(string folderPath, string databaseStorage)
		{
			InitializeComponent();
			MainPage = new NavigationPage(new MainPage());
			folderPathSave = folderPath;
			DatabaseLocation = databaseStorage;
		}
		public static void ChangeColor(Color color)
		{
			((NavigationPage)Current.MainPage).BarBackgroundColor = color;
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
