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
    public class Bg : StoryboardObjectGenerator
    {
        [Configurable]
        public string BackgroundPath = "sb/etc/bg.jpg";

        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 347010;

        [Configurable]
        public double Opacity = 0.2;

        public override void Generate()
        {
            var bitmap = GetMapsetBitmap(BackgroundPath);

            if (StartTime == EndTime)
                EndTime = (int)Beatmap.HitObjects.Last().EndTime;

            var bg = GetLayer("").CreateSprite(BackgroundPath, OsbOrigin.Centre);
            bg.Scale(0, 0, 0, 7.5, 4.5, 7.5, 4.5);
            bg.Fade(StartTime - 500, StartTime, 0, Opacity);
            bg.Fade(EndTime, EndTime + 500, Opacity, 0);
        }
    }
}
