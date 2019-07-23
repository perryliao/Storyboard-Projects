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
    public class SineWave : StoryboardObjectGenerator
    {
        [Configurable]
        public int numSprites = 100;
        [Configurable]
        public double size = 200;
        [Configurable]
        public double startTime = 11830;
        [Configurable]
        public double endTime = 23259;
        [Configurable]
        public double timeStep = Constants.beatLength/2;

        StoryboardLayer layer;
        Color4[] colours = { Colours.blue,  Colours.cyan };

        public override void Generate()
        {
            layer = GetLayer("SineWave");

		    for (int i = 0; i < numSprites; i++) {
                // y = size * (sin( (x + varyingX) / size)) + varyingY + 240;
                double varyingX = Random(size)/2 - size/4;
                double varyingY = Random(size)/2 - size/4;
                double scale = Random(2, 6);

                float x = (float) Random(Constants.xFloor - 320, Constants.xCeil - 50);
                float y = (float) (size * (Math.Sin( (x + varyingX) / size)) + varyingY + 240);
                OsbSprite circ = layer.CreateSprite("sb/float.png", OsbOrigin.Centre, new Vector2(x, y)); 
                circ.Scale(startTime, scale/10);
                circ.Color(startTime, colours[Random(colours.Length)]);
                circ.Fade(startTime, startTime + timeStep, 0, 0.4);

                for (double j = startTime; j < endTime - timeStep; j += timeStep) {
                    x += (float)size/10;
                    y = (float) (size * (Math.Sin( (x + varyingX) / size)) + varyingY + 240);
                    circ.Move(j, j + timeStep, circ.PositionAt(j), x, y);
                }
                circ.Fade(endTime - timeStep, endTime, 0.4, 0);
            }
        }
    }
}
