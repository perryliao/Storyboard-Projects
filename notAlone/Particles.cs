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
    public class Particles : StoryboardObjectGenerator
    {
        public override void Generate() {

            var layer = GetLayer("Main");

            var sprite = layer.CreateSprite("sb/sprites/dot.png", OsbOrigin.TopLeft);

            sprite.StartLoopGroup(27381, 10);
            var newSprite = layer.CreateSprite("sb/sprites/dot.png", OsbOrigin.TopLeft);
            //newSprite.MoveX(27381, 27381 + 500,  53);

            sprite.EndGroup();

		    
            
        }
    }
}
