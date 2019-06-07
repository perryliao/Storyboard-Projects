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
		    var layer = GetLayer("tileBar");
		    OsbSprite blackBar = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.CentreLeft);
            OsbSprite whiteBar = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.CentreLeft, new Vector2(-100 , 240));
            OsbSprite smallWhiteBar = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.CentreLeft, new Vector2(width * 0.85f - 100 , 240));

            whiteBar.Color(startTime, 247, 230, 213);
            whiteBar.ScaleVec(startTime, 44939, 0, barHeight, width * 0.1, barHeight);
            whiteBar.ScaleVec(OsbEasing.OutExpo, 44939, 45468, whiteBar.ScaleAt(44939), width * 0.85, barHeight);
            whiteBar.ScaleVec(OsbEasing.OutExpo, 45468, endTime, whiteBar.ScaleAt(45468), width * 0.6, barHeight);
            whiteBar.Fade(OsbEasing.InExpo, 45468, endTime, whiteBar.OpacityAt(45115), 0);

            blackBar.Color(45468, 0,0,0);
            blackBar.ScaleVec(endTime, width / 2, barHeight);
            blackBar.Fade(OsbEasing.InExpo, 45115, endTime, blackBar.OpacityAt(45115), 0);

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
                square = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.TopLeft, new Vector2(width * 0.6f - 100, (float) (height/2 - barHeight / 2 + (barHeight - squareHeight) * rnd.Next(0, 100) / 100 )));
                square.Color(squareStartTime, 247, 230, 213);
                square.ScaleVec(squareStartTime, endTime, 0, squareHeight, rnd.Next(5, 15), squareHeight);

                square.MoveX(OsbEasing.OutCirc, squareStartTime, endTime, square.PositionAt(squareStartTime).X, square.PositionAt(squareStartTime).X + rnd.Next(0, 130) );
                square.Fade(OsbEasing.InExpo, 45468, endTime, square.OpacityAt(45115), 0);
            }
        }
    }
}
