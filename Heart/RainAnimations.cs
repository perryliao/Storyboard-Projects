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
            moveAroundPointWithAcceleration(circle, 34880, (36644 - 34880) / 2, origin);
            moveAroundPointWithDeceleration(circle, (36644 - 34880) / 2, 36644, origin);
        }

        /// <summary>Move a sprite around a point while accelerating (Similar to Easing.In)</summary>
        /// <param name="sprite">The sprite to animate</param>
        /// <param name="startTime">The starting time of the animation</param>
        /// <param name="endTime">The ending time of the animation</param>
        /// <param name="point">The point to revolve the sprite around, default center (320, 240)</param>
        private void moveAroundPointWithAcceleration(OsbSprite sprite, double startTime, double endTime, Vector2 point) {
            // TODO implement me
        }

        /// <summary>Move a sprite around a point while decelerating (Similar to Easing.Out)</summary>
        /// <param name="sprite">The sprite to animate</param>
        /// <param name="startTime">The starting time of the animation</param>
        /// <param name="endTime">The ending time of the animation</param>
        /// <param name="point">The point to revolve the sprite around</param>
        private void moveAroundPointWithDeceleration(OsbSprite sprite, double startTime, double endTime, Vector2 point) {
            // TODO implement me
        }
    }
}
