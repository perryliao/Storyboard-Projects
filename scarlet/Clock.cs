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
    public class Clock : StoryboardObjectGenerator
    {
        public override void Generate()
        {
		    var layer = GetLayer("clock");
            var clock = layer.CreateSprite("sb/clock/clock.png", OsbOrigin.Centre);
            var minHand = layer.CreateSprite("sb/clock/clockhand.png", OsbOrigin.BottomCentre);
            var hourHand = layer.CreateSprite("sb/clock/clockhand.png", OsbOrigin.BottomCentre);
            
            OsbSprite[] allParts = new OsbSprite[] { clock, minHand, hourHand };

            minHand.ScaleVec(0, 0.6, 1.1);
            hourHand.ScaleVec(0, 0.8, 0.7);

            foreach(OsbSprite elem in allParts) {
                elem.Fade(OsbEasing.InExpo, 0, 121, 0, 1);
                elem.Fade(OsbEasing.InExpo, 1008, 1186, 1, 0);
            }

            hourHand.Rotate(653, 831, 0, Math.PI/6);

            foreach(OsbSprite elem in allParts) {
                elem.Fade(OsbEasing.InExpo, 2073, 2251, 0, 1);
                elem.Color(2072, 2073, elem.ColorAt(2072), 1, 0, 0); // TODO fix this hack
                elem.Fade(OsbEasing.InExpo, 2783, 2961, 1, 0);
            }

            minHand.Rotate(2073, 2961, 0, -Math.PI*2*5);
            hourHand.Rotate(2073, 2961, Math.PI*5/6, -Math.PI*2/6);
        }
    }
}
