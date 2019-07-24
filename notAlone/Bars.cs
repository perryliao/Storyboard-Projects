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
    public class Bars : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            var layer = GetLayer("");
		    var screen = layer.CreateSprite("sb/sprites/dot.jpg", OsbOrigin.TopCentre);
            var screen2 = layer.CreateSprite("sb/sprites/dot.jpg", OsbOrigin.BottomCentre);

            screen.ColorHsb(60278, 255, 0, 0.13);
            screen2.ColorHsb(60278, 255, 0, 0.13);

            screen.ScaleVec(60108, 440, 110); 
            screen.MoveY(60108, -100);
            screen2.ScaleVec(60108, 440, 160); 
            screen2.MoveY(60108, 530);


            screen.Fade(60108, 1);
            screen2.Fade(60108, 1);

            screen.StartLoopGroup(0, 4);
            var rnd = Random(0, 20);
            while (rnd > 0 && rnd < 10) rnd = Random(0, 20);
            if (randomTF()) rnd *= -1;

            screen.MoveY(OsbEasing.OutExpo, 60278, 61642, -100, -100 + rnd);
            screen.MoveY(OsbEasing.OutExpo, 61642, 63006, -100 + rnd, -100);
            screen.EndGroup();

            screen2.StartLoopGroup(0, 4);
            var rand = Random(0, 30);
            while (rand > 0 && rand < 10) rand = Random(0, 30);
            if (randomTF()) rand *= -1;

            screen2.MoveY(OsbEasing.OutExpo, 60278, 61642, 530, 530 + rand);
            screen2.MoveY(OsbEasing.OutExpo, 61642, 63006, 530 + rand, 530);
            screen2.EndGroup();

            screen.ScaleVec(OsbEasing.InExpo, 70847, 71188, screen.ScaleAt(70847), 440, 0);
            screen2.ScaleVec(OsbEasing.InExpo, 70847, 71188, screen2.ScaleAt(70847), 440, 0);

            screen.Fade(71188, 0);
            screen2.Fade(71188, 0);
            
        }

        private bool randomTF() {
            var rnd = Random(0,1);
            return rnd == 1;
        }
    }
}
