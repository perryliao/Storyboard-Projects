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
    public class SmokeBG : StoryboardObjectGenerator
    {
        public override void Generate()
        {
		    var layer = GetLayer("SmokeBG");
            OsbSprite smoke = layer.CreateSprite("sb/Pool 5/Smoke14.png", OsbOrigin.Centre, new Vector2(320 - 150, 240));
            OsbSprite smoke2 = layer.CreateSprite("sb/Pool 5/Smoke14.png", OsbOrigin.Centre, new Vector2(320 + 150, 240));
            
            smoke.Fade(OsbEasing.InExpo, 45468, 45821, 0, 0.4);
            smoke.Scale(45468, 48644, 3, 3.2);
            smoke.Rotate(45468, Math.PI/2);
            smoke.Color(OsbEasing.InOutCirc, 45468, 48644, 0.2, 0.5, 0.3, 0.9, 0.2, 0.9);
            smoke.Fade(48644, 0);

            smoke2.Fade(OsbEasing.InExpo, 45468, 45821, 0, 0.4);
            smoke2.Scale(45468, 48644, 3, 3.2);
            smoke2.Rotate(45468, Math.PI/2);
            smoke2.Color(OsbEasing.InOutCirc, 45468, 48644, 0.2, 0.5, 0.3, 0.9, 0.2, 0.9);
            smoke2.Fade(48644, 0);
        }
    }
}
