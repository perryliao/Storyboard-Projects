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
        public double barHeight = 80;
        [Configurable]
        public float FontScale = Constants.fontScale;

        StoryboardLayer layer;
        StoryboardLayer bgLayer;
        public override void Generate()
        {
            layer = GetLayer("ScrollingLyrics");
            bgLayer = GetLayer("ScrollingLyricsBG");
            OsbSprite barRef;
            OsbSprite bar3 = bgLayer.CreateSprite("sb/othersbg.jpg", OsbOrigin.Centre);
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
            
            int count = 0;
            foreach (SubtitleLine line in subtitles.Lines) {
                barRef = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.BottomCentre);
                bool singer1 = count % 2 == 0;
                float y = (float)(Constants.height + barHeight/2) + (singer1 ? 0 : -23);
                double rotation = Math.PI/Random(16, 32) * (singer1 ? -1 : 1);
                foreach (string lyric in line.Text.Split('\n')) {
                    float width = 0f;
                    float height = 0f;
                    foreach (char character in lyric) {
                        FontTexture texture = font.GetTexture(character.ToString());
                        width += texture.BaseWidth * FontScale;
                        height = Math.Max(height, texture.BaseHeight * FontScale);
                    }

                    float x = (float)(320 - (width/2 * Math.Cos(rotation)));
                    float relativeY = y;
                    double relativeStart = line.StartTime;
                    foreach (char character in lyric) {
                        FontTexture texture = font.GetTexture(character.ToString());
                        if (!texture.IsEmpty) {
                            Vector2 pos = new Vector2(x, relativeY) + texture.OffsetFor(OsbOrigin.BottomCentre) * FontScale;
                            OsbSprite sprite = layer.CreateSprite(texture.Path, OsbOrigin.BottomCentre, pos);

                            sprite.Scale(relativeStart, FontScale);
                            handleLyricBar(sprite, line.StartTime, rotation, Colours.white, (float)x, (float)relativeY, true);
                        }
                        x += (float)(texture.BaseWidth * FontScale * Math.Cos(rotation));
                        relativeY += (float)(texture.BaseWidth * FontScale * Math.Sin(rotation));
                    }
                    y += height;
                }
                handleLyricBar(barRef, line.StartTime, rotation, singer1 ? Colours.blue : Colours.pink);
                
                count++;
            }
            
            bar3.Fade(74330, 74330 + Constants.beatLength/2, 0, 1);
            bar3.Fade(83259 - Constants.beatLength*4, 83259, 1, 0);
            bar3.Fade(160044, 160044 + Constants.beatLength/2, 0, 1);
            bar3.Fade(170401 - Constants.beatLength*4, 170401, 1, 0);
            bar3.Scale(160044, 480.0f / bar3BitMap.Height);
        }

        private void handleLyricBar(OsbSprite bar, double start, double rotation, Color4 colour, double x = 320, double y = -1, bool skipScale = false) {
            if (y == -1) y = Constants.height + barHeight;
            if (!skipScale) bar.ScaleVec(start, Constants.width/Math.Cos(rotation) + 40, barHeight);
            
            bar.Rotate(start, rotation);
            bar.Color(start, colour);

            bar.Fade(start,  1);
            bar.Fade(start + Constants.beatLength * 9 / 2, 0);
            bar.Move(OsbEasing.OutCirc, start, start + Constants.beatLength/2, x, y, x, y - barHeight*2);
            bar.Move(OsbEasing.OutCirc, start + Constants.beatLength/2, start + Constants.beatLength * 4, bar.PositionAt(start + Constants.beatLength/2), bar.PositionAt(start + Constants.beatLength/2).X, bar.PositionAt(start + Constants.beatLength/2).Y - 10);
            bar.Move(OsbEasing.InCirc, start + Constants.beatLength * 4, start + Constants.beatLength * 9 / 2, bar.PositionAt(start + Constants.beatLength * 4), bar.PositionAt(start + Constants.beatLength * 4).X, bar.PositionAt(start + Constants.beatLength/2).Y - 240 - barHeight );
        }
    } 
}
