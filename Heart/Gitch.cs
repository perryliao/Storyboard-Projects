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
    public class Gitch : StoryboardObjectGenerator
    {
        // [Configurable]
        // public double startTime = 20409;
        // [Configurable]
        // public double endTime = 23233;

        private double blockDuration = Constants.beatLength;
        StoryboardLayer layer;
        public override void Generate()
        {
            layer = GetLayer("Glitch");
            OsbSprite s = layer.CreateSprite("sb/Pool 5/CoreO.png", OsbOrigin.Centre);

            s.Fade(OsbEasing.OutBack, 20409, 20586, 0, 1);
            s.Fade(OsbEasing.InOutElastic, 22527, 23233, 1, 0);

            s.Scale(20409, 0.33);

            s.Rotate(20409, 23233, 0, Math.PI/4);

            s.Color(20409, Constants.white);
            s.Color(OsbEasing.OutExpo, 21292, 21468, Constants.red, Constants.white);

            for (double i = 21821; i < 23233 - Constants.beatLength/2; i += Constants.beatLength/2) {
                s.Color(OsbEasing.InOutElastic, i, i + Constants.beatLength/4, s.ColorAt(i), Constants.randomColours[Random(Constants.randomColours.Length)]);
                s.Color(OsbEasing.InOutElastic, i + Constants.beatLength/4, i + Constants.beatLength/2, s.ColorAt(i), Constants.white);
            }
        }

        private void makeGlitch(double start, double end, double x, double y) {
            OsbSprite main = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.Centre, new Vector2((float)x, (float)y));
            double height = Random(100, 200);
            double width = Random(300, 400);

            main.ScaleVec(start, width, height);
            main.Fade(start, 0.4);
            main.Fade(end, 0);

            OsbSprite sub;
            for (int i = 0; i < 5; i++) {
                sub = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.Centre);
            }
        }
    }
}
