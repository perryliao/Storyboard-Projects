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
        public double particleDuration = 1000;
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
                double angle = Random(-5, 5);
                double length = Random(50, 200);

                spec.StartLoopGroup(startTime + Random(0, beatLength*4), (int) ((endTime - startTime) / particleDuration - 2));
                
                spec.Rotate(0, degToRad(angle));
                spec.ScaleVec(0, 1, length);
                double startX = Random(-107, 747), startY = -100;
                if (angle < 0) {
                    // points to the right, move right
                    spec.Move(OsbEasing.InOutExpo, 0, particleDuration, startX, startY, startX + Random(0, 5), 600);
                } else {
                    // points to the left, move left
                    spec.Move(OsbEasing.InOutExpo, 0, particleDuration, startX, startY, startX - Random(0, 5), 600);
                }
                
                spec.EndGroup();
            }
        }
    }
}
