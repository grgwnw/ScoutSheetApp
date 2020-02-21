using Newtonsoft.Json;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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
			SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
			conn.CreateTable<Matches>();
			int rows = conn.Insert(Scouting.RecordAllData());
			conn.Close();
			if(rows > 0)
			{
				DisplayAlert("Successful", "Data Store is successful!", "Ok");
			}
			else
			{
				DisplayAlert("Error", "Something is wrong. Please contact me...", "Ok");
			}
			//Save into database or what????
		}

		private async void Export_Clicked(object sender, EventArgs e)
		{
			PermissionStatus status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

			if (status != PermissionStatus.Granted)
			{
				if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Storage))
				{
					await DisplayAlert("Need storage access", "Need storage access to export data", "OK");
				}

				status = (await CrossPermissions.Current.RequestPermissionsAsync(new Permission[] { Permission.Storage }))[Permission.Storage];
			}

			if (status == PermissionStatus.Granted)
			{
				var json = JsonConvert.SerializeObject(Scouting.RecordAllData());
				File.WriteAllText(App.DatabaseLocation, json);
				await DisplayAlert("Export successed", "Data exported.", "OK");
			}
			else
			{
				await DisplayAlert("Export failed", "Insufficient permissions to export data.", "OK");
			}
			//Exports the last thing stored in the database Match object. Export Multiple? I'm not sure....
		}
	}
}
