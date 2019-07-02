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

            double boxEndTime = 38056;
            OsbSprite boxOut = layer.CreateSprite("sb/boxcircle.png", OsbOrigin.Centre);
            boxOut.Fade(OsbEasing.In, 37350 - beatLength/4, 37350, 0, 1);
            boxOut.Scale(37350 - beatLength/4, 0.2);
            boxOut.Rotate(37350 - beatLength/4, Math.PI/16);
            boxOut.Scale(OsbEasing.In, 37350, boxEndTime, boxOut.ScaleAt(37350).X, 3);
            boxOut.Rotate(37350, boxEndTime, boxOut.RotationAt(37350), boxOut.RotationAt(37350) + Math.PI * 5 / 8);
            boxOut.Fade(boxEndTime, 0);
            OsbSprite boxMid = layer.CreateSprite("sb/boxcircle.png", OsbOrigin.Centre);
            boxMid.Fade(OsbEasing.In, 37350 - beatLength/4, 37350, 0, 1);
            boxMid.Scale(37350 - beatLength/4, 0.15);
            boxMid.Rotate(37350 - beatLength/4, Math.PI/8);
            boxMid.Scale(OsbEasing.InCubic, 37350, boxEndTime, boxMid.ScaleAt(37350).X, 2);
            boxMid.Rotate(37350, boxEndTime, boxMid.RotationAt(37350), boxMid.RotationAt(37350) + Math.PI * 5 / 8);
            boxMid.Fade(boxEndTime, 0);
            OsbSprite boxIn = layer.CreateSprite("sb/boxcircle.png", OsbOrigin.Centre);
            boxIn.Fade(OsbEasing.In, 37350 - beatLength/4, 37350, 0, 1);
            boxIn.Scale(37350 - beatLength/4, 0.1);
            boxIn.Rotate(37350 - beatLength/4, Math.PI*3/8);
            boxIn.Fade(40174, 0);
            boxIn.Scale(OsbEasing.InExpo, 37350, boxEndTime, boxIn.ScaleAt(37350).X, 0.4);
            boxIn.Rotate(37350, boxEndTime, boxIn.RotationAt(37350), boxIn.RotationAt(37350) + Math.PI * 5 / 8);

        }
    }
}
