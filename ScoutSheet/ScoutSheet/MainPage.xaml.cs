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
		}
		private void Reset_Clicked(object sender, EventArgs e)
		{
			Debug.WriteLine(Scout.TestingThis);
		}

		private void SaveData_Clicked(object sender, EventArgs e)
		{

		}

		private void Export_Clicked(object sender, EventArgs e)
		{

		}
	}
}
