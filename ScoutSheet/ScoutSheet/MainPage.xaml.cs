using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ScoutSheet
{
	// Learn more about making custom code visible in the Xamarin.Forms previewer
	// by visiting https://aka.ms/xamarinforms-previewer
	[DesignTimeVisible(false)]
	public partial class MainPage : TabbedPage
	{
		public MainPage()
		{
			InitializeComponent();
			var assembly = typeof(MainPage);
			Scouting.IconImageSource = ImageSource.FromResource("ScoutSheet.Assets.Icons.Scout.png");
			PastMatchesTab.IconImageSource = ImageSource.FromResource("ScoutSheet.Assets.Icons.Past Matches.png");
			SettingsTab.IconImageSource = ImageSource.FromResource("ScoutSheet.Assets.Icons.Settings.png");
		}
		private async void Reset_Clicked(object sender, EventArgs e)
		{
			//Some ConfirmationDialog that checks whetheer it's ok. Maybe an overloaded version of AlertDialog?
			if (await DisplayAlert("Are you sure?", "Would you really like to reset data? Unless you saved it, there is no way of retrieving the data!!!! Proceed with caution.", "Yes", "No")) //Somehow get the boolean out of option and true = yes, false = no... Seriously, it doesn't work atmm...
			{
				Scouting.ResetData();
			}
		}

		private void SaveData_Clicked(object sender, EventArgs e)
		{
			Matches ThingToBeStored = Scouting.RecordAllData();
			//Save into database or what????
		}

		private void Export_Clicked(object sender, EventArgs e)
		{
			//Exports the last thing stored in the database Match object. Export Multiple? I'm not sure....
		}
	}
}
