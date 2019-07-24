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
    public class Title : StoryboardObjectGenerator
    {

        StoryboardLayer layer;
        public override void Generate()
        {
            layer = GetLayer("Title"); 
            // Font is "Adobe Garamond Pro"
            setTitle("sb/title/true.png", 402, Constants.beatLength*3);
            setTitle("sb/title/chihara.png", 3259, Constants.beatLength*3);
            setTitle("sb/title/futarigoto.png", 6116, Constants.beatLength*3);
            setTitle("sb/title/credits.png", 8973, Constants.beatLength*3);

            OsbSprite violet = layer.CreateSprite("sb/title/violet.png", OsbOrigin.Centre, new Vector2(320, 240));
            var bitmap = GetMapsetBitmap("sb/title/violet.png");

            violet.Fade(11830, 12187, 0, 0.8);
            violet.Fade(22902, 23259, violet.OpacityAt(22902), 0);
            violet.Color(11830, Colours.black);
            violet.Scale(11830, 45.0f / bitmap.Height);
        }

        private void setTitle(string path, double startTime, double duration, float x = 320, float y = 240) {
            OsbSprite title = layer.CreateSprite(path, OsbOrigin.Centre, new Vector2(x, y));
            var bitmap = GetMapsetBitmap(path);

            title.Fade(startTime, 1);
            title.Fade(startTime + duration, 0);
            title.Color(startTime, Colours.black);
            title.Scale(startTime, 30.0f / bitmap.Height);
        }
    }
}
