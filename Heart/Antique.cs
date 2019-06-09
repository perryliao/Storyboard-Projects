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
    public class Antique : StoryboardObjectGenerator
    {
        [Configurable]
        public double startTime = 23233;
        [Configurable]
        public double endTime = 34527;
        [Configurable]
        public double staticTime = 0;

        private double beatLength = 23939 - 23233;
        private Random rnd = new Random();

        // private int width = 854;
        // private int height = 480;

        public override void Generate()
        {
		    var layer = GetLayer("Antique");
            OsbSprite mask = layer.CreateSprite("sb/Pool 2/Border Mask.png", OsbOrigin.Centre);

            // Layer mask
            mask.Fade(startTime, 0.8);
            mask.Color(startTime, 0, 0, 0);
            mask.ScaleVec(startTime, mask.ScaleAt(startTime).X, mask.ScaleAt(startTime).Y * 0.7);
            mask.Scale(startTime, startTime + 4*(beatLength * 4), 1, 0.75);
            mask.Fade(OsbEasing.InCirc, endTime - beatLength*4, endTime, 0.8, 0);

            // vertical line effect
            double i;
            double tempEndTime;
            for (i = startTime; i < endTime; i += beatLength / 2) {
                // each half a beat has a 10% chance to generate a new line 
                if (rnd.Next(0, 100) < 100) {
                    tempEndTime = i + (beatLength * (rnd.Next(0, 300) / 100));
                    configureLineEffect(i, tempEndTime <= endTime ? tempEndTime : endTime);
                }
            }

            if (staticTime > 0 && staticTime < endTime) {
                for (i = 0; i < 854; i += 1) {
                    configureLineEffect(rnd.Next((int)staticTime, (int)endTime - 1), endTime, i - 107);
                }
            }
        }

        /// <summary>This is where line effect sprites are created, It is intended to set states common to every sprite.</summary>
        /// <param name="lineStartTime">Time when the line will appear</param>
        /// <param name="lineEndTime">Time when the line will fade out</param>
        /// <param name="xCoord">(Optional) If you want to set a specific x coordinate to the line</param>
        /// 
        private OsbSprite configureLineEffect(double lineStartTime, double lineEndTime, double? xCoord = null) {
            OsbSprite line = GetLayer("Antique").CreateSprite("sb/Pool 1/rain.png", OsbOrigin.Centre, new Vector2(xCoord == null ? rnd.Next(-107, 747) : (float) xCoord, 240));
            double actualStart = lineStartTime - beatLength/4 < startTime ? startTime : lineStartTime - beatLength/4;
            line.Fade(OsbEasing.OutCirc, actualStart, lineStartTime, 0, 0.6);
            line.ScaleVec(actualStart, rnd.Next(0, 200)/150 + 1, 14);
            line.ColorHsb(actualStart, 0, 0, 0.1);
            if (xCoord == null) {
                line.MoveX(actualStart, lineEndTime, line.PositionAt(actualStart).X, line.PositionAt(actualStart).X + rnd.Next(-20, 20));
                line.Fade(OsbEasing.OutCirc, lineEndTime - beatLength / 4, lineEndTime, 0.6, 0);
            } else {
                line.MoveX(actualStart, lineEndTime, line.PositionAt(actualStart).X, line.PositionAt(actualStart).X);
                line.Fade(endTime, 0);
            }
            return line;
        }
    }
}
