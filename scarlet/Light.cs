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
    public class Light : StoryboardObjectGenerator
    {
        [Configurable]
        public double startTime = 5801;
        [Configurable]
        public double endTime = 17162;

        public double beatLength = 356;

        public double fadeTo = 0.4;
        public override void Generate()
        {
		    var layer = GetLayer("light");
            var light = layer.CreateSprite("sb/light.png", OsbOrigin.CentreLeft);
            var light2 = layer.CreateSprite("sb/light.png", OsbOrigin.CentreLeft);
            light.ScaleVec(0, 0.9, 2.0);
            light.Move(0, 280, -100);
            light2.ScaleVec(0, 0.9, 2.0);
            light2.Move(0, 280, -100);
            
            light.Fade(startTime - beatLength/2, startTime, 0, fadeTo); 
            light.Fade(endTime - beatLength/2, endTime, fadeTo, 0);
            
            light.Rotate(startTime - beatLength/2, endTime, Math.PI/2, Math.PI*3/4);

            var secondStartTime = (endTime - startTime) / 2 + startTime; 
            light2.Fade(secondStartTime - 4*beatLength, secondStartTime, 0, fadeTo);
            light2.Fade(endTime - beatLength/2, endTime, fadeTo, 0);
            light2.Rotate(secondStartTime - 4*beatLength, endTime, Math.PI*7/16, Math.PI*9/16);
        }
    }
}
