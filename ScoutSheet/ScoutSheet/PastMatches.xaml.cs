using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
			DisplayAlert("Refreshed!", "Refresh", "Ok");
			MatchesListView.IsRefreshing = false;
		}

		private void ToolbarItem_Clicked(object sender, EventArgs e)
		{
			using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
			{
				conn.DeleteAll<Matches>();
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
	}
}