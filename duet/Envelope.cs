using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System.Linq;
using System;

namespace StorybrewScripts
{
    public class Envelope : StoryboardObjectGenerator
    {
        [Configurable]
        public double startTime = 108973;
        [Configurable]
        public double endTime = 131473;

        StoryboardLayer layer;
        public override void Generate()
        {
		    layer = GetLayer("Envelope");

            top(startTime, startTime + Constants.beatLength*3);
            
            
            OsbSprite seal = layer.CreateSprite("sb/seal.png", OsbOrigin.Centre);
            seal.Fade(OsbEasing.InExpo, 108794, startTime, 0, 1);
            seal.Scale(startTime, 50.0f / GetMapsetBitmap("sb/seal.png").Height);
            seal.Fade(endTime, 0);
        }

        private OsbSprite top(double start, double moveEndTime) {
            double lineHeight = 5;
            OsbSprite sprite = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.CentreLeft, new Vector2(Constants.xFloor, 0));
            double angle = Math.Atan2( Constants.height/2, Constants.width/2);
            double length = Constants.width/2 / Math.Cos(angle);
            
            sprite.ScaleVec(OsbEasing.OutCirc, start, moveEndTime, 0, lineHeight, length, lineHeight);
            sprite.Color(start, Colours.black);
            sprite.Rotate(start, angle);
            sprite.Fade(start, 0.4);
            sprite.Fade(endTime, 0);
            return sprite;
        }

        private OsbSprite bot(double start, double moveEndTime) {
            OsbSprite sprite = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.CentreLeft, new Vector2(Constants.xFloor, Constants.height));
        
            sprite.Fade(endTime, 0);
            return sprite;
        }
    }
}
