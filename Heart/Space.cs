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

        private double particleDuration = 1000;
        private double particleAmount = 20;
        public override void Generate()
        {
		    StoryboardLayer layer = GetLayer("Space");
            double i, j;

            for (i = 50; i < 640; i++) {
                double iterations = 1;
                if (i > 200) iterations = 3;
                else if (i > 100) iterations = 2;
                
                for (j = 0; j < iterations; j++) {
                    Vector2 pos = findStartingPosition( i );
                    OsbSprite circ = layer.CreateSprite("sb/Pool 2/dot.png", OsbOrigin.Centre, pos);
                    circ.Scale(startTime, (double) Random(0,100)/800);
                    circ.Fade(OsbEasing.InQuad, startTime, startTime + Constants.beatLength/2, 0, 1 );
                    circ.Fade(OsbEasing.InQuad, endTime - Constants.beatLength/2, endTime, 1, 0 );
                    circ.Move(startTime, endTime, circ.PositionAt(startTime), calculateEndPoint(pos.X, pos.Y, i));
                }
            }

            // using (OsbSpritePool pool = new OsbSpritePool(layer, "sb/Pool 2/cir2.png", OsbOrigin.Centre, (sprite, startTime, endTime) =>
            // {
            //     // This action runs for every sprite created from the pool, after all of them are created (AFTER the for loop below).
            //     // It is intended to set states common to every sprite:
                
            // }))
            // {
                double timeStep = particleDuration / particleAmount;
                double start;
                for (start = startTime; start <= endTime - particleDuration; start += timeStep) {
                    // This is where sprites are created from the pool.
                    // Commands here are specific to each sprite.
                    double finish = start + particleDuration;
                    OsbSprite sprite = layer.CreateSprite("sb/Pool 2/cir2.png", OsbOrigin.Centre);

                    sprite.StartLoopGroup(start, (int)((endTime - start)/particleDuration));
                    int startX = Random(-107, 747);
                    int startY = Random(0, 480);
                    sprite.Move(OsbEasing.InCirc, 0, particleDuration, new Vector2(startX, startY), calculateEndPoint(startX, startY, 25));

                    double fadeInTime = 0 + (particleDuration * 0.2);
                    sprite.Fade(OsbEasing.InSine, 0, fadeInTime, 0, 0.9);
                    sprite.Fade(OsbEasing.InOutCirc, fadeInTime, particleDuration, 0.9, 0);
                    
                    double scale = (double) Random(1, 30)/1000;
                    sprite.Scale(0, particleDuration, scale, scale/10);
                    sprite.EndGroup();
                }
            // }
        }

        /// <summary>Creates a static star object in the scene</summary>
        /// <param name="sTime">Starting time of the object</param>
        /// <param name="eTime">Ending time of the object</param>
        ///
        // private OsbSprite createStar(double sTime, double eTime) {
        //     OsbSprite dot = GetLayer("Space").CreateSprite("sb/Pool 2/dot.png", OsbOrigin.Centre, new Vector2(Random(-307, 947), Random(-100, 580)));
        //     dot.Scale(sTime, (double) Random(0,100)/800);
        //     dot.Fade(OsbEasing.InQuad, sTime, sTime + beatLength/2, 0, 1 );
        //     dot.Fade(OsbEasing.InQuad, eTime - beatLength/2, eTime, 1, 0 );
        //     return dot;
        // }

        /// <summary>Calculates the starting point of a star, given the radius away from the center it should be at the end</summary>
        /// <param name="radius">Radius of the circle that the end point will be on</param>
        private Vector2 findStartingPosition(double radius) {
            Vector2 o = new Vector2(320, 240);
            Vector2 ret = new Vector2(Random(-307, 947), Random(-100, 580));
            while ((o-ret).Length < radius) {
                ret = new Vector2(Random(-307, 947), Random(-100, 580));
            }
            return ret;
        } 

        /// <summary>Calculates the endpoint of the current sprite, given its start position</summary>
        /// <param name="spriteX">x coordinate of the object</param>
        /// <param name="spriteY">y coordinate of the object</param>
        /// <param name="radius">Radius of the circle that the end point will be on</param>
        ///
        private Vector2 calculateEndPoint(float spriteX, float spriteY, double radius) {
            Vector2 origin = new Vector2(320, 240);
            
            // Formula of circle: (x-h)^2 + (y-k)^2 = r^2
            float h = origin.X;
            float k = origin.Y;
            // Formula of line: y = m*x + b
            float m = (k - spriteY)/(h - spriteX);
            float b = k - m*h;

            // Formula for end point: intersect of the 2 equations...
            // (x-h)^2 + (y-k)^2 = r^2 ; where y = m*x + b
            // ...
            // x^2*(m^2 + 1) + x*(2*m*(b - k) - 2*h) + (h^2 + (b-k)^2 - r^2) = 0
            // use quadratic equation here...

            double quadA = Math.Pow(m, 2) + 1;
            double quadB = 2*m*(b-k) - 2*h;
            double quadC = Math.Pow(h, 2) + Math.Pow((b-k), 2) - Math.Pow(radius, 2);
            float x1 = (float) ((-1*quadB + Math.Sqrt(Math.Pow(quadB, 2) - 4 * quadA * quadC))/(2*quadA));
            float x2 = (float) ((-1*quadB - Math.Sqrt(Math.Pow(quadB, 2) - 4 * quadA * quadC))/(2*quadA));

            // Plug x coordinates back to origin line equation
            float y1 = (float) m*x1 + b;
            float y2 = (float) m*x2 + b;

            Vector2 o = new Vector2(spriteX, spriteY);
            Vector2 p1 = new Vector2(x1, y1);
            Vector2 p2 = new Vector2(x2, y2);

            // Pick the point closest to the sprite
            return (o-p1).Length < (o-p2).Length ? p1 : p2;
        }
    }
}
