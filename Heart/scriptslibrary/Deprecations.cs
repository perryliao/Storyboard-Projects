using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;


namespace StorybrewScripts
{
    public class Deprecations : StoryboardObjectGenerator {
        public override void Generate() {}
        ///////////////////////////////////////////////
        // 3x3 squares
        ///////////////////////////////////////////////
        public void squareMatrix() {
            float spacing = 150;
            double timeBetween = Constants.beatLength/4, squareScale = 100, squareStart;
            Vector2 squareStartPos = new Vector2(320 - spacing, 240 - spacing);
            int i, x, y;
            OsbSprite[][] field = new OsbSprite[3][], fieldRings = new OsbSprite[field.Length][];
            for (i = 0; i < 3; i++) {
                field[i] = new OsbSprite[field.Length];
                fieldRings[i] = new OsbSprite[field[i].Length];
            }

            for (y = 0; y < field.Length; y++) {
                for (x = 0; x < field[y].Length; x++) {
                    squareStart = 57115 + (x + y) * timeBetween;
                    field[y][x] = GetLayer("").CreateSprite("sb/1x1.jpg", OsbOrigin.Centre, new Vector2(squareStartPos.X + x*spacing, squareStartPos.Y + y*spacing));
                    field[y][x].Fade(squareStart, 1);
                    field[y][x].Color(squareStart, Constants.grey);
                    field[y][x].Scale(OsbEasing.OutBack, squareStart, squareStart + timeBetween, 0, squareScale);
                    field[y][x].Fade(59939, 0);
                    
                    fieldRings[y][x] = GetLayer("").CreateSprite("sb/boxy.png", OsbOrigin.Centre, new Vector2(squareStartPos.X + x*spacing, squareStartPos.Y + y*spacing));
                    fieldRings[y][x].Fade(squareStart, 1);
                    fieldRings[y][x].Scale(OsbEasing.OutBack, squareStart, squareStart + timeBetween, 0, 0.18);
                    fieldRings[y][x].Fade(59939, 0);
                }
            }
        }

        public void progressBar() {
            double progressStartTime = 40174;
            OsbSprite line = GetLayer("").CreateSprite("sb/1x1.jpg", OsbOrigin.CentreLeft, new Vector2(-107, 240));
            OsbSprite lineEnd = GetLayer("").CreateSprite("sb/Pool 1/cir.png", OsbOrigin.CentreLeft, new Vector2(-39 - 70, 240));
            line.ScaleVec(progressStartTime - Constants.beatLength/4, 30, 100);
            line.Fade(progressStartTime - Constants.beatLength/4, 0);
            line.Fade(progressStartTime, 1);
            line.Fade(42997, 0);
            lineEnd.ScaleVec(progressStartTime - Constants.beatLength/4, 0.1, 0.16666);
            lineEnd.Fade(progressStartTime - Constants.beatLength/4, 0);
            lineEnd.Fade(progressStartTime, 1);
            lineEnd.Fade(42997, 0);

            double relativeTime = progressStartTime;
            line.ScaleVec(OsbEasing.OutBack, relativeTime, relativeTime + Constants.beatLength, line.ScaleAt(relativeTime), line.ScaleAt(relativeTime).X + 150, line.ScaleAt(relativeTime).Y + 210);
            lineEnd.ScaleVec(OsbEasing.OutBack, relativeTime, relativeTime + Constants.beatLength, lineEnd.ScaleAt(relativeTime), lineEnd.ScaleAt(relativeTime).X + 0.351, lineEnd.ScaleAt(relativeTime).Y + 0.351 );
            lineEnd.MoveX(OsbEasing.OutBack, relativeTime, relativeTime + Constants.beatLength, lineEnd.PositionAt(relativeTime).X, lineEnd.PositionAt(relativeTime).X + 31 );
            
            relativeTime += Constants.beatLength;
            line.ScaleVec(OsbEasing.OutBack, relativeTime, relativeTime + Constants.beatLength*3/2, line.ScaleAt(relativeTime), line.ScaleAt(relativeTime).X + 350, line.ScaleAt(relativeTime).Y - 110);
            lineEnd.ScaleVec(OsbEasing.OutBack, relativeTime, relativeTime + Constants.beatLength*3/2, lineEnd.ScaleAt(relativeTime), lineEnd.ScaleAt(relativeTime).X - 0.184, lineEnd.ScaleAt(relativeTime).Y - 0.184 );
            lineEnd.MoveX(OsbEasing.OutBack, relativeTime, relativeTime + Constants.beatLength*3/2, lineEnd.PositionAt(relativeTime).X, lineEnd.PositionAt(relativeTime).X + 410 );
        }
    }
}
