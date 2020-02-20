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
        // All of the data that we need to keep track of
        public int MatchNumberEntry { get; set; } = 0; //AutoIncrement. Do we need to have this as a user option?
        public string TeamNumber { get; set; } //Team numbers but it doesn't go above 4 digits... So....
        public string Scouters { get; set; } // Consists of Scouter Names
        public int StartingGamePieces { get; set; } = 0; //Number of game pieces stored in the robot at the start of the match
        //Autonomous
        public bool CrossesInitiationLine { get; set; } //Pretty self explanitory for autonomous
        public string StartingLocation { get; set; }
        public int ALowerScored { get; set; } = 0;
        public int AOuterScored { get; set; } = 0;
        public int AInnerScored { get; set; } = 0;
        public int AMissedBalls { get; set; } = 0;
        public int ABallsPickedUp { get; set; }
        public string AComments { get; set; }
        //TeleOp 
        public bool Defense { get; set; }
        public int TBallsFromLoadStation { get; set; }
        public int TBallsFromFloor { get; set; }
        public bool FitsUnderTrench { get; set; }
        public int TLowerScored { get; set; }
        public int TOuterScored { get; set; }
        public int TInnerScored { get; set; }
        public int TMissedBalls { get; set; }
        public string TShootingLocation { get; set; }
        public string TComments { get; set; }
        public string ColorWheelColor { get; set; } = "Aqua";
        public bool Rotations { get; set; } = false;
        //Endgame
        public int EScore { get; set; }
        public bool Park { get; set; }
        public bool Climb { get; set; }
        //public Timer ClimbTime { get; set; }    Until we figure out what to do with the climb timer
        public string InitialClimbHeight { get; set; } //Low, Balanced, 
        public string ClimbPosition { get; set; } //Edge, Bar, 
        public string EComments { get; set; }
        public bool YellowCards { get; set; }
        public bool RedCards { get; set; }
        private Color ButtonClickedColor = Color.Beige;
        public Scout()
	    {
        InitializeComponent();
			List<string> MatchTypeList = new List<string>();
			MatchTypeList.Add("Qualification");
			MatchTypeList.Add("Quaterfinals");
			MatchTypeList.Add("Semifinals");
			MatchTypeList.Add("Finals");
			Match_Type.ItemsSource = MatchTypeList;
			var assembly = typeof(MainPage);
			PowerCellPhoto.Source = ImageSource.FromResource("ScoutSheet.Assets.Images.PowerCell.jpg", assembly);
			Field.Source = ImageSource.FromResource("ScoutSheet.Assets.Images.field.jpg", assembly);
            Field2.Source = ImageSource.FromResource("ScoutSheet.Assets.Images.field.jpg", assembly);
            Field3.Source = ImageSource.FromResource("ScoutSheet.Assets.Images.field.jpg", assembly);
            PowerCellCount.Text = "0/3";
        }
		public void RecordAllData() { //RECORDS ALL DATA INTO PROPERTIES AND FIELDS SO MAINPAGE.XAML.CS CAN ACCESS AND CONVERT TO STRING.
		
		}
		private void PlusMNumber_Clicked(object sender, EventArgs e)
		{
            try
            {
                int something = Int32.Parse(MatchNumber.Text);
                MatchNumber.Text = (something + 1).ToString();
                MatchNumberEntry++;
            }
            catch (Exception) { DisplayAlert("Error","You have not entered a number in the match number. Please try again","Ok!"); }
		}

		private void MinusMNumber_Clicked(object sender, EventArgs e)
		{
            try
            {
                int something = Int32.Parse(MatchNumber.Text);
                MatchNumber.Text = (something - 1).ToString();
                MatchNumberEntry--;
            }
            catch (Exception) { DisplayAlert("Error", "You have not entered a number in the match number. Please try again", "Ok!"); }
        }

		private void AddPCellNumber_Clicked(object sender, EventArgs e)
		{
            int something = (Int32.Parse(PowerCellCount.Text.Substring(0, 1)) + 1 > 3)? 3: Int32.Parse(PowerCellCount.Text.Substring(0, 1)) + 1;
            PowerCellCount.Text = something + PowerCellCount.Text.Substring(1);
            StartingGamePieces++;
		}

		private void MinusPCellNumber_Clicked(object sender, EventArgs e)
		{
            int something = (Int32.Parse(PowerCellCount.Text.Substring(0, 1)) - 1 < 0) ? 0 : Int32.Parse(PowerCellCount.Text.Substring(0, 1)) - 1;
            PowerCellCount.Text = something + PowerCellCount.Text.Substring(1);
            StartingGamePieces--;
        }

        private void ILine_Clicked(object sender, EventArgs e)
        {
            if (ILine.BackgroundColor == ButtonClickedColor)
            {
                ILine.BackgroundColor = Color.Default;
                CrossesInitiationLine = false;
                return;
            }
            ILine.BackgroundColor = ButtonClickedColor;
            CrossesInitiationLine = true;
        }

        private void Starting_Clicked(object sender, EventArgs e)
        {

            StartingLeft.BackgroundColor = Color.Default; //Color.Default apparently doens't work?
            StartingMiddle.BackgroundColor = Color.Default;
            StartingRight.BackgroundColor = Color.Default;
            StartingLocation = ((Button)sender).Text;
            ((Button)sender).BackgroundColor = ButtonClickedColor;
        }
        private int getParenthesisValue(string value)
        {
            return Int32.Parse(Regex.Match(value, @"\d+").Value);
        }
        private void ALow_Clicked(object sender, EventArgs e)
        {
            ALowerScored = getParenthesisValue(((Button)sender).Text) + 1;
            ALow.Text = "Low (" + ALowerScored + ")";
        }

        private void AOuter_Clicked(object sender, EventArgs e)
        {
            AOuterScored = getParenthesisValue(((Button)sender).Text) + 1;
            AOuter.Text = "Outer (" + AOuterScored + ")";
        }

        private void AInner_Clicked(object sender, EventArgs e)
        {
            AInnerScored = getParenthesisValue(((Button)sender).Text) + 1;
            AInner.Text = "Inner (" + AInnerScored + ")";
        }

        private void AMissed_Clicked(object sender, EventArgs e)
        {
            AMissedBalls = getParenthesisValue(((Button)sender).Text) + 1;
            AMissed.Text = "Missed (" + AMissedBalls + ")";
        }
        private void APickedUp_Clicked(object sender, EventArgs e)
        {
            ABallsPickedUp = getParenthesisValue(((Button)sender).Text) + 1;
            APickedUp.Text = "Balls Picked Up (" + ABallsPickedUp + ")";
        }
        private void Defense_Clicked(object sender, EventArgs e)
        {
            if (DefenseButton.BackgroundColor == ButtonClickedColor)
            {
                DefenseButton.BackgroundColor = Color.Default;
                Defense = false;
                return;
            }
            DefenseButton.BackgroundColor = ButtonClickedColor;
            Defense = true;
        }

        private void BallsFromLoadingStationTeleop_Clicked(object sender, EventArgs e)
        {
            TBallsFromLoadStation = getParenthesisValue(((Button)sender).Text) + 1;
            BallsFromLoadingStationTeleop.Text = "Balls Picked Up From Loading Station (" + TBallsFromLoadStation + ")";
        }

        private void ColorWheel_Clicked(object sender, EventArgs e)
        {
            if(((Button)sender).BackgroundColor == Color.Aqua)
            {
                ((Button)sender).BackgroundColor = Color.DeepPink;
                ColorWheelColor = "Deep Pink";
            }
            else if (((Button)sender).BackgroundColor == Color.DeepPink)
            {
                ((Button)sender).BackgroundColor = Color.Yellow;
                ColorWheelColor = "Yellow";
            }
            else if (((Button)sender).BackgroundColor == Color.Yellow)
            {
                ((Button)sender).BackgroundColor = Color.Aqua;
                ColorWheelColor = "Aqua";
            }
        }
        private void UnderTrench_Clicked(object sender, EventArgs e)
        {
            if (((Button)sender).BackgroundColor == ButtonClickedColor)
            {
                ((Button)sender).BackgroundColor = Color.Default;
                FitsUnderTrench = false;
                return;
            }
            ((Button)sender).BackgroundColor = ButtonClickedColor;
            FitsUnderTrench = true;
        }

        private void TLow_Clicked(object sender, EventArgs e)
        {
            TLowerScored = getParenthesisValue(((Button)sender).Text) + 1;
            ((Button)sender).Text = "Low (" + TLowerScored + ")";
        }

        private void TOuter_Clicked(object sender, EventArgs e)
        {
            TOuterScored = getParenthesisValue(((Button)sender).Text) + 1;
            ((Button)sender).Text = "Outer (" + TOuterScored + ")";
        }

        private void TInner_Clicked(object sender, EventArgs e)
        {
            TInnerScored = getParenthesisValue(((Button)sender).Text) + 1;
            ((Button)sender).Text = "Inner (" + TInnerScored + ")";
        }

        private void TMissed_Clicked(object sender, EventArgs e)
        {
            TMissedBalls = getParenthesisValue(((Button)sender).Text) + 1;
            ((Button)sender).Text = "Missed (" + TMissedBalls + ")";
        }

        private void PickedUpT_Clicked(object sender, EventArgs e)
        {
            TBallsFromFloor = getParenthesisValue(((Button)sender).Text) + 1;
            ((Button)sender).Text = "Balls Picked Up From The Floor (" + TBallsFromFloor + ")";
        }

        private void EScores_Clicked(object sender, EventArgs e)
        {
            EScore = getParenthesisValue(((Button)sender).Text) + 1;
            ((Button)sender).Text = "Balls Scored (" + EScore + ")";
        }

        private void YellowCard_Clicked(object sender, EventArgs e)
        {
            if (YellowCard.BackgroundColor == Color.Yellow)
            {
                YellowCard.BackgroundColor = Color.Default;
                YellowCards = false;
                return;
            }
            YellowCard.BackgroundColor = Color.Yellow;
            YellowCards = true;
        }

        private void RedCard_Clicked(object sender, EventArgs e)
        {
            if (RedCard.BackgroundColor == Color.Red)
            {
                RedCard.BackgroundColor = Color.Default;
                RedCards = false;
                return;
            }
            RedCard.BackgroundColor = Color.Red;
            RedCards = true;
        }

        private void Rotation_Clicked(object sender, EventArgs e)
        {
            if (Rotation.BackgroundColor == ButtonClickedColor)
            {
                Rotation.BackgroundColor = Color.Default;
                Rotations = false;
                return;
            }
            Rotation.BackgroundColor = ButtonClickedColor;
            Rotations = true;
        }
        private void Shooting_Location(object sender, EventArgs e) //Doesn't Seem to work....
        {
            Trench.BackgroundColor = Color.Default;
            Target.BackgroundColor = Color.Default;
            Other.BackgroundColor = Color.Default;
            TShootingLocation = ((Button)sender).Text;
            ((Button)sender).BackgroundColor = ButtonClickedColor;
        }
    }
}