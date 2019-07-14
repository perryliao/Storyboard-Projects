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

        public override void Generate()
        {
		    StoryboardLayer layer = GetLayer("BeatDots");
            OsbSprite[] dots = new OsbSprite[4];
            double d = Constants.width / (dots.Length + 1);
            int numIterations = (int)((endTime - startTime) / (Constants.beatLength*4) + 1);

            for (int i = 0; i < dots.Length; i++) {
                double relativeStart = startTime + Constants.beatLength/2 + i*Constants.beatLength*3/4;
                double loopEnd = startTime + Constants.beatLength*7/2 - relativeStart;

                dots[i] = layer.CreateSprite("sb/Pool 3/Animation_1/Ellipse1.png", OsbOrigin.Centre, new Vector2((float)(d*(i+1) + Constants.xFloor), 240));
                dots[i].Scale(relativeStart, scale);

                dots[i].StartLoopGroup(relativeStart, numIterations);
                dots[i].Fade(0, 1);
                dots[i].Fade(loopEnd, 0);
                dots[i].Color(Constants.beatLength * 4, Constants.white);
                dots[i].EndGroup();
            }
            
        }
    }
}
