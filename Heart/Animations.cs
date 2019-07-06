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
    public class Animations : StoryboardObjectGenerator
    {
        public override void Generate()
        {
		    var layer = GetLayer("Animations");
            Vector2 squareOrigin = new Vector2(30, 270);
            
            // OsbSprite square2 = layer.CreateSprite("sb/boxy.png", OsbOrigin.Centre, new Vector2(100, 257));
            // square2.Fade(45821, 1);
            // square2.Scale(OsbEasing.InQuart, 45821, 46527, 0.0, 0.2);
            // square2.Rotate(45821, 46527, Math.PI/4,  -Math.PI/32);
            // square2.Fade(48644, 0);
            int i;
            int numSquares = 21;
            double squareStart = 45821;
            double squareEnd = 47939;
            for (i = 0; i < numSquares; i++) {
                OsbSprite square = layer.CreateSprite("sb/boxy.png", OsbOrigin.Centre, squareOrigin);
                square.Fade(squareStart, 1); 
                square.Scale(OsbEasing.OutBack, squareStart, squareEnd, 0.02, 0.2);
                square.Move(OsbEasing.InExpo, squareStart, squareEnd, square.PositionAt(squareStart), squareOrigin.X + i*29.4, squareOrigin.Y - i*3);
                square.Rotate(squareStart, squareEnd, Math.PI/4,  -Math.PI/32);
                square.Fade(48644, 0);
            }
        }
    }
}
