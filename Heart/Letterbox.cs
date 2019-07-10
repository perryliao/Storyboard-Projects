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
    public class Letterbox : StoryboardObjectGenerator
    {
        [Configurable]
        public double startTime = 23233;
        [Configurable]
        public double endTime = 34527;

        private double beatLength = Constants.beatLength;

        /// <summary>Creates 2 black bar sprites, on the top and bottom.</summary>
        public override void Generate()
        {
		    StoryboardLayer layer = GetLayer("Letterbox");
            OsbSprite[] bars = new OsbSprite[2] {
                layer.CreateSprite("sb/1x1.jpg", OsbOrigin.TopCentre, new Vector2(320, 0)), 
                layer.CreateSprite("sb/1x1.jpg", OsbOrigin.BottomCentre, new Vector2(320, 480))
                };

            foreach (OsbSprite bar in bars) {
                bar.Fade(startTime, 1);
                bar.Color(startTime, 0, 0, 0);
                bar.ScaleVec(startTime, 854, 480 * 0.15);
                bar.Fade(OsbEasing.InExpo, endTime - beatLength / 2, endTime, 1, 0);
            }
        }
    }
}
