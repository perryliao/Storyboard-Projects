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
    public class Particles : StoryboardObjectGenerator
    {
        [Configurable]
        public string Path = "sb/sprites/circleGlow.png";


        [Configurable]
        public double particleAmount = 20;

        [Configurable]
        public double particleDuration = 3000;

        [Configurable]
        public double fadeInDuration = 500;

        [Configurable]
        public double fadeOutDuration = 500;


        public override void Generate() {

            var layer = GetLayer("");

            using (var pool = new OsbSpritePool(layer, Path, OsbOrigin.Centre, (sprite, startTime, endTime) => {
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
                    while (x > 140 && x < 500 && y > 60 && y < 420) { // real values: (132, 508, 52, 427)
                        x = Random(0, 640);
                        y = Random(0, 480);
                    } 
                    var fadeTime = Math.Max(startTime, startTime + fadeInDuration);

                    sprite.Move(startTime, x, y);

                    sprite.Fade(OsbEasing.In, startTime, fadeTime, 0, 1);
                    sprite.Scale(OsbEasing.OutElasticHalf, startTime, fadeTime, 0, 1);
                    sprite.Fade(OsbEasing.In, fadeTime, fadeTime + fadeOutDuration, 1, 0);
                    sprite.Scale(OsbEasing.InExpo, fadeTime, fadeTime + fadeOutDuration, 1, 0);
                
                }
            }
        }
    }
}
