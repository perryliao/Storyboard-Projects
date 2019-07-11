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
    public class RainAnimations : StoryboardObjectGenerator
    {
        private Vector2 origin = new Vector2(320, 240);
        public override void Generate()
        {
		    StoryboardLayer layer = GetLayer("RainAnimations");
            OsbSprite circle = layer.CreateSprite("sb/Pool 1/cir.png", OsbOrigin.Centre, new Vector2(320, -100));
            
            circle.Scale(34527, 0.15 );
            circle.Fade(34527, 1);
            circle.Fade(37350, 0);

            circle.Move(OsbEasing.OutQuad, 34527, 34880, circle.PositionAt(34527), 320, 100);
            moveAroundPointWithAcceleration(circle, 34880, (36644 - 34880) / 2, origin, 3*Math.PI/4);
            moveAroundPointWithDeceleration(circle, (36644 - 34880) / 2, 36644, origin, 3*Math.PI/4);
        }

        /// <summary>Move a sprite around a point while accelerating (Similar to Easing.In)</summary>
        /// <param name="sprite">The sprite to animate</param>
        /// <param name="startTime">The starting time of the animation</param>
        /// <param name="endTime">The ending time of the animation</param>
        /// <param name="p">The point to revolve the sprite around</param>
        /// <param name="angle">Angle in radians to rotate</param>
        private void moveAroundPointWithAcceleration(OsbSprite sprite, double startTime, double endTime, Vector2 p, double angle) {
            // TODO implement me
            double duration = endTime - startTime; 
            Vector2 sP = new Vector2(sprite.PositionAt(startTime).X, sprite.PositionAt(startTime).Y);
            float r = (origin - sP).Length;
            // a = v/t = d/t^2

            double l = 2*Math.Sin(angle/2) * r; 
            // l = sqrt((x - sP.X)^2 + (y - sP.Y)^2 ) => l^2 = (x - sP.X)^2 + (y - sP.Y)^2 => x^2 - 2*x*sP.X + sP.X^2 + y^2 - 2*y*sP.Y + sP.Y^2 - l^2 = 0 
            // r = sqrt((x - p.X)^2 + (y - p.Y)^2 )   => r^2 = (x - p.X)^2 + (y - p.Y)^2   => x^2 - 2*x*p.X + p.X^2 + y^2 - 2*y*p.Y + p.Y^2 - r^2 = 0 
            // Subtract eqations together...
            // 2*x*sP.X - 2*x*p.X + p.X^2 - sP.X^2 - 2*y*p.Y + 2*y*sP.Y + p.Y^2 + sP.Y^2 + l^2 - r^2 = 0
            double C = Math.Pow(p.X, 2) - Math.Pow(sP.X, 2) + Math.Pow(p.Y, 2) + Math.Pow(sP.Y, 2) - Math.Pow(l, 2) + Math.Pow(r, 2); // constants
            Log(C.ToString());
            // 2*x*sP.X - 2*x*p.X + 2*y*sP.Y - 2*y*p.Y + C = 0
            // x(2*sP.X - 2*p.X) + y(2*sP.Y - 2*p.Y) + C = 0
            // y = (x(2*sP.X - 2*p.X) + C)/-(2*sP.Y - 2*p.Y)
            float m = -(sP.X - p.X)/(sP.Y - p.Y);
            Log((sP.X - p.X).ToString() +" " + (sP.Y - p.Y).ToString());
            float b = (float) C/(2*sP.Y - 2*p.Y);
            // Use this line equation to find intersects with the circle with radius = r
            // Formula for circle: (x-h)^2 + (y-k)^2 = r^2
            Vector2 dest = getNearestIntersectPoint(m, b, p.X, p.Y, r);
            Log(dest.ToString());
        }

        private Vector2 getNearestIntersectPoint(float m, float b, float h, float k, float r) {
            Log(m.ToString() + " " + b.ToString() + " " + h.ToString() + " " + k.ToString() + " " + r.ToString()); 
            double quadA = Math.Pow(m, 2) + 1;
            double quadB = 2*m*(b-k) - 2*h;
            double quadC = Math.Pow(h, 2) + Math.Pow((b-k), 2) - Math.Pow(r, 2);
            float x1 = (float) ((-1*quadB + Math.Sqrt(Math.Pow(quadB, 2) - 4 * quadA * quadC))/(2*quadA));
            float x2 = (float) ((-1*quadB - Math.Sqrt(Math.Pow(quadB, 2) - 4 * quadA * quadC))/(2*quadA));

            // Plug x coordinates back to origin line equation
            float y1 = (float) m*x1 + b;
            float y2 = (float) m*x2 + b;

            Vector2 o = new Vector2(h, k);
            Vector2 p1 = new Vector2(x1, y1);
            Vector2 p2 = new Vector2(x2, y2);

            // Pick the point closest to the sprite
            return (o-p1).Length < (o-p2).Length ? p1 : p2;
        }

        /// <summary>Move a sprite around a point while decelerating (Similar to Easing.Out)</summary>
        /// <param name="sprite">The sprite to animate</param>
        /// <param name="startTime">The starting time of the animation</param>
        /// <param name="endTime">The ending time of the animation</param>
        /// <param name="point">The point to revolve the sprite around</param>
        /// <param name="angle">Angle in radians to rotate</param>
        private void moveAroundPointWithDeceleration(OsbSprite sprite, double startTime, double endTime, Vector2 point, double angle) {
            // TODO implement me
        }

        /// <summary>Calculate which of the 2 points (p1,p2) is closest to p0</summary>
        /// <param name="p0">The point of interest</param>
        /// <param name="p1">One of the points to check</param>
        /// <param name="p2">The other point to check</param>
        /// <returns>The point closer to p0</returns>
        private Vector2 findClosestPoint(Vector2 p0, Vector2 p1, Vector2 p2) {
            return (p0-p1).Length < (p0-p2).Length ? p1 : p2;
        }
    }
}
