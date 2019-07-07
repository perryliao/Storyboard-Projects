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
            
            ///////////////////////////////////////////////
            // Square effect
            ///////////////////////////////////////////////
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

            ///////////////////////////////////////////////
            // Triangle/circle effect
            ///////////////////////////////////////////////

            // circles
            int numCircles = 15;
            OsbSprite circle;
            for( i = 0; i < numCircles; i++) {
                if (i != 13 && i != 11 ) {
                    circle = layer.CreateSprite("sb/Pool 1/hollow cir.png", OsbOrigin.Centre);
                    circle.Fade(OsbEasing.OutExpo, 48997, 49350, 0, 1);
                    circle.Color(48997, 1,1,1);
                    circle.Scale(OsbEasing.OutBack, 48997, 49350, 0.2, 0.05 + 0.017 * Math.Pow(i, 4)/1050 );
                    circle.Scale(OsbEasing.In, 49350, 50409, circle.ScaleAt(49350).X, circle.ScaleAt(49350).X - 0.02 );
                    circle.Scale(OsbEasing.InExpo, 50409, 50762, circle.ScaleAt(50409).X, 4 );
                    circle.Fade(OsbEasing.InOutExpo, 50409, 50762, 1, 0);
                }
            }

            circle = layer.CreateSprite("sb/Pool 3/Animation_1/Ellipse21.png", OsbOrigin.Centre);
            circle.Fade(OsbEasing.OutExpo, 48997, 49350, 0, 1);
            circle.Color(48997, 1,1,1);
            circle.Scale(OsbEasing.OutBack, 48997, 49350, 0.2, 1.2 );
            circle.Scale(OsbEasing.In, 49350, 50409, circle.ScaleAt(49350).X, circle.ScaleAt(49350).X - 0.02 );
            circle.Scale(OsbEasing.InExpo, 50409, 50762, circle.ScaleAt(50409).X, 4 );
            circle.Fade(OsbEasing.InOutExpo, 50409, 50762, 1, 0);

            // triangle
            float hypotenuse = 130;
            float split = (float) Math.Cos(Math.PI/6) * hypotenuse;
            float deltaY = (float)  Math.Sin(Math.PI/6) * hypotenuse;
            Vector2 BottomMid = new Vector2(320, 240 + hypotenuse); // A
            Vector2 TopLeft = new Vector2(320 - split, 240 - deltaY); // B
            Vector2 TopRight = new Vector2(320 + split, 240 - deltaY); // C

            OsbSprite AB = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.CentreRight, new Vector2((float)(TopLeft.X - Math.Tan(Math.PI/6)*TopLeft.Y), 0));
            OsbSprite AC = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.CentreRight, new Vector2((float)( BottomMid.X - (480 - BottomMid.Y)/Math.Tan(Math.PI/3) ), 480));
            OsbSprite BC = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.CentreLeft, new Vector2(747, TopLeft.Y));

            OsbSprite[] Triangle = new OsbSprite[3] { AB, BC, AC };   

            AB.Rotate(48468, Math.PI/3);
            AC.Rotate(48468, -Math.PI/3);

            foreach( OsbSprite line in Triangle ) {
                line.Fade(OsbEasing.InExpo, 48468, 48644, 0, 1);
                line.ScaleVec(48468, 50762, 350, 3, 226, 3);
                line.ScaleVec(OsbEasing.InOutExpo, 50762, 51468, line.ScaleAt(50762), 0, 2);
                line.Fade(OsbEasing.InOutQuart, 50762, 51468, 1, 0);
            }

            AB.Move(OsbEasing.InOutExpo, 48468, 49350, AB.PositionAt(48468), BottomMid.X, BottomMid.Y);
            AC.Move(OsbEasing.InOutExpo, 48468, 49350, AC.PositionAt(48468), TopRight.X, TopRight.Y);
            BC.Move(OsbEasing.InOutExpo, 48468, 49350, BC.PositionAt(48468), TopLeft.X, TopLeft.Y);

            AB.Move(OsbEasing.InOutExpo, 50762, 51468, AB.PositionAt(50762), BottomMid.X + Math.Cos(Math.PI/3) * ( 480 - BottomMid.Y), 480);
            AC.Move(OsbEasing.InOutExpo, 50762, 51468, AC.PositionAt(50762), TopRight.X + TopRight.Y/Math.Tan(Math.PI/3), 0);
            BC.Move(OsbEasing.InOutExpo, 50762, 51468, BC.PositionAt(50762), -107, TopLeft.Y);
            
        }
    }
}
