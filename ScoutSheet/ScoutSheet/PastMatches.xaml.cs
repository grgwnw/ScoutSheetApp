using SQLite;
using System;
using System.Collections.Generic;
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
			DisplayAlert("Refreshed!", "Refresh", "Ok");
		}

		private void MatchesListView_ItemTapped(object sender, ItemTappedEventArgs e)
		{
			DisplayAlert("What Object is this?",sender.ToString(), "Ok!"); //Apparently it gives you the listview and not the Match object itself. How do we differentiate which item is picked?
			DisplayAlert("What Object is this?", e.ToString(), "Ok!");
		}
	}
}