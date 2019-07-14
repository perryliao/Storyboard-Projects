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
    public class Poison : StoryboardObjectGenerator
    {
        [Configurable]
        public double startTime = 68409;

        [Configurable]
        public double endTime = 90997;

        [Configurable]
        public double scrollDuration = Constants.beatLength;

        public double edgeHeight = PoisonScripts.edgeHeight;
        private StoryboardLayer layer;

        public override void Generate()
        {
		    layer = GetLayer("Poison");

            setUpFrame(320, 170);
            setUpFrame(320 + 321, 170);
            setUpFrame(320 - 321, 170);
        }

        private void setUpFrame(float x, float y) {
            float width = 220, height = 290;

            makeRekt(x == 320 ? x : (x < 320 ? (float)Constants.xFloor : (float)Constants.xCeil), y, width - 20, height, Constants.black);

            if (x != 320) {
                handleScreenAnimations(layer, 69468, Constants.beatLength*3/2, x, y, width, height);
                skewFrame(x, y, width, height);
            } else {
                handleScreenAnimations(layer, 69468, Constants.beatLength*3/2, x, y, width, height);
                makeRekt(x, y + height/2 - (float)edgeHeight/2, width, edgeHeight, Constants.realBlack);
                makeRekt(x, y - height/2 + (float)edgeHeight/2, width, edgeHeight, Constants.realBlack);
                makeRekt(x - width/2, y, edgeHeight, height, Constants.realBlack);
                makeRekt(x + width/2, y, edgeHeight, height, Constants.realBlack);
            }
        }

        private void handleScreenAnimations(StoryboardLayer layer, double startTime, double duration, float x, float y, double width, double height) {
            PoisonScripts.functions[Random(PoisonScripts.functions.Length)](layer, 69468, Constants.beatLength*3/2, x, y, width, height);
            PoisonScripts.upArrow(layer, 70527, Constants.beatLength, x, y, width);
            PoisonScripts.functions[Random(PoisonScripts.functions.Length)](layer, 71233, Constants.beatLength*3/2, x, y, width, height);

        }

        private void skewFrame(float x, float y, float width, float height) {
            bool left = x < 320;
            
            OsbSprite tt = layer.CreateSprite("sb/t.png", OsbOrigin.BottomCentre, new Vector2(x, y - height/2));
            OsbSprite tb = layer.CreateSprite("sb/t.png", OsbOrigin.CentreRight, new Vector2(x, y - height/2));
            OsbSprite bt = layer.CreateSprite("sb/t.png", OsbOrigin.BottomCentre, new Vector2(x, y + height/2));
            OsbSprite bb = layer.CreateSprite("sb/t.png", OsbOrigin.CentreRight, new Vector2(x, y + height/2));

            makeRekt((float)(x + width/2 * (left ? 1 : -1) + edgeHeight * (left ? -1 : 1)), y, edgeHeight, height, Constants.realBlack);
            
            tt.Fade(startTime, 1);
            tt.Fade(endTime, 0);
            tb.Fade(startTime, 1);
            tb.Fade(endTime, 0);
            
            bt.Fade(startTime, 1);
            bt.Fade(endTime, 0);
            bb.Fade(startTime, 1);
            bb.Fade(endTime, 0);

            tt.Color(startTime, Constants.realBlack);
            tb.Color(startTime, Constants.realBlack);
            bt.Color(startTime, Constants.realBlack);
            bb.Color(startTime, Constants.realBlack);

            tb.Rotate(startTime, -Math.PI/2);
            bb.Rotate(startTime, -Math.PI/2);
            
            if (left) {
                tt.FlipH(startTime, startTime);
                bb.FlipV(startTime, startTime);
            } else {
                tb.FlipV(startTime, startTime);
                bt.FlipH(startTime, startTime);
            }

            tt.ScaleVec(startTime, 2.2, 0.3);
            tb.ScaleVec(startTime, 0.3, 2.2);
            bt.ScaleVec(startTime, 2.2, 0.3);
            bb.ScaleVec(startTime, 0.3, 2.2);
        }

        private void makeRekt(float x, float y, double width, double height, Color4 colour) {
            OsbOrigin o;
            if (x < 320) {
                o = OsbOrigin.CentreLeft;
            } else if (x > 320) {
                o = OsbOrigin.CentreRight;
            } else {
                o = OsbOrigin.Centre;
            }
            OsbSprite rekt = layer.CreateSprite("sb/1x1.jpg", o, new Vector2(x, y));
            if (colour == Constants.realBlack) {
                rekt.Fade(startTime, 1);
            } else {
                rekt.Fade(OsbEasing.InExpo, startTime, startTime + Constants.beatLength/4, 0, 1);
            }
            rekt.Fade(endTime, 0);
            rekt.Color(startTime, colour);

            rekt.ScaleVec(startTime, width, height);
        }
    }
}
