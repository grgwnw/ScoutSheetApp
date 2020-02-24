using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ScoutSheet
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Scout : ContentPage
	{
        private Matches currentMatch = new Matches();
        private Color ButtonClickedColor = Color.Beige;
        private Stopwatch timeElapsedClimb = new Stopwatch();
        public Scout()
	    {
            InitializeComponent();
            List<string> MatchTypeList = new List<string>
            {
                "Qualification",
                "Quaterfinals",
                "Semifinals"
            };
            Match_Type.ItemsSource = MatchTypeList;
			var assembly = typeof(MainPage);
			PowerCellPhoto.Source = ImageSource.FromResource("ScoutSheet.Assets.Images.PowerCell.jpg", assembly);
			Field.Source = ImageSource.FromResource("ScoutSheet.Assets.Images.field.jpg", assembly);
            Field2.Source = ImageSource.FromResource("ScoutSheet.Assets.Images.field.jpg", assembly);
            Field3.Source = ImageSource.FromResource("ScoutSheet.Assets.Images.field.jpg", assembly);
            Bar_Thingy.Source = ImageSource.FromResource("ScoutSheet.Assets.Images.Climb Position.png", assembly);
            currentMatch.MatchNumberEntry = 0;
            LowInitClimb.Source = ImageSource.FromResource("ScoutSheet.Assets.Images.End Low.png", assembly);
            BallInitClimb.Source = ImageSource.FromResource("ScoutSheet.Assets.Images.End Level.png", assembly);
            HighInitClimb.Source = ImageSource.FromResource("ScoutSheet.Assets.Images.End High.png", assembly);
            None.BackgroundColor = ButtonClickedColor;
        }
        public void ResetData()
        {
            currentMatch = new Matches();
            TeamNumberEntry.Text = "";
            Match_Type.SelectedIndex = 0;
            PowerCellCount.Text = "0/3";
            ILine.BackgroundColor = Color.Default;
            StartingLeft.BackgroundColor = Color.Default;
            StartingMiddle.BackgroundColor = Color.Default;
            StartingRight.BackgroundColor = Color.Default;
            ALow.Text = "Low (0)";
            AOuter.Text = "Outer (0)";
            AInner.Text = "Inner (0)";
            AMissed.Text = "Missed (0)";
            APickedUp.Text = "Balls Picked Up (0)";
            ACommentsEntry.Text = "";
            DefenseButton.BackgroundColor = Color.Default;
            BallsFromLoadingStationTeleop.Text = "Balls Picked Up From Loading Station (0)";
            Rotation.BackgroundColor = Color.Default;
            ColorWheel.BackgroundColor = Color.Default;
            UnderTrench.BackgroundColor = Color.Default;
            PickedUpT.Text = "Balls Picked Up from Floor (0)";
            Trench.BackgroundColor = Color.Default;
            Target.BackgroundColor = Color.Default;
            Other.BackgroundColor = Color.Default;
            TLow.Text = "Low (0)";
            TOuter.Text = "Outer (0)";
            TInner.Text = "Inner (0)";
            TMissed.Text = "Missed (0)";
            TeleopCommentsEntry.Text = "";
            EScores.Text = "Balls Scored (0)";
            Park.BackgroundColor = Color.Default;
            Climb.BackgroundColor = Color.Default;
            None.BackgroundColor = Color.Default;
            Stopwatch.IsEnabled = true;
            Stopwatch.Text = "Start Stopwatch";
            LowInitClimb.BorderColor = Color.Default;
            BallInitClimb.BorderColor = Color.Default;
            HighInitClimb.BorderColor = Color.Default;
            EdgeLocation.BackgroundColor = Color.Default;
            CenterLocation.BackgroundColor = Color.Default;
            ChangeClimb(false);
            EndgameCommentsEntry.Text = "";
            YellowCard.BackgroundColor = Color.Default;
            RedCard.BackgroundColor = Color.Default;
            timeElapsedClimb = new Stopwatch();
            DisplayAlert("Successful!", "You cleared the data!", "Confirm");
        }
		public Matches RecordAllData() { //RECORDS ALL DATA INTO PROPERTIES AND FIELDS SO MAINPAGE.XAML.CS CAN ACCESS AND CONVERT TO STRING.
            currentMatch.TeamNumber = TeamNumberEntry.Text;
            currentMatch.Scouters = ScouterEntry.Text;
            currentMatch.MatchNumberEntry = Int32.Parse(MatchNumber.Text);
            currentMatch.StartingGamePieces = Int32.Parse(PowerCellCount.Text.Substring(0, 1));
            currentMatch.CrossesInitiationLine = ((ILine.BackgroundColor) == ButtonClickedColor)? "Yes":"No";
            currentMatch.ALowerScored = GetParenthesisValue(ALow.Text);
            currentMatch.AOuterScored = GetParenthesisValue(AOuter.Text);
            currentMatch.AInnerScored = GetParenthesisValue(AInner.Text);
            currentMatch.AMissedBalls = GetParenthesisValue(AMissed.Text);
            currentMatch.AComments = ACommentsEntry.Text;
            currentMatch.ABallsPickedUp = GetParenthesisValue(APickedUp.Text);
            currentMatch.Defense = DefenseButton.BackgroundColor == ButtonClickedColor? "Yes" : "No";
            currentMatch.TBallsFromLoadStation = GetParenthesisValue(BallsFromLoadingStationTeleop.Text);
            currentMatch.FitsUnderTrench = UnderTrench.BackgroundColor == ButtonClickedColor ? "Yes" : "No";
            currentMatch.TLowerScored = GetParenthesisValue(TLow.Text);
            currentMatch.TOuterScored = GetParenthesisValue(TOuter.Text);
            currentMatch.TInnerScored = GetParenthesisValue(TInner.Text);
            currentMatch.TMissedBalls = GetParenthesisValue(TMissed.Text);
            currentMatch.TBallsFromFloor = GetParenthesisValue(PickedUpT.Text);
            currentMatch.TComments = TeleopCommentsEntry.Text;
            currentMatch.EScore = GetParenthesisValue(EScores.Text);
            currentMatch.EComments = EndgameCommentsEntry.Text;
            currentMatch.Penalities = (YellowCard.BackgroundColor == Color.Yellow && RedCard.BackgroundColor == Color.Red)? "Yellow and Red": (YellowCard.BackgroundColor == Color.Yellow)? "Yellow" : (RedCard.BackgroundColor == Color.Red)? "Red" : "None";
            currentMatch.Rotations = Rotation.BackgroundColor == ButtonClickedColor ? "Yes" : "No";
            currentMatch.ClimbTime = "" + (timeElapsedClimb.ElapsedMilliseconds / 1000) + "." + (timeElapsedClimb.ElapsedMilliseconds % 1000);
            if (LowInitClimb.BorderColor == ButtonClickedColor) currentMatch.InitialClimbHeight = "Low";
            if (BallInitClimb.BorderColor == ButtonClickedColor) currentMatch.InitialClimbHeight = "Middle";
            if (HighInitClimb.BorderColor == ButtonClickedColor) currentMatch.InitialClimbHeight = "High";
            currentMatch.ColorWheelColor = ColorWheel.BackgroundColor == ButtonClickedColor ? "Yes" : "No";
            return currentMatch;
        }
        //Match Number Methods. MatchNumber.Text is the data value for MatchNumberEntry
		private void PlusMNumber_Clicked(object sender, EventArgs e)
		{
            try
            {
                int something = Int32.Parse(MatchNumber.Text);
                MatchNumber.Text = (something + 1).ToString();
            }
            catch (Exception) { DisplayAlert("Error","You have not entered a number in the match number. Please try again","Ok!"); }
		}

		private void MinusMNumber_Clicked(object sender, EventArgs e)
		{
            try
            {
                int something = Int32.Parse(MatchNumber.Text);
                MatchNumber.Text = (something - 1).ToString();
            }
            catch (Exception) { DisplayAlert("Error", "You have not entered a number in the match number. Please try again", "Ok!"); }
        }
        //Number of Power Cell Methods "0/3". PowerCellCount.Text.Substring(0,1) is the data point for StartingGamePieces
		private void AddPCellNumber_Clicked(object sender, EventArgs e)
		{
            int something = (Int32.Parse(PowerCellCount.Text.Substring(0, 1)) + 1 > 3)? 3: Int32.Parse(PowerCellCount.Text.Substring(0, 1)) + 1;
            PowerCellCount.Text = something + PowerCellCount.Text.Substring(1);
		}

		private void MinusPCellNumber_Clicked(object sender, EventArgs e)
		{
            int something = (Int32.Parse(PowerCellCount.Text.Substring(0, 1)) - 1 < 0) ? 0 : Int32.Parse(PowerCellCount.Text.Substring(0, 1)) - 1;
            PowerCellCount.Text = something + PowerCellCount.Text.Substring(1);
        }
        //Changes boolean CrossesInitiationLine
        private void ILine_Clicked(object sender, EventArgs e)
        {
            if (ILine.BackgroundColor == ButtonClickedColor)
            {
                ILine.BackgroundColor = Color.Default;
                return;
            }
            ILine.BackgroundColor = ButtonClickedColor;
        }
        //Maybe this one might just remain as is. Don't instantiate StartingLocation property
        private void Starting_Clicked(object sender, EventArgs e)
        {

            StartingLeft.BackgroundColor = Color.Default; //Color.Default apparently doens't work?
            StartingMiddle.BackgroundColor = Color.Default;
            StartingRight.BackgroundColor = Color.Default;
            currentMatch.StartingLocation = ((Button)sender).Text;
            ((Button)sender).BackgroundColor = ButtonClickedColor;
        }
        private int GetParenthesisValue(string value)
        {
            return Int32.Parse(Regex.Match(value, @"\d+").Value);
        }
        //This controls the ALow button for the ALowerScored Property
        private void ALow_Clicked(object sender, EventArgs e)
        {
            ALow.Text = "Low (" + (GetParenthesisValue(((Button)sender).Text) + 1) + ")";
        }
        //AOuter Button with AOuterScored Property
        private void AOuter_Clicked(object sender, EventArgs e)
        {
            AOuter.Text = "Outer (" + (GetParenthesisValue(((Button)sender).Text) + 1) + ")";
        }
        //AInner Button with AInnerScored Text
        private void AInner_Clicked(object sender, EventArgs e)
        {
            AInner.Text = "Inner (" + (GetParenthesisValue(((Button)sender).Text) + 1) + ")";
        }
        //AMissed Button with AMissedBalls Property
        private void AMissed_Clicked(object sender, EventArgs e)
        {
            AMissed.Text = "Missed (" + (GetParenthesisValue(((Button)sender).Text) + 1) + ")";
        }
        //APickedUp Button with ABallsPickedUp Property
        private void APickedUp_Clicked(object sender, EventArgs e)
        {
            APickedUp.Text = "Balls Picked Up (" + (GetParenthesisValue(((Button)sender).Text) + 1) + ")";
        }
        //DefenseButton Button with Defense Property
        private void Defense_Clicked(object sender, EventArgs e)
        {
            if (DefenseButton.BackgroundColor == ButtonClickedColor)
            {
                DefenseButton.BackgroundColor = Color.Default;
                return;
            }
            DefenseButton.BackgroundColor = ButtonClickedColor;
        }
        //BallsFromLoadingStationTeleop is button Name (longest button name ever...) and TBallsFromLoadStation is the property
        private void BallsFromLoadingStationTeleop_Clicked(object sender, EventArgs e)
        {
            BallsFromLoadingStationTeleop.Text = "Balls Picked Up From Loading Station (" + (GetParenthesisValue(((Button)sender).Text) + 1) + ")";
        }
        private void ColorWheel_Clicked(object sender, EventArgs e)
        {
            if (((Button)sender).BackgroundColor == ButtonClickedColor)
            {
                ((Button)sender).BackgroundColor = Color.Default;
                return;
            }
            ((Button)sender).BackgroundColor = ButtonClickedColor;
        }
        //UnderTrench is button name with FitsUnderButton property
        private void UnderTrench_Clicked(object sender, EventArgs e)
        {
            if (((Button)sender).BackgroundColor == ButtonClickedColor)
            {
                ((Button)sender).BackgroundColor = Color.Default;
                return;
            }
            ((Button)sender).BackgroundColor = ButtonClickedColor;
        }
        //TLow is the button Name and TLowerScored is the property
        private void TLow_Clicked(object sender, EventArgs e)
        {
            ((Button)sender).Text = "Low (" + (GetParenthesisValue(((Button)sender).Text) + 1) + ")";
        }
        //TOuter is button Name and TOuterScored is the property
        private void TOuter_Clicked(object sender, EventArgs e)
        {
            ((Button)sender).Text = "Outer (" + (GetParenthesisValue(((Button)sender).Text) + 1) + ")";
        }
        //TInner is the button Name with TInnerScored
        private void TInner_Clicked(object sender, EventArgs e)
        {
            ((Button)sender).Text = "Inner (" + (GetParenthesisValue(((Button)sender).Text) + 1) + ")";
        }
        //TMissed is button name and TMissedBalls is property
        private void TMissed_Clicked(object sender, EventArgs e)
        {
            ((Button)sender).Text = "Missed (" + (GetParenthesisValue(((Button)sender).Text) + 1) + ")";
        }
        //PickedUpT is the button name and TBallsFromFloor is the property
        private void PickedUpT_Clicked(object sender, EventArgs e)
        {
            ((Button)sender).Text = "Balls Picked Up From The Floor (" + (GetParenthesisValue(((Button)sender).Text) + 1) + ")";
        }
        //EScores is button name and EScore is the property
        private void EScores_Clicked(object sender, EventArgs e)
        {
            ((Button)sender).Text = "Balls Scored (" + (GetParenthesisValue(((Button)sender).Text) + 1) + ")";
        }
        //YellowCard is the button and YellowCards is the property
        private void YellowCard_Clicked(object sender, EventArgs e)
        {
            if (YellowCard.BackgroundColor == Color.Yellow)
            {
                YellowCard.BackgroundColor = Color.Default;
                return;
            }
            YellowCard.BackgroundColor = Color.Yellow;
        }
        //RedCard is button name and RedCards is the property
        private void RedCard_Clicked(object sender, EventArgs e)
        {
            if (RedCard.BackgroundColor == Color.Red)
            {
                RedCard.BackgroundColor = Color.Default;
                return;
            }
            RedCard.BackgroundColor = Color.Red;
        }
        //Rotation is the button name and Rotations is the property
        private void Rotation_Clicked(object sender, EventArgs e)
        {
            if (Rotation.BackgroundColor == ButtonClickedColor)
            {
                Rotation.BackgroundColor = Color.Default;
                return;
            }
            Rotation.BackgroundColor = ButtonClickedColor;
        }
        //Shooting is th button name and TShootingLocation is the property
        private void Shooting_Location(object sender, EventArgs e) 
        {
            Trench.BackgroundColor = Color.Default; //DEFAULT COLOR!!!!!!
            Target.BackgroundColor = Color.Default;
            Other.BackgroundColor = Color.Default;
            currentMatch.TShootingLocation = ((Button)sender).Text;
            ((Button)sender).BackgroundColor = ButtonClickedColor;
        }

        private void Stopwatch_Clicked(object sender, EventArgs e)
        {
            if(((Button)sender).Text == "Start Stopwatch"){
                ((Button)sender).Text = "Stop Stopwatch";
                timeElapsedClimb.Start();
                return;
            }
            if(((Button)sender).Text == "Stop Stopwatch")
            {
                ((Button)sender).IsEnabled = false;
                timeElapsedClimb.Stop();
            }
        }
        private void InitClimb_Clicked(object sender, EventArgs e)
        {
            LowInitClimb.BorderColor = Color.Gray;
            BallInitClimb.BorderColor = Color.Gray;
            HighInitClimb.BorderColor = Color.Gray;
            ((ImageButton)sender).BorderColor = ButtonClickedColor;
        }

        private void EndLocation_Clicked(object sender, EventArgs e)
        {
            Park.BackgroundColor = Color.Default;
            Climb.BackgroundColor = Color.Default;
            None.BackgroundColor = Color.Default;
            ((Button)sender).BackgroundColor = ButtonClickedColor;
            currentMatch.EndLocation = ((Button)sender).Text;
            if (((Button)sender).Text == "Climb") { ChangeClimb(true); }
            else { ChangeClimb(false); }
        }
        private void ChangeClimb(bool value)
        {
            InitialClimbHeightLabel.IsVisible = value;
            LowInitClimb.IsVisible = value;
            BallInitClimb.IsVisible = value;
            HighInitClimb.IsVisible = value;
            ClimbPositionTitle.IsVisible = value;
            EdgeLocation.IsVisible = value;
            MiddleBarLocation.IsVisible = value;
            CenterLocation.IsVisible = value;
            Bar_Thingy.IsVisible = value;
            Stopwatch.IsVisible = value;
        }

        private void ClimbLocation_Clicked(object sender, EventArgs e)
        {
            EdgeLocation.BackgroundColor = Color.Default;
            MiddleBarLocation.BackgroundColor = Color.Default;
            CenterLocation.BackgroundColor = Color.Default;
            currentMatch.ClimbPosition = ((Button)sender).Text;
            ((Button)sender).BackgroundColor = ButtonClickedColor;
        }
    }
}