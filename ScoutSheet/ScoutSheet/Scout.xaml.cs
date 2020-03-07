using SQLite;
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
        private Stopwatch timeInBetweenPresses = new Stopwatch();
        private bool BlueIsClicked = true;
        public Scout()
        { 
            InitializeComponent();
            List<string> MatchTypeList = new List<string>
            {
                "Qualification",
                "Quarterfinals",
                "Semifinals"
            };
            Match_Type.ItemsSource = MatchTypeList;
            var assembly = typeof(MainPage);
            PowerCellPhoto.Source = ImageSource.FromResource("ScoutSheet.Assets.Images.PowerCell.jpg", assembly);
            Field.Source = ImageSource.FromResource("ScoutSheet.Assets.Images.field.jpg", assembly);
            Field2.Source = ImageSource.FromResource("ScoutSheet.Assets.Images.field.jpg", assembly);
            Bar_Thingy.Source = ImageSource.FromResource("ScoutSheet.Assets.Images.Climb Position.png", assembly);
            currentMatch.MatchNumberEntry = 0;
            LowInitClimb.Source = ImageSource.FromResource("ScoutSheet.Assets.Images.End Low.png", assembly);
            BallInitClimb.Source = ImageSource.FromResource("ScoutSheet.Assets.Images.End Level.png", assembly);
            HighInitClimb.Source = ImageSource.FromResource("ScoutSheet.Assets.Images.End High.png", assembly);
            None.BackgroundColor = ButtonClickedColor;
            //MatchInfo.Children.Add(PowerCellPhoto, 4, 7, 0, 4); //Some stuff goes here, but I eat dinner first...Then come back.... GTG then back
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
            ChangeClimb(false);
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
        }
        public Matches RecordAllData()
        { //RECORDS ALL DATA INTO PROPERTIES AND FIELDS SO MAINPAGE.XAML.CS CAN ACCESS AND CONVERT TO STRING.
            currentMatch.TeamNumber = TeamNumberEntry.Text;
            currentMatch.Scouters = Scouter.Text;
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
            currentMatch.TShootingLocation = ((Trench.BackgroundColor == ButtonClickedColor) ? "Trench " : "") + ((Target.BackgroundColor == ButtonClickedColor) ? "Target " : "") + ((Other.BackgroundColor == ButtonClickedColor) ? "Other":"");
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
            return Int32.Parse(Regex.Match(sender.Text, @"\d+").Value);
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
            ClimbLayout.IsVisible = value;
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
            EndgameCommentsEntry.IsVisible = value;
            EndGameCommentsLabel.IsVisible = value;
            EndgameCommentsEntry2.IsVisible = !value;
            EndGameCommentsLabel2.IsVisible = !value;
            GridStuff.IsVisible = value;
            GridStuff2.IsVisible = value;
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
            List<Matches> matchList = conn.Table<Matches>().ToList();
            foreach(Matches m in matchList)
            {
                if (m.MatchNumberEntry == Int32.Parse(MatchNumber.Text))
                {
                    if (await DisplayAlert("Do you want to replace match #" + m.MatchNumberEntry + "?", "Proceed with caution.", "Yes", "No"))
                    {
                        Matches match = RecordAllData();
                        match.Id = m.Id;
                        conn.Update(match);
                        return;
                    }
                    else return;
                }
            }
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
            SwitchComponents(true);
            
        }
        private void Blue_Clicked(object sender, EventArgs e)
        {
            App.ChangeColor(Color.Blue);
            SwitchComponents(false);
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
            string buttonText = ((Button)sender).Text;
            string beforeText = null;
            for (int i = 0; i < buttonText.Length; i++)
            {
                if (buttonText[i] == '(')
                {
                    beforeText = buttonText.Substring(0, i);
                }
            }
            if (beforeText == null | GetParenthesisValue((Button)sender) == 0) return;
            ((Button)sender).Text = beforeText + "(" + (GetParenthesisValue((Button)sender) - 1) + ")";
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
            if (beforeText == null) return;
            ((Button)sender).Text = beforeText + "(" + (GetParenthesisValue((Button)sender) + 1) + ")";
        }
        private void Color_Change(object sender, EventArgs e)
        {
            if (((Button)sender).BackgroundColor == ButtonClickedColor)
            {
                ((Button)sender).BackgroundColor = DefaultColor;
                return;
            }
            ((Button)sender).BackgroundColor = ButtonClickedColor;
        }

        private void SwitchComponents(bool colorValue) //RedClicked is true BlueClicked is false
        {
            List<View> allButtons = new List<View>(new View[] { TShootLabel, TBallLabel, ABallLabel,ILine, StartingLeft, StartingMiddle, StartingRight, ALow, AOuter, AInner, AMissed, APickedUp, DefenseButton, BallsFromLoadingStationTeleop, RotationButton, Target, ColorWheel, UnderTrench, PickedUpT, Trench, Target, Other, Target, TLow, TOuter, TInner, TMissed });
            if (colorValue && BlueIsClicked)
            {
                Grid.SetRow(APickedUp, Grid.GetRow(APickedUp) + 2);
                for (int i = 0; i < allButtons.Capacity; i++) //Autonomous Buttons. Total Columns: 15
                {
                    Grid.SetColumn(allButtons[i], 15 - Grid.GetColumn(allButtons[i]) - Grid.GetColumnSpan(allButtons[i]));
                    Grid.SetRow(allButtons[i], 12 - Grid.GetRow(allButtons[i]) - Grid.GetRowSpan(allButtons[i]));
                }
                Grid.SetRow(APickedUp, Grid.GetRow(APickedUp) + 2);
                Grid.SetRow(Trench, Grid.GetRow(Trench) - 1);
                Grid.SetRow(Target, Grid.GetRow(Target) - 1);
                Grid.SetRow(Other, Grid.GetRow(Other) + 2);
                Grid.SetRow(TShootLabel, Grid.GetRow(TShootLabel) - 4);
                BlueIsClicked = false;
            }
            else if(!colorValue && !BlueIsClicked)
            {
                Grid.SetRow(APickedUp, Grid.GetRow(APickedUp) - 2);
                Grid.SetRow(Trench, Grid.GetRow(Trench) + 1);
                Grid.SetRow(Target, Grid.GetRow(Target) + 1);
                Grid.SetRow(Other, Grid.GetRow(Other) - 2);
                Grid.SetRow(TShootLabel, Grid.GetRow(TShootLabel) + 4);
                for (int i = 0; i < allButtons.Capacity; i++) //Autonomous Buttons. Total Columns: 15
                {
                    Grid.SetColumn(allButtons[i], 15 - Grid.GetColumn(allButtons[i]) - Grid.GetColumnSpan(allButtons[i]));
                    Grid.SetRow(allButtons[i], 12 - Grid.GetRow(allButtons[i]) - Grid.GetRowSpan(allButtons[i]));
                }
                Grid.SetRow(APickedUp, Grid.GetRow(APickedUp) - 2);
                BlueIsClicked = true;
            }
        }

        private void Value_Pressed(object sender, EventArgs e)
        {
            timeInBetweenPresses.Start();
            while (timeInBetweenPresses.ElapsedMilliseconds > App.PressLength)
            {
                ((Button)sender).BackgroundColor = ButtonClickedColor;
            }
        }

        private void Value_Released(object sender, EventArgs e)
        {
            timeInBetweenPresses.Stop();
            if (timeInBetweenPresses.ElapsedMilliseconds > App.PressLength)
            {
                Decrement_Clicked(sender, e);
            }
            else
            {
                Increment_Clicked(sender, e);
            }
            ((Button)sender).BackgroundColor = DefaultColor;
            timeInBetweenPresses.Reset();
        }
    }
}
