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
    public class Space : StoryboardObjectGenerator
    {
        [Configurable]
        public double startTime = 11939;

        [Configurable]
        public double endTime = 23233;

        private double beatLength = 706;
        public override void Generate()
        {
		    var layer = GetLayer("Space");

            int numStars = 100;
            int i;

            for (i = 0; i < numStars; i++) {
                createStar(startTime, endTime);
            }
        }

        /// <summary>Creates a static star object in the scene</summary>
        /// <param name="sTime">Starting time of the object</param>
        /// <param name="eTime">Ending time of the object</param>
        ///
        private OsbSprite createStar(double sTime, double eTime) {
            OsbSprite dot = GetLayer("Space").CreateSprite("sb/Pool 2/dot.png", OsbOrigin.Centre, new Vector2(Random(-107, 747), Random(0, 480)));
            dot.Scale(sTime, (double) Random(0,100)/800);
            dot.Fade(OsbEasing.OutQuad, sTime, sTime + beatLength/2, 0, 1 );
            dot.Fade(OsbEasing.InQuad, eTime - beatLength/2, eTime, 1, 0 );
            return dot;
        }
    }
}
