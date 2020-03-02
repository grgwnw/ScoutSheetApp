using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
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
		public void SetBarColor(Color color)
		{
			BarBackgroundColor = color;
		}
	}
}
