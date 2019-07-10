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
    public class Title : StoryboardObjectGenerator
    {
        private double startTime = 23233;
        private double endTime = 34527;
        
        public override void Generate()
        {
		    StoryboardLayer layer = GetLayer("Title");
            OsbSprite[] words = new OsbSprite[2] {
                layer.CreateSprite("sb/grim.png", OsbOrigin.CentreRight, new Vector2(310, 240)), 
                layer.CreateSprite("sb/heart.png", OsbOrigin.CentreLeft, new Vector2(330, 240))
                };

            foreach (OsbSprite word in words) {
                word.Fade(startTime, 1);
                word.Scale(startTime, 0.5);
                word.Fade(OsbEasing.InExpo, 33821, endTime, 1, 0);
            }
        }
    }
}
