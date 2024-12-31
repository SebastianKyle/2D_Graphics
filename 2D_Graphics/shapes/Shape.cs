using _2D_Graphics.transformations;
using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Graphics
{
    enum ShapeType
    {
        Line,
        Circle,
        Ellipse,
        Rectangle,
        Triangle,
        Pentagon,
        Hexagon,
        Polygon,
        Curve
    }

    internal class Shape
    {
        protected List<Point> vertices;
        protected Point pStart, pEnd;
        protected float thick;
        protected Color shapeColor;
        protected Color fillColor;
        protected bool isFilling;
        protected bool isRegular;
        protected List<Point> shapePts;
        public List<Point> ctrlPts;

        //protected Fill fill;

        protected Boolean preserveRatio;
        public double angleRotated;

        public Shape(List<Point> vertices, Point Start, Point End, float thick, Color shapeColor, Color fillColor, Boolean isFilling, Boolean isRegular = false, Boolean copy = false)
        {
            preserveRatio = false;
            pStart = Start;
            pEnd = End;
            this.thick = thick;
            this.shapeColor = shapeColor;
            this.fillColor = fillColor;
            this.isFilling = isFilling;
            this.isRegular = isRegular;
            //fill = new Fill();
            shapePts = new List<Point>();
            angleRotated = 0;

            if (copy)
            {
                this.vertices = new List<Point>(vertices);
            }
        }

        public virtual void calculateShapePts() { }

        public virtual void setToRegularShape() { }

        public virtual void setToNormalShape() { }

        public virtual void showShape(OpenGL gl) { }

        public virtual void fillShape(OpenGL gl) { }

        public virtual void editShape(List<Point> vertices, List<Point> pts, List<Point> ctrlPts, float thick, Color shapeColor, Color fillColor, Boolean isFilling)
        {
            this.vertices = new List<Point>(vertices);
            this.ctrlPts = new List<Point>(ctrlPts);
            this.thick = thick;
            this.shapeColor = shapeColor;
            this.fillColor = fillColor;
            this.isFilling = isFilling;
        }

        public virtual void showEditShape(OpenGL gl) { }

        public virtual void setCtrlPts() { }

        public void drawCtrlPts(OpenGL gl)
        {
            if (ctrlPts.Count == 8 && !preserveRatio)
            {
                int glHeight = gl.RenderContextProvider.Height;

                Color a = Color.Indigo;
                gl.Color(a.R / 255.0, a.R / 255.0, a.B / 255.0);

                gl.LineWidth(1f);
                gl.Enable(OpenGL.GL_LINE_STIPPLE);
                gl.LineStipple(4, 0xAAAA);

                gl.Begin(OpenGL.GL_LINE_STRIP);
                gl.Vertex(ctrlPts[0].X + 1, glHeight - ctrlPts[0].Y);
                gl.Vertex(ctrlPts[2].X + 1, glHeight - ctrlPts[2].Y);
                gl.Vertex(ctrlPts[4].X + 1, glHeight - ctrlPts[4].Y);
                gl.Vertex(ctrlPts[6].X + 1, glHeight - ctrlPts[6].Y);
                gl.Vertex(ctrlPts[0].X + 1, glHeight - ctrlPts[0].Y);
                gl.End();

                gl.Flush();

                gl.Disable(OpenGL.GL_LINE_STIPPLE);
            }

            for (int i = 0; i < ctrlPts.Count; i++)
            {
                drawCtrlPt(gl, ctrlPts[i]);
            }
        }

        public void drawCtrlPt(OpenGL gl, Point p)
        {
            gl.Color(0f, 0f, 0.8f, 0); // Can be set to black (blue currently)
            gl.PointSize(7f);

            gl.Begin(OpenGL.GL_POINT);
            gl.Vertex(p.X, gl.RenderContextProvider.Height - p.Y);
            gl.End();

            gl.Flush();

            Color c = new Color();
            c = Color.White;
            Rectangle rec = new Rectangle(null, new Point(p.X - 3, p.Y - 3), new Point(p.X + 3, p.Y + 3), 1f, c, c, false);
            rec.showShape(gl);
        }

        public void drawListPts(List<Point> pts, OpenGL gl)
        {
            gl.Color(shapeColor.R / 255.0, shapeColor.G / 255.0, shapeColor.B / 255.0);

            gl.PointSize(thick);

            gl.Begin(OpenGL.GL_POINTS);
            for (int i = 0; i < pts.Count(); i++)
            {
                gl.Vertex(pts[i].X, gl.RenderContextProvider.Height - pts[i].Y);
            }
            gl.End();

            gl.Flush();
        }

        public void drawLineDDA(Point start, Point end, OpenGL gl)
        {
            int dx = end.X - start.X, dy = end.Y - start.Y;
            float xIncrement, yIncrement;
            float x = start.X, y = start.Y;

            int steps = Math.Abs(dx) > Math.Abs(dy) ? Math.Abs(dx) : Math.Abs(dy);

            xIncrement = (float)dx / (float)steps;
            yIncrement = (float)dy / (float)steps;

            Point curPt = new Point(start.X, start.Y);
            shapePts.Add(curPt);
            for (int k = 0; k < steps; k++)
            {
                curPt.X = (int)(curPt.X + xIncrement + 0.5);
                curPt.Y = (int)(curPt.Y + yIncrement + 0.5);
                shapePts.Add(curPt);
            }

            drawListPts(shapePts, gl);
        }

        public void drawLineBresenham(Point start, Point end, OpenGL gl)
        {
            int dx = end.X - start.X;
            int dy = end.Y - start.Y;
            int stepX, stepY;
            Point temp = new Point(start.X, start.Y);
            shapePts.Add(temp);

            if (dx < 0)
            {
                dx *= -1;
                stepX = -1;
            }
            else
            {
                stepX = 1;
            }

            if (dy < 0)
            {
                dy *= -1;
                stepY = -1;
            }
            else
            {
                stepY = 1;
            }

            if (dx > dy)
            {
                int p = 2 * dy - 2 * dx;
                while (temp.X != end.X)
                {
                    if (p < 0)
                    {
                        p += 2 * dy;
                    }
                    else
                    {
                        temp.Y += stepY;
                        p += 2 * dy - 2 * dx;
                    }

                    temp.X += stepX;
                    shapePts.Add(temp);
                }

            }
            else
            {
                int p = 2 * dx - 2 * dy;
                while (temp.Y != end.Y)
                {
                    if (p < 0)
                    {
                        p += 2 * dx;
                    }
                    else
                    {
                        temp.X += stepX;
                        p += 2 * dx - 2 * dy;
                    }

                    temp.Y += stepY;
                    shapePts.Add(temp);
                }
            }

            drawListPts(shapePts, gl);
        }

        public bool isInShape(Point cur)
        {
            if (!shapePts.Exists(c => c.Y > cur.Y && c.X == cur.X) || !shapePts.Exists(c => c.Y < cur.Y && c.X == cur.X))
            {
                return false;
            }
            if (!shapePts.Exists(c => c.X > cur.X && c.Y == cur.Y) || !shapePts.Exists(c => c.X < cur.X && c.Y == cur.Y))
            {
                return false;
            }

            return true;
        }

        public virtual bool isOnShape(Point cur)
        {
            Point curPt = new Point(cur.X, cur.Y);
            if (isCtrlPt(cur) != -1)
            {
                return false;
            }

            return shapePts.Contains(curPt);
        }

        public int isCtrlPt(Point cur)
        {
            for (int i = 0; i < ctrlPts.Count; i++)
            {
                if (Math.Abs(ctrlPts[i].X - cur.X) < 5 && Math.Abs(ctrlPts[i].Y - cur.Y) < 5)
                {
                    return i;
                }
            }

            return -1;
        }

        public List<Point> getVertices()
        {
            return (vertices == null) ? new List<Point>() : new List<Point>(vertices);
        }

        public void setVertices(List<Point> vertices)
        {
            this.vertices = new List<Point>(vertices);
        }

        public List<Point> getPoints()
        {
            return (shapePts == null) ? new List<Point>() : new List<Point>(shapePts);
        }

        public List<Point> getCtrlPoints()
        {
            return (ctrlPts == null) ? new List<Point>() : new List<Point>(ctrlPts);
        }

        public float getThick()
        {
            return thick;
        }

        public Color GetColor()
        {
            return shapeColor;
        }

        public Color getFillColor()
        {
            return fillColor;
        }

        public Boolean getFillVal()
        {
            return isFilling;
        }

        public virtual Point getCenter()
        {
            return new Point();
        }

        public virtual ShapeType getShapeType()
        {
            return 0;
        }

        public virtual Shape Clone() {
            // Create a deep copy of the shape
            return new Shape(new List<Point>(vertices), pStart, pEnd, thick, shapeColor, fillColor, isFilling, isRegular, copy: true);
        }

    }
}
