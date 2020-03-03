﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ScoutSheet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Scout : ContentPage
    {
        private Matches currentMatch = new Matches();
        private Color ButtonClickedColor = Color.Orange;
        private Color DefaultColor = (Device.RuntimePlatform == Device.Android) ? Color.FromRgb(214, 215, 215) : Color.White;
        private Stopwatch timeElapsedClimb = new Stopwatch();
        public Entry TeamNumberEntry = new Entry();
        private Button LastClickedButton = null;
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
            Image PowerCellPhoto = new Image();
            Entry scouter = new Entry();
            TeamNumberEntry.Placeholder = "Enter Team Number Here";
            TeamNumberEntry.Margin = new Thickness(110, 0, 0, 0);
            MatchInfo.Children.Add(TeamNumberEntry, 0, 3, 0, 1);
            scouter.Placeholder = "Scouters";
            scouter.Margin = new Thickness(80, 0, 0, 0);
            MatchInfo.Children.Add(scouter, 0, 3, 3, 4);
            PowerCellPhoto.Source = ImageSource.FromResource("ScoutSheet.Assets.Images.PowerCell.jpg", assembly);
            Field.Source = ImageSource.FromResource("ScoutSheet.Assets.Images.field.jpg", assembly);
            Field2.Source = ImageSource.FromResource("ScoutSheet.Assets.Images.field.jpg", assembly);
            Bar_Thingy.Source = ImageSource.FromResource("ScoutSheet.Assets.Images.Climb Position.png", assembly);
            currentMatch.MatchNumberEntry = 0;
            LowInitClimb.Source = ImageSource.FromResource("ScoutSheet.Assets.Images.End Low.png", assembly);
            BallInitClimb.Source = ImageSource.FromResource("ScoutSheet.Assets.Images.End Level.png", assembly);
            HighInitClimb.Source = ImageSource.FromResource("ScoutSheet.Assets.Images.End High.png", assembly);
            None.BackgroundColor = ButtonClickedColor;
            PowerCellPhoto.Aspect = Aspect.AspectFit;
            MatchInfo.Children.Add(PowerCellPhoto, 4, 7, 0, 4); //Some stuff goes here, but I eat dinner first...Then come back.... GTG then back
            ILine.BackgroundColor = DefaultColor;
            StartingLeft.BackgroundColor = DefaultColor;
            StartingMiddle.BackgroundColor = DefaultColor;
            StartingRight.BackgroundColor = DefaultColor;
            ALow.BackgroundColor = DefaultColor;
            AOuter.BackgroundColor = DefaultColor;
            AInner.BackgroundColor= DefaultColor;
            AMissed.BackgroundColor = DefaultColor;
            APickedUp.BackgroundColor = DefaultColor;
            DefenseButton.BackgroundColor = DefaultColor;
            BallsFromLoadingStationTeleop.BackgroundColor = DefaultColor;
            RotationButton.BackgroundColor = DefaultColor;
            ColorWheel.BackgroundColor = DefaultColor;
            UnderTrench.BackgroundColor = DefaultColor;
            PickedUpT.BackgroundColor = DefaultColor;
            Trench.BackgroundColor = DefaultColor;
            Target.BackgroundColor = DefaultColor;
            Other.BackgroundColor = DefaultColor;
            TLow.BackgroundColor = DefaultColor;
            TOuter.BackgroundColor = DefaultColor;
            TInner.BackgroundColor = DefaultColor;
            TMissed.BackgroundColor = DefaultColor;
            Park.BackgroundColor = DefaultColor;
            Climb.BackgroundColor = DefaultColor;
            None.BackgroundColor = DefaultColor;
            EScores.BackgroundColor = DefaultColor;
            LowInitClimb.BorderColor = DefaultColor;
            BallInitClimb.BorderColor = DefaultColor;
            HighInitClimb.BorderColor = DefaultColor;
            EdgeLocation.BackgroundColor = DefaultColor;
            CenterLocation.BackgroundColor = DefaultColor;
            YellowCard.BackgroundColor = DefaultColor;
            RedCard.BackgroundColor = DefaultColor;
            MiddleBarLocation.BackgroundColor = DefaultColor;
            CenterLocation.BackgroundColor = DefaultColor;
            EdgeLocation.BackgroundColor = DefaultColor;
            ChangeClimb(true);
            
        }
        public void ResetData()
        {
            currentMatch = new Matches();
            TeamNumberEntry.Text = "";
            Match_Type.SelectedIndex = 0;
            PowerCellCount.Text = "0/3";
            ILine.BackgroundColor = DefaultColor;
            StartingLeft.BackgroundColor = DefaultColor;
            StartingMiddle.BackgroundColor = DefaultColor;
            StartingRight.BackgroundColor = DefaultColor;
            ALow.Text = "Low (0)";
            AOuter.Text = "Outer (0)";
            AInner.Text = "Inner (0)";
            AMissed.Text = "Missed (0)";
            APickedUp.Text = "Balls Picked Up (0)";
            ACommentsEntry.Text = "";
            DefenseButton.BackgroundColor = DefaultColor;
            BallsFromLoadingStationTeleop.Text = "Balls Picked Up From Loading Station (0)";
            RotationButton.BackgroundColor = DefaultColor;
            ColorWheel.BackgroundColor = DefaultColor;
            UnderTrench.BackgroundColor = DefaultColor;
            PickedUpT.Text = "Balls Picked Up from Floor (0)";
            Trench.BackgroundColor = DefaultColor;
            Target.BackgroundColor = DefaultColor;
            Other.BackgroundColor = DefaultColor;
            TLow.Text = "Low (0)";
            TOuter.Text = "Outer (0)";
            TInner.Text = "Inner (0)";
            TMissed.Text = "Missed (0)";
            TeleopCommentsEntry.Text = "";
            EScores.Text = "Balls Scored (0)";
            Park.BackgroundColor = DefaultColor;
            Climb.BackgroundColor = DefaultColor;
            None.BackgroundColor = DefaultColor;
            Stopwatch.IsEnabled = true;
            Stopwatch.Text = "Start Stopwatch";
            LowInitClimb.BorderColor = DefaultColor;
            BallInitClimb.BorderColor = DefaultColor;
            HighInitClimb.BorderColor = DefaultColor;
            EdgeLocation.BackgroundColor = DefaultColor;
            CenterLocation.BackgroundColor = DefaultColor;
            ChangeClimb(false);
            EndgameCommentsEntry.Text = "";
            YellowCard.BackgroundColor = DefaultColor;
            RedCard.BackgroundColor = DefaultColor;
            timeElapsedClimb = new Stopwatch();
            DisplayAlert("Successful!", "You cleared the data!", "Confirm");
        }
        public Matches RecordAllData()
        { //RECORDS ALL DATA INTO PROPERTIES AND FIELDS SO MAINPAGE.XAML.CS CAN ACCESS AND CONVERT TO STRING.
            currentMatch.TeamNumber = TeamNumberEntry.Text;
            currentMatch.Scouters = ScouterEntry.Text;
            currentMatch.MatchNumberEntry = Int32.Parse(MatchNumber.Text);
            currentMatch.StartingGamePieces = Int32.Parse(PowerCellCount.Text.Substring(0, 1));
            currentMatch.CrossesInitiationLine = ((ILine.BackgroundColor) == ButtonClickedColor) ? "Yes" : "No";
            currentMatch.ALowerScored = GetParenthesisValue(ALow);
            currentMatch.AOuterScored = GetParenthesisValue(AOuter);
            currentMatch.AInnerScored = GetParenthesisValue(AInner);
            currentMatch.AMissedBalls = GetParenthesisValue(AMissed);
            currentMatch.AComments = ACommentsEntry.Text;
            currentMatch.ABallsPickedUp = GetParenthesisValue(APickedUp);
            currentMatch.Defense = DefenseButton.BackgroundColor == ButtonClickedColor ? "Yes" : "No";
            currentMatch.TBallsFromLoadStation = GetParenthesisValue(BallsFromLoadingStationTeleop);
            currentMatch.FitsUnderTrench = UnderTrench.BackgroundColor == ButtonClickedColor ? "Yes" : "No";
            currentMatch.TLowerScored = GetParenthesisValue(TLow);
            currentMatch.TOuterScored = GetParenthesisValue(TOuter);
            currentMatch.TInnerScored = GetParenthesisValue(TInner);
            currentMatch.TMissedBalls = GetParenthesisValue(TMissed);
            currentMatch.TBallsFromFloor = GetParenthesisValue(PickedUpT);
            currentMatch.TComments = TeleopCommentsEntry.Text;
            currentMatch.EScore = GetParenthesisValue(EScores);
            currentMatch.EComments = EndgameCommentsEntry.Text;
            currentMatch.Penalities = (YellowCard.BackgroundColor == Color.Yellow && RedCard.BackgroundColor == Color.Red) ? "Yellow and Red" : (YellowCard.BackgroundColor == Color.Yellow) ? "Yellow" : (RedCard.BackgroundColor == Color.Red) ? "Red" : "None";
            currentMatch.Rotations = RotationButton.BackgroundColor == ButtonClickedColor ? "Yes" : "No";
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
            catch (Exception) { DisplayAlert("Error", "You have not entered a number in the match number. Please try again", "Ok!"); }
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
            int something = (Int32.Parse(PowerCellCount.Text.Substring(0, 1)) + 1 > 3) ? 3 : Int32.Parse(PowerCellCount.Text.Substring(0, 1)) + 1;
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
                ILine.BackgroundColor = DefaultColor;
                return;
            }
            ILine.BackgroundColor = ButtonClickedColor;
        }
        //Maybe this one might just remain as is. Don't instantiate StartingLocation property
        private void Starting_Clicked(object sender, EventArgs e)
        {

            StartingLeft.BackgroundColor = DefaultColor; //DefaultColor apparently doens't work?
            StartingMiddle.BackgroundColor = DefaultColor;
            StartingRight.BackgroundColor = DefaultColor;
            currentMatch.StartingLocation = ((Button)sender).Text;
            ((Button)sender).BackgroundColor = ButtonClickedColor;
        }
        private int GetParenthesisValue(Button sender)
        {
            LastClickedButton = sender;
            return Int32.Parse(Regex.Match(sender.Text, @"\d+").Value);
        }
        //This controls the ALow button for the ALowerScored Property
        private void ALow_Clicked(object sender, EventArgs e)
        {
            ALow.Text = "Low (" + (GetParenthesisValue(((Button)sender)) + 1) + ")";
        }
        //AOuter Button with AOuterScored Property
        private void AOuter_Clicked(object sender, EventArgs e)
        {
            AOuter.Text = "Outer (" + (GetParenthesisValue(((Button)sender)) + 1) + ")";
        }
        //AInner Button with AInnerScored Text
        private void AInner_Clicked(object sender, EventArgs e)
        {
            AInner.Text = "Inner (" + (GetParenthesisValue(((Button)sender)) + 1) + ")";
        }
        //AMissed Button with AMissedBalls Property
        private void AMissed_Clicked(object sender, EventArgs e)
        {
            AMissed.Text = "Missed (" + (GetParenthesisValue(((Button)sender)) + 1) + ")";
        }
        //APickedUp Button with ABallsPickedUp Property
        private void APickedUp_Clicked(object sender, EventArgs e)
        {
            APickedUp.Text = "Balls Picked Up (" + (GetParenthesisValue(((Button)sender)) + 1) + ")";
        }
        //DefenseButton Button with Defense Property
        private void Defense_Clicked(object sender, EventArgs e)
        {
            if (DefenseButton.BackgroundColor == ButtonClickedColor)
            {
                DefenseButton.BackgroundColor = DefaultColor;
                return;
            }
            DefenseButton.BackgroundColor = ButtonClickedColor;
        }
        //BallsFromLoadingStationTeleop is button Name (longest button name ever...) and TBallsFromLoadStation is the property
        private void BallsFromLoadingStationTeleop_Clicked(object sender, EventArgs e)
        {
            BallsFromLoadingStationTeleop.Text = "Balls Picked Up From Loading Station (" + (GetParenthesisValue(((Button)sender)) + 1) + ")";
        }
        private void ColorWheel_Clicked(object sender, EventArgs e)
        {
            if (((Button)sender).BackgroundColor == ButtonClickedColor)
            {
                ((Button)sender).BackgroundColor = DefaultColor;
                return;
            }
            ((Button)sender).BackgroundColor = ButtonClickedColor;
        }
        //UnderTrench is button name with FitsUnderButton property
        private void UnderTrench_Clicked(object sender, EventArgs e)
        {
            if (((Button)sender).BackgroundColor == ButtonClickedColor)
            {
                ((Button)sender).BackgroundColor = DefaultColor;
                return;
            }
            ((Button)sender).BackgroundColor = ButtonClickedColor;
        }
        //TLow is the button Name and TLowerScored is the property
        private void TLow_Clicked(object sender, EventArgs e)
        {
            ((Button)sender).Text = "Low (" + (GetParenthesisValue(((Button)sender)) + 1) + ")";
        }
        //TOuter is button Name and TOuterScored is the property
        private void TOuter_Clicked(object sender, EventArgs e)
        {
            ((Button)sender).Text = "Outer (" + (GetParenthesisValue(((Button)sender)) + 1) + ")";
        }
        //TInner is the button Name with TInnerScored
        private void TInner_Clicked(object sender, EventArgs e)
        {
            ((Button)sender).Text = "Inner (" + (GetParenthesisValue(((Button)sender)) + 1) + ")";
        }
        //TMissed is button name and TMissedBalls is property
        private void TMissed_Clicked(object sender, EventArgs e)
        {
            ((Button)sender).Text = "Missed (" + (GetParenthesisValue(((Button)sender)) + 1) + ")";
        }
        //PickedUpT is the button name and TBallsFromFloor is the property
        private void PickedUpT_Clicked(object sender, EventArgs e)
        {
            ((Button)sender).Text = "Balls Picked Up From The Floor (" + (GetParenthesisValue(((Button)sender)) + 1) + ")";
        }
        //EScores is button name and EScore is the property
        private void EScores_Clicked(object sender, EventArgs e)
        {
            ((Button)sender).Text = "Balls Scored (" + (GetParenthesisValue(((Button)sender)) + 1) + ")";
        }
        //YellowCard is the button and YellowCards is the property
        private void YellowCard_Clicked(object sender, EventArgs e)
        {
            if (YellowCard.BackgroundColor == Color.Yellow)
            {
                YellowCard.BackgroundColor = DefaultColor;
                return;
            }
            YellowCard.BackgroundColor = Color.Yellow;
        }
        //RedCard is button name and RedCards is the property
        private void RedCard_Clicked(object sender, EventArgs e)
        {
            if (RedCard.BackgroundColor == Color.Red)
            {
                RedCard.BackgroundColor = DefaultColor;
                return;
            }
            RedCard.BackgroundColor = Color.Red;
        }
        //Rotation is the button name and Rotations is the property
        private void Rotation_Clicked(object sender, EventArgs e)
        {
            if (RotationButton.BackgroundColor == ButtonClickedColor)
            {
                RotationButton.BackgroundColor = DefaultColor;
                return;
            }
            RotationButton.BackgroundColor = ButtonClickedColor;
        }
        //Shooting is th button name and TShootingLocation is the property
        private void Shooting_Location(object sender, EventArgs e)
        {
            Trench.BackgroundColor = DefaultColor; //DEFAULT COLOR!!!!!!
            Target.BackgroundColor = DefaultColor;
            Other.BackgroundColor = DefaultColor;
            currentMatch.TShootingLocation = ((Button)sender).Text;
            ((Button)sender).BackgroundColor = ButtonClickedColor;
        }

        private void Stopwatch_Clicked(object sender, EventArgs e)
        {
            if (((Button)sender).Text == "Start Stopwatch")
            {
                ((Button)sender).Text = "Stop Stopwatch";
                timeElapsedClimb.Start();
                return;
            }
            if (((Button)sender).Text == "Stop Stopwatch")
            {
                ((Button)sender).IsEnabled = false;
                ResetStopwatch.IsVisible = true;
                StopwatchLabel.IsVisible = true;
                StopwatchLabel.Text = (timeElapsedClimb.ElapsedMilliseconds / 1000).ToString();
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
            Park.BackgroundColor = DefaultColor;
            Climb.BackgroundColor = DefaultColor;
            None.BackgroundColor = DefaultColor;
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
            BalanceClimb.IsVisible = value;
            LowClimb.IsVisible = value;
            HighClimb.IsVisible = value;
            ECommentsLabel.IsVisible = value;
        }

        private void ClimbLocation_Clicked(object sender, EventArgs e)
        {
            EdgeLocation.BackgroundColor = DefaultColor;
            MiddleBarLocation.BackgroundColor = DefaultColor;
            CenterLocation.BackgroundColor = DefaultColor;
            currentMatch.ClimbPosition = ((Button)sender).Text;
            ((Button)sender).BackgroundColor = ButtonClickedColor;
        }
        private async void Reset_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Are you sure?", "Would you really like to reset data? Unless you saved it, there is no way of retrieving the data!!!! Proceed with caution.", "Yes", "No")) //Somehow get the boolean out of option and true = yes, false = no... Seriously, it doesn't work atmm...
            {
                ResetData();
            }
        }

        private async void SaveData_Clicked(object sender, EventArgs e)
        {
            SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
            conn.CreateTable<Matches>();
            int rows = conn.Insert(RecordAllData()); //KEY DOESN'T WORRRRRKKKKK!!!!!!!
            if (rows > 0)
            {
                await DisplayAlert("Successful", "Data Store is successful!", "Ok");
            }
            else
            {
                await DisplayAlert("Error", "Something is wrong. Please contact me...", "Ok");
            }
            conn.Dispose();
        }

        private async void Export_Clicked(object sender, EventArgs e)
        {
            SaveData_Clicked(sender, e);
            RecordAllData().SerializeCsv();
            await Share.RequestAsync(new ShareFileRequest
            {
                Title = "Title",
                File = new ShareFile(Path.Combine(App.folderPathSave, RecordAllData().Scouters + RecordAllData().MatchNumberEntry + ".csv")),
                PresentationSourceBounds = DeviceInfo.Platform == DevicePlatform.iOS && DeviceInfo.Idiom == DeviceIdiom.Tablet ? new System.Drawing.Rectangle(0, 20, 50, 40) : System.Drawing.Rectangle.Empty
            });
        }
        private void Red_Clicked(object sender, EventArgs e)
        {
            App.ChangeColor(Color.Red);
        }

        private void Blue_Clicked(object sender, EventArgs e)
        {
            App.ChangeColor(Color.Blue);
        }

        private void ResetStopwatch_Clicked(object sender, EventArgs e)
        {
            timeElapsedClimb = new Stopwatch();
            ResetStopwatch.IsVisible = false;
            StopwatchLabel.IsVisible = false;
            Stopwatch.IsEnabled = true;
            Stopwatch.Text = "Start Stopwatch";
        }

        private void Decrement_Clicked(object sender, EventArgs e)
        {
            string buttonText = LastClickedButton.Text;
            string beforeText = null;
            for (int i = 0; i < buttonText.Length; i++)
            {
                if (buttonText[i] == '(')
                {
                    beforeText = buttonText.Substring(0, i);
                }
            }
            if (beforeText == null | GetParenthesisValue(LastClickedButton) == 0) return;
            LastClickedButton.Text = beforeText + "(" + (GetParenthesisValue(LastClickedButton) - 1) + ")";
        }
        private void Increment_Clicked(object sender, EventArgs e)
        {
            string buttonText = ((Button)sender).Text;
            string beforeText = null;
            for (int i = 0; i < buttonText.Length; i++)
            {
                if (buttonText[i] == '(')
                {
                    beforeText = buttonText.Substring(0, i);
                }
            }
            if (beforeText == null | GetParenthesisValue(LastClickedButton) == 0) return;
            LastClickedButton.Text = beforeText + "(" + (GetParenthesisValue(LastClickedButton) + 1) + ")";
        }
    }
}
//You can use onButtonPressed and onButtonReleased to do hold for decrementing
