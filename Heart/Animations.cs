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
        private double beatLength = Constants.beatLength;

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
            
            OsbSprite[] squares = new OsbSprite[numSquares];

            for (i = 0; i < numSquares; i++) {
                squares[i] = layer.CreateSprite("sb/boxy.png", OsbOrigin.Centre, squareOrigin);
                square = squares[i];
                square.Fade(squareStart, 1); 
                square.Color(squareStart, Constants.white);
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
                    circle.Color(48997, Constants.white);
                    circle.Scale(OsbEasing.OutQuart, 48997, 49350, 0.2, 0.05 + 0.017 * Math.Pow(i, 4)/1050 );
                    circle.Scale(OsbEasing.In, 49350, 50409, circle.ScaleAt(49350).X, circle.ScaleAt(49350).X - 0.02 );
                    circle.Scale(OsbEasing.InExpo, 50409, 50762, circle.ScaleAt(50409).X, 4 );
                    circle.Fade(OsbEasing.InOutExpo, 50409, 50762, 1, 0);
                }
            }

            circle = layer.CreateSprite("sb/Pool 3/Animation_1/Ellipse21.png", OsbOrigin.Centre);
            circle.Fade(OsbEasing.OutExpo, 48997, 49350, 0, 1);
            circle.Color(48997, Constants.white);
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
                line.Color(48468, Constants.white);
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
            boxOut.Color(OsbEasing.InExpo, 53409, 53762, Constants.white, Constants.black);
            boxOut.Fade(55350, 0);

            boxMid.Fade(OsbEasing.InCirc, 52174, 52527, 0, 1);
            boxMid.Scale(OsbEasing.InExpo, 52174, 52527, 0.75, 0.345);
            boxMid.Rotate(OsbEasing.InExpo, 52174, 52527, 0, Math.PI/4);
            boxMid.Color(OsbEasing.InExpo, 53409, 53762, Constants.white, Constants.black);
            boxMid.Fade(54821, 0);

            boxIn.Fade(OsbEasing.InCirc, 51644, 51997, 0, 1);
            boxIn.Scale(OsbEasing.InExpo, 51644, 51997, 0.5, 0.225);
            boxIn.Rotate(OsbEasing.InExpo, 51644, 51997, Math.PI*3/4, Math.PI/2);
            boxIn.Color(OsbEasing.InExpo, 53409, 53762, Constants.white, Constants.black);
            boxIn.Fade(54292, 0); 

            boxInIn.Fade(OsbEasing.InCirc, 51115, 51468, 0, 1);
            boxInIn.Scale(OsbEasing.InExpo, 51115, 51468, 0.25, 0.149);
            boxInIn.Rotate(OsbEasing.InExpo, 51115, 51468, 0, Math.PI/4);
            boxInIn.Color(OsbEasing.InExpo, 53409, 53762, Constants.white, Constants.black);
            boxInIn.Fade(57115, 0);
            boxInIn.Fade(OsbEasing.InExpo, 53409, 53674, 1, 0);

            boxyCirc.Fade(53409, 1);
            boxyCirc.Color(53409, Constants.white);
            boxyCirc.Scale(OsbEasing.InExpo, 53409, 53762, 0, 0.8);
            boxyCirc.Color(OsbEasing.InExpo, 54115, 54292, Constants.white, Constants.red);
            boxyCirc.Fade(57115, 0);

            boxBreak(54292, 54821, 66.4, 3);
            boxBreakDiag(54821, 55350, 143.8, 3.8);
            boxBreak(55350, 57115, 156.5, 5.2); 

            ///////////////////////////////////////////////
            // circle matrix effect
            ///////////////////////////////////////////////

            circle = layer.CreateSprite("sb/Pool 1/cir.png", OsbOrigin.Centre);
            double circleStartTime = 57115;
            
            circle.Fade(OsbEasing.OutExpo, circleStartTime, circleStartTime + beatLength/4, 0, 1);
            circle.Scale(OsbEasing.OutBack, circleStartTime, circleStartTime + beatLength/4, 0, 0.2);
            circle.Color(circleStartTime, Constants.black);
            circle.Fade(circleStartTime + beatLength*4, 0);

            OsbSprite arc1 = layer.CreateSprite("sb/Pool 2/Arc-B-R2.png", OsbOrigin.Centre);
            OsbSprite arc2 = layer.CreateSprite("sb/Pool 2/Arc-B-R1.png", OsbOrigin.Centre);

            arc1.Fade(circleStartTime + beatLength * 3/4, 1);
            arc1.Color(circleStartTime + beatLength * 3/4, Constants.black);
            arc1.Scale(circleStartTime + beatLength * 3/4, 0.5);
            arc1.Rotate(OsbEasing.OutExpo, circleStartTime + beatLength * 3/4, circleStartTime + 4*beatLength, 0, Math.PI*3/4);
            arc1.Fade(circleStartTime + beatLength*4, 0);

            arc2.Fade(circleStartTime + beatLength * 3/4, 1);
            arc2.Color(circleStartTime + beatLength * 3/4, Constants.black);
            arc2.Scale(circleStartTime + beatLength * 3/4, 0.6);
            arc2.Rotate(OsbEasing.OutExpo, circleStartTime + beatLength * 3/4, circleStartTime + 4*beatLength, -Math.PI/2, -Math.PI*23.5/16);
            arc2.Fade(circleStartTime + beatLength*4, 0);

            circleRoundedLines(circleStartTime);

            ///////////////////////////////////////////////
            // square diamond effect
            ///////////////////////////////////////////////

            squareStart = 59939;
            
            double timestep = (beatLength/2)/(squares.Length), fadeInTime;

            for (i = 0; i < squares.Length - 1; i++) {
                square = squares[i];
                fadeInTime = squareStart + timestep*i;
                square.Move(fadeInTime, 320, 240);
                square.Color(fadeInTime, Constants.grey);

                square.Fade(OsbEasing.OutCirc, fadeInTime, squareStart + beatLength/2, 0, 0.6);
                square.Fade(OsbEasing.InCirc, squareStart + beatLength/2, squareStart + beatLength, 0.6, 0);

                square.Rotate(OsbEasing.OutCirc, fadeInTime, squareStart + beatLength/2, -Math.PI/4, -Math.PI*3/4);
                square.Rotate(OsbEasing.InCirc, squareStart + beatLength/2, squareStart + beatLength, -Math.PI*3/4, -Math.PI/2);

                square.Scale(OsbEasing.OutCirc, fadeInTime, squareStart + beatLength/2, 0, 0.4);
                square.Scale(OsbEasing.InCirc, squareStart + beatLength/2, squareStart + beatLength, 0.4, 0);
            }

            squareRoundedLines(squareStart);

            square = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.Centre);
            square.Fade(squareStart, 1);
            square.Color(squareStart, Constants.black);
            square.Color(OsbEasing.InOutExpo, squareStart + beatLength*3, squareStart + beatLength*4, Constants.black, Constants.white);
            square.Scale(OsbEasing.OutCirc, squareStart, squareStart + beatLength/2, 0, 150);
            square.Scale(OsbEasing.InCirc, squareStart + beatLength/2, squareStart + beatLength, 150, 110);
            square.Rotate(OsbEasing.OutCirc, squareStart, squareStart + beatLength/2, -Math.PI/4, -Math.PI*3/4);
            square.Rotate(OsbEasing.InCirc, squareStart + beatLength/2, squareStart + beatLength, -Math.PI*3/4, -Math.PI/2);
            square.Fade(squareStart + beatLength*4, 0);

            ///////////////////////////////////////////////
            // kkr sketch 
            ///////////////////////////////////////////////

            OsbSprite kkr2 = layer.CreateAnimation("sb/sketchKkr.png", 2, 150, OsbLoopType.LoopForever, OsbOrigin.Centre);
            // OsbSprite kkr2 = layer.CreateSprite("sb/sketchKkr1.png",  OsbOrigin.Centre);
            kkr2.Fade(62762, 1);
            kkr2.Color(62762, Constants.white);
            kkr2.Scale(62762, 0.8);
            kkr2.Fade(68409, 0);

            OsbSprite cut = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.CentreRight, new Vector2(320 - 100, 240 + 100));
            cut.Fade(65233, 1);
            cut.Rotate(65233, -Math.PI/4);
            cut.ScaleVec(OsbEasing.InQuart, 65233, 65233 + beatLength/2, 0, 1, 100, 1);
            cut.ScaleVec(OsbEasing.OutQuart, 65233 + beatLength/2, 65939, 100, 1, 0, 1);
            cut.Move(OsbEasing.InOutQuart, 65233, 65939, cut.PositionAt(65233), cut.PositionAt(65233).X + 200, cut.PositionAt(65233).Y - 200);
            cut.Fade(65939, 0);
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
                l.Color(start, Constants.black);

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

        private void circleRoundedLines(double circleStartTime) {
            showRoundLine(circleStartTime + 2*beatLength, circleStartTime + 4*beatLength, 0, 10, 185, 163);
            showRoundLine(circleStartTime + 2*beatLength, circleStartTime + 4*beatLength, 0, 9, 125, 192f);
            showRoundLine(circleStartTime + 2*beatLength, circleStartTime + 4*beatLength, 0, 9.5, 185, 298f);
            showRoundLine(circleStartTime + 2*beatLength, circleStartTime + 4*beatLength, 0, 9, 125, 327f);

            showRoundLine(circleStartTime + beatLength*2.5, circleStartTime + 4*beatLength, Math.PI/2, 8.5, 234, 115);
            showRoundLine(circleStartTime + beatLength*2.5, circleStartTime + 4*beatLength, Math.PI/2, 8, 262f, 64);
            showRoundLine(circleStartTime + beatLength*2.5, circleStartTime + 4*beatLength, Math.PI/2, 8.5, 367f, 64);
            showRoundLine(circleStartTime + beatLength*2.5, circleStartTime + 4*beatLength, Math.PI/2, 8.5, 397, 78);
            
            showRoundLine(circleStartTime + beatLength*3, circleStartTime + 4*beatLength, -Math.PI/4, 5, 174, 252);
            showRoundLine(circleStartTime + beatLength*3, circleStartTime + 4*beatLength, -Math.PI/4, 13, 320 - 200, 240 + 200);
            showRoundLine(circleStartTime + beatLength*3, circleStartTime + 4*beatLength, -Math.PI/4, 5, 312, 382);

            OsbSprite t1 = GetLayer("Animations").CreateSprite("sb/t.png", OsbOrigin.BottomRight, new Vector2(320 + 76, 240 - 76));
            OsbSprite t2 = GetLayer("Animations").CreateSprite("sb/t.png", OsbOrigin.BottomRight, new Vector2(320 - 20, 240 + 20));
            OsbSprite rekt = GetLayer("Animations").CreateSprite("sb/1x1.jpg", OsbOrigin.TopCentre, new Vector2(320 + 10, 240 - 10));
            
            t1.Fade(circleStartTime + beatLength*7/2, 1);
            t1.Rotate(circleStartTime + beatLength*7/2, -Math.PI/2);
            t1.Color(circleStartTime + beatLength*7/2, Constants.black);
            t1.ScaleVec(circleStartTime + beatLength*7/2, circleStartTime + beatLength*7/2 + beatLength/6, 0, 0, 1.33, 1.33);
            t1.Fade(circleStartTime + beatLength*4, 0);

            rekt.Fade(circleStartTime + beatLength*7/2 + beatLength/6, 1);
            rekt.Rotate(circleStartTime + beatLength*7/2 + beatLength/6, Math.PI/4);
            rekt.Color(circleStartTime + beatLength*7/2 + beatLength/6, Constants.black);
            rekt.ScaleVec(circleStartTime + beatLength*7/2 + beatLength/6, circleStartTime + beatLength*7/2 + beatLength*11/48, 188, 0, 188, 43);
            rekt.Fade(circleStartTime + beatLength*4, 0);

            t2.Fade(circleStartTime + beatLength*7/2 + beatLength*11/48, 1);
            t2.Rotate(circleStartTime + beatLength*7/2 + beatLength*11/48, Math.PI/2);
            t2.Color(circleStartTime + beatLength*7/2 + beatLength*11/48, Constants.black);
            t2.ScaleVec(OsbEasing.OutExpo, circleStartTime + beatLength*7/2 + beatLength*11/48, 59894, 0, 0, 1.33, 1.33);
            t2.Move(OsbEasing.OutExpo, circleStartTime + beatLength*7/2 + beatLength*11/48, 
                59894, 
                t2.PositionAt(circleStartTime + beatLength*7/2 + beatLength*11/48).X, 
                t2.PositionAt(circleStartTime + beatLength*7/2 + beatLength*11/48).Y, 
                320 - 86 , 
                240 + 86 
            );
            t2.Fade(circleStartTime + beatLength*4, 0);
        }

        private void squareRoundedLines(double squareStartTime) {
            double lineStart = squareStartTime + beatLength;
            showRoundLine(lineStart, squareStartTime + 4*beatLength, -Math.PI/6, 2.9, 320 - 54, 240 - 54);
            showRoundLine(lineStart, squareStartTime + 4*beatLength, Math.PI*5/6, 2.9, 320 + 54, 240 + 54);

            showRoundLine(lineStart + beatLength/2, squareStartTime + 4*beatLength, -Math.PI*5/6, 9, 320  + 240, 240);
            showRoundLine(lineStart + beatLength/2, squareStartTime + 4*beatLength, Math.PI/6, 9, 320  - 240, 240);
            
            showRoundLine(lineStart + beatLength, squareStartTime + 4*beatLength, Math.PI*5/6, 13, 320, 101); 
            showRoundLine(lineStart + beatLength, squareStartTime + 4*beatLength, -Math.PI/6, 13, 320, 379); 
            
            showRoundLine(lineStart + beatLength*3/2, squareStartTime + 4*beatLength, -Math.PI*2.9/9 - Math.PI*3.21/9 + Math.PI, 2.55, 320, 101); 
            showRoundLine(lineStart + beatLength*3/2, squareStartTime + 4*beatLength, -Math.PI*2.9/9 + Math.PI, 2.55, 320, 101); 
            showRoundLine(lineStart + beatLength*3/2, squareStartTime + 4*beatLength, -Math.PI*2.9/9 - Math.PI*3.21/9, 2.55, 320, 379); 
            showRoundLine(lineStart + beatLength*3/2, squareStartTime + 4*beatLength, -Math.PI*2.9/9, 2.55, 320, 379); 
            showRoundLine(lineStart + beatLength*3/2, squareStartTime + 4*beatLength, 0, 6, 320 - 240, 240); 
            showRoundLine(lineStart + beatLength*3/2, squareStartTime + 4*beatLength, Math.PI, 6, 320 + 240, 240); 
        }

        private void showRoundLine(double startTime, double endTime, double angle, double length, float xPos, float yPos) {
            OsbSprite line = GetLayer("Animations").CreateSprite("sb/roundedLine.png", OsbOrigin.CentreLeft, new Vector2(xPos, yPos));
            line.Fade(startTime, 1);
            line.Color(startTime, Constants.black);
            line.Rotate(startTime, angle);
            line.ScaleVec(OsbEasing.OutExpo, startTime, endTime, 0, 0.85, 0.1*length, 0.85);
            line.Fade(endTime, 0);
        }
    }
}
