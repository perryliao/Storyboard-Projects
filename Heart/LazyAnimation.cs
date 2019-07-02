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

    public class LazyAnimation : StoryboardObjectGenerator
    {
        private double beatLength = 706;
        public override void Generate()
        {
		    var layer = GetLayer("Lazy");
            OsbSprite circle = layer.CreateSprite("sb/cir.png", OsbOrigin.BottomCentre, new Vector2(320, 0));

            circle.Color(34527, 1, 1, 1);
            circle.Scale(34527, 0.48 );
            circle.Fade(34527, 1);
            // circle.Fade(37350, 0);
            
            circle.Move(OsbEasing.OutQuad, 34527, 34880, circle.PositionAt(34527), 320, 240);
            circle.Rotate(OsbEasing.InOutQuint, 34880, 36644, 0, Math.PI * 2 * 3 / 4);
            circle.Move(OsbEasing.InQuad, 36644, 37350, circle.PositionAt(36644), 800, 240);
            circle.Fade(OsbEasing.InOutElastic, 36644, 37350, 1, 0);
            circle.Color(36821, 1, 0, 0);

            double boxEndTime = 38233;
            OsbSprite boxOut = setUpBox(37350, 40174);
            boxOut.Scale(OsbEasing.Out, 37350, boxEndTime, boxOut.ScaleAt(37350).X, 1.5);
            boxOut.Rotate(37350, boxEndTime, 0, 5*Math.PI/4);
            OsbSprite boxMid = setUpBox(37350, 40174);
            boxMid.Scale(OsbEasing.Out, 37527, boxEndTime, boxMid.ScaleAt(37527).X, 1);
            boxMid.Rotate(37527, boxEndTime, 0, Math.PI);
            OsbSprite boxIn = setUpBox(37350, 40174);
            boxIn.Scale(OsbEasing.Out, 37703, boxEndTime, boxIn.ScaleAt(37703).X, 0.5);
            boxIn.Rotate(37703, boxEndTime, 0, 3*Math.PI/4);

        }

        private OsbSprite setUpBox( double start, double end) {
            OsbSprite box = GetLayer("boxy").CreateSprite("sb/boxy.png", OsbOrigin.Centre);
            box.Fade(OsbEasing.In, start - beatLength/4, start, 0, 1);
            box.Scale(start - beatLength/4, 0);
            box.Fade(end, 0);
            return box;
        }
    }
}
