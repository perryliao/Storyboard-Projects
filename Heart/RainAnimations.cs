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
		    var layer = GetLayer("RainAnimations");
            OsbSprite circle = layer.CreateSprite("sb/Pool 1/cir.png", OsbOrigin.Centre, new Vector2(320, -100));
            
            circle.Scale(34527, 0.15 );
            circle.Fade(34527, 1);
            circle.Fade(37350, 0);

            circle.Move(OsbEasing.OutQuad, 34527, 34880, circle.PositionAt(34527), 320, 100);
            moveAroundPointWithAcceleration(circle, 34880, (36644 - 34880) / 2, origin, Math.PI/4);
            moveAroundPointWithDeceleration(circle, (36644 - 34880) / 2, 36644, origin, Math.PI);
        }

        /// <summary>Move a sprite around a point while accelerating (Similar to Easing.In)</summary>
        /// <param name="sprite">The sprite to animate</param>
        /// <param name="startTime">The starting time of the animation</param>
        /// <param name="endTime">The ending time of the animation</param>
        /// <param name="point">The point to revolve the sprite around</param>
        /// <param name="angle">Angle in radians to rotate</param>
        private void moveAroundPointWithAcceleration(OsbSprite sprite, double startTime, double endTime, Vector2 point, double angle) {
            // TODO implement me
            double duration = endTime - startTime; 
            float radius = (origin - new Vector2(sprite.PositionAt(startTime).X, sprite.PositionAt(startTime).Y)).Length;
            // a = v/t = d/t^2
            // Formula for circle: (x-h)^2 + (y-k)^2 = r^2

            double len = 2*Math.Sin(angle/2) * radius;
            Log("angle = " + ( angle/2).ToString());
            Log("sin = " + Math.Sin(angle/2).ToString());
            Log("len = " + len.ToString());

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
