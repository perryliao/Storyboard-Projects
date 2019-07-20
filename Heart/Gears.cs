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
    public class Gears : StoryboardObjectGenerator
    {

        private string[] gearPaths = new string[6] {
            "sb/Pool 3/3.png",
            "sb/Pool 3/4.png",
            "sb/Pool 3/6.png",
            "sb/Pool 4/1.png",
            "sb/Pool 4/2.png",
            "sb/Pool 4/5.png"
        };
        StoryboardLayer layer;
        public override void Generate()
        {
		    layer = GetLayer("gears");
            
            OsbSprite Ref = gearConfig(gearPaths[0], 70527, 320, 240, 0, 0, Constants.black);
            Ref.Fade(OsbEasing.InBounce, 70527, 70703, Ref.OpacityAt(70527), 1);
            Ref.Fade(OsbEasing.InOutElastic, 70703, 71233, 1, 0);
            Ref.Scale(OsbEasing.OutBack, 70527, 70880, Ref.ScaleAt(70527).X, 0.7);
            Ref.Rotate(70527, 71233, 0, Math.PI/16);

            Ref = gearConfig(gearPaths[4], 71939, 100, 300, 0, 0.2, Constants.black);
            Ref.Fade(OsbEasing.InElastic, 71939, 72292, Ref.OpacityAt(71939), 0.4);
            Ref.Fade(OsbEasing.InElastic, 72644, 72762, Ref.OpacityAt(72644), 0.2);
            Ref.Fade(OsbEasing.InElastic, 72762, 72821, Ref.OpacityAt(72762), 0.5);
            Ref.Fade(OsbEasing.InElastic, 72821, 73174, Ref.OpacityAt(72821), 0);
            Ref.Rotate(71939, 74056, 0, Math.PI/4);
            Ref.Fade(74056, 0);

            Ref = gearConfig(gearPaths[5], 71939, 33, 175, 0, 0.2, Constants.black);
            Ref.Fade(OsbEasing.InElastic, 71939, 72292, Ref.OpacityAt(71939), 0.4);
            Ref.Fade(OsbEasing.InElastic, 72644, 72762, Ref.OpacityAt(72644), 0.2);
            Ref.Fade(OsbEasing.InElastic, 72762, 72821, Ref.OpacityAt(72762), 0.5);
            Ref.Fade(OsbEasing.InElastic, 72821, 73174, Ref.OpacityAt(72821), 0);
            Ref.Rotate(71939, 74056, -Math.PI/32, -Math.PI/5.5);
            Ref.Fade(74056, 0);

            Ref = gearConfig(gearPaths[1], 73350, (float) Constants.xCeil, 240, 1, 0.4, Constants.black);
            Ref.Rotate(73350, 74056, -Math.PI/32, Math.PI/4);
            Ref.Fade(74056, 0);

            gearDrop(100, 100, 74056, 76880);
            gearDrop(24, 222, 74056, 76880, false, gearPaths[5]);
            gearDrop(160, 230, 74056, 76880, true, gearPaths[2]);
            gearDrop(-70, 140, 74056, 76880, true, gearPaths[3]);
        }

        private OsbSprite gearConfig(string path, double start, float x, float y, double fade, double scale, Color4 colour) {
            OsbSprite gear = layer.CreateSprite(path, OsbOrigin.Centre, new Vector2(x, y));
            gear.Color(start, colour);
            gear.Fade(start, fade);
            gear.Scale(start, scale);
            return gear;
        }

        private void gearDrop(float x, float y, double realStart, double end, bool cw = true, string path = "") {
            string actualPath = path == "" ? gearPaths[Random(gearPaths.Length)] : path;
            OsbSprite ogGear, gear;
            double lor = Random(10);
            double yTo = Random (240, 330);
            double rot = Random(3);
            double start = 76174;

            ogGear = gearConfig(actualPath, realStart, x, y, 1, 0.2, Constants.black);
            ogGear.Fade(start, 0);
            ogGear.Rotate(realStart, end, rot, rot + (Math.PI/6) * (cw ? 1 : -0.7));

            gear = gearConfig(actualPath, start, x, y, 0.3, 0.2, Constants.blue);
            gear.Fade(OsbEasing.InBack, start + Constants.beatLength/2, end, gear.OpacityAt(start + Constants.beatLength/2), 0);
            gear.Move(OsbEasing.OutExpo, start, end, gear.PositionAt(start), gear.PositionAt(start).X + lor, yTo + Random(10));
            gear.Rotate(start, end, ogGear.RotationAt(start), ogGear.RotationAt(end));

            gear = gearConfig(actualPath, start, x, y, 0.3, 0.2, Constants.red);
            gear.Fade(OsbEasing.InBack, start + Constants.beatLength/2, end, gear.OpacityAt(start + Constants.beatLength/2), 0);
            gear.Move(OsbEasing.OutExpo, start, end, gear.PositionAt(start), gear.PositionAt(start).X - lor, yTo + Random(5));
            gear.Rotate(start, end, ogGear.RotationAt(start), ogGear.RotationAt(end));
        }
    }
}
