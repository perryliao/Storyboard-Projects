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
    public class Intro : StoryboardObjectGenerator
    {
        public override void Generate()
        {
		    var layer = GetLayer("Main");
            var screen = layer.CreateSprite("sb/sprites/dot.jpg", OsbOrigin.Centre);
            var circle = layer.CreateSprite("sb/sprites/circle.png", OsbOrigin.Centre);
            var circleBeat = layer.CreateSprite("sb/sprites/circle.png", OsbOrigin.Centre);
            
            OsbSprite[] nowLoading = new OsbSprite[13];


            //Loading intro text into array
            for (int i = 0; i < nowLoading.Length; i++) {
                if (i >= 10) {
                    nowLoading[i] = layer.CreateSprite("sb/fonts/helvetica/10.png", OsbOrigin.Centre);
                } else if (i == 4) { 
                    nowLoading[i] = layer.CreateSprite("sb/fonts/helvetica/1.png", OsbOrigin.Centre);
                } else {
                    nowLoading[i] = layer.CreateSprite("sb/fonts/helvetica/" + i + ".png", OsbOrigin.Centre);
                }
            }

           // int width = bg.Width;
           // int height = bg.Height;
            var beatDuration = Beatmap.GetTimingPointAt(0).BeatDuration;
           // var screenScale = 480.0 / 1080;

            // Text: Now
            for (int i = 0; i < 3; i++) {
                nowLoading[i].Scale(0, 0.3);
                nowLoading[i].MoveX(0, i*25 + 200);
            }

            // Text: Loading
            for (int i = 3; i < nowLoading.Length - 3; i++) {
                nowLoading[i].Scale(0, 0.3);
                nowLoading[i].MoveX(0, i*22 + 220);
            }

            // Text: ...
            for (int i = nowLoading.Length - 3; i < nowLoading.Length; i++) {
                nowLoading[i].Scale(0, 0.3);
                nowLoading[i].MoveX(0, i*15 + 290);
            }
            
            /*

            //enter loop
            foreach (OsbSprite text in nowLoading) {
                text.StartLoopGroup(0, 16);
            }

            //fading in , TODO work on the math
            int count = 0;
            foreach (OsbSprite text in nowLoading) {
                text.Fade(OsbEasing.In, 0 + count*25, 500 + count*20, 0, 1);
                count++;
            }

            //fading out, TODO math
            int count2 = 0;
            foreach (OsbSprite text in nowLoading) {
                text.Fade(OsbEasing.In, 760 + count2*30, 1500 + count2*30, 1, 0);
                count2++;
            }


            //exit loop
            foreach (OsbSprite text in nowLoading) {
                text.EndGroup();
            }

            */

            for (int i = 0; i < nowLoading.Length; i++) {
                var text = nowLoading[i];

                text.StartLoopGroup(0,16);
                text.Fade(OsbEasing.In, 0 + i*25, 500 + i*20, 0, 1);
                text.Fade(OsbEasing.In, 600 + i*30, 1500 + i*30, 1, 0);
                text.EndGroup();
            }

            screen.Scale(0, 1000);
            screen.Fade(OsbEasing.In, 24824, 27381, 0, 0.7);
            screen.Fade(OsbEasing.In, 27381, 27551, 0.7, 0);

            circle.Scale(27381, 27466, 0, 1.75);
            circle.Scale(27466, 27551, 1.75, 1.5);
            circle.Fade(OsbEasing.In, 27381, 27551, 0, 1);
            circle.Fade(49369, 1);

            //initialize circle pulses
            circleBeat.Fade(0, 0);
            circleBeat.Scale(0, 1.5);

            // loop pulse effect
            circleBeat.StartLoopGroup(beatDuration, 15);
            circleBeat.Scale(27551, 27551 + beatDuration, 1.5, 1.75);
            circleBeat.Fade(OsbEasing.In, 27551,27551 + beatDuration, 0.1, 0);
            circleBeat.Scale(27551 + beatDuration, 27551 + 2*beatDuration, 1.75, 1.5);
            circleBeat.EndGroup();

            // shrink circle for drum roll effect
            circle.Scale(37438- ((38290-37438)/32), 37438, 1.5, 1.75);
            circle.Scale(37438, 38290, 1.75, 0);
            circle.Scale(38290, 38460, 0, 1.5);

            // repeat pulsing
            circleBeat.StartLoopGroup(beatDuration, 13);
            circleBeat.Scale(38460, 38460 + beatDuration, 1.5, 1.75);
            circleBeat.Fade(OsbEasing.In, 38460,38460 + beatDuration, 0.15, 0);
            circleBeat.Scale(38460 + beatDuration, 38460 + 2*beatDuration, 1.75, 1.5);
            circleBeat.EndGroup();

        }
    }
}
