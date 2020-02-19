using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ScoutSheet
{
	public partial class App : Application
	{
		public static string DatabaseLocation = string.Empty;
		public App(string databaseStorage)
		{
			InitializeComponent();
			MainPage = new NavigationPage(new MainPage());
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
