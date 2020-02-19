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
	public partial class Scout : ContentPage
	{
		public static string TestingThis { get; set; }
        // All of the data that we need to keep track of
        public int MatchNumber { get; set; } //AutoIncrement. Do we need to have this as a user option?
        public string TeamNumber { get; set; } //Team numbers but it doesn't go above 4 digits... So....
        public string Scouters { get; set; } // Consists of Scouter Names
        public int StartingGamePieces { get; set; } //Number of game pieces stored in the robot at the start of the match
        //Autonomous
        public bool CrossesInitiationLine { get; set; } //Pretty self explanitory for autonomous
        public string StartingLocation { get; set; } // Left, Right or 
        public int ALowerScored { get; set; }
        public int AOuterScored { get; set; }
        public int AInnerScored { get; set; }
        public int AMissed { get; set; }
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
        public int TMissed { get; set; }
        public bool Trench { get; set; }
        public bool Target { get; set; }
        public bool Other { get; set; }
        public string TComments { get; set; }
        //Endgame
        public int EScore { get; set; }
        public bool Park { get; set; }
        public bool Climb { get; set; }
        //public Timer ClimbTime { get; set; }    Until we figure out what to do with the climb timer
        public string InitialClimbHeight { get; set; } //Low, Balanced, 
        public string ClimbPosition { get; set; } //Edge, Bar, 
        public string EComments { get; set; }
        public int YellowCards { get; set; }
        public bool RedCard { get; set; }
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
			Field.Source = ImageSource.FromResource("", assembly);
		}
		public void RecordAllData() { //RECORDS ALL DATA INTO PROPERTIES AND FIELDS SO MAINPAGE.XAML.CS CAN ACCESS AND CONVERT TO STRING.
		
		}
		private void PlusMNumber_Clicked(object sender, EventArgs e)
		{
			TestingThis = "Seomthing is clicked!";
			DisplayAlert("Alert", "Something is now stored", "Cool!");
		}

		private void MinusMNumber_Clicked(object sender, EventArgs e)
		{

		}

		private void AddPCellNumber_Clicked(object sender, EventArgs e)
		{

		}

		private void MinusPCellNumber_Clicked(object sender, EventArgs e)
		{

		}
	}
}