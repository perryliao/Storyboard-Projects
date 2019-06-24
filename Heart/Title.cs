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

        private double beatLength = 706;
        
        public override void Generate()
        {
		    var layer = GetLayer("Title");
            Vector2 pos1 = new Vector2(310, 240);
            Vector2 pos2 = new Vector2(330, 240);

            OsbSprite[] words = new OsbSprite[2] {
                layer.CreateSprite("sb/grim.png", OsbOrigin.CentreRight, pos1), 
                layer.CreateSprite("sb/heart.png", OsbOrigin.CentreLeft, pos2)
                };

            foreach (OsbSprite word in words) {
                word.Fade(startTime, 1);
                word.Scale(startTime, 0.5);
                word.Fade(OsbEasing.InExpo, endTime - beatLength, endTime, 1, 0);
            }

            double i;
            int tempX, tempY;
            for (i = startTime; i < endTime; i += beatLength * 2) {
                tempX = Random(-3, 3);
                tempY = Random(-8, 8);
                handleMove(words[0], pos1, i, tempX, tempY);
                handleMove(words[1], pos2, i, tempX, tempY);
            }
        }

        private void handleMove(OsbSprite sprite, Vector2 pos, double time, int x, int y) {
                sprite.Move(time, time + beatLength/32, sprite.PositionAt(time), pos.X + x, pos.Y + y );
                sprite.Move(time + beatLength/32, pos.X, pos.Y );
        }
    }
}
