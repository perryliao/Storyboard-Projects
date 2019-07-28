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
        public float padding = 60f;

        StoryboardLayer textLayer;
        StoryboardLayer glowLayer;
        StoryboardLayer lyricBarLayer;

        OsbSprite bar;
        OsbSprite skinnyBar;
        public override void Generate()
        {
            textLayer = GetLayer("Lyrics");
            glowLayer = GetLayer("Glow");
            lyricBarLayer = GetLayer("LyricBar");
            skinnyBar = lyricBarLayer.CreateSprite("sb/1x1.jpg", OsbOrigin.CentreRight);
            bar = lyricBarLayer.CreateSprite("sb/1x1.jpg", OsbOrigin.CentreLeft);

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

            SubtitleSet subtitles = LoadSubtitles("lyrics/true.ass");

            generateLyrics(font, subtitles, false);
            generateLyrics(fontGlow, subtitles, true);
        }

        private void generateLyrics(FontGenerator font, SubtitleSet subtitles, bool additive) {
            foreach (SubtitleLine line in subtitles.Lines) {
                if (line.Text == "ふたりごと") {
                    preChorus(font, line, additive);
                } else if (line.StartTime > 220401 && line.StartTime < 260402) {
                    verticalPositioning(font, line, additive);
                } else {
                    handlePlacement(font, line, additive);
                }
            }
        }

        private void handlePlacement(FontGenerator font, SubtitleLine line, bool additive) {
            StoryboardLayer layer = additive ? glowLayer : textLayer;
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
                double relativeStart = line.StartTime - Constants.beatLength/2;
                foreach (char character in lyric) {
                    FontTexture texture = font.GetTexture(character.ToString());
                    if (!texture.IsEmpty) {
                        Vector2 pos = new Vector2(x, y + (lyric.ToString() == "明るいから" ? height : 0)) + texture.OffsetFor(OsbOrigin.CentreLeft) * Constants.fontScale;
                        OsbSprite sprite = layer.CreateSprite(texture.Path, OsbOrigin.CentreLeft, pos);

                        sprite.Scale(relativeStart, Constants.fontScale);
                        sprite.Fade(relativeStart, relativeStart + Constants.beatLength/2, 0, 1);
                        sprite.Fade(line.EndTime - Constants.beatLength/2, line.EndTime, 1, 0);
                        if (additive) {
                            sprite.Additive(relativeStart, line.EndTime);
                            sprite.Color(relativeStart, Colours.cyan); 
                        } else {
                            sprite.Color(relativeStart, character.ToString() == "青" ? Colours.blue : Colours.black);                        
                        }
                    }
                    x += texture.BaseWidth * Constants.fontScale;
                    relativeStart += Constants.beatLength/8;
                }
                y += height;
            }
        }

        private void preChorus(FontGenerator font, SubtitleLine line, bool additive) {
            StoryboardLayer layer = additive ? glowLayer : textLayer;
            float y = (float)Constants.height - padding - 40f;
            float x = (float)Constants.xFloor + padding + 30f;
            foreach (string lyric in line.Text.Split('\n')) {
                float width = 0f;
                float height = 0f;
                foreach (char character in lyric) {
                    FontTexture texture = font.GetTexture(character.ToString());
                    width += texture.BaseWidth * Constants.fontScale;
                    height = Math.Max(height, texture.BaseHeight * Constants.fontScale);
                }

                // bg bar 
                if (additive) {
                    double barStart = line.StartTime - Constants.beatLength;
                    bar.Color(barStart, Colours.blue);
                    bar.Fade(barStart, line.StartTime, 0, 0.9);
                    bar.Fade(line.EndTime - Constants.beatLength, line.EndTime, bar.OpacityAt(line.EndTime - Constants.beatLength), 0);

                    bar.ScaleVec(OsbEasing.OutCirc, barStart, line.StartTime, 0, height*0.66, width + 20, height*0.66);
                    bar.ScaleVec(OsbEasing.OutCirc, line.EndTime - Constants.beatLength, line.EndTime, bar.ScaleAt(line.EndTime - Constants.beatLength).X, bar.ScaleAt(line.EndTime - Constants.beatLength).Y, 0, bar.ScaleAt(line.EndTime - Constants.beatLength).Y);
                    bar.Move(OsbEasing.OutCirc, barStart, line.StartTime, x - 20, y + height/2 - 9 , x - 10, y + height/2 - 9 );
                    bar.Move(OsbEasing.None, line.StartTime, line.EndTime - Constants.beatLength, bar.PositionAt(line.StartTime), bar.PositionAt(line.StartTime).X + 5, bar.PositionAt(line.StartTime).Y);
                    bar.Move(OsbEasing.OutCirc, line.EndTime - Constants.beatLength, line.EndTime, bar.PositionAt(line.EndTime - Constants.beatLength), bar.PositionAt(line.EndTime - Constants.beatLength).X + width + 20, bar.PositionAt(line.EndTime - Constants.beatLength).Y);
                
                    skinnyBar.Color(barStart, Colours.cyan);
                    skinnyBar.Fade(barStart, line.StartTime, 0, 0.3);
                    skinnyBar.Fade(line.EndTime - Constants.beatLength, line.EndTime, skinnyBar.OpacityAt(line.EndTime - Constants.beatLength), 0);

                    skinnyBar.ScaleVec(OsbEasing.OutCirc, barStart, line.StartTime, 0, 13, width + 20, 13);
                    skinnyBar.ScaleVec(OsbEasing.OutCirc, line.EndTime - Constants.beatLength, line.EndTime, skinnyBar.ScaleAt(line.EndTime - Constants.beatLength).X, skinnyBar.ScaleAt(line.EndTime - Constants.beatLength).Y, 0, skinnyBar.ScaleAt(line.EndTime - Constants.beatLength).Y);
                    skinnyBar.Move(OsbEasing.OutCirc, barStart, line.StartTime, x + width + 40, y + height * 0.6 , x + width + 30, y + height * 0.6 );
                    skinnyBar.Move(OsbEasing.OutCirc, line.EndTime - Constants.beatLength, line.EndTime, skinnyBar.PositionAt(line.EndTime - Constants.beatLength), skinnyBar.PositionAt(line.EndTime - Constants.beatLength).X - width - 20, skinnyBar.PositionAt(line.EndTime - Constants.beatLength).Y);
                }

                foreach (char character in lyric) {
                    FontTexture texture = font.GetTexture(character.ToString());
                    if (!texture.IsEmpty) {
                        Vector2 pos = new Vector2(x, y) + texture.OffsetFor(OsbOrigin.CentreLeft) * Constants.fontScale;
                        OsbSprite sprite = layer.CreateSprite(texture.Path, OsbOrigin.CentreLeft, pos);

                        sprite.Scale(line.StartTime - Constants.beatLength/2, Constants.fontScale);
                        sprite.Fade(line.StartTime - Constants.beatLength/2, line.StartTime, 0, 1);
                        sprite.Fade(line.EndTime - Constants.beatLength/2, line.EndTime, 1, 0);
                        if (additive) {
                            sprite.Additive(line.StartTime - Constants.beatLength/2, line.EndTime);
                            sprite.Color(line.StartTime - Constants.beatLength/2, Colours.cyan); 
                        }
                    }
                    x += texture.BaseWidth * Constants.fontScale;
                }
                y += height;
            }
        }

        private void verticalPositioning(FontGenerator font, SubtitleLine line, bool additive) {
            StoryboardLayer layer = additive ? glowLayer : textLayer;
            float x = (float)(Constants.width / 4 + Constants.xFloor);
            foreach (string lyric in line.Text.Split('\n')) {
                float width = 0f;
                float height = 0f;
                foreach (char character in lyric) {
                    FontTexture texture = font.GetTexture(character.ToString());
                    width += texture.BaseWidth * Constants.fontScale;
                    height = Math.Max(height, texture.BaseHeight * Constants.fontScale);
                }
                
                float y = 240 - width/2;
                double relativeStart = line.StartTime - Constants.beatLength/2;
                foreach (char character in lyric) {
                    FontTexture texture = font.GetTexture(character.ToString());
                    if (!texture.IsEmpty) {
                        Vector2 pos = new Vector2(x, y) + texture.OffsetFor(OsbOrigin.Centre) * Constants.fontScale;
                        OsbSprite sprite = layer.CreateSprite(texture.Path, OsbOrigin.Centre, pos);
                    
                        sprite.Scale(relativeStart, Constants.fontScale);
                        sprite.Fade(relativeStart, relativeStart + Constants.beatLength/2, 0, 1);
                        sprite.Fade(line.EndTime - Constants.beatLength, line.EndTime - Constants.beatLength/2, 1, 0);
                        if (additive) {
                            sprite.Additive(relativeStart, line.EndTime);
                            sprite.Color(relativeStart, Colours.cyan); 
                        } else { 
                            sprite.Color(relativeStart, Colours.black);
                        }
                    }
                    y += texture.BaseWidth * Constants.fontScale;
                    relativeStart += Constants.beatLength/8;
                }
                x += height;
            }
        }
    }
}
