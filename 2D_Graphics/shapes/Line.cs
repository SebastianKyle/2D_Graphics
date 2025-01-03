using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Graphics
{
    internal class Line : Shape
    {
        public Line(List<Point> vertices, Point Start, Point End, float thick, Color shapeColor, Color fillColor, Boolean isFilling, Boolean copy = false) : base(vertices, Start, End, thick, shapeColor, fillColor, isFilling, copy: copy)
        {
            this.vertices = new List<Point>();
            this.vertices.Add(Start);
            this.vertices.Add(End);

            if (copy)
            {
                this.vertices = new List<Point>(vertices);
            }

            setCtrlPts();
        }

        public override void showShape(OpenGL gl)
        {
            if (shapePts.Count > 0)
            {
                shapePts.Clear();
            }

            if (vertices[0].X != vertices[1].X || vertices[0].Y != vertices[1].Y)
            {
                drawLineBresenham(vertices[0], vertices[1], gl);
                //drawLineDDA(vertices[0], vertices[1], gl);
            }
        }

        public override void showEditShape(OpenGL gl)
        {
            showShape(gl);
            drawCtrlPts(gl);
        }

        public override void setCtrlPts()
        {
            ctrlPts = new List<Point>();
            ctrlPts.Add(pStart);
            ctrlPts.Add(pEnd);
        }

        public override bool isOnShape(Point cur)
        {
            int n = shapePts.Count;
            for (int i = 8; i < n - 8; i++)
            {
                if (Math.Abs(shapePts[i].X - cur.X) < 5 && Math.Abs(shapePts[i].Y - cur.Y) < 5)
                {
                    return true;
                }
            }

            return false;
        }

        public override Point getCenter()
        {
            return new Point((vertices[0].X + vertices[1].X) / 2, 
                (vertices[0].Y + vertices[1].Y) / 2);
        }

        public override ShapeType getShapeType()
        {
            return ShapeType.Line;
        }

        public override Shape Clone() {
            // Create a deep copy of the shape
            return new Line(new List<Point>(vertices), pStart, pEnd, thick, shapeColor, fillColor, isFilling, copy: true);
        }

    }
}
