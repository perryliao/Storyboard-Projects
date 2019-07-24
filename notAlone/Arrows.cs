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
    public class Arrows : StoryboardObjectGenerator
    {
        [Configurable]
        public double particleAmount = 15;

        [Configurable]
        public double particleDuration = 3000;

        [Configurable]
        public double fadeInDuration = 500;

        [Configurable]
        public double fadeOutDuration = 500;

        


        public override void Generate() {

            var layer = GetLayer("");

            using (var pool = new OsbSpritePool(layer, "sb/sprites/toDown.png", OsbOrigin.Centre, (sprite, startTime, endTime) => {
                // This action runs for every sprite created from the pool, after all of them are created (AFTER the for loop below).

                // It is intended to set states common to every sprite:
                // In this exemple, this handles cases where all sprites will have the same color / opacity / scale / rotation / additive mode.

                // Note that the pool is in a using block, this is necessary to run this action.

                // Initial setup.
                sprite.Scale(27551, 0);
                

            }))

            { 
                var timeStep = particleDuration / particleAmount;
                for (double startTime = 27551; startTime < 49369 - particleDuration; startTime += timeStep) {
                    var endTime = startTime + particleDuration;

                       // This is where sprites are created from the pool.
                       // Commands here are specific to each sprite.
                    var sprite = pool.Get(startTime, endTime);
                    var x = Random(0, 640);
                    var y = Random(0, 480);
                    while (x > 140 && x < 500 && y > 60 && y < 420) {
                        x = Random(0, 640);
                        y = Random(0, 480);
                    }

                    var arrowCount = Random(3, 6);

                    List<OsbSprite> arrowList = new List<OsbSprite>();

                    while (arrowCount >= 0) {
                        arrowList.Add(layer.CreateSprite("sb/sprites/toDown.png", OsbOrigin.Centre));
                        arrowCount--;
                    }
                        
                    var loopAmount = Random(2, 5);
                    
                    for (int i = 0; i < arrowList.Count; i++) {
                        OsbSprite arrow = arrowList[i]; 

                        
                        arrow.Scale(startTime, 0.2);

                        arrow.StartLoopGroup(0, loopAmount);
                        
                        arrow.Fade(OsbEasing.InOutExpo, startTime, startTime + 300 + 2*i, 0, 1);
                        arrow.Move(OsbEasing.InOutExpo, startTime, startTime + 300 + 2*i, x, y, x, y + 10 + i*15);

                        arrow.Fade(OsbEasing.InExpo, startTime + 300 + 2*i, startTime + 600 + 2*i, 1, 0);
                        arrow.Move(OsbEasing.InExpo, startTime + 300 + 2*i, startTime + 600 + 2*i, x, y + 10 + i*15, x, y + 20 + i*26);
                        arrow.EndGroup();
                    }
                }
            }
        }
    }
}
