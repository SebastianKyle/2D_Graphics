using SharpGL;
using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Graphics.fillers
{
    class Edge
    {
        public int yMax, yMin;
        public double xMin, invSlope;

        public Edge(Point pStart, Point pEnd)
        {
            yMax = pEnd.Y;
            yMin = pStart.Y;
            xMin = pStart.X;
            invSlope = (pEnd.X - pStart.X) / (double)(pEnd.Y - pStart.Y);
        }

    }

    internal class Filler
    {
        public static void scanFill(List<Point> vertices, Color fillColor, OpenGL gl)
        {
            if (vertices.Count < 3)
            {
                return;
            }

            List<List<Edge>> edgeTable = new List<List<Edge>>();
            int maxY = vertices[0].Y;
            int minY = vertices[0].Y;

            for (int k = 0; k < vertices.Count; k++)
            {
                if (vertices[k].Y < minY)
                {
                    minY = vertices[k].Y;
                }

                if (vertices[k].Y > maxY)
                {
                    maxY = vertices[k].Y;
                }
            }

            for (int k = 0; k <= maxY - minY; k++)
            {
                edgeTable.Add(new List<Edge>());
            }

            for (int k = 0; k < vertices.Count; k++)
            {
                Point pStart = vertices[k];
                Point pEnd = vertices[(k + 1) % vertices.Count];

                if (pStart.Y == pEnd.Y) // Ignore horizontal edge
                {
                    continue;
                }

                if (pStart.Y > pEnd.Y)
                {
                    (pStart, pEnd) = (pEnd, pStart);
                }

                edgeTable[pStart.Y - minY].Add(new Edge(pStart, pEnd));
            }

            foreach (List<Edge> list in edgeTable)
            {
                list.Sort((e1, e2) => e1.xMin.CompareTo(e2.xMin));
            }

            List<Edge> aet = new List<Edge>();
            for (int scanLineY = 0; scanLineY <= maxY - minY; scanLineY++)
            {
                if (scanLineY < edgeTable.Count)
                {
                    foreach (Edge e in edgeTable[scanLineY])
                    {
                        // Remove previous active edge if its endpoint overlaps with start point of new active edge
                        foreach (Edge e2 in aet)
                        {
                            if (e2.yMax - minY == scanLineY)
                            {
                                aet.Remove(e2);
                                break;
                            }
                        }

                        aet.Add(e);
                    }
                }

                // Remove all edges that are no longer active
                aet.RemoveAll(e => e.yMax - minY <= scanLineY);

                aet.Sort((e1, e2) => e1.xMin.CompareTo(e2.xMin));

                for (int k = 0; k < aet.Count; k += 2)
                {
                    if (k + 1 < aet.Count)
                    {
                        gl.Color(fillColor.R / 255.0, fillColor.G / 255.0, fillColor.B / 255.0);
                        gl.Begin(OpenGL.GL_LINES);
                        gl.Vertex(aet[k].xMin, gl.RenderContextProvider.Height - (scanLineY + minY));
                        gl.Vertex(aet[k + 1].xMin, gl.RenderContextProvider.Height - (scanLineY + minY));
                        gl.End();
                    }
                }

                // Update xMin for each active edges as the scanline moves upward
                foreach (Edge e in aet)
                {
                    e.xMin += e.invSlope;
                }
            }
        }
    }
}
