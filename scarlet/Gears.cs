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
    public class Gears : StoryboardObjectGenerator
    {
        [Configurable]
        public string gearNumber = "0";
        [Configurable]
        public double startTime = 2961;
        [Configurable]
        public double endTime = 4913;
        [Configurable]
        public double scale = 1.0;
        [Configurable]
        public double xPos = 0.0;
        [Configurable]
        public double yPos = 0.0;
        [Configurable]
        public double initialRotation = 0;
        [Configurable]
        public double rotationAmount = 1/8;
        [Configurable]
        public bool clockwise = true; // will fall to the right if fall is set to true
        [Configurable]
        public bool fall = true;
        [Configurable]
        public double fallTime = 4203; // Only used if fall == true
        [Configurable]
        public double collapseTime = 4736; // Only used if fall == true
        
        public double beatLength = 356;
        public override void Generate()
        {
		    var layer = GetLayer("gears");
            OsbSprite gear = layer.CreateSprite("sb/gears/gear" + gearNumber + ".png", OsbOrigin.Centre);

            gear.Scale(0, scale);
            gear.Move(startTime - beatLength/2, xPos, yPos);
            gear.Rotate(0, Math.PI*initialRotation);
            gear.Fade(OsbEasing.InExpo, startTime - beatLength/2, startTime, 0, 1);
            gear.Rotate(startTime - beatLength/2, endTime, initialRotation, initialRotation + Math.PI*rotationAmount * (clockwise ? -1 : 1));
        
            if (fall) {
                Random rnd = new Random();
                double newX =  xPos + (clockwise ? 1 : -1) * (10 + rnd.Next(20)); 
                double newY = 420 + rnd.Next(60);
                gear.Move(OsbEasing.OutCirc,fallTime, collapseTime, gear.PositionAt(fallTime), newX, newY);
                gear.Move(
                        OsbEasing.OutCirc,
                        collapseTime,
                        endTime,
                        gear.PositionAt(collapseTime),
                        newX + (clockwise ? 1 : -1) * (60 + rnd.Next(60)),
                        newY + 20 + rnd.Next(20)
                    );
            }
            
            gear.Fade(OsbEasing.InExpo, endTime - beatLength/2, endTime, 1, 0);
            
        }
    }
}
