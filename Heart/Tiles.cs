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
    public class Tiles : StoryboardObjectGenerator
    {
        private int width = 854;
        private int height = 480;
        private int scale = 8; // 4 or lower is too slow
        public override void Generate()
        {
		    var layer = GetLayer("tiles");            
            OsbSprite[][] field = new OsbSprite[height/scale][];  // field[y][x]
            for (int i = 0; i < height/scale; i++) {
                field[i] = new OsbSprite[width/scale];
            }

            for (int y = 0; y < height/scale; y++) {
                for (int x = 0; x < width/scale; x++) {
                    field[y][x] = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.Centre);
                    field[y][x].Scale(0,scale);
                    field[y][x].Move(0, x*scale - 107, y*scale); 
                    if (x*scale < 400) field[y][x].Fade(0, 0.4);
                    field[y][x].Fade(237821, 0);
                }
            }
        }
    }
}
