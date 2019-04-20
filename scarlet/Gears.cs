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
        [Configurable]
        public string gearNumber = "0";
        [Configurable]
        public double startTime = 1363;
        [Configurable]
        public double endTime = 2073;
        [Configurable]
        public bool clockwise = true;
        [Configurable]
        public double scale = 1.0;
        [Configurable]
        public double xValue = 0.0;
        [Configurable]
        public double yValue = 0.0;

        [Configurable]
        public double rotationAmount = 1/8;
        
        public double beatLength = 356;
        public override void Generate()
        {
		    var layer = GetLayer("gears");
            OsbSprite gear = layer.CreateSprite("sb/gears/gear" + gearNumber + ".png", OsbOrigin.Centre);

            gear.Scale(0, scale);
            gear.Move(startTime - beatLength/2, xValue, yValue);
            gear.Fade(OsbEasing.InExpo, startTime - beatLength/2, startTime, 0, 1);
            gear.Fade(OsbEasing.OutExpo, endTime - beatLength/2, endTime, 1, 0);
            gear.Rotate(startTime - beatLength/2, endTime, 0, Math.PI*rotationAmount * (clockwise ? -1 : 1));
        }
    }
}
