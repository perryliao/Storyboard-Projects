using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System.Linq;

namespace StorybrewScripts
{
    public class Background : StoryboardObjectGenerator
    {
        StoryboardLayer background;
        StoryboardLayer foreground;
        public override void Generate()
        {
		    background = GetLayer("Background");
		    foreground = GetLayer("Foreground");
            
            OsbSprite violet = background.CreateSprite("violet.jpg", OsbOrigin.Centre);
            violet.Fade(0, 0);

            OsbSprite bg = background.CreateSprite("sb/1x1.jpg", OsbOrigin.Centre);
            bg.ScaleVec(0, Constants.width, Constants.height);
            bg.Color(0, Colours.white);
            bg.Fade(0, 1);

            bg.Color(OsbEasing.OutCirc, 194330, 194687, bg.ColorAt(194330), Colours.blue);
            bg.Fade(206116, 0);
            bg.Fade(208973, 211830, 0, 1);
            bg.Color(208973, Colours.white);

            bg.Fade(287445, 0);
            var maskBitmap = GetMapsetBitmap("sb/bgshadow.png");
            OsbSprite bgMask = foreground.CreateSprite("sb/bgshadow.png", OsbOrigin.Centre);
            bgMask.Fade(0, 1);
            bgMask.Scale(0, 480.0f / maskBitmap.Height);
            bgMask.Fade(287445, 0);
        }
    }
}
