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
        public override void Generate()
        {
		    var layer = GetLayer("light");
            var light = layer.CreateSprite("sb/light.png", OsbOrigin.CentreLeft);
            light.ScaleVec(0, 0.9, 2.0);
            light.Move(0, 280, -100);
            
            light.Fade(startTime - beatLength/2, startTime, 0, 0.6); 
            light.Fade(endTime - beatLength/2, endTime, 0.6, 0);
            
            light.Rotate(startTime - beatLength/2, endTime, Math.PI/2, Math.PI*3/4);
        }
    }
}
