using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
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
			Trench.Text = match.FitsUnderTrench;
			Defense.Text = match.Defense;
			Penalties.Text = match.Penalities;
			StartingGamePieces.Text = match.StartingGamePieces.ToString();
			StartingLocation.Text = match.StartingLocation;
			CrossesInitiationLine.Text = match.CrossesInitiationLine;
			ABallsPickedUp.Text = match.ABallsPickedUp.ToString();
			ALowerScored.Text = match.ALowerScored.ToString();
			AOuterScored.Text = match.AOuterScored.ToString();
			AInnerScored.Text = match.AInnerScored.ToString();
			AMissedBalls.Text = match.AMissedBalls.ToString();
			AComments.Text = match.AComments;
			TBallsFromLoadStation.Text = match.TBallsFromLoadStation.ToString();
			TBallsFromFloor.Text = match.TBallsFromFloor.ToString();
			TLowerScored.Text = match.TLowerScored.ToString();
			TOuterScored.Text = match.TOuterScored.ToString();
			TInnerScored.Text = match.TInnerScored.ToString();
			TMissedBalls.Text = match.TMissedBalls.ToString();
			TShootingLocation.Text = match.TShootingLocation;
			Rotations.Text = match.Rotations;
			ColorWheelColor.Text = match.ColorWheelColor;
			TComments.Text = match.TComments;
			EndLocation.Text = match.EndLocation;
			EScore.Text = match.EScore.ToString();
			InitialClimbHeight.Text = match.InitialClimbHeight;
			ClimbPosition.Text = match.ClimbPosition;
			ClimbTime.Text = match.ClimbTime.ToString();
			EComments.Text = match.EComments;
		}

		private async void ToolbarItem_Clicked(object sender, EventArgs e)
		{
			try
			{
				matchReference.TeamNumber = TeamNumber.Text;
				matchReference.MatchNumberEntry = Int32.Parse(MatchNumber.Text);
				matchReference.FitsUnderTrench = Trench.Text;
				matchReference.Scouters = Scouters.Text;
				matchReference.Defense = Defense.Text;
				matchReference.Penalities = Penalties.Text;
				matchReference.StartingGamePieces = Int32.Parse(StartingGamePieces.Text);
				matchReference.StartingLocation = StartingLocation.Text;
				matchReference.CrossesInitiationLine = CrossesInitiationLine.Text;
				matchReference.ABallsPickedUp = Int32.Parse(ABallsPickedUp.Text);
				matchReference.ALowerScored = Int32.Parse(ALowerScored.Text);
				matchReference.AOuterScored = Int32.Parse(AOuterScored.Text);
				matchReference.AInnerScored = Int32.Parse(AInnerScored.Text);
				matchReference.AMissedBalls = Int32.Parse(AMissedBalls.Text);
				matchReference.AComments = AComments.Text;
				matchReference.TBallsFromLoadStation = Int32.Parse(TBallsFromLoadStation.Text);
				matchReference.TBallsFromFloor = Int32.Parse(TBallsFromFloor.Text);
				matchReference.TLowerScored = Int32.Parse(TLowerScored.Text);
				matchReference.TOuterScored = Int32.Parse(TOuterScored.Text);
				matchReference.TInnerScored = Int32.Parse(TInnerScored.Text);
				matchReference.TMissedBalls = Int32.Parse(TMissedBalls.Text);
				matchReference.TShootingLocation = TShootingLocation.Text;
				matchReference.Rotations = Rotations.Text;
				matchReference.ColorWheelColor = ColorWheelColor.Text;
				matchReference.TComments = TComments.Text;
				matchReference.EndLocation = EndLocation.Text;
				matchReference.EScore = Int32.Parse(EScore.Text);
				matchReference.InitialClimbHeight = InitialClimbHeight.Text;
				matchReference.ClimbPosition = ClimbPosition.Text;
				matchReference.ClimbTime = ClimbTime.Text;
				matchReference.EComments = EComments.Text;
			}
			catch (FormatException)
			{
				await DisplayAlert("Not a number", "One of your data entries coult not be converted to a number. Please check again", "Ok");
			}
			using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
			{
				conn.CreateTable<Matches>();
				int rows = conn.InsertOrReplace(matchReference);
				await DisplayAlert("Saved!", "Match number " + matchReference.MatchNumberEntry + " was updated", "Ok!");
			}
			await Application.Current.MainPage.Navigation.PopAsync();
		}

		private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
		{
			using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
			{
				if (await DisplayAlert("Are you sure?", "Would you really like to delete the match? Unless you saved another copy, there is no way of retrieving the data!!!! Proceed with caution.", "Yes", "No")) //Somehow get the boolean out of option and true = yes, false = no... Seriously, it doesn't work atmm...
				{
					conn.CreateTable<Matches>();
					int rows = conn.Delete(matchReference);
					await DisplayAlert("Success", "Updated the match!", "Ok!");
					await Application.Current.MainPage.Navigation.PopAsync(); //Test out this? Make sure that you are able to remove the page...
					return;
				}
			}
			
		}

		private async void ToolbarItem_Clicked_2(object sender, EventArgs e)
		{
			matchReference.TeamNumber = TeamNumber.Text;
			matchReference.MatchNumberEntry = Int32.Parse(MatchNumber.Text);
			matchReference.FitsUnderTrench = Trench.Text;
			matchReference.Scouters = Scouters.Text;
			matchReference.Defense = Defense.Text;
			matchReference.Penalities = Penalties.Text;
			matchReference.StartingGamePieces = Int32.Parse(StartingGamePieces.Text);
			matchReference.StartingLocation = StartingLocation.Text;
			matchReference.CrossesInitiationLine = CrossesInitiationLine.Text;
			matchReference.ABallsPickedUp = Int32.Parse(ABallsPickedUp.Text);
			matchReference.ALowerScored = Int32.Parse(ALowerScored.Text);
			matchReference.AOuterScored = Int32.Parse(AOuterScored.Text);
			matchReference.AInnerScored = Int32.Parse(AInnerScored.Text);
			matchReference.AMissedBalls = Int32.Parse(AMissedBalls.Text);
			matchReference.AComments = AComments.Text;
			matchReference.TBallsFromLoadStation = Int32.Parse(TBallsFromLoadStation.Text);
			matchReference.TBallsFromFloor = Int32.Parse(TBallsFromFloor.Text);
			matchReference.TLowerScored = Int32.Parse(TLowerScored.Text);
			matchReference.TOuterScored = Int32.Parse(TOuterScored.Text);
			matchReference.TInnerScored = Int32.Parse(TInnerScored.Text);
			matchReference.TMissedBalls = Int32.Parse(TMissedBalls.Text);
			matchReference.TShootingLocation = TShootingLocation.Text;
			matchReference.Rotations = Rotations.Text;
			matchReference.ColorWheelColor = ColorWheelColor.Text;
			matchReference.TComments = TComments.Text;
			matchReference.EndLocation = EndLocation.Text;
			matchReference.EScore = Int32.Parse(EScore.Text);
			matchReference.InitialClimbHeight = InitialClimbHeight.Text;
			matchReference.ClimbPosition = ClimbPosition.Text;
			matchReference.ClimbTime = ClimbTime.Text;
			matchReference.EComments = EComments.Text;
			matchReference.SerializeCsv();
			await Share.RequestAsync(new ShareFileRequest
			{
				Title = Title,
				File = new ShareFile(Path.Combine(App.folderPathSave, matchReference.Scouters + matchReference.MatchNumberEntry + ".csv")),
				PresentationSourceBounds = DeviceInfo.Platform == DevicePlatform.iOS && DeviceInfo.Idiom == DeviceIdiom.Tablet? new System.Drawing.Rectangle(0, 20, 50, 40): System.Drawing.Rectangle.Empty
			});
		}
	}
}