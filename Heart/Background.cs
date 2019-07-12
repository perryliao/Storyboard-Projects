using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System.Linq;

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

        
        private int width = 854;
        private int height = 480;

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
            tileBG.ColorHsb(34527, 0, 0, 0.1);
            tileBG.ScaleVec(34527, width, height);
            tileBG.Fade(OsbEasing.OutExpo, 33821, 34527, 0, 0.6);
            tileBG.Fade(42997, 0);

            OsbSprite letterboxBG = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.Centre);
            letterboxBG.ColorHsb(23233, 51, 0.09, 0.9);
            letterboxBG.Fade(23233, 1);
            letterboxBG.ScaleVec(23233, width, height * 0.7);
            letterboxBG.Fade(OsbEasing.InCirc, 33821, 34527, 1, 0);

            
            letterboxBG.Fade(209586, 1);
            letterboxBG.Fade(232174, 0);
        }
    }
}
