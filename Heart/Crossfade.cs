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
        public double startTime = 65586;

        private StoryboardLayer layer; 
        public override void Generate()
        {
            layer = GetLayer("CrossFade");
            CrossBlock(new Vector2((float)Constants.xFloor, 0), 200, true);
            CrossBlock(new Vector2((float)Constants.xCeil, 200), 160, false);
            CrossBlock(new Vector2((float)Constants.xFloor, 360), 20, true);
            CrossBlock(new Vector2((float)Constants.xCeil, 380), 100, false);

            ExpoTransition(startTime + Constants.beatLength*2, startTime + Constants.beatLength*4);
        }

        private void CrossBlock(Vector2 startPos, double height, bool right) {
            OsbSprite block = layer.CreateSprite("sb/1x1.jpg", right ? OsbOrigin.TopLeft : OsbOrigin.TopRight, startPos);

            block.Fade(OsbEasing.OutExpo, startTime, startTime + Constants.beatLength/2, 0, 0.4);
            block.Color(OsbEasing.InOutExpo, startTime, startTime +  Constants.beatLength*3/2, Constants.red, Constants.white);
            block.ScaleVec(startTime, Constants.width, height);
            block.MoveX(OsbEasing.InExpo, startTime, startTime + Constants.beatLength*5/2, 
                startPos.X, 
                right ? startPos.X + 80 : startPos.X - 80
            );
            block.MoveX(OsbEasing.None, startTime + Constants.beatLength*5/2, 68056, 
                block.PositionAt(startTime + Constants.beatLength*5/2).X, 
                right ? block.PositionAt(startTime + Constants.beatLength*5/2).X + 30 : block.PositionAt(startTime + Constants.beatLength*5/2).X - 30
            );
            block.MoveX(OsbEasing.OutExpo, 68056, 68409, 
                block.PositionAt(68056).X, 
                right ? Constants.xCeil : Constants.xFloor
            );
            block.Fade(68409, 0);
        }

        private void ExpoTransition(double start, double end) {
            int i, numBars = 42;
            double j, scale = Constants.height / numBars;
            OsbSprite bar;
            for(i = 0; i < numBars; i++) {
                bar = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.TopLeft);
                bar.Color(start, Constants.white);
                bar.Move(start, Constants.xFloor, i*scale);
                bar.Fade(start, 1);
                bar.Fade(end, 0);
                for(j = start; j < end; j += Constants.beatLength/8) {
                    double amount = Random(40, 300);
                    double toScale = Math.Min(bar.ScaleAt(j - Constants.beatLength/2).X + amount, Constants.width);
                    if (j + Constants.beatLength/2 > end) toScale = Constants.width;
                    bar.ScaleVec(j, toScale, scale);
                }
            }
        }
    }
}
