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
    public class PoisonScripts : StoryboardObjectGenerator {
        public override void Generate() {}

        public static double edgeHeight = 20;
        public static Func<double, double> degToRad = (deg) => Math.PI * deg / 180.0;
        public static Func<StoryboardLayer, double, double, float, float, double, double, bool>[] functions = {
            scrollBars, xStatic
        };
        private static Random rnd = new Random();
        private static double sideAngle = 7.7;

        private static bool scrollBars(StoryboardLayer layer, double startTime, double duration, float x, float y, double width, double height) {
            int numIterations = 2;
            int i, numBars = 10;
            bool left = x < 320, right = x > 320;

            for (i = 0; i < numBars; i++) {
                OsbSprite bar = layer.CreateSprite("sb/1x1.jpg", right ? OsbOrigin.BottomLeft : OsbOrigin.BottomRight, new Vector2(x + (float)(width/2 - edgeHeight + 1)*(right ? -1 : 1), y - (float)(height/2 - edgeHeight)));
                double relativeStartTime = startTime + i*Constants.beatLength/numBars;
                bar.Fade(relativeStartTime, 1);
                bar.ScaleVec(relativeStartTime, rnd.Next(15, (int)(width - edgeHeight*2 - 5)), edgeHeight - 5);
                
                bar.StartLoopGroup(relativeStartTime, numIterations);

                if (left)
                    bar.Rotate(0, duration/numIterations, degToRad(sideAngle), degToRad(-sideAngle));
                if (right)
                    bar.Rotate(0, duration/numIterations, degToRad(-sideAngle), degToRad(sideAngle));

                bar.MoveY(0, duration/numIterations, bar.PositionAt(0).Y, y + height/2);
                
                bar.EndGroup();
                bar.Fade(startTime + duration, 0);
            }
            return true;
        }

        private static bool xStatic(StoryboardLayer layer, double startTime, double duration, float x, float y, double width, double height) {
            double numCrosses = rnd.Next(1, 5);
            double xHeight = (height - 4*edgeHeight )/numCrosses + 5;
            double length = Math.Min(100, xHeight / Math.Cos(Math.PI/4) - 14);
            string path = "sb/1x1.jpg";

            for (int i = 0; i < (int)numCrosses; i++) {
                OsbSprite x1 = layer.CreateSprite(path, OsbOrigin.Centre, new Vector2(x, (float)(y - height/2 + edgeHeight + xHeight/2 + i*xHeight)));
                OsbSprite x2 = layer.CreateSprite(path, OsbOrigin.Centre, new Vector2(x, (float)(y - height/2 + edgeHeight + xHeight/2 + i*xHeight)));
                x1.Fade(startTime, 1);
                x1.Fade(startTime + duration, 0);
                x1.ScaleVec(startTime, length, 14);
                x1.Rotate(startTime, Math.PI/4);

                x2.Fade(startTime, 1);
                x2.Fade(startTime + duration, 0);
                x2.ScaleVec(startTime, length, 14);
                x2.Rotate(startTime, -Math.PI/4);
            }

            return true;
        }

        public static bool upArrow(StoryboardLayer layer, double startTime, double duration, float x, float y, double width) {
            string path;
            float displacement = 20;

            if (x == 320) { 
                path = "sb/arrow.png"; 
            } else {
                path = "sb/arrow_s.png";
            }


            OsbSprite[] arrows = new OsbSprite[2] {
                layer.CreateSprite(path, OsbOrigin.Centre, new Vector2(x, y - (float)(width/4) + displacement)), 
                layer.CreateSprite(path, OsbOrigin.Centre, new Vector2(x, y + (float)(width/4) + displacement)) 
            };

            foreach (OsbSprite arrow in arrows) {
                arrow.Fade(startTime, 1);
                arrow.Fade(startTime + duration, 0);

                if (x > 320) {
                    arrow.FlipH(startTime, startTime);
                }

                arrow.Scale(startTime, 0.65);
                arrow.MoveY(startTime, startTime + duration, arrow.PositionAt(startTime).Y, arrow.PositionAt(startTime).Y - displacement);
            }
            
            return true;
        }
    }
}