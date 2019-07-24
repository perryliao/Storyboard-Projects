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
    public class CircleBG : StoryboardObjectGenerator
    {
        [Configurable]
        public int numParticles = 200;
        [Configurable]
        public double dotOpacity = 0.4;
        [Configurable]
        public double threshold = 150;

        StoryboardLayer background;
        StoryboardLayer foreground;
        public override void Generate()
        {
            background = GetLayer("Background");
            foreground = GetLayer("Foreground");

            OsbSprite ring = background.CreateSprite("sb/circle.png", OsbOrigin.Centre);
            OsbSprite circle = background.CreateSprite("sb/circle.png", OsbOrigin.Centre);

		    double startTime = 194330;
            double endTime = 206116;
            ring.Color(startTime, Colours.pink);
            ring.Scale(OsbEasing.OutBack, startTime, startTime + Constants.beatLength/2, 0, 1.2);
            ring.Fade(startTime, 1);
            ring.Fade(endTime, 0);

            circle.Color(startTime, Colours.white);
            circle.Scale(OsbEasing.Out, startTime, startTime + Constants.beatLength/2, 0, 1.05);
            circle.Fade(startTime, 1);
            circle.Fade(endTime, 0);

            // particles moving 
            for (int i = 0; i < numParticles; i++) {
                OsbSprite dot = foreground.CreateSprite("sb/circle2.png", OsbOrigin.Centre);
                var bitMap = GetMapsetBitmap("sb/circle2.png");
                float height = (float)Random(2, 15);
                dot.Color(startTime, Random(2) % 2 == 0 ? Colours.pink : Colours.blue);
                dot.Fade(startTime, startTime + Constants.beatLength/2, 0, dotOpacity);
                dot.Fade(endTime - Constants.beatLength/2, endTime, dotOpacity, 0);
                dot.Scale(startTime, height/bitMap.Height);

                // dot.Move(startTime, Random(-107, 747), Random(0, 480));
                Vector2 startPos = new Vector2( getRandomX(), getRandomY()), endPos = new Vector2( getRandomX(), getRandomY());
                if (Random(100) < 10) {
                    // 10% chance of this 
                    dot.Move(startTime, endTime, startPos, endPos);
                } else {
                    while ((endPos - startPos).Length > threshold) {
                        endPos = new Vector2( getRandomX(), getRandomY());
                    }
                    dot.Move(startTime, endTime, startPos, endPos);
                }
            }
        }
        private float getRandomX() {
            return (float)Random(Constants.xFloor, Constants.xCeil);
        }
        private float getRandomY() {
            return (float)Random(0, Constants.height);
        }
    }
}
