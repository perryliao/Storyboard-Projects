using OpenTK.Graphics;

namespace StorybrewScripts
{
    public class Constants {
        public static double beatLength = 706;
        public static double xFloor = -107;
        public static double xCeil = 747;
        public static double width = 854;
        public static double height = 480;
        
        
        public static Color4 red = new Color4((float)225/255, (float)9/255, (float)11/255, 1f);
        public static Color4 darkRed = new Color4((float)82/255, (float)3/255, (float)7/255, 1f);
        
        public static Color4 blue = new Color4(0, 0.8f, 0.9f, 1f);
        public static Color4 black = new Color4((float)26/255, (float)26/255, (float)26/255, 1f);
        public static Color4 white = new Color4((float) (242f/255), (float) (242f/255), (float) (242f/255), 1f);
        public static Color4 grey = new Color4(0.5f, 0.5f, 0.5f, 1f);

        public static Color4[] randomColours = new Color4[14] {
            new Color4((float) (246f/255), (float) (4f/255), (float) (88f/255), 1f),    // #f60458
            new Color4(1, (float) (195f/255), (float) (123f/255), 1f),                  // #ffc37b
            new Color4((float) (64f/255), (float) (201f/255), (float) (112f/255), 1f),  // #40c970
            new Color4((float) (148f/255), (float) (135f/255), (float) (245f/255), 1f), // #9487f5
            new Color4((float) (197f/255), (float) (135f/255), (float) (245f/255), 1f), // #c587f5
            new Color4((float) (245f/255), (float) (155f/255), (float) (234f/255), 1f), // #f587ea
            new Color4((float) (11f/255), (float) (249f/255), (float) (234f/255), 1f),  // #0bf9ea
            new Color4(1, (float) (214f/255), (float) (123f/255), 1f),                  // #ffd67b
            new Color4((float) (146f/255), 1, (float) (123f/255), 1f),                  // #92ff7b
            new Color4((float) (123f/255), 1, (float) (212f/255), 1f),                  // #7bffd4
            new Color4((float) (217f/255), (float) (129f/255), 1, 1f),                  // #d981ff
            new Color4((float) (243f/255), (float) (110f/255), 1, 1f),                  // #f36eff
            new Color4((float) (213f/255), 1, (float) (123f/255), 1f),                  // #d5ff7b
            new Color4(1, (float) (179f/255), (float) (123f/255), 1f)                   // #ffb37b
        };
    }
}
