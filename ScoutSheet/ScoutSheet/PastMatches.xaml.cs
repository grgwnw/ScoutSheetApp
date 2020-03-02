using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration;
using System.IO;
using CsvHelper;
using System.Globalization;
using Xamarin.Essentials;

namespace ScoutSheet
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PastMatches : ContentPage
	{
		public PastMatches()
		{
			InitializeComponent();
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
			{
				conn.CreateTable<Matches>();
				var match = conn.Table<Matches>().ToList();
				MatchesListView.ItemsSource = match;
			}
		}
		private void MatchesListView_Refreshing(object sender, EventArgs e)
		{
			using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation)) {
				MatchesListView.ItemsSource = conn.Table<Matches>().ToList();
			}
			MatchesListView.IsRefreshing = false;
		}

		private async void ToolbarItem_Clicked(object sender, EventArgs e)
		{
			using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
			{
				if (await DisplayAlert("Are you sure?", "Would you really like to delete ALL of the the matches? There is no way of retrieving the data!!!! Proceed with caution.", "Yes", "No")) //Somehow get the boolean out of option and true = yes, false = no... Seriously, it doesn't work atmm...
				{
					conn.DeleteAll<Matches>();
				}
				MatchesListView_Refreshing(sender, e);
			}
		}
		private void MatchesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var selectedMatch = MatchesListView.SelectedItem as Matches;
			if(selectedMatch != null)
			{
				Navigation.PushAsync(new PastMatchesDetailsPage(selectedMatch));
			}
			else
			{
				DisplayAlert("Error", "Something happened. Please see me...", "Ok!");
			}
		}

		private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
		{
			var records = new List<Matches>();
			using(SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
			{
				records = conn.Table<Matches>().ToList();
			}
			using (var writer = new StreamWriter(Path.Combine(App.folderPathSave,"Total.csv")))
			using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
			{
				csv.WriteRecords(records);
			}
			await Share.RequestAsync(new ShareFileRequest
			{
				Title = "Title",
				File = new ShareFile(Path.Combine(App.folderPathSave, "Total.csv")),
				PresentationSourceBounds = DeviceInfo.Platform == DevicePlatform.iOS && DeviceInfo.Idiom == DeviceIdiom.Tablet ? new System.Drawing.Rectangle(0, 20, 50, 40) : System.Drawing.Rectangle.Empty
			});
		}
	}
}