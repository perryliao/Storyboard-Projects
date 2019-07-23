using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System.Linq;

namespace StorybrewScripts
{
    public class Background : StoryboardObjectGenerator
    {
        StoryboardLayer layer;
        public override void Generate()
        {
		    layer = GetLayer("Background");
            
            OsbSprite violet = layer.CreateSprite("violet.jpg", OsbOrigin.Centre);
            violet.Fade(0, 0);

            OsbSprite bg = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.Centre);
            bg.ScaleVec(0, 854, 480);
            bg.Color(0, Colours.white);
            bg.Fade(0, 1);
            bg.Fade(287445, 0);

            var maskBitmap = GetMapsetBitmap("sb/bgshadow.png");
            OsbSprite bgMask = layer.CreateSprite("sb/bgshadow.png", OsbOrigin.Centre);
            bgMask.Fade(0, 1);
            bgMask.Scale(0, 480.0f / maskBitmap.Height);
            bgMask.Fade(287445, 0);
        }
    }
}
