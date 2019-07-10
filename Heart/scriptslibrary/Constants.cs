using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorybrewScripts
{
    public class Constants {
        public static double beatLength = 706;
        
        public static Color4 red = new Color4((float)225/255, (float)9/255, (float)11/255, 1f);
        public static Color4 blue = new Color4(0, 0.8f, 0.9f, 1f);
        public static Color4 black = new Color4((float)26/255, (float)26/255, (float)26/255, 1f);
        public static Color4 white = new Color4((float) (242f/255), (float) (242f/255), (float) (242f/255), 1f);
        public static Color4 grey = new Color4(0.5f, 0.5f, 0.5f, 1f);

        public static Color4[] colours = new Color4[4] {
            white,
            black,
            grey,
            red
        };
    }
}
