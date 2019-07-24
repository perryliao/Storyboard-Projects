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
    public class Background2 : StoryboardObjectGenerator
    {
        public override void Generate()
        {
		    var layer = GetLayer("tae");
            var bg1 = layer.CreateSprite("tae.jpg", OsbOrigin.Centre);
            var bg2 = layer.CreateSprite("tae2.jpg", OsbOrigin.Centre);

            
            bg1.Scale(0, 0.45);
            bg2.Scale(0, 0.45);

            bg1.Fade(OsbEasing.In, 12072, 13572, 0, 1);
            bg1.Fade(OsbEasing.In, 66072, 66822, 1, 0);

            bg2.Fade(OsbEasing.InExpo, 67197, 67572, 0, 1);
            bg2.Fade(OsbEasing.In, 104885, 108072, 1, 0.55);
            bg2.Fade(OsbEasing.InExpo, 108072, 108354, 0.55, 0);
            
        }
    }
}
