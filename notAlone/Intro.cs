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
            var circle = layer.CreateSprite("sb/sprites/opaqueCircle.png", OsbOrigin.Centre);
            
            OsbSprite[] nowLoading = new OsbSprite[13];


            //Loading intro text into array
            for (int i = 0; i < nowLoading.Length; i++) {
                if (i >= 10) {
                    nowLoading[i] = layer.CreateSprite("sb/fonts/helvetica/10.png", OsbOrigin.Centre);
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

            //enter loop
            foreach (OsbSprite text in nowLoading) {
                text.StartLoopGroup(0, 16);
            }

            //fading in , total 1000ms
            int count = 0;
            foreach (OsbSprite text in nowLoading) {
                text.Fade(OsbEasing.In, 0 + count*25, 500 + count*20, 0, 1);
                count++;
            }

            //fading out, total 1000ms
            int count2 = 0;
            foreach (OsbSprite text in nowLoading) {
                text.Fade(OsbEasing.In, 760 + count2*30, 1500 + count2*30, 1, 0);
                count2++;
            }


            //exit loop
            foreach (OsbSprite text in nowLoading) {
                text.EndGroup();
            }

            screen.Scale(0, 1000);
            screen.Fade(OsbEasing.In, 24824, 27381, 0, 0.7);
            screen.Fade(OsbEasing.In, 27381, 27551, 0.7, 0);

            circle.Scale(0, 1.5);
            circle.Fade(OsbEasing.In, 27381, 27551, 0, 1);
            circle.Fade(49369, 1);
        }
    }
}
