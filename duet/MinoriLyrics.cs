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
    public class MinoriLyrics : StoryboardObjectGenerator
    {
        [Configurable]
        public float padding = 60f;
        [Configurable]
        public int fineness = 2;
        [Configurable]
        public float chrousScale = Constants.fontScale;

        private string[] pushUpStrings = { "何色だろう", "言えないけど", "与えられていく", "茶化したけど" };

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

            SubtitleSet subtitles = LoadSubtitles("lyrics/minori.ass");

            generateLyrics(font, subtitles, false);
            generateLyrics(fontGlow, subtitles, true);
        }

        private void generateLyrics(FontGenerator font, SubtitleSet subtitles, bool additive) {
            foreach (SubtitleLine line in subtitles.Lines) {
                if (Helpers.inChorus(line.StartTime, line.EndTime)) {
                    chorus(font, line, additive);
                } else if (line.StartTime > 217544 && line.StartTime < 260402) { 
                    verticalPositioning(font, line, additive);
                } else {
                    handlePlacement(font, line, additive);
                }
            }
        }

        private void pixelize(string path, double start, double end, Vector2 pos) {
            Bitmap textBitmap = GetMapsetBitmap(path);
            double duration = end - start;
            double relativeStart;
            double timestep = Constants.beatLength/4;
            for (int x = 0; x < textBitmap.Width ; x += fineness) {
                for (int y = 0; y < textBitmap.Height ; y += fineness) {
                    Vector2 spritePos = new Vector2((float)x, (float)y - textBitmap.Height/2);
                    spritePos = Vector2.Multiply(spritePos, chrousScale);
                    Color pixelColor = textBitmap.GetPixel(x, y);
                    relativeStart = start + (x+y)*5;
                    if (pixelColor.A > 0) {
                        var sprite = textLayer.CreateSprite("sb/circle2.png", OsbOrigin.Centre, spritePos + pos);
                        sprite.Scale(OsbEasing.None, relativeStart, relativeStart + timestep, 0, 0.075);
                        sprite.Fade(relativeStart, 0.4);
                        sprite.Fade(end - Constants.beatLength, end, 1, 0);
                        sprite.Color(relativeStart, Colours.black);

                        double tmp = 0, inc = 9;
                        for (double i = relativeStart; i < relativeStart + duration - timestep; i += timestep) {
                            // y = sqrt(x * Random(inc))
                            sprite.Move(i, i + timestep, sprite.PositionAt(i), sprite.PositionAt(i).X + tmp, sprite.PositionAt(i).Y - Math.Sqrt(tmp * Random(10) / Random(5)));
                            tmp += inc;
                        }
                    }
                }
            }
        }

        private void handlePlacement(FontGenerator font, SubtitleLine line, bool additive) {
            StoryboardLayer layer = additive ? glowLayer : textLayer;
            float y = (float)Constants.height - padding;
            foreach (string lyric in line.Text.Split('\n')) {
                float width = 0f;
                float height = 0f;
                foreach (char character in lyric) {
                    FontTexture texture = font.GetTexture(character.ToString());
                    width += texture.BaseWidth * Constants.fontScale;
                    height = Math.Max(height, texture.BaseHeight * Constants.fontScale);
                }

                float x = (float)Constants.xCeil - padding - width;
                double relativeStart = line.StartTime - Constants.beatLength/2;
                foreach (char character in lyric) {
                    FontTexture texture = font.GetTexture(character.ToString());
                    if (!texture.IsEmpty) {
                        Vector2 pos = new Vector2(x, y - (pushUpStrings.Any(lyric.ToString().Contains) ? height : 0)) + texture.OffsetFor(OsbOrigin.CentreLeft) * Constants.fontScale;
                        OsbSprite sprite = layer.CreateSprite(texture.Path, OsbOrigin.CentreLeft, pos);
                        sprite.Scale(relativeStart, Constants.fontScale);
                        sprite.Fade(relativeStart, relativeStart + Constants.beatLength/2, 0, 1);
                        sprite.Fade(line.EndTime - Constants.beatLength/2, line.EndTime, 1, 0);
                        if (additive) {
                            sprite.Additive(relativeStart, line.EndTime);
                            sprite.Color(relativeStart, Colours.pink);
                        } else {
                            sprite.Color(relativeStart, character.ToString() == "色" ? Colours.pink : Colours.black);                        
                        }
                    }
                    x += texture.BaseWidth * Constants.fontScale;
                    relativeStart += Constants.beatLength/8;
                }
                y += height;
            }
        }

        private void verticalPositioning(FontGenerator font, SubtitleLine line, bool additive) {
            StoryboardLayer layer = additive ? glowLayer : textLayer;
            float x = (float)(Constants.width * 3 / 4 + Constants.xFloor);
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
                        sprite.Fade(line.EndTime - Constants.beatLength/2, line.EndTime, 1, 0);
                        if (additive) {
                            sprite.Additive(relativeStart, line.EndTime);
                            sprite.Color(relativeStart, Colours.pink); 
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

        private void chorus(FontGenerator font, SubtitleLine line, bool additive) {
            if (additive) return;
            StoryboardLayer layer = additive ? glowLayer : textLayer;
            float y = (float) Random(240, Constants.height - padding);
            foreach (string lyric in line.Text.Split('\n')) {
                float width = 0f;
                float height = 0f;
                foreach (char character in lyric) {
                    FontTexture texture = font.GetTexture(character.ToString());
                    width += texture.BaseWidth * chrousScale;
                    height = Math.Max(height, texture.BaseHeight * chrousScale);
                }

                float x = (float)Random(320, Constants.xCeil - padding - width);
                double relativeStart = line.StartTime - Constants.beatLength/2;
                bool vertFlag = true;
                foreach (char character in lyric) {
                    FontTexture texture = font.GetTexture(character.ToString());
                    if (!texture.IsEmpty) {
                        Vector2 pos = new Vector2(x, y) + texture.OffsetFor(OsbOrigin.CentreLeft) * chrousScale;
                        OsbSprite sprite = layer.CreateSprite(texture.Path, OsbOrigin.CentreLeft, pos);
                        sprite.Scale(relativeStart, chrousScale);
                        sprite.Move(OsbEasing.Out, relativeStart, line.EndTime, sprite.PositionAt(relativeStart), sprite.PositionAt(relativeStart).X + 3, sprite.PositionAt(relativeStart).Y + (vertFlag ? -Random(4) : Random(4)));
                        
                        sprite.Fade(relativeStart, relativeStart + Constants.beatLength/2, 0, 1);
                        sprite.Fade(OsbEasing.InQuart, line.EndTime, line.EndTime + Constants.beatLength/2, 1, 0);
                        
                        if (line.StartTime > 230401) {
                            pixelize(texture.Path, line.EndTime, line.EndTime + Constants.beatLength*2, sprite.PositionAt(line.EndTime));
                        } else {
                            bool left = Random(-6, 5) < 0;
                            sprite.Move(line.EndTime, line.EndTime + Constants.beatLength/2, sprite.PositionAt(line.EndTime), 
                                sprite.PositionAt(line.EndTime).X + (left ? -Random(5) : Random(5)),
                                sprite.PositionAt(line.EndTime).Y + Random(15, 20)
                            );
                            sprite.Rotate(line.EndTime, line.EndTime + Constants.beatLength/2, sprite.RotationAt(line.EndTime), sprite.RotationAt(line.EndTime) + Math.PI/Random(8, 32) * (left ? -1 : 1));
                        }
                        sprite.Color(relativeStart, Colours.black);                        
                        
                    }
                    x += texture.BaseWidth * chrousScale;
                    relativeStart += Constants.beatLength/8;
                    vertFlag = !vertFlag;
                }
                y += height;
            }
        }
    }
}
