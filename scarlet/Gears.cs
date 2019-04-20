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
        public double startTime = 1363;
        public double endTime1 = 2073;

        public override void Generate()
        {
		    var layer = GetLayer("gears");
            OsbSprite[] gears = new OsbSprite[] {
                    layer.CreateSprite("sb/gears/gear0.png", OsbOrigin.Centre),
                    layer.CreateSprite("sb/gears/gear1.png", OsbOrigin.Centre),
                    layer.CreateSprite("sb/gears/gear2.png", OsbOrigin.Centre),
                    layer.CreateSprite("sb/gears/gear3.png", OsbOrigin.Centre),
                    layer.CreateSprite("sb/gears/gear4.png", OsbOrigin.Centre),
                    layer.CreateSprite("sb/gears/gear5.png", OsbOrigin.Centre)
                 };
            
            int count = 0;
            foreach(OsbSprite gear in gears) {
                gear.Move(0, 0, 0);
                gear.Scale(0, 0.3); // TODO remove
                gear.Fade(OsbEasing.InExpo, 1186, startTime, 0, 1);
                gear.Fade(endTime1, 0);

                gear.Rotate(1186, endTime1, 0, Math.PI/8 * ((count % 2) == 1 ? -1 : 1));
                count++;
            }
            
            // First section 
            gears[0].Scale(0, 0.9);
            gears[0].Move(0, 620, 410);

            gears[1].Scale(0, 0.6);
            gears[1].Move(0, 320, 410);
            gears[1].Rotate(0, Math.PI*3/Math.Pow(2, 8));

            gears[2].Move(0, 200, 200);

            gears[3].Scale(0, 0.45);
            gears[3].Move(0, 620, 100);
        }
    }
}
