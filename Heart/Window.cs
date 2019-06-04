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
            double displacement = 100;
            if (dir == WindowDirection.LEFT) {
                window.ScaleVec(easing, startTime - timeOffset, endTime - timeOffset, window.ScaleAt(startTime), coord + displacement, window.ScaleAt(startTime).Y);
            }
        }
        public override void Generate()
        {
            // Initialize
		    var layer = GetLayer("Window");
            OsbSprite leftWindow = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.CentreLeft, new Vector2(-100 , 480/2));
            leftWindow.ScaleVec(0, 0, 480);
            // leftWindow.ScaleVec(0, 10000, leftWindow.ScaleAt(0), 200, leftWindow.ScaleAt(0).Y);
            // MoveWindow(leftWindow, WindowDirection.LEFT, 300, 116409, 117115); 
            OsuHitObject prevObject = null; 
            foreach( OsuHitObject hitobject in Beatmap.HitObjects) {
                if (hitobject.StartTime >= 116409 && hitobject.EndTime <= 150292) {
                    if (hitobject is OsuSlider) {        
                        double timestep = Beatmap.GetTimingPointAt((int)hitobject.StartTime).BeatDuration / 8;
                        double sliderStartTime = hitobject.StartTime;
                        while (true) {
                            double sliderEndTime = sliderStartTime + timestep;
                            bool complete = hitobject.EndTime - sliderEndTime < 5;
                            if (complete) sliderEndTime = hitobject.EndTime;
                            
                            MoveWindow(leftWindow, WindowDirection.LEFT, hitobject.PositionAtTime(sliderStartTime).X, sliderStartTime, sliderEndTime); 

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


            leftWindow.Fade(237821, 1);
        }
    }
}
