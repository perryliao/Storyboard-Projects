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
    public class Window : StoryboardObjectGenerator
    {
        public enum WindowDirection {
            TOP,
            BOTTOM,
            LEFT,
            RIGHT,
        }

        /**
         * @param OsbSprite window:     The window sprite to move
         * @param WindowDirection dir:  Direction the window is expanding. 
         * @param double coord:         The coordinate to move the window to, depending on the direction; LEFT/RIGHT == x, TOP/BOT == y 
         * @param double startTime:     Time when the action starts
         * @param double endTime:       Time when the action ends
         * @param OsbEasing easing:     Easing to use to move the window, default = InOutSine
         */
        private void MoveWindow(OsbSprite window, WindowDirection dir, double coord, double startTime, double endTime, OsbEasing easing = OsbEasing.InOutSine) {
            double timeOffset = 89;
            double displacement = 107;
            switch (dir) {
                case WindowDirection.LEFT: 
                case WindowDirection.RIGHT: 
                    window.ScaleVec(easing, startTime - timeOffset, endTime - timeOffset, window.ScaleAt(startTime), coord + displacement, window.ScaleAt(startTime).Y);
                    break;
                case WindowDirection.TOP: 
                case WindowDirection.BOTTOM: 
                    // do something else 
                    break; 
                default: 
                    // no action...
                    break; 
            }
        }

        private double windowStartTime = 116409;
        private double windowEndTime = 150292;

        public override void Generate()
        {
            // Initialize
		    StoryboardLayer layer = GetLayer("Window");
            OsbSprite leftWindow = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.CentreLeft, new Vector2(-107 , 480/2));
            leftWindow.Fade(windowStartTime, 1);
            leftWindow.ScaleVec(windowStartTime - 100, 0, 480);
            leftWindow.Color(windowStartTime, 247, 230, 213);

            // leftWindow.ScaleVec(0, 10000, leftWindow.ScaleAt(0), 200, leftWindow.ScaleAt(0).Y);
            // MoveWindow(leftWindow, WindowDirection.LEFT, 300, windowStartTime, 117115); 
            OsuHitObject prevObject = null; 
            foreach( OsuHitObject hitobject in Beatmap.HitObjects) {
                if (hitobject.StartTime >= windowStartTime && hitobject.EndTime <= windowEndTime) {
                    if (hitobject is OsuSlider) {        
                        double timestep = Beatmap.GetTimingPointAt((int)hitobject.StartTime).BeatDuration / 8;
                        double sliderStartTime = hitobject.StartTime;
                        while (true) {
                            double sliderEndTime = sliderStartTime + timestep;
                            bool complete = hitobject.EndTime - sliderEndTime < 5;
                            OsbEasing ease = OsbEasing.InOutSine;
                            if (complete) {
                                sliderEndTime = hitobject.EndTime;
                            } else {
                                ease = OsbEasing.None;
                            }
                            MoveWindow(leftWindow, WindowDirection.LEFT, hitobject.PositionAtTime(sliderStartTime).X, sliderStartTime, sliderEndTime, ease); 

                            if (complete) break;
                            sliderStartTime += timestep;
                        }

                        prevObject = hitobject;
                    } else {
                        // circle
                        if (prevObject != null) {
                            MoveWindow(leftWindow, WindowDirection.LEFT, hitobject.PositionAtTime(prevObject.EndTime).X, prevObject.EndTime, hitobject.EndTime);
                        } else {
                            MoveWindow(leftWindow, WindowDirection.LEFT, hitobject.PositionAtTime(hitobject.EndTime).X, hitobject.EndTime, hitobject.EndTime);
                        }
                      
                        prevObject = hitobject;
                    }
                }
            }


            leftWindow.Fade(windowEndTime, 0);
        }
    }
}
