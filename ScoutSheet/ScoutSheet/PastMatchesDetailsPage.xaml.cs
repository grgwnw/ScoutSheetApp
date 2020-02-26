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
	public partial class PastMatchesDetailsPage : ContentPage
	{
		private Matches matchReference;
		public PastMatchesDetailsPage(Matches match)
		{
			InitializeComponent();
			matchReference = match;
			TeamNumber.Text = match.TeamNumber;
			MatchNumber.Text = match.MatchNumberEntry.ToString();
			Scouters.Text = match.Scouters;
		}

		private void ToolbarItem_Clicked(object sender, EventArgs e)
		{
			matchReference.TeamNumber = TeamNumber.Text;
			matchReference.MatchNumberEntry = Int32.Parse(MatchNumber.Text);
			matchReference.Scouters = Scouters.Text;
			using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
			{
				conn.CreateTable<Matches>();
				int rows = conn.InsertOrReplace(matchReference);
			}
		}

		private void ToolbarItem_Clicked_1(object sender, EventArgs e)
		{
			using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
			{
				conn.CreateTable<Matches>();
				int rows = conn.Delete(matchReference);
			}
		}
	}
}