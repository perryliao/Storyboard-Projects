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
    public class Crossfade : StoryboardObjectGenerator
    {
        [Configurable]
        public double StartTime = 65586;

        private StoryboardLayer layer; 
        public override void Generate()
        {
            layer = GetLayer("CrossFade");
            CrossBlock(StartTime + Constants.beatLength*3, new Vector2((float)Constants.xFloor, 0), 200, true);
            CrossBlock(StartTime + Constants.beatLength*3, new Vector2((float)Constants.xCeil, 200), 160, false);
            CrossBlock(StartTime + Constants.beatLength*3, new Vector2((float)Constants.xFloor, 360), 20, true);
            CrossBlock(StartTime + Constants.beatLength*3, new Vector2((float)Constants.xCeil, 380), 100, false);

            ExpoTransition(StartTime + Constants.beatLength*2, StartTime + Constants.beatLength*3);

            OsbSprite screen = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.Centre);
            screen.ScaleVec(65233, Constants.width, Constants.height);
            screen.Fade(OsbEasing.InExpo, 65233, StartTime, 0, 0.6);
            screen.Fade(OsbEasing.InQuad, StartTime, StartTime + Constants.beatLength, screen.OpacityAt(StartTime), 0);
            screen.Color(OsbEasing.InExpo, 65233, StartTime, Constants.white, Constants.red);
            screen.Color(OsbEasing.InQuad, StartTime, StartTime + Constants.beatLength, Constants.darkRed, Constants.black);
        }

        private void CrossBlock(double startTime, Vector2 startPos, double height, bool right) {
            OsbSprite block = layer.CreateSprite("sb/1x1.jpg", right ? OsbOrigin.TopLeft : OsbOrigin.TopRight, startPos);

            // block.Fade(OsbEasing.OutExpo, startTime, startTime + Constants.beatLength/2, 0, 0.4);
            block.Fade(startTime, 0.4);
            block.Color(startTime, Constants.white);
            block.ScaleVec(startTime, Constants.width, height);
            // block.MoveX(OsbEasing.InExpo, startTime, startTime + Constants.beatLength*5/2, 
            //     startPos.X, 
            //     right ? startPos.X + 80 : startPos.X - 80
            // );
            block.MoveX(startTime, startTime + Constants.beatLength/2, 
                block.PositionAt(startTime).X, 
                right ? block.PositionAt(startTime).X + 30 : block.PositionAt(startTime).X - 30
            );
            block.MoveX(OsbEasing.OutCirc, startTime + Constants.beatLength/2, 68409, 
                block.PositionAt(startTime + Constants.beatLength/2).X, 
                right ? Constants.xCeil : Constants.xFloor
            );
            block.Fade(68409, 0);
        }

        private void ExpoTransition(double start, double end) {
            int i, numBars = 42;
            double j, amount, toScale, scale = Constants.height / numBars;
            OsbSprite bar;
            for(i = 0; i < numBars; i++) {
                bar = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.TopLeft);
                bar.Color(start, Constants.white);
                bar.Move(start, Constants.xFloor, i*scale);
                bar.Fade(start, 0.4);
                bar.Fade(end, 0);
                for(j = start; j < end; j += Constants.beatLength/8) {
                    amount = Random(60, 350);
                    toScale = Math.Min(bar.ScaleAt(j - Constants.beatLength/8 ).X + amount, Constants.width);
                    if (j + Constants.beatLength/4 > end) toScale = Constants.width;
                    bar.ScaleVec(j, toScale, scale);
                }
            }
        }
    }
}
