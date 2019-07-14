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
        public static Func<StoryboardLayer, double, double, float, float, double, double, bool>[] functions = {
            PoisonScripts.test1
        };

        private static bool test1(StoryboardLayer layer, double startTime, double duration, float x, float y, double width, double height) {
            OsbSprite test = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.Centre, new Vector2(x, y));
            Random rnd = new Random();
            test.Fade(startTime, 1);
            test.ScaleVec(startTime, rnd.Next(30, (int)width - 30), 15);
            test.Fade(startTime + duration, 0);

            return true;
        }
    }
}