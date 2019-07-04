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
    public class Rain : StoryboardObjectGenerator
    {
        [Configurable]
        public double startTime = 34527;
        [Configurable]
        public double endTime = 42997;
        [Configurable]
        public double particleDuration = 1500;
        [Configurable]
        public double particleAmount = 100;

        private double beatLength = 40174 - 39468;
        public override void Generate()
        {
            Func<double, double> degToRad = (deg) => Math.PI * deg / 180.0;
		    var layer = GetLayer("Rain");
            
            double i;
            for (i = 0; i < particleAmount; i++) {
                OsbSprite spec = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.Centre);
                double startX = Random(-107, 747), startY = -150;
                double angle = degToRad(Random(-10, 10));
                double length = Random(40, 120);
                float depth = 660;
                Vector2 endPos = new Vector2( (float) (depth * Math.Tan(angle) + startX), depth);
                
                Assert(angle > 0 ? endPos.X > startX : endPos.X <= startX, startX.ToString() + " " + endPos.X.ToString());

                spec.StartLoopGroup(startTime + Random(0, beatLength*4), (int) ((endTime - startTime) / particleDuration - 1));
                
                // Actions done per loop
                spec.Rotate(0, -angle);
                spec.ScaleVec(0, 1, length);
                spec.Move(OsbEasing.InOutExpo, 0, particleDuration, startX, startY, endPos.X, endPos.Y);
                
                spec.EndGroup();
            }
        }
    }
}
