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
    public class Body : StoryboardObjectGenerator
    {
        public override void Generate()
        {
		    var layer = GetLayer("");
            var bodyMain = layer.CreateSprite("sb/body/body_main.png", OsbOrigin.Centre);
            var bodyDarken = layer.CreateSprite("sb/body/body_darken.png", OsbOrigin.Centre);
            var bodyHighlight = layer.CreateSprite("sb/body/body_highlight.png", OsbOrigin.Centre);
            var bodyPixelate = layer.CreateSprite("sb/body/body_pixelate.png", OsbOrigin.Centre);

            bodyMain.Scale(0, 0.40);
            bodyDarken.Scale(0, 0.40);
            bodyHighlight.Scale(0, 0.40);
            bodyPixelate.Scale(0, 0.40);

            bodyMain.Fade(OsbEasing.In, 5623, 5801, 0, 1);
            bodyDarken.Fade(OsbEasing.In, 62428, 62606, 0, 1);
            bodyHighlight.Fade(OsbEasing.In, 62428, 62606, 0, 1);
            bodyPixelate.Fade(OsbEasing.In, 5623, 5801, 0, 1);

            bodyMain.Fade(OsbEasing.In, 100771, 100949, 0, 1);
            bodyDarken.Fade(OsbEasing.In, 100771, 100949, 0, 1);
            bodyHighlight.Fade(OsbEasing.In, 100771, 100949, 0, 1);
            bodyPixelate.Fade(OsbEasing.In, 100771, 100949, 0, 1);
        }
    }
}
