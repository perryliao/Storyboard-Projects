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
    public class PoisonScripts : StoryboardObjectGenerator {
        public override void Generate() {}

        public static double edgeHeight = 20;
        public static Func<StoryboardLayer, double, double, float, float, double, double, bool>[] functions = {
            scrollBars
        };

        private static bool scrollBars(StoryboardLayer layer, double startTime, double duration, float x, float y, double width, double height) {
            Random rnd = new Random();
            int numIterations = 4;
            int i, numBars = 10;
            for (i = 0; i < numBars; i++) {
                OsbSprite bar = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.BottomRight, new Vector2(x + (float)width/2, y - (float)(height/2 - edgeHeight)));
                double relativeStartTime = startTime + i*Constants.beatLength/numBars;
                bar.Fade(relativeStartTime, 1);
                
                bar.StartLoopGroup(relativeStartTime, numIterations);
                
                bar.ScaleVec(0, rnd.Next(30, (int)width - 30), edgeHeight - 5);
                bar.MoveY(0, duration/numIterations, bar.PositionAt(0).Y, y + height/2);
                
                bar.EndGroup();
                bar.Fade(startTime + duration, 0);
            }
            return true;
        }
    }
}