using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System;
using System.Linq;
using System.Collections.Generic;

namespace StorybrewScripts
{
    public class Background : StoryboardObjectGenerator
    {
        [Configurable]
        public string BackgroundPath = "";

        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 0;

        [Configurable]
        public double Opacity = 0.2;

        private double[] poisonArr = new double[34] { 69468, 69644, 69821,  70350, 72292,  72468, 72644, 73174,  73703,  74056, 74586, 75115, 76527, 76880, 77409, 77939, 80762, 80939, 81115, 81644, 83586, 83762, 83939, 84468, 84997, 85350, 85880, 86409, 87997, 88174, 88703, 89233, 89762, 90292 };
        private double[] poisonFadeArr = new double[6] { 70350, 75115, 77939, 81821, 86409, 90292 };

        public override void Generate()
        {
            // Configuring default bg
            if (BackgroundPath == "") BackgroundPath = Beatmap.BackgroundPath ?? string.Empty;
            if (StartTime == EndTime) EndTime = (int)(Beatmap.HitObjects.LastOrDefault()?.EndTime ?? AudioDuration);

            StoryboardLayer layer = GetLayer("Background");
            var bitmap = GetMapsetBitmap(BackgroundPath);
            OsbSprite bg = layer.CreateSprite(BackgroundPath, OsbOrigin.Centre);
            bg.Scale(StartTime, 480.0f / bitmap.Height);
            bg.Fade(StartTime, 0);

            OsbSprite tileBG = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.Centre);
            tileBG.Color(34527, Constants.black);
            tileBG.ScaleVec(34527, Constants.width, Constants.height);
            tileBG.Fade(OsbEasing.OutExpo, 33821, 34527, 0, 0.6);
            tileBG.Fade(42997, 0);

            OsbSprite letterboxBG = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.Centre);
            letterboxBG.ColorHsb(23233, 51, 0.09, 0.9);
            letterboxBG.Fade(23233, 1);
            letterboxBG.ScaleVec(23233, Constants.width, Constants.height * 0.7);
            letterboxBG.Fade(OsbEasing.InCirc, 33821, 34527, 1, 0);
            
            poisonBG(tileBG, 0.4);

            letterboxBG.Fade(209586, 1);
            letterboxBG.Fade(232174, 0);
        }

        public void poisonBG(OsbSprite bg, double opacity) {
            double start, end;
            Color4 colour;

            // Set Opacity
            foreach(double time in poisonFadeArr) {
                bg.Fade(OsbEasing.InCirc, time, time + Constants.beatLength, opacity, 0);
            }

            // Set Colour
            foreach(double time in poisonArr) {
                start = time - Constants.beatLength/4;
                end = time;

                do {
                    colour = Constants.randomColours[Random(Constants.randomColours.Length)];
                } while (colour.R * 255 == bg.ColorAt(start).R && 
                       colour.G * 255 == bg.ColorAt(start).G && 
                       colour.B * 255 == bg.ColorAt(start).B );

                if (bg.OpacityAt(start) == 0) 
                    bg.Fade(OsbEasing.InExpo, start, time, bg.OpacityAt(start - Constants.beatLength/4), opacity);
                bg.Color(OsbEasing.InExpo, start, end, bg.ColorAt(start), colour);
            }
        }
    }
}
