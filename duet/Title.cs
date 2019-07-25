using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Subtitles;
using System;
using System.Linq;
using System.Drawing;
using System.IO;

namespace StorybrewScripts
{
    public class Title : StoryboardObjectGenerator
    {

        StoryboardLayer layer;
        public override void Generate()
        {
            layer = GetLayer("Title"); 
            setTitleSubs(Constants.jpFont, "lyrics/credit_jp.ass");
            setTitleSubs(Constants.enFont, "lyrics/credit_en.ass");

            OsbSprite violet = layer.CreateSprite("sb/title/violet.png", OsbOrigin.Centre, new Vector2(320, 240));
            var bitmap = GetMapsetBitmap("sb/title/violet.png");

            violet.Fade(11830, 12187, 0, 0.8);
            violet.Fade(22902, 23259, violet.OpacityAt(22902), 0);
            violet.Color(11830, Colours.black);
            violet.Scale(11830, 45.0f / bitmap.Height);
        }

        private void setTitleSubs(string fontType, string subtitlePath) {
            FontGenerator font = LoadFont("sb/title/" + fontType, new FontDescription() {
                FontPath = fontType,
                FontSize = 60,
                Color = Colours.black,
                TrimTransparency = true,
                EffectsOnly = false,
                Debug = false,
            });

            SubtitleSet subtitles = LoadSubtitles(subtitlePath);
            foreach (SubtitleLine line in subtitles.Lines) {
                float width = 0f;
                float height = 0f;
                foreach (char character in line.Text) {
                    FontTexture texture = font.GetTexture(character.ToString());
                    width += texture.BaseWidth * Constants.fontScale;
                    height = Math.Max(height, texture.BaseHeight * Constants.fontScale);
                }

                float x = 320 - width/2;
                foreach (char character in line.Text) {
                    FontTexture texture = font.GetTexture(character.ToString());
                    if (!texture.IsEmpty) {
                        Vector2 pos = new Vector2(x, 240 - height/2 + 8) + texture.OffsetFor(OsbOrigin.TopCentre) * Constants.fontScale;
                        OsbSprite sprite = layer.CreateSprite(texture.Path, OsbOrigin.TopCentre, pos);

                        sprite.Scale(line.StartTime, Constants.fontScale);
                        sprite.Fade(line.StartTime, 1);
                        sprite.Fade(line.EndTime, 0);
                    }
                    x += texture.BaseWidth * Constants.fontScale;
                }
            }
        }
    }
}
