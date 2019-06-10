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
            
            OsbOrigin Origin = OsbOrigin.Centre;
            using (OsbSpritePool pool = new OsbSpritePool(layer, "sb/Pool 1/rain2.png", Origin, (rain2, startTime, endTime) => {
                // No states common to every sprite here.
            })) {
                double timeStep = particleDuration / particleAmount;
                double curStartTime, curEndTime;
                for (curStartTime = startTime; curStartTime <= endTime - particleDuration; curStartTime += timeStep) {
                    curEndTime = curStartTime + particleDuration;
                    OsbSprite rain2 = pool.Get(curStartTime, curEndTime);
                    double angle = Random(-5, 5);
                    rain2.Rotate(curStartTime, degToRad(angle));
                    rain2.ScaleVec(curStartTime, 1, Random(50, 200) / 100);

                    double startX = Random(-107, 747), startY = -100; // Random(0, 480);
                    if (angle < 0) {
                        // points to the right, move right
                        rain2.Move(OsbEasing.InOutExpo, curStartTime, curEndTime, startX, startY, startX + Random(0, 5), 600);
                    } else {
                        // points to the left, move left
                        rain2.Move(OsbEasing.InOutExpo, curStartTime, curEndTime, startX, startY, startX - Random(0, 5), 600);
                    }

                    rain2.Fade(OsbEasing.InExpo, curStartTime, curStartTime + beatLength / 2, 0, 0.4);
                    rain2.Fade(OsbEasing.OutExpo, curEndTime - beatLength / 2, curEndTime, 0.4, 0);
                    
                }
            }
        }
    }
}
