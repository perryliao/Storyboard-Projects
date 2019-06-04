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

        /*
         *  @param OsbSprite[][] field  - The field of tiles
         *  @param int coord            - x (if vert == true) or y (if vert == false) coordinate of the new line to split to 
         *  @param double startTime     - Time of when the movement starts 
         *  @param double endTIme       - Time of when the movement ends
         *  @param OsbEasing easing     - Fade Easing type to use: https://github.com/Damnae/storybrew/blob/master/common/Storyboarding/OsbSprite.cs#L319
         *  @param bool vert            - True if the splitting line is vertical
         */
        private void moveBoundary(OsbSprite[][] field, int coord, double startTime, double endTime, OsbEasing easing, bool vert = true) {
            if (vert) {
                
            } else {
                // horizontal coordinate
            }
        }
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
