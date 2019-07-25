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
    public class ScrollingLyricBG : StoryboardObjectGenerator
    {
        [Configurable]
        public double startTime = 68616; // the upbeat before the vocal downbeat
        [Configurable]
        public double barHeight = 80;

        StoryboardLayer layer;
        public override void Generate()
        {
            layer = GetLayer("ScrollingLyricBG");
            OsbSprite bar1 = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.BottomCentre);
            OsbSprite bar2 = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.BottomCentre);
            OsbSprite bar3 = layer.CreateSprite("sb/othersbg.jpg", OsbOrigin.Centre);
            var bar3BitMap = GetMapsetBitmap("sb/othersbg.jpg");

            FontGenerator font = LoadFont("sb/lyrics/", new FontDescription() {
                FontPath = Constants.jpFont,
                FontSize = Constants.fontSize,
                Color = Colours.white,
                TrimTransparency = true,
                EffectsOnly = false,
                Debug = false,
            });
            
            SubtitleSet subtitles = LoadSubtitles("lyrics/scrolltext.ass");
            foreach (SubtitleLine line in subtitles.Lines) {
                float y = (float)(Constants.height + barHeight);
                int count = 0;
                foreach (string lyric in line.Text.Split('\n')) {
                    float width = 0f;
                    float height = 0f;
                    foreach (char character in lyric) {
                        FontTexture texture = font.GetTexture(character.ToString());
                        width += texture.BaseWidth * Constants.fontScale;
                        height = Math.Max(height, texture.BaseHeight * Constants.fontScale);
                    }

                    float x = 320 - width/2;
                    float relativeY = y;
                    double relativeStart = line.StartTime;
                    foreach (char character in lyric) {
                        FontTexture texture = font.GetTexture(character.ToString());
                        if (!texture.IsEmpty) {
                            Vector2 pos = new Vector2(x, relativeY) + texture.OffsetFor(OsbOrigin.BottomCentre) * Constants.fontScale;
                            OsbSprite sprite = layer.CreateSprite(texture.Path, OsbOrigin.BottomCentre, pos);

                            sprite.Scale(relativeStart, Constants.fontScale);
                            sprite.Fade(relativeStart, relativeStart + Constants.beatLength/2, 0, 1);
                            sprite.Fade(line.EndTime - Constants.beatLength/2, line.EndTime, 1, 0);
                            handleLyricBar(sprite, line.StartTime, 0, Colours.white, (float)x, (float)y, true);
                        }
                        x += texture.BaseWidth * Constants.fontScale;
                    }
                    y += height;
                }
            }

            handleLyricBar(bar1, startTime, -Math.PI/Random(16, 32), Colours.blue);
            handleLyricBar(bar2, startTime + Constants.beatLength*4, Math.PI/Random(16, 32), Colours.pink);

            double finalIn = startTime + Constants.beatLength * 8;
            double finalOut = finalIn + Constants.beatLength * 12;
            
            bar3.Fade(finalIn, finalIn + Constants.beatLength/2, 0, 1);
            bar3.Fade(finalOut - Constants.beatLength*4, finalOut, 1, 0);
            bar3.Scale(finalIn, 480.0f / bar3BitMap.Height);
        }

        private void handleLyricBar(OsbSprite bar, double start, double rotation, Color4 colour, double x = 320, double y = 480, bool skipScale = false) {
            if (!skipScale) bar.ScaleVec(start, Constants.width/Math.Cos(rotation) + 40, barHeight);
            
            bar.Rotate(start, rotation);
            bar.Color(start, colour);

            bar.Move(OsbEasing.OutCirc, start, start + Constants.beatLength/2, 320, Constants.height + barHeight, 320, 240 + barHeight);
            bar.Move(OsbEasing.OutCirc, start + Constants.beatLength/2, start + Constants.beatLength * 4, bar.PositionAt(start + Constants.beatLength/2), bar.PositionAt(start + Constants.beatLength/2).X, 240 + barHeight/2);
            bar.Move(OsbEasing.InCirc, start + Constants.beatLength * 4, start + Constants.beatLength * 9 / 2, bar.PositionAt(start + Constants.beatLength * 4), bar.PositionAt(start + Constants.beatLength * 4).X, 0);
        }
    } 
}
