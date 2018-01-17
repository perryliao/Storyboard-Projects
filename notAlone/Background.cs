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
		    var layer = GetLayer("Main");
            var screen = layer.CreateSprite("sb/sprites/dot.jpg", OsbOrigin.Centre);

            screen.Scale(0,1000);
            screen.ColorHsb(0, 225, 0.6, 0.3);
            screen.Fade(27381, 27551, 0, 1);
            screen.Fade(91642, 1); //stay until first chorus
            
            
        }
    }
}
