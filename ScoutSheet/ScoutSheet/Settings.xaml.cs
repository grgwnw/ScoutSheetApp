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
	public partial class Settings : ContentPage
	{
		public Settings()
		{
			InitializeComponent();
			PressLength_Entry.Text = (App.PressLength).ToString();
		}

		private void ToolbarItem_Clicked(object sender, EventArgs e)
		{
			try
			{
				App.PressLength = Int32.Parse(PressLength_Entry.Text);
			}
			catch (Exception) { DisplayAlert("Exception", "Something went wrong. Perhaps you didn't type a number?", "Ok"); return; }
			DisplayAlert("", "Settings Saved!", "Confirm");
		}
	}
}