using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System.Linq;
using OpenTK.Graphics;
using OpenTK;


namespace StorybrewScripts
{
    public class EnvelopeTransition : StoryboardObjectGenerator
    {
        [Configurable]
        public double numBars  = 3;
        [Configurable]
        public double startTime = 108259;
        [Configurable]
        public double timeStep = Constants.beatLength / 4;
        [Configurable]
        public Color4 barColour = Colours.beige;


        private StoryboardLayer layer;

        public override void Generate()
        {
		    double height = Constants.height / numBars;
            double endTime = startTime + numBars * timeStep;
            layer = GetLayer("Letter");
            
            int i;
            OsbSprite bar;
            bool left;
            for (i = 0; i < numBars; i++) {
                left = i % 2 == 0;
                bar = layer.CreateSprite("sb/1x1.jpg", left ? OsbOrigin.TopLeft : OsbOrigin.TopRight, new Vector2((float)(left ? Constants.xFloor : Constants.xCeil), (float) (i * height)));

                bar.ScaleVec(startTime + i * timeStep, endTime, 0, height, Constants.width, height);
                bar.Color(startTime + i * timeStep, barColour);
            }
        }
    }
}
