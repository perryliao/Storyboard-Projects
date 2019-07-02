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
        private double glitchStartTime = 38056;
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

            OsbSprite boxOut = layer.CreateSprite("sb/boxcircle.png", OsbOrigin.Centre);
            boxOut.Fade(OsbEasing.In, 37350 - beatLength/4, 37350, 0, 1);
            boxOut.Scale(37350 - beatLength/4, 0.2);
            boxOut.Rotate(37350 - beatLength/4, Math.PI/16);
            boxOut.Scale(OsbEasing.In, 37350, glitchStartTime, boxOut.ScaleAt(37350).X, 3);
            boxOut.Rotate(37350, glitchStartTime, boxOut.RotationAt(37350), boxOut.RotationAt(37350) + Math.PI * 5 / 8);
            boxOut.Fade(glitchStartTime, 0);
            OsbSprite boxMid = layer.CreateSprite("sb/boxcircle.png", OsbOrigin.Centre);
            boxMid.Fade(OsbEasing.In, 37350 - beatLength/4, 37350, 0, 1);
            boxMid.Scale(37350 - beatLength/4, 0.15);
            boxMid.Rotate(37350 - beatLength/4, Math.PI/8);
            boxMid.Scale(OsbEasing.InCubic, 37350, glitchStartTime, boxMid.ScaleAt(37350).X, 2);
            boxMid.Rotate(37350, glitchStartTime, boxMid.RotationAt(37350), boxMid.RotationAt(37350) + Math.PI * 5 / 8);
            boxMid.Fade(glitchStartTime, 0);
            OsbSprite boxIn = layer.CreateSprite("sb/boxcircle.png", OsbOrigin.Centre);
            boxIn.Fade(OsbEasing.In, 37350 - beatLength/4, 37350, 0, 1);
            boxIn.Scale(37350 - beatLength/4, 0.1);
            boxIn.Rotate(37350 - beatLength/4, Math.PI*3/8);
            boxIn.Fade(40174, 0);
            boxIn.Scale(OsbEasing.InExpo, 37350, glitchStartTime, boxIn.ScaleAt(37350).X, 0.4);
            boxIn.Rotate(37350, glitchStartTime, boxIn.RotationAt(37350), boxIn.RotationAt(37350) + Math.PI * 5 / 8);
            boxIn.Rotate(glitchStartTime, 40174, boxIn.RotationAt(glitchStartTime), boxIn.RotationAt(glitchStartTime) - Math.PI / 16);

            OsbSprite boxInBox = glitchFadeIn(0.365, 0.4, "sb/boxy.png");
            boxInBox.Rotate(glitchStartTime, 40174, boxInBox.RotationAt(glitchStartTime), boxInBox.RotationAt(glitchStartTime) - Math.PI / 16);
            OsbSprite[] rotations = new OsbSprite[4];
            OsbSprite circInBox;
            int i;
            for (i = 0; i < rotations.Length; i++) {
                rotations[i] = glitchFadeIn(0.065, 1, "sb/Pool 1/cir.png");
                if (i == 1) circInBox = glitchFadeIn(0.09, 1, "sb/Pool 1/cir.png");
                rotations[i].ColorHsb(glitchStartTime, 32, 0.18, 0.85);
            }

            rotations[0].Move(glitchStartTime, 375, 215);
            rotations[0].Scale(glitchStartTime, 0.025);

            rotations[1].Move(glitchStartTime, 280, 225);
            rotations[1].Scale(glitchStartTime, 0.01);

            rotations[2].Move(glitchStartTime, 250, 280);
            rotations[2].Scale(glitchStartTime, 0.045);
            
            rotations[3].Move(glitchStartTime, 390, 260);



            for (i = 0; i < rotations.Length; i++) {
                rotations[i].StartLoopGroup(glitchStartTime, 3);
                rotations[i].Move(OsbEasing.InSine, 0, beatLength, rotations[i].PositionAt(glitchStartTime), rotations[(i+1)%4].PositionAt(glitchStartTime));
                rotations[i].Scale(OsbEasing.InSine, 0, beatLength, rotations[i].ScaleAt(glitchStartTime).X, rotations[(i+1)%4].ScaleAt(glitchStartTime).X);
                rotations[i].EndGroup();
            }

            double progressStartTime = 40174;
            OsbSprite line = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.CentreLeft, new Vector2(-107, 240));
            OsbSprite lineEnd = layer.CreateSprite("sb/Pool 1/cir.png", OsbOrigin.CentreLeft, new Vector2(-39 - 70, 240));
            line.ScaleVec(progressStartTime - beatLength/4, 30, 100);
            line.Fade(progressStartTime - beatLength/4, 0);
            line.Fade(progressStartTime, 1);
            line.Fade(42997, 0);
            lineEnd.ScaleVec(progressStartTime - beatLength/4, 0.1, 0.16666);
            lineEnd.Fade(progressStartTime - beatLength/4, 0);
            lineEnd.Fade(progressStartTime, 1);
            lineEnd.Fade(42997, 0);

            line.ScaleVec(OsbEasing.InQuint, progressStartTime, progressStartTime + beatLength, line.ScaleAt(progressStartTime), line.ScaleAt(progressStartTime).X + 150, line.ScaleAt(progressStartTime).Y + 210);
            lineEnd.ScaleVec(OsbEasing.InQuint, progressStartTime, progressStartTime + beatLength, lineEnd.ScaleAt(progressStartTime), lineEnd.ScaleAt(progressStartTime).X + 0.351, lineEnd.ScaleAt(progressStartTime).Y + 0.351 );
            lineEnd.MoveX(OsbEasing.InQuint, progressStartTime, progressStartTime + beatLength, lineEnd.PositionAt(progressStartTime).X, lineEnd.PositionAt(progressStartTime).X + 31 );
        
        }

        private OsbSprite glitchFadeIn(double scale, double fade, string path) {
            OsbSprite sprite = GetLayer("Lazy").CreateSprite(path, OsbOrigin.Centre);
            sprite.Scale(glitchStartTime, scale);
            sprite.Fade(OsbEasing.InElastic, glitchStartTime, 38321, 0, fade);
            sprite.Fade(40174, 0);
            return sprite;
        }
    }
}
