using OpenTK.Graphics;

namespace StorybrewScripts
{
    public class Constants {
        public static double beatLength = 714;
        public static float xFloor = -107;
        public static float xCeil = 747;
        public static float width = 854;
        public static float height = 480;
        public static int glowRadius = 5;
        public static string jpFont = "Yu Mincho";
        public static string enFont = "Adobe Garamond Pro";
        public static int fontSize = 34;
        public static float fontScale = 0.5f;
    }

    public class Helpers {
        public static bool inChorus(double startTime, double endTime) {
            return ( (startTime > 84687 && endTime < 108973) || 
                     (startTime > 170401 && endTime < 194687) || 
                     (startTime > 230401 && endTime < 251830));
        }
    }
}