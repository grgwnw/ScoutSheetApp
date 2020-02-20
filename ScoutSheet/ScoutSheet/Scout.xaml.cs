using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ScoutSheet
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Scout : ContentPage
	{
        private Matches currentMatch = new Matches();
        private Color ButtonClickedColor = Color.Beige;
        public Scout()
	    {
            InitializeComponent();
			List<string> MatchTypeList = new List<string>();
			MatchTypeList.Add("Qualification");
			MatchTypeList.Add("Quaterfinals");
			MatchTypeList.Add("Semifinals");
			Match_Type.ItemsSource = MatchTypeList;
			var assembly = typeof(MainPage);
			PowerCellPhoto.Source = ImageSource.FromResource("ScoutSheet.Assets.Images.PowerCell.jpg", assembly);
			Field.Source = ImageSource.FromResource("ScoutSheet.Assets.Images.field.jpg", assembly);
            Field2.Source = ImageSource.FromResource("ScoutSheet.Assets.Images.field.jpg", assembly);
            Field3.Source = ImageSource.FromResource("ScoutSheet.Assets.Images.field.jpg", assembly);
            currentMatch.MatchNumberEntry = 0;
        }
        public void ResetData()
        {
            DisplayAlert("Successful!", "Your data has been cleared", "Confirm");
            //MANUALLY RESET EVERYTHING!!!!!!!!MWAHAHAHAHAHA!!!!!!
        }
		public Matches RecordAllData() { //RECORDS ALL DATA INTO PROPERTIES AND FIELDS SO MAINPAGE.XAML.CS CAN ACCESS AND CONVERT TO STRING.
            currentMatch.TeamNumber = TeamNumberEntry.Text;
            currentMatch.Scouters = ScouterEntry.Text;
            currentMatch.MatchNumberEntry = Int32.Parse(MatchNumber.Text);
            currentMatch.StartingGamePieces = Int32.Parse(PowerCellCount.Text.Substring(0, 1));
            currentMatch.CrossesInitiationLine = ((ILine.BackgroundColor) == ButtonClickedColor);
            currentMatch.ALowerScored = getParenthesisValue(ALow.Text);
            currentMatch.AOuterScored = getParenthesisValue(AOuter.Text);
            currentMatch.AInnerScored = getParenthesisValue(AInner.Text);
            currentMatch.AMissedBalls = getParenthesisValue(AMissed.Text);
            currentMatch.AComments = ACommentsEntry.Text;
            currentMatch.ABallsPickedUp = getParenthesisValue(APickedUp.Text);
            currentMatch.Defense = DefenseButton.BackgroundColor == ButtonClickedColor;
            currentMatch.TBallsFromLoadStation = getParenthesisValue(BallsFromLoadingStationTeleop.Text);
            currentMatch.FitsUnderTrench = UnderTrench.BackgroundColor == ButtonClickedColor;
            currentMatch.TLowerScored = getParenthesisValue(TLow.Text);
            currentMatch.TOuterScored = getParenthesisValue(TOuter.Text);
            currentMatch.TInnerScored = getParenthesisValue(TInner.Text);
            currentMatch.TMissedBalls = getParenthesisValue(TMissed.Text);
            currentMatch.TBallsFromFloor = getParenthesisValue(PickedUpT.Text);
            currentMatch.TComments = TeleopCommentsEntry.Text;
            currentMatch.EScore = getParenthesisValue(EScores.Text);
            currentMatch.EComments = EndgameCommentsEntry.Text;
            currentMatch.YellowCards = YellowCard.BackgroundColor == Color.Yellow;
            currentMatch.RedCards = RedCard.BackgroundColor == Color.Red;
            currentMatch.Rotations = Rotation.BackgroundColor == ButtonClickedColor;
            //Missing TShootingLocation and StartingLocation because of other buttons....
            //Missing Timers because hasn't been implemented yet
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
        private int getParenthesisValue(string value)
        {
            return Int32.Parse(Regex.Match(value, @"\d+").Value);
        }
        //This controls the ALow button for the ALowerScored Property
        private void ALow_Clicked(object sender, EventArgs e)
        {
            ALow.Text = "Low (" + (getParenthesisValue(((Button)sender).Text) + 1) + ")";
        }
        //AOuter Button with AOuterScored Property
        private void AOuter_Clicked(object sender, EventArgs e)
        {
            AOuter.Text = "Outer (" + (getParenthesisValue(((Button)sender).Text) + 1) + ")";
        }
        //AInner Button with AInnerScored Text
        private void AInner_Clicked(object sender, EventArgs e)
        {
            AInner.Text = "Inner (" + (getParenthesisValue(((Button)sender).Text) + 1) + ")";
        }
        //AMissed Button with AMissedBalls Property
        private void AMissed_Clicked(object sender, EventArgs e)
        {
            AMissed.Text = "Missed (" + (getParenthesisValue(((Button)sender).Text) + 1) + ")";
        }
        //APickedUp Button with ABallsPickedUp Property
        private void APickedUp_Clicked(object sender, EventArgs e)
        {
            APickedUp.Text = "Balls Picked Up (" + (getParenthesisValue(((Button)sender).Text) + 1) + ")";
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
            BallsFromLoadingStationTeleop.Text = "Balls Picked Up From Loading Station (" + (getParenthesisValue(((Button)sender).Text) + 1) + ")";
        }
        //ColorWheelColor Property. Do not instantiate this...
        private void ColorWheel_Clicked(object sender, EventArgs e)
        {
            if(((Button)sender).BackgroundColor == Color.Aqua)
            {
                ((Button)sender).BackgroundColor = Color.DeepPink;
                currentMatch.ColorWheelColor = "Deep Pink";
            }
            else if (((Button)sender).BackgroundColor == Color.DeepPink)
            {
                ((Button)sender).BackgroundColor = Color.Yellow;
                currentMatch.ColorWheelColor = "Yellow";
            }
            else if (((Button)sender).BackgroundColor == Color.Yellow)
            {
                ((Button)sender).BackgroundColor = Color.Aqua;
                currentMatch.ColorWheelColor = "Aqua";
            }
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
            ((Button)sender).Text = "Low (" + (getParenthesisValue(((Button)sender).Text) + 1) + ")";
        }

        //TOuter is button Name and TOuterScored is the property
        private void TOuter_Clicked(object sender, EventArgs e)
        {
            ((Button)sender).Text = "Outer (" + (getParenthesisValue(((Button)sender).Text) + 1) + ")";
        }
        //TInner is the button Name with TInnerScored
        private void TInner_Clicked(object sender, EventArgs e)
        {
            ((Button)sender).Text = "Inner (" + (getParenthesisValue(((Button)sender).Text) + 1) + ")";
        }
        //TMissed is button name and TMissedBalls is property
        private void TMissed_Clicked(object sender, EventArgs e)
        {
            ((Button)sender).Text = "Missed (" + (getParenthesisValue(((Button)sender).Text) + 1) + ")";
        }
        //PickedUpT is the button name and TBallsFromFloor is the property
        private void PickedUpT_Clicked(object sender, EventArgs e)
        {
            ((Button)sender).Text = "Balls Picked Up From The Floor (" + (getParenthesisValue(((Button)sender).Text) + 1) + ")";
        }
        //EScores is button name and EScore is the property
        private void EScores_Clicked(object sender, EventArgs e)
        {
            ((Button)sender).Text = "Balls Scored (" + (getParenthesisValue(((Button)sender).Text) + 1) + ")";
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
    }
}