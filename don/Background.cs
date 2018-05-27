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
		    var layer = GetLayer("");
            var arisa1 = layer.CreateSprite("arisa.jpg", OsbOrigin.Centre);
            var arisa2 = layer.CreateSprite("arisa2.jpg", OsbOrigin.Centre);

            
            arisa1.Scale(0, 0.45);
            arisa2.Scale(0, 0.45);

            arisa1.Fade(OsbEasing.In, 12072, 13572, 0, 1);
            arisa1.Fade(OsbEasing.In, 66072, 66822, 1, 0);

            arisa2.Fade(OsbEasing.InExpo, 67197, 67572, 0, 1);
            arisa2.Fade(OsbEasing.In, 104885, 108072, 1, 0.55);
            arisa2.Fade(OsbEasing.InExpo, 108072, 108354, 0.55, 0);
        }
    }
}
