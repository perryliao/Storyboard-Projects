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
    public class Kokoro : StoryboardObjectGenerator
    {
        [Configurable]
        public double startTime = 292;

        [Configurable]
        public double endTime = 20409;

        [Configurable]
        public double slowFadeTime = 11939;

        private double beatLength = 706;

        public override void Generate()
        {
		    var layer = GetLayer("");
            OsbSprite kokoro = layer.CreateSprite("sb/Pool 1/heart.png", OsbOrigin.Centre);
            kokoro.Scale(startTime, 0.2);

            double timestep = beatLength * 2;
            double i;
            
            if (slowFadeTime >= endTime) slowFadeTime = endTime;

            for (i = startTime; i < slowFadeTime; i += timestep) {
                kokoro.Fade(OsbEasing.OutBounce, i, i + beatLength / 4, 0, 0.6);
                kokoro.Fade(OsbEasing.OutCirc, i + (beatLength / 4), i + (3 * beatLength / 8), 0.6, 1);
                kokoro.Fade(OsbEasing.InCirc, i + (3 * beatLength / 8), i + (beatLength / 2) , 1, 0);
            }

            // kokoro.StartLoopGroup(startTime, 1);
            // kokoro.Fade(OsbEasing.OutBounce, startTime, 468, 0, 1);
            // // kokoro.Fade(OsbEasing.InExpo, 468, 644, 1, 0);
            // kokoro.EndGroup();
        }
    }
}
