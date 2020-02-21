using Newtonsoft.Json;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace ScoutSheet
{
	public class Matches
	{
        //Thngs that have 0 references next to it must be looked over!!!!!
        [JsonProperty]
        public int MatchNumberEntry { get; set; } = 0; //AutoIncrement. Do we need to have this as a user option?
        [JsonProperty]
        public string TeamNumber { get; set; } //Team numbers but it doesn't go above 4 digits... So....
        [JsonProperty]
        public string Scouters { get; set; } // Consists of Scouter Names
        [JsonProperty]
        public int StartingGamePieces { get; set; } = 0; //Number of game pieces stored in the robot at the start of the match
        //Autonomous
        [JsonProperty]
        public bool CrossesInitiationLine { get; set; } //Pretty self explanitory for autonomous
        [JsonProperty]
        public string StartingLocation { get; set; }
        [JsonProperty]
        public int ALowerScored { get; set; } = 0;
        [JsonProperty]
        public int AOuterScored { get; set; } = 0;
        [JsonProperty]
        public int AInnerScored { get; set; } = 0;
        [JsonProperty]
        public int AMissedBalls { get; set; } = 0;
        [JsonProperty]
        public int ABallsPickedUp { get; set; }
        [JsonProperty]
        public string AComments { get; set; }
        //TeleOp
        [JsonProperty]
        public bool Defense { get; set; }
        [JsonProperty]
        public int TBallsFromLoadStation { get; set; }
        [JsonProperty]
        public int TBallsFromFloor { get; set; }
        [JsonProperty]
        public bool FitsUnderTrench { get; set; }
        [JsonProperty]
        public int TLowerScored { get; set; }
        [JsonProperty]
        public int TOuterScored { get; set; }
        [JsonProperty]
        public int TInnerScored { get; set; }
        [JsonProperty]
        public int TMissedBalls { get; set; }
        [JsonProperty]
        public string TShootingLocation { get; set; }
        [JsonProperty]
        public string TComments { get; set; }
        [JsonProperty]
        public string ColorWheelColor { get; set; } = "Aqua";
        [JsonProperty]
        public bool Rotations { get; set; } = false;
        //Endgame
        [JsonProperty]
        public int EScore { get; set; }
        [JsonProperty]
        public string EndLocation { get; set; }
        [JsonProperty]
        public string ClimbTime { get; set; }   
        [JsonProperty]
        public string InitialClimbHeight { get; set; } //Low, Balanced, 
        [JsonProperty]
        public string ClimbPosition { get; set; } //Edge, Bar, 
        [JsonProperty]
        public string EComments { get; set; }
        [JsonProperty]
        public bool YellowCards { get; set; }
        [JsonProperty]
        public bool RedCards { get; set; }
        public Matches()
        {

        }

        public void SerializeJson(string file)
        {
            System.Diagnostics.Debug.WriteLine(file);
            var value = JsonConvert.SerializeObject(this);
            //using (StreamWriter writer = new StreamWriter(file, false))
            //{
            //    writer.Write(value);
            //}
            Resource
            File.WriteAllText(file, value);
        }
    }
}
