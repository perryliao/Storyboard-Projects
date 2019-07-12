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
    public class TileBar : StoryboardObjectGenerator
    {
        [Configurable]
        private double barHeight = 20;

        private double startTime = 42997;
        private double endTime = 45821;
        private int width = 854;
        private int height = 480;

        public override void Generate()
        {
		    StoryboardLayer layer = GetLayer("tileBar");
		    OsbSprite blackBar = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.CentreLeft);
            OsbSprite whiteBar = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.CentreLeft, new Vector2(-107 , 240));
            OsbSprite smallWhiteBar = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.CentreLeft, new Vector2(width * 0.85f - 100 , 240));

            whiteBar.Color(startTime, 247, 230, 213);
            whiteBar.ScaleVec(startTime, 44939, 0, barHeight, width * 0.1, barHeight);
            whiteBar.ScaleVec(OsbEasing.OutExpo, 44939, 45468, whiteBar.ScaleAt(44939), width * 0.85, barHeight);
            whiteBar.ScaleVec(OsbEasing.OutExpo, 45468, endTime, whiteBar.ScaleAt(45468), width * 0.6, barHeight);
            whiteBar.Fade(OsbEasing.InExpo, 45468, endTime, whiteBar.OpacityAt(45115), 0);

            blackBar.Color(45468, 0,0,0);
            blackBar.ScaleVec(endTime, width / 2, barHeight);
            blackBar.Fade(OsbEasing.InExpo, 45468, endTime, blackBar.OpacityAt(45115), 0);

            smallWhiteBar.Color(45468, 247, 230, 213);
            smallWhiteBar.ScaleVec(OsbEasing.OutExpo, 45468, 45644, 0, barHeight, width * 0.1, barHeight);
            smallWhiteBar.MoveX(OsbEasing.InOutExpo, 45468, 45644, smallWhiteBar.PositionAt(45468).X, smallWhiteBar.PositionAt(45468).X + width * 0.1);
            smallWhiteBar.Fade(OsbEasing.InExpo, 45115, 45644, smallWhiteBar.OpacityAt(45115), 0);

            int i;
            int numSquares = 25;
            double squareStartTime = 45468;
            OsbSprite square;
            Random rnd = new Random(); 
            
            for (i = 0; i < numSquares; i++) {
                double squareHeight = barHeight * ((float) rnd.Next(5,30) / 100);
                square = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.TopLeft, new Vector2(width * 0.6f - 107, (float) (height/2 - barHeight / 2 + (barHeight - squareHeight) * rnd.Next(0, 100) / 100 )));
                square.Color(squareStartTime, 247, 230, 213);
                square.ScaleVec(squareStartTime, endTime, 0, squareHeight, rnd.Next(5, 15), squareHeight);

                square.MoveX(OsbEasing.OutCirc, squareStartTime, endTime, square.PositionAt(squareStartTime).X, square.PositionAt(squareStartTime).X + rnd.Next(0, 130) );
                square.Fade(OsbEasing.InExpo, 45468, endTime, square.OpacityAt(45115), 0);
            }

            // hard coded squares 
            // square 1
            double fixedHeight = barHeight * 0.45;
            square = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.TopLeft, new Vector2(width * 0.6f - 107, (float) (height/2 - barHeight / 2 + (barHeight - fixedHeight))));
            square.Color(squareStartTime, 247, 230, 213);
            square.ScaleVec(squareStartTime, endTime, 0, fixedHeight, rnd.Next(10, 15), fixedHeight);
            square.MoveX(OsbEasing.OutCirc, squareStartTime, endTime, square.PositionAt(squareStartTime).X, square.PositionAt(squareStartTime).X + rnd.Next(0, 4) );
            square.Fade(OsbEasing.InExpo, 45468, endTime, square.OpacityAt(45115), 0);

            // square 2
            fixedHeight = barHeight * 0.33;
            square = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.TopLeft, new Vector2(width * 0.6f - 107, (float) (height/2 - barHeight / 2 + rnd.Next(0, 4))));
            square.Color(squareStartTime, 247, 230, 213);
            square.ScaleVec(squareStartTime, endTime, 0, fixedHeight, rnd.Next(13, 18), fixedHeight);
            square.MoveX(OsbEasing.OutCirc, squareStartTime, endTime, square.PositionAt(squareStartTime).X, square.PositionAt(squareStartTime).X + rnd.Next(3, 6) );
            square.Fade(OsbEasing.InExpo, 45468, endTime, square.OpacityAt(45115), 0);

        }

        /// <summary>Sets up the square sprites for the trailing bar effect</summary>
        /// <param name="startTime">Time when the square will appear</param>
        /// <param name="endTime">Time when the square will fade out</param>
        /// <param name="movementOverride">If true, set your own movement after calling this method</param>
        /// <param name="squareHeight">Height of the new square,</param>
        /// <returns>The created square sprite.</returns>
        private OsbSprite squareSetUp(double startTime, double endTime, bool movementOverride, double squareHeight) {
            Random rnd = new Random();
            if (squareHeight == -1) { squareHeight = barHeight * ((float) rnd.Next(5,30) / 100); }
            OsbSprite square = GetLayer("TileBar").CreateSprite("sb/1x1.jpg", OsbOrigin.TopLeft, new Vector2(width * 0.6f - 100, (float) (height/2 - barHeight / 2 + (barHeight - squareHeight) * rnd.Next(0, 100) / 100 )));
            square.Color(startTime, 247, 230, 213);
            square.ScaleVec(startTime, endTime, 0, squareHeight, rnd.Next(5, 15), squareHeight);
            square.Fade(OsbEasing.InExpo, 45468, endTime, square.OpacityAt(45115), 0);
            return square;
        }
    }
}
