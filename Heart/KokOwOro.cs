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
    public class KokOwOro : StoryboardObjectGenerator
    {
        public override void Generate() {
		    StoryboardLayer layer = GetLayer("KokOwOro");
            OsbSprite kkr2 = layer.CreateAnimation("sb/TsUwUmaki KokOwOro/sketchKkr.png", 2, 150, OsbLoopType.LoopForever, OsbOrigin.Centre);
            kkr2.Fade(62762, 1);
            kkr2.Color(62762, Constants.white);
            kkr2.Scale(62762, 0.8);
            kkr2.Fade(68409, 0);

            OsbSprite cut = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.CentreRight, new Vector2(320 - 100, 240 + 100));
            cut.Fade(65233, 1);
            cut.Rotate(65233, -Math.PI/4);
            cut.ScaleVec(OsbEasing.InQuart, 65233, 65233 + Constants.beatLength/2, 0, 1, 100, 1);
            cut.ScaleVec(OsbEasing.OutQuart, 65233 + Constants.beatLength/2, 65939, 100, 1, 0, 1);
            cut.Move(OsbEasing.InOutQuart, 65233, 65939, cut.PositionAt(65233), cut.PositionAt(65233).X + 200, cut.PositionAt(65233).Y - 200);
            cut.Fade(65939, 0);
        }
    }
}
