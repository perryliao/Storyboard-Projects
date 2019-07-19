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
        public double slowFadeTime = 12997;

        [Configurable]
        public Color4 colour = Constants.white;

        private double beatLength = Constants.beatLength;

        public override void Generate()
        {
		    StoryboardLayer layer = GetLayer("");
            OsbSprite kokoro = layer.CreateSprite("sb/kkr.png", OsbOrigin.Centre);
            kokoro.Scale(startTime, 0.4);
            kokoro.Fade(startTime, 0); 
            kokoro.Color(startTime, colour);

            double timestep = beatLength * 2;
            double i;
            
            if (slowFadeTime >= endTime) slowFadeTime = endTime;

            for (i = startTime; i < slowFadeTime; i += timestep) {
                heartThumpingAnimations(kokoro, i, 0.6, 1);
            }

            double fadeOut = 0.9;
            double scaleRate = 0.1;
            for (i = slowFadeTime; i < endTime; i += timestep) {
                heartThumpingAnimations(kokoro, i, Math.Max(fadeOut - 0.4, 0), fadeOut);
                
                if (fadeOut > scaleRate) fadeOut -= scaleRate;
            }
        }

        /// <summary>Sets the thumping animations for the given sprite. This will automatically fade out half a beat after the given start time.</summary>
        /// <param name="kkr">The sprite to animate</param>
        /// <param name="start">The start time</param>
        /// <param name="midwayFade">The opacity for the first (minor) heart beat</param>
        /// <param name="finalFade">The opacity for the second (major) heart beat</param>
        ///
        private void heartThumpingAnimations( OsbSprite kkr, double start, double midwayFade, double finalFade) {
            kkr.Fade(OsbEasing.OutBounce, start, start + beatLength / 4, kkr.OpacityAt(start), midwayFade);
            kkr.Fade(OsbEasing.OutCirc, start + (beatLength / 4), start + (3 * beatLength / 8), midwayFade, finalFade);
            kkr.Fade(OsbEasing.InCirc, start + (3 * beatLength / 8), start + (beatLength / 2) , finalFade, 0);
        }
    }
}
