using SQLite;
using System.IO;
using Xamarin.Forms;
using System.Diagnostics;
using CsvHelper;
using System.Globalization;
using System.Collections.Generic;
using CsvHelper.Configuration.Attributes;

namespace ScoutSheet
{
	public class Matches
	{
        public string TeamNumber { get; set; }
        public int MatchNumberEntry { get; set; } = 0; //AutoIncrement. Do we need to have this as a user option
        [CsvHelper.Configuration.Attributes.Ignore]
        public string Scouters { get; set; } // Consists of Scouter Names

        
        
        public string FitsUnderTrench { get; set; }

        
        
        public string Defense { get; set; }
        public string Penalities { get; set; }
        //Autonomous
        public int StartingGamePieces { get; set; } = 0; //Number of game pieces stored in the robot at the start of the match
        public string StartingLocation { get; set; }

        
        
        public string CrossesInitiationLine { get; set; } //Pretty self explanitory for autonomous
        public int ABallsPickedUp { get; set; } = 0;        
        public int ALowerScored { get; set; } = 0;
        
        public int AOuterScored { get; set; } = 0;
        
        public int AInnerScored { get; set; } = 0;
        
        public int AMissedBalls { get; set; } = 0;

        public string AComments { get; set; }
        //TeleOp

        public int TBallsFromLoadStation { get; set; }
        
        public int TBallsFromFloor { get; set; }

        public int TLowerScored { get; set; }
        
        public int TOuterScored { get; set; }
        
        public int TInnerScored { get; set; }

        public int TMissedBalls { get; set; }
        
        public string TShootingLocation { get; set; }

        
        
        public string Rotations { get; set; }

        
        
        public string ColorWheelColor { get; set; }
        public string TComments { get; set; }

        //Endgame
        public string EndLocation { get; set; }
        public int EScore { get; set; }
        public string InitialClimbHeight { get; set; } //Low, Balanced, 
        public string ClimbPosition { get; set; } //Edge, Bar, 
        public string ClimbTime { get; set; }   
        public string EComments { get; set; }
        

        public Matches()
        {

        }

        public void SerializeCsv()
        {
            var records = new List<Matches>{this};
            using (var writer = new StreamWriter(Path.Combine(App.folderPathSave,"Test.csv")))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(records);
            }
        }
    }
}
