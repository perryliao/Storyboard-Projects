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
    public class Paint : StoryboardObjectGenerator
    {
        [Configurable]
        public double startTime = 5801;
        [Configurable]
        public int numIterations = 4;

        private OsbSprite findValidEntry( bool[] arr ) {
		        var layer = GetLayer("paint");
                Random rnd = new Random();
                int idx = rnd.Next(arr.Length);
                while(!arr[idx]) {
                    // if arr[idx] == false, look for another index
                    idx = rnd.Next(arr.Length);
                }
                // set arr[idx] to false and return the sprite found
                arr[idx] = false;
                return layer.CreateSprite("sb/paint/paint" + idx.ToString() + ".png", OsbOrigin.Centre);
            }

        private double measureLength = 7221 - 5801; 

        public override void Generate()
        {
            bool[] boolArr = new bool[8];
            for (int i = 0; i < boolArr.Length; i++) {
                boolArr[i] = true; // initialize all to true
            }

            for (int i = 0; i < numIterations; i++) {
                double newStartTime = startTime + i * measureLength * 2;
                OsbSprite sprite = findValidEntry(boolArr);
                int height = 160;

                sprite.Fade(OsbEasing.InExpo, newStartTime - measureLength/16, newStartTime, 0, 1);
                sprite.Move(OsbEasing.InExpo, newStartTime - measureLength/16, newStartTime, 150, height, 110, height);
                sprite.Move(newStartTime, newStartTime + 2*measureLength - measureLength/4, sprite.PositionAt(newStartTime), 100, height);
                sprite.Move(
                    OsbEasing.InExpo, 
                    newStartTime + 2*measureLength - measureLength/4, 
                    newStartTime + 2*measureLength - measureLength/8,
                    sprite.PositionAt(newStartTime + 2*measureLength - measureLength/4),
                    60,
                    height
                    );
                sprite.Fade(OsbEasing.InExpo, newStartTime + 2*measureLength - measureLength/4, newStartTime + 2*measureLength - measureLength/8, 1, 0);
            }
        }
    }
}
