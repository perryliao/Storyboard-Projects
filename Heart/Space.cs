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
        public double startTime = 11586;

        [Configurable]
        public double endTime = 23233;

        private double beatLength = 706;
        public override void Generate()
        {
		    var layer = GetLayer("Space");

            int numStars = 400;
            int i;

            for (i = 0; i < numStars; i++) {
                OsbSprite circ = createStar(startTime, endTime);
                circ.Move(startTime, endTime*3, circ.PositionAt(startTime), new Vector2(320, 240));
                var tmp = circ.PositionAt(endTime);

                circ.Move(startTime, endTime, circ.PositionAt(startTime), tmp);
                // circ.Move(startTime, startTime + beatLength, circ.PositionAt(startTime), new Vector2(Mathf.PerlinNoise(0, startTime), *2 - 1, 0));
            }
        }

        /// <summary>Creates a static star object in the scene</summary>
        /// <param name="sTime">Starting time of the object</param>
        /// <param name="eTime">Ending time of the object</param>
        ///
        private OsbSprite createStar(double sTime, double eTime) {
            OsbSprite dot = GetLayer("Space").CreateSprite("sb/Pool 2/dot.png", OsbOrigin.Centre, new Vector2(Random(-307, 947), Random(-100, 580)));
            dot.Scale(sTime, (double) Random(0,100)/800);
            dot.Fade(OsbEasing.InQuad, sTime, sTime + beatLength/2, 0, 1 );
            dot.Fade(OsbEasing.InQuad, eTime - beatLength/2, eTime, 1, 0 );
            return dot;
        }
    }
}
