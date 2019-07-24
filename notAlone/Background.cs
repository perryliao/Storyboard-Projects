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
    public class Background : StoryboardObjectGenerator
    {
        public override void Generate()
        {
		    var layer = GetLayer("MainBG");
            var bg = layer.CreateSprite("notAlone.jpg", OsbOrigin.Centre);
            var screen = layer.CreateSprite("sb/sprites/dot.jpg", OsbOrigin.Centre);
            var bw = layer.CreateSprite("sb/bgs/bw.jpg", OsbOrigin.Centre);
            screen.ScaleVec(27381, 440, 240);
            screen.ColorHsb(27381, 225, 0.6, 0.3); //dark blue
            screen.Fade(27381, 27551, 0, 1);

            screen.ColorHsb(OsbEasing.OutExpo, 37438, 38290, 225, 0.6, 0.3, 0, 0.7, 0.35); //drum roll, change to red
            screen.ColorHsb(OsbEasing.OutCirc, 38290, 38460, 0, 0.7, 0.35, 225, 0.6, 0.3); //back to blue
            
            screen.ColorHsb(OsbEasing.InCirc, 48858, 49369, 225, 0.6, 0.3, 225, 0, 0.13); // dark grey

            screen.Fade(OsbEasing.OutExpo, 60108, 60364, 1, 0);

            bw.Fade(60108, 1);
            bw.Scale(60108, 0.45);
            bw.Fade(OsbEasing.OutExpo, 60108, 60364, 0, 1);

            bw.Fade(OsbEasing.InExpo, 70847, 71188, 1, 0);

            bg.Scale(70847, 0.45);
            bg.Fade(OsbEasing.InExpo, 70847, 71188, 0, 1);

            bg.Fade(93006, 0);
        } 
    }
}
