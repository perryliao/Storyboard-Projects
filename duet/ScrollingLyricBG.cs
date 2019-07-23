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
            OsbSprite bar3 = layer.CreateSprite("sb/othersbg.jpg", OsbOrigin.Centre);
            var bar3BitMap = GetMapsetBitmap("sb/othersbg.jpg");
            
            handleLyricBar(bar1, startTime, -Math.PI/Random(16, 32), Colours.blue);
            handleLyricBar(bar2, startTime + Constants.beatLength*4, Math.PI/Random(16, 32), Colours.pink);

            double finalIn = startTime + Constants.beatLength * 8;
            double finalOut = finalIn + Constants.beatLength * 12;
            
            bar3.Fade(finalIn, finalIn + Constants.beatLength/2, 0, 1);
            bar3.Fade(finalOut - Constants.beatLength*4, finalOut, 1, 0);
            bar3.Scale(finalIn, 480.0f / bar3BitMap.Height);
            // bar3.Move(finalIn, finalIn + Constants.beatLength/2, 320, Constants.height + )
        }

        private void handleLyricBar(OsbSprite bar, double start, double rotation, Color4 colour) {
            bar.Rotate(start, rotation);
            bar.ScaleVec(start, Constants.width/Math.Cos(rotation) + 40, barHeight);
            bar.Color(start, colour);

            bar.Move(OsbEasing.OutCirc, start, start + Constants.beatLength/2, 320, Constants.height + barHeight, 320, 240 + barHeight);
            bar.Move(OsbEasing.OutCirc, start + Constants.beatLength/2, start + Constants.beatLength * 4, bar.PositionAt(start + Constants.beatLength/2), bar.PositionAt(start + Constants.beatLength/2).X, 240 + barHeight/2);
            bar.Move(OsbEasing.InCirc, start + Constants.beatLength * 4, start + Constants.beatLength * 9 / 2, bar.PositionAt(start + Constants.beatLength * 4), bar.PositionAt(start + Constants.beatLength * 4).X, 0);
        }
    } 
}
