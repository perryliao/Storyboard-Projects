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
    public class Test : StoryboardObjectGenerator
    {
        public override void Generate()
        {var layer = GetLayer("Main");
        var bg = layer.CreateSprite("snow.jpg", OsbOrigin.Centre);
        bg.Scale(0, 480.0 / 1080);
        bg.Fade(10500, 10750, 0, 1);
        bg.Fade(100001, 100250, 1, 0);
		    
            
        }
    }
}
