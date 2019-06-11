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
        public override void Generate()
        {
		    var layer = GetLayer("");
            OsbSprite kokoro = layer.CreateSprite("sb/Pool 1/heart.png", OsbOrigin.Centre);
            kokoro.Scale(startTime, 0.2);

            kokoro.StartLoopGroup(startTime, 1);
            kokoro.Fade(OsbEasing.OutBounce, startTime, 468, 0, 1);
            // kokoro.Fade(OsbEasing.InExpo, 468, 644, 1, 0);
            kokoro.EndGroup();
        }
    }
}
