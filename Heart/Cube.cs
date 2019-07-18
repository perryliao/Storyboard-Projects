using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;

namespace StorybrewScripts
{

    public class Cube : StoryboardObjectGenerator
    {
        [Configurable]
        public double startTime = 164409;
        [Configurable]
        public double endTime = 209586;
        [Configurable]
        public double timeStep = 100; // 1ms
        [Configurable]
        public float width = 100;
        [Configurable]
        public double speed = 1;
        [Configurable]
        public double rotX = 0;
        [Configurable]
        public double rotY = 0;
        [Configurable]
        public double rotZ = 0;

        StoryboardLayer layer;
        public override void Generate()
        {
		    layer = GetLayer("Cube");
            createCube(320, 240, width);
        }

        private void createCube(float x, float y, float width) {
            Vector3[] points = new Vector3[8] {
                new Vector3( 1,  1,  1),
                new Vector3(-1,  1,  1),
                new Vector3( 1, -1,  1),
                new Vector3( 1,  1, -1),
                new Vector3( 1, -1, -1),
                new Vector3(-1,  1, -1),
                new Vector3(-1, -1,  1),
                new Vector3(-1, -1, -1)
            };

            int i;
            OsbSprite[] edges = makeEdges(startTime, points);
            for ( i = 0; i < edges.Length; i++) {
                points[i] = Vector3.Multiply(points[i], width/2);
                moveEdge(startTime, endTime, x, y, edges[i], points[i]);
            }
        }

        private void moveEdge(double start, double end, float xOffset, float yOffset, OsbSprite s, Vector3 v) {
            double i, r;
            double x = v.X, y = v.Y, z = v.Z, x1, y1, z1;

            s.Move(start, x + xOffset, y + yOffset);

            for (i = start; i < end - timeStep; i += timeStep) {
                r = (i - start)/(1000*timeStep/speed);

                x1 = x;
                y1 = y * Math.Cos(rotX + r) - z * Math.Sin(rotX + r);
                z1 = y * Math.Sin(rotX + r) + z * Math.Cos(rotX + r);
                x = x1;
                y = y1;
                z = z1;

                x1 = x * Math.Cos( rotY + r) + z * Math.Sin( rotY + r);
                y1 = y;
                z1 = -x * Math.Sin( rotY + r) + z * Math.Cos( rotY + r);
                x = x1;
                y = y1;
                z = z1;

                x1 = x * Math.Cos( rotZ + r) - y * Math.Sin( rotZ + r);
                y1 = x * Math.Sin( rotZ + r) + y * Math.Cos( rotZ + r);
                z1 = z;
                x = x1;
                y = y1;
                z = z1;

                s.Move(i, i + timeStep, s.PositionAt(i), x + xOffset, y + yOffset);
            }
        }

        private OsbSprite[] makeEdges(double start, Vector3[] points) {
            Assert(points.Length == 8);
            OsbSprite[] edges = new OsbSprite[8];
             
            for (int i = 0; i < edges.Length; i++) {
                edges[i] = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.Centre);
                
                edges[i].Fade(start, 1);
                edges[i].Scale(start, 2);
                edges[i].Fade(endTime, 0);

            }
            return edges;
        }
    }
}
