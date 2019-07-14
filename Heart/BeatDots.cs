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
    public class BeatDots : StoryboardObjectGenerator
    {
        [Configurable]
        public double startTime = 90997;
        [Configurable]
        public double endTime = 113586;
        [Configurable]
        public double scale = 0.2;
        [Configurable]
        public int numDots = 4;

        public override void Generate()
        {
		    StoryboardLayer layer = GetLayer("BeatDots");
            OsbSprite dot, dotAni;
            double d = Constants.width / (numDots + 1);
            double frameDelay = 8;
            int numIterations = (int)((endTime - startTime) / (Constants.beatLength*4) + 1);

            for (int i = 0; i < numDots; i++) {
                double relativeStart = startTime + Constants.beatLength/2 + i*Constants.beatLength*3/4;
                double loopEnd = startTime + Constants.beatLength*7/2 - relativeStart;
                Vector2 pos = new Vector2((float)(d*(i+1) + Constants.xFloor), 240);

                dot = layer.CreateSprite("sb/Pool 3/Animation_1/Ellipse1.png", OsbOrigin.Centre, pos);
                dot.Scale(relativeStart, scale);

                dot.StartLoopGroup(relativeStart, numIterations);
                dot.Fade(0, 1);
                dot.Fade(loopEnd, 0);
                dot.Color(Constants.beatLength * 4, Constants.white);
                dot.EndGroup();

                dotAni = layer.CreateAnimation("sb/Pool 3/Animation_1/Ellipse.png", 32, frameDelay, OsbLoopType.LoopForever, OsbOrigin.Centre, pos);
                dotAni.StartLoopGroup(relativeStart + loopEnd, numIterations);
                dotAni.Scale(OsbEasing.InBack, 0, (frameDelay - 1)*32, scale, 0);
                dotAni.Fade(OsbEasing.InExpo, 0, (frameDelay - 1)*32, 1, 0);
                dotAni.Color(Constants.beatLength * 4, Constants.white);
                dotAni.EndGroup();

            }
            
        }
    }
}
