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
    public class SmokeBG : StoryboardObjectGenerator
    {
        [Configurable]
        public double startTime = 45821;
        [Configurable]
        public double endTime = 68409;

        private string[] smokeMap = new string[9] {
            "2",
            "4",
            "5",
            "6",
            "6b",
            "9",
            "10",
            "11",
            "14"
        };

        public override void Generate()
        {
		    StoryboardLayer layer = GetLayer("SmokeBG");
            // OsbSprite smoke = layer.CreateSprite("sb/Pool 5/Smoke14.png", OsbOrigin.Centre, new Vector2(320 - 150, 240));
            // OsbSprite smoke2 = layer.CreateSprite("sb/Pool 5/Smoke14.png", OsbOrigin.Centre, new Vector2(320 + 150, 240));
            
            // smoke.Fade(OsbEasing.InExpo, 45468, 45821, 0, 0.4);
            // smoke.Scale(45468, 48644, 3, 3.2);
            // smoke.Rotate(45468, Math.PI/2);
            // // smoke.Fade(48644, 0);

            // smoke2.Fade(OsbEasing.InExpo, 45468, 45821, 0, 0.4);
            // smoke2.Scale(45468, 48644, 3, 3.2);
            // smoke2.Rotate(45468, Math.PI/2);
            // smoke2.Color(OsbEasing.InOutCirc, 45468, 48644, smoke2.ColorAt(Constants.beatLength*4), Constants.randomColours[Random(Constants.randomColours.Length)]);
            // // smoke2.Fade(48644, 0);

            // smoke.StartLoopGroup(45821, 4);
            // smoke.Color(OsbEasing.InOutCirc, 0, Constants.beatLength*4, smoke.ColorAt(0), Constants.randomColours[Random(Constants.randomColours.Length)]);
            // smoke.MoveX(0, Constants.beatLength*4, smoke.PositionAt(0).X, smoke.PositionAt(0).X + 30);
            // smoke.Color(Constants.beatLength*4, Constants.beatLength*8, smoke.ColorAt(Constants.beatLength*4), Constants.randomColours[Random(Constants.randomColours.Length)]);
            // smoke.MoveX(Constants.beatLength*4, Constants.beatLength*8, smoke.PositionAt(Constants.beatLength*4).X, smoke.PositionAt(0).X);
            // smoke.EndGroup();

            double i, j;
            OsbSprite smoke;
            string currentSmoke;
            for (i = startTime; i < endTime; i += Constants.beatLength*4) {
                for (j = 0; j < 1; j++) {
                    currentSmoke = smokeMap[Random(smokeMap.Length)];
                    smoke = layer.CreateSprite($"sb/Pool 5/Smoke{currentSmoke}.png", OsbOrigin.Centre);

                    smoke.Fade(i, 1);
                    smoke.Fade(i + Constants.beatLength*4, 0);
                    smoke.Scale(i, i + Constants.beatLength*4, 3, 3.2);

                    smoke.Move(i, i + Constants.beatLength*4, 
                        320 + Random(-150, 150),
                        240 + Random(-20, 20),
                        smoke.PositionAt(i).X + Random(-10, 10),
                        smoke.PositionAt(i).Y + Random(-10, 10)
                    );

                    smoke.Color(i, i + Constants.beatLength*4, 
                        Constants.randomColours[Random(Constants.randomColours.Length)],
                        Constants.randomColours[Random(Constants.randomColours.Length)]
                    );
                }
            }        
        }
    }
}
