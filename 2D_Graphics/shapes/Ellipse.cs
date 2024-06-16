using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Graphics
{
    internal class Ellipse : Shape
    {
        protected int rx, ry;
        protected Point center;

        public Ellipse(List<Point> vertices, Point Start, Point End, float thick, Color shapeColor, Color fillColor, bool isFilling, Boolean isRegular = false) : base(vertices, Start, End, thick, shapeColor, fillColor, isFilling)
        {
            if (isRegular) // To circle shape
            {
                setToRegularShape();
            }
            else
            {
                setToNormalShape();
            }

            this.vertices = new List<Point>();
            this.vertices.Add(this.center);
            this.vertices.Add(pStart);
            this.vertices.Add(pEnd);

            calculateShapePts();

            setCtrlPts();
        }

        public override void calculateShapePts()
        {
            if (shapePts.Count != 0)
            {
                shapePts.Clear();
            }

            // Calculate rx^2, ry^2, 2*rx^2, 2*ry^2
            int rx2 = rx * rx;
            int ry2 = ry * ry;
            int twoRx2 = 2 * rx2;
            int twoRy2 = 2 * ry2;

            // Initial point at region 1 (0, ry)
            int x = 0, y = ry;

            // p0 at region 1, initial 2*ry^2*x and 2*rx^2*y
            int p;
            int px = 0, py = twoRx2 * y;

            addEllipsePts(x, y);

            // Region 1
            p = ry2 - (rx2 * ry) + (1 / 4 * rx2);
            while (px < py)
            {
                x++;
                px += twoRy2;
                if (p < 0)
                {
                    p += px + ry2;
                }
                else
                {
                    y--;
                    py -= twoRx2;
                    p += px - py + ry2;
                }

                addEllipsePts(x, y);
            }

            // Region 2
            p = ry2 * (x + 1 / 2) * (x + 1 / 2) + rx2 * (y - 1) * (y - 1) - rx2 * ry2;
            while (y > 0)
            {
                y--;
                py -= twoRx2;
                if (p > 0)
                {
                    p += -py + rx2;
                }
                else
                {
                    x++;
                    px += twoRy2;
                    p += -py + px + rx2;
                }

                addEllipsePts(x, y);
            }
        }

        public override void setToRegularShape()
        {
            int dy = pEnd.Y - pStart.Y;
            int dx = pEnd.X - pStart.X;

            if (Math.Abs(dx) > Math.Abs(dy))
            {
                if (dx > 0)
                {
                    this.center = new Point(pStart.X + Math.Abs(dy / 2), pStart.Y + dy / 2);
                }
                else
                {
                    this.center = new Point(pStart.X - Math.Abs(dy / 2), pStart.Y + dy / 2);
                }
            }
            else
            {
                if (dy > 0)
                {
                    this.center = new Point(pStart.X + dx / 2, pStart.Y + Math.Abs(dx / 2));
                }
                else
                {
                    this.center = new Point(pStart.X + dx / 2, pStart.Y - Math.Abs(dx / 2));
                }
            }

            rx = ry = Math.Abs(center.X - pStart.X);
        }

        public override void setToNormalShape()
        {
            int dx = pEnd.X - pStart.X;
            int dy = pEnd.Y - pStart.Y;
            this.rx = Math.Abs(dx) / 2;
            this.ry = Math.Abs(dy) / 2;
            this.center = new Point(pStart.X + dx / 2, pStart.Y + dy / 2);
        }

        public void addEllipsePts(int x, int y)
        {
            this.shapePts.Add(new Point(center.X + x, center.Y + y));
            this.shapePts.Add(new Point(center.X - x, center.Y + y));
            this.shapePts.Add(new Point(center.X + x, center.Y - y));
            this.shapePts.Add(new Point(center.X - x, center.Y - y));
        }

        public override void showShape(OpenGL gl)
        {
            fillShape(gl);
            drawListPts(shapePts, gl);
        }

        public override void editShape(List<Point> vertices, List<Point> pts, List<Point> ctrlPts, float thick, Color shapeColor, Color fillColor, bool isFilling)
        {
            base.editShape(vertices, pts, ctrlPts, thick, shapeColor, fillColor, isFilling);
            this.shapePts = new List<Point>(pts);
        }

        public override void showEditShape(OpenGL gl)
        {
            showShape(gl);
            drawCtrlPts(gl);
        }

        public override void fillShape(OpenGL gl)
        {
            if (isFilling)
            {
                int ymin = int.MaxValue, ymax = int.MinValue;
                int n = shapePts.Count();
                for (int i = 0; i < n; i++)
                {
                    int ycur = shapePts[i].Y;
                    if (ycur > ymax)
                    {
                        ymax = ycur;
                    }
                    if (ycur < ymin)
                    {
                        ymin = ycur;
                    }
                }

                Point A, B;
                for (int y = ymin + 1; y < ymax; y++)
                {
                    A = shapePts.Find(c => c.Y == y);
                    B = shapePts.Find(c => c.Y == y && c.X != A.X);

                    if (A.IsEmpty || B.IsEmpty)
                    {
                        continue;
                    }

                    gl.Color(fillColor.R / 255.0, fillColor.G / 255.0, fillColor.B / 255.0);
                    gl.Begin(OpenGL.GL_LINES);
                    gl.Vertex(A.X, gl.RenderContextProvider.Height - A.Y);
                    gl.Vertex(B.X, gl.RenderContextProvider.Height - B.Y);
                    gl.End();
                }
            }
        }

        public override void setCtrlPts()
        {
            Point topLeft, topMid, topRight, midLeft, midRight, bottomLeft, bottomMid, bottomRight;
            topLeft = new Point(vertices[0].X - rx, (vertices[0].Y - ry));
            topMid = new Point(vertices[0].X, (vertices[0].Y - ry));
            topRight = new Point(vertices[0].X + rx, (vertices[0].Y - ry));

            midLeft = new Point(vertices[0].X - rx, vertices[0].Y);
            midRight = new Point(vertices[0].X + rx, vertices[0].Y);

            bottomLeft = new Point(vertices[0].X - rx, (vertices[0].Y + ry));
            bottomMid = new Point(vertices[0].X, (vertices[0].Y + ry));
            bottomRight = new Point(vertices[0].X + rx, (vertices[0].Y + ry));

            ctrlPts = new List<Point>();
            ctrlPts.Add(topLeft);
            ctrlPts.Add(topMid);
            ctrlPts.Add(topRight);
            ctrlPts.Add(midRight);
            ctrlPts.Add(bottomRight);
            ctrlPts.Add(bottomMid);
            ctrlPts.Add(bottomLeft);
            ctrlPts.Add(midLeft);
        }

        public override Point getCenter()
        {
            //return vertices[0];
            int dx = vertices[2].X - vertices[1].X;
            int dy = vertices[2].Y - vertices[1].Y;
            return new Point(vertices[1].X + dx / 2, vertices[1].Y + dy / 2);
        }

        public override ShapeType getShapeType()
        {
            return ShapeType.Ellipse;
        }

    }
}
