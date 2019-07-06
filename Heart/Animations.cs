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
    public class Animations : StoryboardObjectGenerator
    {
        public override void Generate()
        {
		    var layer = GetLayer("Animations");
            OsbSprite square = layer.CreateSprite("sb/boxcircle.png", OsbOrigin.TopRight, new Vector2(220, 240));
            square.Fade(45821, 1);
            square.ScaleVec(45821, 0.5, 0.3);
            // square.Rotate(45821, -Math.PI/4);
            square.Fade(48644, 0);
        }
    }
}
