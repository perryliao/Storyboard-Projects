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
    public class UnisonLyrics : StoryboardObjectGenerator
    {

        private string[] pushLeftStrings = { "輝いている", "つなぎ合わせば" };
        StoryboardLayer textLayer;
        StoryboardLayer glowLayer;

        public override void Generate()
        {
            textLayer = GetLayer("Lyrics");
            glowLayer = GetLayer("Glow");

		    FontGenerator font = LoadFont("sb/lyrics", new FontDescription() {
                FontPath = Constants.jpFont,
                FontSize = Constants.fontSize,
                Color = Colours.white,
                TrimTransparency = true,
                EffectsOnly = false,
                Debug = false,
            });

            FontGenerator fontGlow = LoadFont("sb/lyrics/glow", new FontDescription() {
                FontPath = Constants.jpFont,
                FontSize = Constants.fontSize,
                Color = Colours.black,
                TrimTransparency = true,
                EffectsOnly = true,
                Debug = false,
            }, new FontGlow() {
                Radius = Constants.glowRadius,
                Color = Colours.white,
            });

            SubtitleSet subtitles = LoadSubtitles("lyrics/unison.ass");

            generateLyrics(font, subtitles, false);
            generateLyrics(fontGlow, subtitles, true);
        }

        private void generateLyrics(FontGenerator font, SubtitleSet subtitles, bool additive) {
            foreach (SubtitleLine line in subtitles.Lines) {
                if ( Helpers.inChorus(line.StartTime, line.EndTime)) {
                    chorus(font, line, additive);
                } else if ((line.StartTime > 73973 && line.EndTime < 86116) || (line.StartTime > 159687 && line.EndTime < 168973)) {
                    preChorus(font, line, additive);
                } else {
                    verticalPositioning(font, line, additive);
                }
            }
        }

        private void chorus(FontGenerator font, SubtitleLine line, bool additive) {
            StoryboardLayer layer = additive ? glowLayer : textLayer;
        }

        private void preChorus(FontGenerator font, SubtitleLine line, bool additive) {
            StoryboardLayer layer = additive ? glowLayer : textLayer;
            float fontScale = Constants.fontScale + 0.2f;
            foreach (string lyric in line.Text.Split('\n')) {
                float width = 0f;
                float height = 0f;
                foreach (char character in lyric) {
                    FontTexture texture = font.GetTexture(character.ToString());
                    width += texture.BaseWidth * fontScale;
                    height = Math.Max(height, texture.BaseHeight * fontScale);
                }
                
                float x = pushLeftStrings.Any(lyric.ToString().Contains) ? 320 - width/lyric.Length/2 - 5: 320 + width/lyric.Length/2 + 5;
                float y = Math.Max(40, 240 - width/2 - 80);
                double relativeStart = line.StartTime - Constants.beatLength/2;
                foreach (char character in lyric) {
                    FontTexture texture = font.GetTexture(character.ToString());
                    if (!texture.IsEmpty) {
                        Vector2 pos = new Vector2(x, y) + texture.OffsetFor(OsbOrigin.Centre) * fontScale;
                        OsbSprite sprite = layer.CreateSprite(texture.Path, OsbOrigin.Centre);
               
                        sprite.Scale(relativeStart, fontScale);
                        sprite.Move(OsbEasing.OutExpo, relativeStart, line.EndTime, pos.X, pos.Y - 20, pos.X, pos.Y);
                        sprite.Fade(OsbEasing.In, relativeStart, relativeStart + Constants.beatLength, 0, 1);
                        sprite.Fade(line.EndTime - Constants.beatLength/2, line.EndTime, 1, 0);
                        if (additive) {
                            sprite.Additive(relativeStart, line.EndTime);
                            sprite.Color(relativeStart, Colours.purple);
                        } else {
                            sprite.Color(relativeStart, Colours.black);
                        }
                    }
                    y += texture.BaseWidth * fontScale;
                }
                x += height;
            }
        }

        private void verticalPositioning(FontGenerator font, SubtitleLine line, bool additive) {
            StoryboardLayer layer = additive ? glowLayer : textLayer;
            float x = 320;
            float fontScale = Constants.fontScale + 0.15f;
            float extraPadding = 15f;
            foreach (string lyric in line.Text.Split('\n')) {
                float width = 0f;
                float height = 0f;
                foreach (char character in lyric) {
                    FontTexture texture = font.GetTexture(character.ToString());
                    width += texture.BaseWidth * fontScale + extraPadding;
                    height = Math.Max(height, texture.BaseHeight * fontScale);
                }
                
                float y = 240 - width/2;
                double relativeStart = line.StartTime - Constants.beatLength/2;
                foreach (char character in lyric) {
                    FontTexture texture = font.GetTexture(character.ToString());
                    if (!texture.IsEmpty) {
                        Vector2 pos = new Vector2(x, y) + texture.OffsetFor(OsbOrigin.Centre) * fontScale;
                        OsbSprite sprite = layer.CreateSprite(texture.Path, OsbOrigin.Centre, new Vector2(320, 240));
               
                        sprite.Scale(relativeStart, fontScale);
                        sprite.MoveY(OsbEasing.OutExpo, relativeStart, line.EndTime, sprite.PositionAt(relativeStart).Y, pos.Y);
                        sprite.Fade(OsbEasing.OutCirc, relativeStart, relativeStart + Constants.beatLength, 0, 1);
                        // sprite.Fade(line.EndTime - Constants.beatLength/2, line.EndTime, 1, 0);
                        if (additive) {
                            sprite.Additive(relativeStart, line.EndTime);
                            sprite.Color(relativeStart, Colours.purple);
                            sprite.Color(OsbEasing.OutCirc, line.EndTime, line.EndTime + Constants.beatLength, Colours.purple, Colours.black);
                            sprite.Fade(line.EndTime, line.EndTime + Constants.beatLength, 1, 0);
                            sprite.Scale(line.EndTime, line.EndTime + Constants.beatLength, sprite.ScaleAt(line.EndTime).X, sprite.ScaleAt(line.EndTime).X + 0.1f);
                        } else {
                            sprite.Fade(OsbEasing.OutCirc, line.EndTime, line.EndTime + Constants.beatLength, 1, 0);
                            sprite.Scale(line.EndTime, line.EndTime + Constants.beatLength, sprite.ScaleAt(line.EndTime).X, sprite.ScaleAt(line.EndTime).X + 0.1f);
                            sprite.Color(relativeStart, Colours.black);
                        }
                    }
                    y += texture.BaseWidth * fontScale + extraPadding;
                    // relativeStart += Constants.beatLength/8;
                }
                x += height;
            }
        }
    }
}
