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
            
            OsbSprite square;
            int i, rng = -1, prevRng = -1, numSquares = 21;
            double squareStart = 45821, squareEnd = 47939;

            for (i = 0; i < numSquares; i++) {
                square = layer.CreateSprite("sb/boxy.png", OsbOrigin.Centre, squareOrigin);
                square.Fade(squareStart, 1); 
                square.Scale(OsbEasing.OutBack, squareStart, squareEnd, 0.02, 0.2);
                square.Move(OsbEasing.InExpo, squareStart, squareEnd, square.PositionAt(squareStart), squareOrigin.X + i*29.4, squareOrigin.Y - i*3);
                square.Move(OsbEasing.OutQuint, 47939, 48292, square.PositionAt(47939), square.PositionAt(47939).X + 5, square.PositionAt(47939).Y - 0.5);
                
                if (i != 0 && i != numSquares - 1) {
                    while (rng == prevRng ) {
                        rng = Random(3);
                    }
                    if (rng != 0)
                        square.Move(OsbEasing.OutQuint, 48292, 48644, square.PositionAt(48292), 
                            rng == 1 ? square.PositionAt(48292).X - 13 : square.PositionAt(48292).X  + 13, 
                            rng == 1 ? square.PositionAt(48292).Y - 80 : square.PositionAt(48292).Y + 80
                        );
                    prevRng = rng;
                }

                square.Rotate(squareStart, squareEnd, Math.PI/4,  -Math.PI/32);
                square.Fade(OsbEasing.InOutQuart, 47939, 48644, 1, 0);
            }

        }
    }
}
