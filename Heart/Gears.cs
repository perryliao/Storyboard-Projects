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
    public class Gears : StoryboardObjectGenerator
    {

        private string[] gearPaths = new string[6] {
            "sb/Pool 3/3.png",
            "sb/Pool 3/4.png",
            "sb/Pool 3/6.png",
            "sb/Pool 4/1.png",
            "sb/Pool 4/2.png",
            "sb/Pool 4/5.png"
        };
        StoryboardLayer layer;
        public override void Generate()
        {
		    layer = GetLayer("gears");
            
            OsbSprite g1 = gearConfig(70527, 320, 240);
            g1.Fade(OsbEasing.InBounce, 70527, 70703, 0, 1);
            g1.Fade(OsbEasing.InOutElastic, 70703, 71233, 1, 0);
            g1.Scale(OsbEasing.OutBack, 70527, 70880, 0, 0.7);
            g1.Rotate(70527, 71233, 0, Math.PI/16);
        }

        private OsbSprite gearConfig(double start, float x, float y) {
            OsbSprite gear = layer.CreateSprite(gearPaths[0], OsbOrigin.Centre, new Vector2(x, y));
            gear.Color(start, Constants.black);
            gear.Fade(start, 0);
            gear.Scale(start, 0.5);
            return gear;
        }
    }
}
