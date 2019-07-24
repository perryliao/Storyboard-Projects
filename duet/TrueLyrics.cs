using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Subtitles;
using System;
using System.Drawing;
using System.IO;

namespace StorybrewScripts
{
    public class TrueLyrics : StoryboardObjectGenerator
    {
        [Configurable]
        public float padding = 50f;

        StoryboardLayer textLayer;
        StoryboardLayer glowLayer;
        public override void Generate()
        {
            textLayer = GetLayer("Lyrics");
            glowLayer = GetLayer("Glow");

		    FontGenerator font = LoadFont("sb/lyrics/true", new FontDescription() {
                FontPath = Constants.jpFont,
                FontSize = Constants.fontSize,
                Color = Colours.black,
                TrimTransparency = true,
                EffectsOnly = false,
                Debug = false,
            }, new FontGlow() {
                Radius = 1,
                Color = Colours.black,
            });

            FontGenerator fontGlow = LoadFont("sb/lyrics/true/glow", new FontDescription() {
                FontPath = Constants.jpFont,
                FontSize = Constants.fontSize,
                Color = Colours.black,
                TrimTransparency = true,
                EffectsOnly = true,
                Debug = false,
            }, new FontGlow() {
                Radius = Constants.glowRadius,
                Color = Colours.blue,
            });

            SubtitleSet subtitles = LoadSubtitles("lyrics/true.ass");

            generateLyrics(font, subtitles, false);
            // generateLyrics(fontGlow, subtitles, true);
        }

        private void generateLyrics(FontGenerator font, SubtitleSet subtitles, bool additive) {
            StoryboardLayer layer = additive ? glowLayer : textLayer;
            foreach (SubtitleLine line in subtitles.Lines) {
                float y = padding;
                foreach (string lyric in line.Text.Split('\n')) {
                    float width = 0f;
                    float height = 0f;
                    foreach (char character in lyric) {
                        FontTexture texture = font.GetTexture(character.ToString());
                        width += texture.BaseWidth * Constants.fontScale;
                        height = Math.Max(height, texture.BaseHeight * Constants.fontScale);
                    }

                    float x = (float)Constants.xFloor + padding;
                    foreach (char character in lyric) {
                        FontTexture texture = font.GetTexture(character.ToString());
                        if (!texture.IsEmpty) {
                            Vector2 pos = new Vector2(x, y) + texture.OffsetFor(OsbOrigin.CentreLeft) * Constants.fontScale;
                            OsbSprite sprite = layer.CreateSprite(texture.Path, OsbOrigin.CentreLeft, pos);

                            sprite.Scale(line.StartTime - Constants.beatLength/2, Constants.fontScale);
                            sprite.Fade(line.StartTime - Constants.beatLength/2, line.StartTime, 0, 1);
                            sprite.Fade(line.EndTime - Constants.beatLength/2, line.EndTime, 1, 0);
                            if (additive) sprite.Additive(line.StartTime - Constants.beatLength/2, line.EndTime);
                        }
                        x += texture.BaseWidth * Constants.fontScale;
                    }
                    y += height;
                }
            }
        }
    }
}
