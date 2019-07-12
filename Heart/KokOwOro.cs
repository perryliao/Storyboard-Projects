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
            kkr2.Fade(OsbEasing.InExpo, 65409, 65586, 1, 0);
            kkr2.Color(OsbEasing.InExpo, 65409, 65586, Constants.white, Constants.randomColours[0]);

            OsbSprite cut = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.CentreRight, new Vector2(320 - 100, 240 + 100));
            cut.Fade(65233, 1);
            cut.Rotate(65233, -Math.PI/4);
            cut.ScaleVec(OsbEasing.InQuart, 65233, 65233 + Constants.beatLength/2, 0, 1, 100, 1);
            cut.ScaleVec(OsbEasing.OutQuart, 65233 + Constants.beatLength/2, 65939, 100, 1, 0, 1);
            cut.Move(OsbEasing.InOutQuart, 65233, 65939, cut.PositionAt(65233), cut.PositionAt(65233).X + 200, cut.PositionAt(65233).Y - 200);
            cut.Fade(65939, 0);

            kkrPiece("kkrTL", 294, 226, -20, -Math.PI/6);
            kkrPiece("kkrTM", 323, 226);
            kkrPiece("kkrTR", 342, 226, 27, Math.PI/5);
            kkrPiece("kkrM", 334, 246);
            kkrPiece("kkrBL", 295, 243);
            kkrPiece("kkrBM", 320, 253);
            kkrPiece("kkrBR", 335, 239);
        }

        private void kkrPiece(string fileName, float xPos, float yPos, int xVariation = 0, double rotation = 0) {
            OsbSprite kkr = GetLayer("KokOwOro").CreateAnimation($"sb/TsUwUmaki KokOwOro/{fileName}.png", 2, 150, OsbLoopType.LoopForever, OsbOrigin.Centre, new Vector2(xPos, yPos));
            double start = 65409;
            if (rotation == 0) rotation = Random(-Math.PI/8, Math.PI/8);
            if (xVariation == 0) xVariation = Random(-15, 15);
            kkr.Fade(OsbEasing.InExpo, start, 65586, 0, 1);
            kkr.Fade(OsbEasing.OutExpo, 68056, 68409, 1, 0);
            kkr.Scale(start, 0.8);
            kkr.Color(start, 65586, Constants.white, Constants.randomColours[0]);
            kkr.Rotate(start, 68409, 0, rotation);
            kkr.Move(OsbEasing.OutQuad, start, 68409, kkr.PositionAt(start), kkr.PositionAt(start).X + xVariation, kkr.PositionAt(start).Y + Random(50, 60));

        }
    }
}
