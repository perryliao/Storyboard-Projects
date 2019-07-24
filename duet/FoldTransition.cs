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
    public class FoldTransition : StoryboardObjectGenerator
    {
        [Configurable]
        public double startTime = 131294; // this should be 1/4 beat before the 1st fold
        [Configurable]
        public int numLines = 10; // this should be 1/4 beat before the 1st fold

        double h = Math.Sqrt(Math.Pow(854, 2) + Math.Pow(480, 2));
        double theta = Math.Tan(480/854);
        StoryboardLayer layer;
        double w;
        public override void Generate()
        {
            w = h/numLines;
            layer = GetLayer("FoldTransition");

            OsbSprite tempBG = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.Centre);
            tempBG.ScaleVec(startTime + Constants.beatLength/4, 854, 480);
            tempBG.Fade(startTime + Constants.beatLength/4, 1);
            tempBG.Fade(startTime + Constants.beatLength*3/4, 0);
            tempBG.Color(startTime + Constants.beatLength/4, Colours.pink);

		    for (int i = 0; i < numLines + 1; i++) {
                Vector2 p = findPos(i);
                OsbSprite bar = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.BottomCentre, p);
                double length = h;

                bar.Rotate(startTime, Math.PI/4);
                bar.ScaleVec(OsbEasing.None, startTime, startTime + Constants.beatLength/4, length, 0, length, w + 3);
                bar.ScaleVec(OsbEasing.None, startTime + Constants.beatLength/4, startTime + Constants.beatLength/2, length, w + 3, length, 0);
                bar.ScaleVec(OsbEasing.None, startTime + Constants.beatLength/2, startTime + Constants.beatLength*3/4, length, 0, length, w + 3);

                bar.Color(startTime, Colours.blue); 
                bar.Color(startTime + Constants.beatLength/2, Colours.white); 
            }
        }

        /// <summary>Helper function for finding the centre of the transition bar</summary>
        private Vector2 findPos(double index) {
            // find width of each line
            float x = (float) ((numLines - index) * w * Math.Cos(theta) * Math.Cos(theta) + Constants.xFloor); // right to left
            float y = (float) (-(Constants.height/Constants.width)*(x - Constants.xFloor) + Constants.height);
            return new Vector2(x, y);
        }
    }
}
