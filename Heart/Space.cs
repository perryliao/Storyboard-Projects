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
    public class Space : StoryboardObjectGenerator
    {
        [Configurable]
        public double startTime = 11586;

        [Configurable]
        public double endTime = 23233;

        private double beatLength = 706;
        private double particleDuration = 1000;
        private double particleAmount = 100;
        public override void Generate()
        {
		    var layer = GetLayer("Space");

            int numStars = 400;
            int i;

            for (i = 0; i < numStars; i++) {
                OsbSprite circ = createStar(startTime, endTime);
                circ.Move(startTime, endTime*3, circ.PositionAt(startTime), calculateEndPoint(circ.PositionAt(startTime).X, circ.PositionAt(startTime).Y));
                var tmp = circ.PositionAt(endTime);

                circ.Move(startTime, endTime, circ.PositionAt(startTime), tmp);
                // circ.Move(startTime, startTime + beatLength, circ.PositionAt(startTime), new Vector2(Mathf.PerlinNoise(0, startTime), *2 - 1, 0));
            }

            using (OsbSpritePool pool = new OsbSpritePool(layer, "sb/Pool 2/cir2.png", OsbOrigin.Centre, (sprite, startTime, endTime) =>
            {
                // This action runs for every sprite created from the pool, after all of them are created (AFTER the for loop below).
                // It is intended to set states common to every sprite:
                
            }))
            {
                double timeStep = particleDuration / particleAmount;
                double start;
                for (start = startTime; start <= endTime - particleDuration; start += timeStep) {
                    // This is where sprites are created from the pool.
                    // Commands here are specific to each sprite.
                    double finish = start + particleDuration;
                    OsbSprite sprite = pool.Get(startTime, endTime);

                    int startX = Random(-107, 747);
                    int startY = Random(0, 480);
                    sprite.Move(OsbEasing.InCirc, start, finish, new Vector2(startX, startY), calculateEndPoint(startX, startY));

                    double fadeInTime = start + (particleDuration * 0.1);
                    sprite.Fade(OsbEasing.OutCirc, start, fadeInTime, 0, 0.9);
                    sprite.Fade(OsbEasing.InOutCirc, fadeInTime, finish, 0.9, 0);
                    
                    double scale = (double) Random(1, 30)/1000;
                    sprite.Scale(start, finish, scale, scale/10);
                }
            }
        }

        /// <summary>Creates a static star object in the scene</summary>
        /// <param name="sTime">Starting time of the object</param>
        /// <param name="eTime">Ending time of the object</param>
        ///
        private OsbSprite createStar(double sTime, double eTime) {
            OsbSprite dot = GetLayer("Space").CreateSprite("sb/Pool 2/dot.png", OsbOrigin.Centre, new Vector2(Random(-307, 947), Random(-100, 580)));
            dot.Scale(sTime, (double) Random(0,100)/800);
            dot.Fade(OsbEasing.InQuad, sTime, sTime + beatLength/2, 0, 1 );
            dot.Fade(OsbEasing.InQuad, eTime - beatLength/2, eTime, 1, 0 );
            return dot;
        }

        /// <summary>Calculates the endpoint of the current sprite, given its start position</summary>
        /// <param name="spriteX">x coordinate of the object</param>
        /// <param name="spriteY">y coordinate of the object</param>
        ///
        private Vector2 calculateEndPoint(float spriteX, float spriteY) {
            // TODO: calculate end point of sprite based on given coordinates
            Vector2 origin = new Vector2(320, 240);
            int radius = 25;
            
            Log("sX: "+ spriteX.ToString() + " xY: " + spriteY.ToString());
            // Formula of circle: (x-h)^2 + (y-k)^2 = r^2
            float h = origin.X;
            float k = origin.Y;
            // Formula of line: y = m*x + b
            float m = (k - spriteY)/(h - spriteX);
            float b = k - m*h;

            Log("h: "+ h.ToString() + " k: " + k.ToString());
            Log("m: "+ m.ToString() + " b: " + b.ToString());
            // Formula for end point: intersect of the 2 equations...
            // (x-h)^2 + (y-k)^2 = r^2 ; where y = m*x + b
            // (x-h)^2 + ( m*x + b - k)^2 = r^2
            // (x^2 - 2*x*h + h^2) + (m^2*x^2 + 2*m*x*(b-k) + (b - k)^2) = r^2
            // ...
            // x^2*(m^2 + 1) + x*(2*h + 2*m*b - 2*m*k) + (b-k)^2 = r^2
            // use quadratic equation here...

            double quadA = Math.Pow(m, 2) + 1;
            double quadB = 2*m*(b-k) - 2*h;
            double quadC = Math.Pow(h, 2) + Math.Pow((b-k), 2) - Math.Pow(radius, 2);
            Log("a: "+ quadA.ToString() + " b: " + quadB.ToString() + " c: " + quadC.ToString());

            double x1 = (-1*quadB + Math.Sqrt(Math.Pow(quadB, 2) - 4 * quadA * quadC))/(2*quadA);
            double x2 = (-1*quadB - Math.Sqrt(Math.Pow(quadB, 2) - 4 * quadA * quadC))/(2*quadA);

            Log("x1: "+ x1.ToString() + " x2: " + x2.ToString());
            Log("");

            Vector2 ret = new Vector2(320, 240);
            return ret;
        }
    }
}
