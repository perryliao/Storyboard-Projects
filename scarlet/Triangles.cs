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
    public class Triangles : StoryboardObjectGenerator
    {
        [Configurable]
        public double startTime = 5801;

        public double beatLength = 6156 - 5801;

        public override void Generate()
        {
		    StoryboardLayer layer = GetLayer("triangles");
            OsbSprite x = layer.CreateSprite("sb/2x2.jpg", OsbOrigin.CentreLeft);
            OsbSprite y = x; // layer.CreateSprite("sb/2x2.png", OsbOrigin.CentreLeft);
            OsbSprite z = x;
            OsbSprite xy = layer.CreateSprite("sb/circle.png", OsbOrigin.Centre);
            OsbSprite xz = xy;
            OsbSprite yz = xy;
            Random rnd = new Random();

            OsbSprite[] triangle = new OsbSprite[] { x, y, z, xy, xz, yz };
            double fadeInTime = startTime - beatLength/2;
            int startingX = rnd.Next(270, 700);
            int startingY = rnd.Next(30, 300);

            // Actions done to all parts of the triangle
            foreach(OsbSprite part in triangle) {
                part.Move(fadeInTime, startingX, startingY);
                part.Fade(fadeInTime, startTime, 0, 1);

                part.Move(startTime, startTime + beatLength * 4, part.PositionAt(startTime), startingX, startingY + 50);
            }

            // Actions done to only the sides
            // for(int i = 0; i < 3; i++) {
            //     triangle[i].ScaleVec(fadeInTime, 10, 1);
            // }
            // Actions done to only the dots
            for(int i = 3; i < 6; i++) {
                triangle[i].Scale(fadeInTime, 0.05);
            }

            int xLength = rnd.Next(10, 60);
            x.ScaleVec(fadeInTime, xLength, 0.75);
            // xz.Move(fadeInTime, startingX + xLength, 300);
        }
    }
}
