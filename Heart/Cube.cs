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
            for ( i = 0; i < points.Length; i++) {
                points[i] = Vector3.Add(Vector3.Multiply(points[i], width/2), new Vector3(x, y, 0));
                Log(points[i].ToString());
            }

            OsbSprite[] edges = makeEdges(startTime, points);
            for ( i = 0; i < edges.Length; i++) {
                moveEdge(startTime, endTime, edges[i], points[i]);
            }
        }

        private void moveEdge(double start, double end, OsbSprite s, Vector3 v) {
            double i, r;
            double x = v.X, y = v.Y, z = v.Z, x1, y1, z1;
            for (i = start; i < end - timeStep; i += timeStep) {
                r = (i - start)/(1000*timeStep/speed);

                x1 = x;
                y1 = y * Math.Cos(r) - z * Math.Sin(r);
                z1 = y * Math.Sin(r) + z * Math.Cos(r);
                x = x1;
                y = y1;
                z = z1;

                x1 = x * Math.Cos(r) - z * Math.Sin(r);
                y1 = y;
                z1 = -x * Math.Sin(r) + z * Math.Cos(r);
                x = x1;
                y = y1;
                z = z1;

                x1 = x * Math.Cos(r) - y * Math.Sin(r);
                y1 = x * Math.Sin(r) + y * Math.Cos(r);
                z1 = z;
                x = x1;
                y = y1;
                z = z1;

                s.Move(i, i + timeStep, s.PositionAt(i), x, y);
            }
        }

        private OsbSprite[] makeEdges(double start, Vector3[] points) {
            Assert(points.Length == 8);
            OsbSprite[] edges = new OsbSprite[8];
             
            for (int i = 0; i < edges.Length; i++) {
                edges[i] = layer.CreateSprite("sb/1x1.jpg", OsbOrigin.Centre, new Vector2(points[i].X, points[i].Y));
                
                edges[i].Fade(start, 1);
                edges[i].Scale(start, 2);
                edges[i].Fade(endTime, 0);

            }
            return edges;
        }
    }
}
