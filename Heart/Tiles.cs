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
        private double blockStartTime = 34527;
        private double startTime = 42997;
        private double endTime = 45821;
        private int width = 854;
        private int height = 480;
        private int scale = 8; // 4 or lower is too slow

        public override void Generate()
        {
		    var layer = GetLayer("tiles");
            OsbSprite bg = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.Centre);
            bg.ColorHsb(blockStartTime, 0, 0, 0.1);
            bg.ScaleVec(blockStartTime, width, height);
            bg.Fade(blockStartTime, 0.6);
            bg.Fade(startTime, 0);

            OsbSprite[][] field = new OsbSprite[height/scale][];  // field[y][x]
            for (int i = 0; i < height/scale; i++) {
                field[i] = new OsbSprite[width/scale];
            }
            
            Random rnd = new Random();
            for (int y = 0; y < height/scale; y++) {
                for (int x = 0; x < width/scale; x++) {
                    // initial set up
                    field[y][x] = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.Centre);
                    OsbSprite sprite = field[y][x];
                    sprite.Scale(startTime, scale);
                    sprite.ColorHsb(startTime, 0, 0, 0.1);
                    sprite.Move(startTime, x*scale - 107, y*scale); 
                    sprite.Fade(startTime, 0.6);

                    // slow move to the right 
                    sprite.Move(startTime, 44939, sprite.PositionAt(startTime), sprite.PositionAt(startTime).X + rnd.Next(5, 15), sprite.PositionAt(startTime).Y + rnd.Next(-10, 10));
                    // quickly change speed & colour 
                    sprite.Move(OsbEasing.OutExpo, 44939, 45468, sprite.PositionAt(44939), sprite.PositionAt(44939).X + rnd.Next(80, 500), sprite.PositionAt(44939).Y + rnd.Next(-5, 5));
                    sprite.ColorHsb(OsbEasing.OutExpo, 44939, 45115, sprite.ColorAt(44939), 257, 0.76, 0.20);

                    sprite.Fade(OsbEasing.InExpo, 45115, endTime, sprite.OpacityAt(45115), 0);
                }
            }

            OsbSprite blackBar = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.CentreLeft);
            OsbSprite whiteBar = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.CentreLeft, new Vector2(-100 , 240));
            double barHeight = 20;
            whiteBar.Color(startTime, 247, 230, 213);
            whiteBar.ScaleVec(startTime, 44939, 0, barHeight, width * 0.2, barHeight);
            whiteBar.ScaleVec(OsbEasing.OutExpo, 44939, 45468, whiteBar.ScaleAt(44939), width * 0.9, barHeight);
            whiteBar.ScaleVec(OsbEasing.OutExpo, 45468, endTime, whiteBar.ScaleAt(45468), width * 0.6, barHeight);
            
            blackBar.Color(45468, 0,0,0);
            blackBar.ScaleVec(endTime, width / 2, barHeight);

            OsbSprite smallWhiteBar = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.CentreLeft, new Vector2(width * 0.9f - 100 , 240));
            smallWhiteBar.Color(45468, 247, 230, 213);
            smallWhiteBar.ScaleVec(OsbEasing.OutExpo, 45468, 45644, 0, barHeight, width * 0.1, barHeight);
            smallWhiteBar.MoveX(OsbEasing.InOutExpo, 45468, 45644, smallWhiteBar.PositionAt(45468).X, smallWhiteBar.PositionAt(45468).X + width * 0.1);

        }
    }
}
