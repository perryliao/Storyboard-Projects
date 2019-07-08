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
        public Color4 white = new Color4(1, 1, 1, 1f);
        public Color4 black = new Color4((float)26/255, (float)26/255, (float)26/255, 1f);
        public Color4 red = new Color4((float)225/255, (float)9/255, (float)11/255, 1f);

        public override void Generate()
        {
		    StoryboardLayer layer = GetLayer("Animations");
            
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
                    circle.Scale(OsbEasing.OutQuart, 48997, 49350, 0.2, 0.05 + 0.017 * Math.Pow(i, 4)/1050 );
                    circle.Scale(OsbEasing.In, 49350, 50409, circle.ScaleAt(49350).X, circle.ScaleAt(49350).X - 0.02 );
                    circle.Scale(OsbEasing.InExpo, 50409, 50762, circle.ScaleAt(50409).X, 4 );
                    circle.Fade(OsbEasing.InOutExpo, 50409, 50762, 1, 0);
                }
            }

            circle = layer.CreateSprite("sb/Pool 3/Animation_1/Ellipse21.png", OsbOrigin.Centre);
            circle.Fade(OsbEasing.OutExpo, 48997, 49350, 0, 1);
            circle.Color(48997, 1,1,1);
            circle.Scale(OsbEasing.InOutQuart, 48997, 49350, 0.2, 1.2 );
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
            
            ///////////////////////////////////////////////
            // square in square part 2
            ///////////////////////////////////////////////
            OsbSprite boxyCirc = layer.CreateSprite("sb/Pool 1/cir.png", OsbOrigin.Centre);
            OsbSprite boxOut = layer.CreateSprite("sb/boxy.png", OsbOrigin.Centre);
            OsbSprite boxMid = layer.CreateSprite("sb/boxy.png", OsbOrigin.Centre);
            OsbSprite boxIn = layer.CreateSprite("sb/boxy.png", OsbOrigin.Centre);
            OsbSprite boxInIn = layer.CreateSprite("sb/boxy.png", OsbOrigin.Centre);

            boxOut.Fade(OsbEasing.InCirc, 53409, 53762, 0, 1);
            boxOut.Scale(OsbEasing.InExpo, 53409, 53762, 1, 0.53);
            boxOut.Rotate(OsbEasing.InExpo, 53409, 53762, Math.PI*3/4, Math.PI/2);
            boxOut.Color(OsbEasing.InExpo, 53409, 53762, white, black);
            boxOut.Fade(55350, 0);

            boxMid.Fade(OsbEasing.InCirc, 52174, 52527, 0, 1);
            boxMid.Scale(OsbEasing.InExpo, 52174, 52527, 0.75, 0.345);
            boxMid.Rotate(OsbEasing.InExpo, 52174, 52527, 0, Math.PI/4);
            boxMid.Color(OsbEasing.InExpo, 53409, 53762, white, black);
            boxMid.Fade(54821, 0); //54821

            boxIn.Fade(OsbEasing.InCirc, 51644, 51997, 0, 1);
            boxIn.Scale(OsbEasing.InExpo, 51644, 51997, 0.5, 0.225);
            boxIn.Rotate(OsbEasing.InExpo, 51644, 51997, Math.PI*3/4, Math.PI/2);
            boxIn.Color(OsbEasing.InExpo, 53409, 53762, white, black);
            boxIn.Fade(54292, 0); 

            boxInIn.Fade(OsbEasing.InCirc, 51115, 51468, 0, 1);
            boxInIn.Scale(OsbEasing.InExpo, 51115, 51468, 0.25, 0.149);
            boxInIn.Rotate(OsbEasing.InExpo, 51115, 51468, 0, Math.PI/4);
            boxInIn.Color(OsbEasing.InExpo, 53409, 53762, white, black);
            boxInIn.Fade(57115, 0);
            boxInIn.Fade(OsbEasing.InExpo, 53409, 53674, 1, 0);

            boxyCirc.Fade(53409, 1);
            boxyCirc.Color(53409, white);
            boxyCirc.Scale(OsbEasing.InExpo, 53409, 53762, 0, 0.8);
            boxyCirc.Color(OsbEasing.InExpo, 54115, 54292, white, red);
            boxyCirc.Fade(57115, 0);

            boxBreak(54292, 54821, 66.4, 3);
            boxBreakDiag(54821, 55350, 143.8, 3.8);
            boxBreak(55350, 57115, 156.5, 5.2); 
        }

        private void boxBreak(double start, double end, double distance, double width) {
            boxBreakDiagHelper(start, end, -distance, distance, width, 0, true);
            boxBreakDiagHelper(start, end, -distance, -distance, width, 0, true);
            boxBreakDiagHelper(start, end, -distance, -distance, width, Math.PI/2, false);
            boxBreakDiagHelper(start, end, distance, -distance, width, Math.PI/2, false);
        }
        
        private void boxBreakDiag(double start, double end, double distance, double width) {
            boxBreakDiagHelper(start, end, 0, distance, width, -Math.PI/4, true);
            boxBreakDiagHelper(start, end, -distance, 0, width, -Math.PI/4, true);
            boxBreakDiagHelper(start, end, 0, -distance, width, Math.PI/4, false);
            boxBreakDiagHelper(start, end, -distance, 0, width, Math.PI/4, false);
        }

        private void boxBreakDiagHelper(double start, double end, double xDisplacement, double yDisplacement, double width, double angle, bool vert ) {
            StoryboardLayer layer = GetLayer("Animations");
            OsbSprite l;
            double moveRange = width*2, accumulatedLength = 0, currentLength;
            bool stopFlag = false;

            double angleLength = angle % Math.PI == Math.PI/2 ? Math.Sin(angle) : Math.Cos(angle);
            double angleLengthInv = angle % Math.PI == Math.PI/2 ? Math.Cos(angle) : Math.Sin(angle);
            double length = Math.Max(Math.Abs(xDisplacement), Math.Abs(yDisplacement)) * angleLength; 
            while (!stopFlag) {
                l = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.CentreLeft, new Vector2((float)(320 + xDisplacement + (vert ? accumulatedLength * angleLength : accumulatedLength * angleLengthInv)), (float)(240 + yDisplacement + (!vert ? accumulatedLength * angleLength : accumulatedLength * angleLengthInv))));
                l.Fade(start, 1);
                l.Color(start, black);

                currentLength = Random(8, 25);
                if (currentLength + accumulatedLength > length * 2 + 2) {
                    currentLength = length*2 - accumulatedLength + 2;
                    stopFlag = true;
                }
                l.ScaleVec(start, currentLength, width);
                l.Rotate(start, end, angle, angle + Random(-Math.PI/6, Math.PI/6));
                l.Move(start, end, l.PositionAt(start), l.PositionAt(start).X + Random(-moveRange, moveRange), l.PositionAt(start).Y + Random(-moveRange, moveRange));
                l.Fade(end, 0);
                accumulatedLength += currentLength;
            }
        }
    }
}
