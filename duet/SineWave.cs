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
        public double waveHeight = 200;
        [Configurable]
        public double waveLength = 200;
        [Configurable]
        public double startTime = 11830;
        [Configurable]
        public double endTime = 23259;
        [Configurable]
        public double timeStep = Constants.beatLength/2; // Lower = faster speed

        StoryboardLayer layer;
        Color4[] colours = { Colours.blue,  Colours.cyan };

        public override void Generate()
        {
            layer = GetLayer("SineWave");

		    for (int i = 0; i < numSprites; i++) {
                // y = waveHeight * (sin( (x + varyingX) / waveLength)) + varyingY + 240;
                double varyingX = Random(waveLength) - waveLength/2;
                double varyingY = Random(waveHeight) - waveHeight/2;
                double scale = Random(2, 6);

                float x = (float) Random(Constants.xFloor - 150, Constants.xCeil - 10);
                float y = (float) (waveHeight * (Math.Sin( (x + varyingX) / waveHeight)) + varyingY + 240);
                OsbSprite circ = layer.CreateSprite("sb/float.png", OsbOrigin.Centre, new Vector2(x, y)); 
                circ.Scale(startTime, scale/10);
                circ.Color(startTime, colours[Random(colours.Length)]);
                circ.Fade(startTime, startTime + timeStep, 0, 0.4);

                for (double j = startTime; j < endTime - timeStep; j += timeStep) {
                    x += (float)waveLength/20;
                    y = (float) (waveHeight * (Math.Sin( (x + varyingX) / waveHeight)) + varyingY + 240);
                    circ.Move(j, j + timeStep, circ.PositionAt(j), x, y);
                    if (x > Constants.xCeil + 5) {
                        circ.Fade(j, j + timeStep, circ.OpacityAt(j), 0);
                        break;
                    }
                }
                circ.Fade(endTime - timeStep, endTime, circ.OpacityAt(endTime - timeStep), 0);
            }
        }
    }
}
