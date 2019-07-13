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
    public class Poison : StoryboardObjectGenerator
    {
        [Configurable]
        public double startTime = 68409;

        [Configurable]
        public double endTime = 90997;

        StoryboardLayer layer;
        public override void Generate()
        {
		    layer = GetLayer("Poison");

            setUpFrame(320, 170);
            setUpFrame(320 + 321, 170);
            setUpFrame(320 - 321, 170);
        }

        private void setUpFrame(float x, float y) {
            OsbSprite frame = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.Centre, new Vector2(x, y));
            float height = 290;

            frame.Fade(startTime, 1);
            frame.Fade(endTime, 0);
            frame.Color(startTime, Constants.black);
            frame.ScaleVec(startTime, 220, height);

            if (x != 320) 
                skewFrame(x, y, height);
        }

        private void skewFrame(float x, float y, float height) {
            OsbSprite top = layer.CreateSprite("sb/t.png", OsbOrigin.BottomCentre, new Vector2(x, y - height/2));
            OsbSprite bot = layer.CreateSprite("sb/t.png", OsbOrigin.CentreRight, new Vector2(x, y + height/2));

            top.Fade(startTime, 1);
            top.Fade(endTime, 0);
            bot.Fade(startTime, 1);
            bot.Fade(endTime, 0);

            top.Color(startTime, Constants.black);
            bot.Color(startTime, Constants.black);    

            bot.Rotate(startTime, -Math.PI/2);

            if (x < 320) {
                top.FlipH(startTime, startTime);
                bot.FlipV(startTime, startTime);
            }

            top.ScaleVec(startTime, 2.19, 0.4);
            bot.ScaleVec(startTime, 0.4, 2.19);
        }
    }
}
