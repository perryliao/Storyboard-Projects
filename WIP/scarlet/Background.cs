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
		    var layer = GetLayer("bg");
            var bg = layer.CreateSprite("Yuuuuuuukina.jpg", OsbOrigin.Centre);
            bg.Fade(0, 0);
            var bottom = layer.CreateSprite("sb/blend.png", OsbOrigin.Centre);
            var top = layer.CreateSprite("sb/blend.png", OsbOrigin.Centre);
		    
            OsbSprite[] allParts = new OsbSprite[] { top, bottom };
            foreach (OsbSprite elem in allParts) {
                elem.ScaleVec(0, 3.0, 1.2);
                elem.Fade(OsbEasing.In, 5623, 5801, 0, 1);
                elem.Fade(OsbEasing.In, 100771, 100949, 1, 0);
            }

            // Bottom
            bottom.Rotate(0, Math.PI/6);
            bottom.Move(0, 0, 240);
            bottom.ColorHsb(0, 9, 0.51, 0.15);

            // Top
            top.Rotate(0, Math.PI/6 + Math.PI);
            top.Move(0, 330, 30);
            top.ColorHsb(0, 13, 0.74, 0.85);

        }
    }
}
