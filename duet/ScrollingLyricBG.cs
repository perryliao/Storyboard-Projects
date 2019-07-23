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
    public class ScrollingLyricBG : StoryboardObjectGenerator
    {
        [Configurable]
        public double startTime = 68616; // the upbeat before the vocal downbeat
        [Configurable]
        public double barHeight = 80;

        StoryboardLayer layer;
        public override void Generate()
        {
            layer = GetLayer("ScrollingLyricBG");
            OsbSprite bar1 = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.BottomCentre);
            OsbSprite bar2 = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.BottomCentre);
		    
            double rotation = -Math.PI/Random(16, 32);
            bar1.Rotate(startTime, rotation);
            bar1.ScaleVec(startTime, Constants.width/Math.Cos(rotation) + 30, barHeight);
            bar1.Color(startTime, Colours.blue);

            bar1.Move(OsbEasing.OutCirc, startTime, startTime + Constants.beatLength/2, 320, Constants.height + barHeight, 320, 240 + barHeight);
            bar1.Move(OsbEasing.OutCirc, startTime + Constants.beatLength/2, startTime + Constants.beatLength * 4, bar1.PositionAt(startTime + Constants.beatLength/2), bar1.PositionAt(startTime + Constants.beatLength/2).X, 240 + barHeight/2);
            bar1.Move(OsbEasing.InExpo, startTime + Constants.beatLength * 4, startTime + Constants.beatLength * 9 / 2, bar1.PositionAt(startTime + Constants.beatLength * 4), bar1.PositionAt(startTime + Constants.beatLength * 4).X, 0);

            rotation = Math.PI/Random(16, 32);
            bar2.Rotate(startTime + Constants.beatLength*4, rotation);
            bar2.ScaleVec(startTime + Constants.beatLength*4, Constants.width/Math.Cos(rotation) + 30, barHeight);
            bar2.Color(startTime + Constants.beatLength*4, Colours.pink);

            bar2.Move(OsbEasing.OutCirc, startTime + Constants.beatLength*4, startTime + Constants.beatLength*4 + Constants.beatLength/2, 320, Constants.height + barHeight, 320, 240 + barHeight);
            bar2.Move(OsbEasing.OutCirc, startTime + Constants.beatLength*4 + Constants.beatLength/2, startTime + Constants.beatLength*4 + Constants.beatLength * 4, bar2.PositionAt(startTime + Constants.beatLength*4 + Constants.beatLength/2), bar2.PositionAt(startTime + Constants.beatLength*4 + Constants.beatLength/2).X, 240 + barHeight/2);
            bar2.Move(OsbEasing.InExpo, startTime + Constants.beatLength*4 + Constants.beatLength * 4, startTime + Constants.beatLength*4 + Constants.beatLength * 9 / 2, bar2.PositionAt(startTime + Constants.beatLength*4 + Constants.beatLength * 4), bar2.PositionAt(startTime + Constants.beatLength*4 + Constants.beatLength * 4).X, 0);

        }
    }
}
