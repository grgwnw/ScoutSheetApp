using SQLite;

using System;

using System.Timers;

namespace ScoutSheet.Model

{

    public class Matches

    {

        //Settings
        [PrimaryKey]
        public int MatchNumber { get; set; } //AutoIncrement. Do we need to have this as a user option?

        public string TeamNumber { get; set; } //Team numbers but it doesn't go above 4 digits... So....

        public string Scouters { get; set; } // Consists of Scouter Names

        public int StartingGamePieces { get; set; } //Number of game pieces stored in the robot at the start of the match



        //Autonomous

        public bool CrossesInitiationLine { get; set; } //Pretty self explanitory for autonomous....

        public string StartingLocation { get; set; } // Left, Right or Center

        public int ALowerScored { get; set; }

        public int AOuterScored { get; set; }

        public int AInnerScored { get; set; }

        public int AMissed { get; set; }

        public int ABallsPickedUp { get; set; }

        public string AComments { get; set; }



        //TeleOp Stuff

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

        public Timer ClimbTime { get; set; }

        [MaxLength(8)]

        public string InitialClimbHeight { get; set; } //Low, Balanced, High

        public string ClimbPosition { get; set; } //Edge, Bar, Center

        public string EComments { get; set; }

        public int YellowCards { get; set; }

        public bool RedCard { get; set; }

    }

}