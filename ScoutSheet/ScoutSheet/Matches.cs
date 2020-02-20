using System;
using System.Collections.Generic;
using System.Text;

namespace ScoutSheet
{
	public class Matches
	{
        //Thngs that have 0 references next to it must be looked over!!!!!
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
        public Matches()
        {

        }
    }
}
