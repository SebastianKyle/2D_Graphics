using _2D_Graphics.fillers;
using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Graphics
{
    internal class Rectangle : Shape
    {
        Point vertex2, vertex3, vertex4;

        public Rectangle(List<Point> vertices, Point Start, Point End, float thick, Color shapeColor, Color fillColor, Boolean isFilling, Boolean isRegular = false, Boolean copy=false)
            : base(vertices, Start, End, thick, shapeColor, fillColor, isFilling, copy)
        {
            if (isRegular)
            {
                setToRegularShape();
            }
            else
            {
                setToNormalShape();
            }

            if (copy)
            {
                this.vertices = new List<Point>(vertices);
                pStart = this.vertices[0];
                vertex2 = this.vertices[1];
                vertex3 = this.vertices[2];
                vertex4 = this.vertices[3];
            }
            else
            {
                this.vertices = new List<Point>();
                this.vertices.Add(pStart);
                this.vertices.Add(vertex2);
                this.vertices.Add(vertex3);
                this.vertices.Add(vertex4);
            }

            setCtrlPts();
        }

        public override void setToRegularShape()
        { 
            int dy = pEnd.Y - pStart.Y;
            int dx = pEnd.X - pStart.X;

            int a = Math.Min(Math.Abs(dx), Math.Abs(dy));

            if (Math.Abs(dx) > Math.Abs(dy))
            {
                vertex2.X = dx > 0 ? pStart.X + a : pStart.X - a;
                vertex2.Y = pStart.Y;
                vertex4.X = pStart.X;
                vertex4.Y = pStart.Y + dy;

                vertex3.X = dx > 0 ? pStart.X + a : pStart.X - a;
                vertex3.Y = pStart.Y + dy;
            }
            else
            {
                vertex2.X = pStart.X + dx;
                vertex2.Y = pStart.Y;
                vertex4.X = pStart.X;
                vertex4.Y = dy > 0 ? pStart.Y + a : pStart.Y - a;

                vertex3.X = pStart.X + dx;
                vertex3.Y = dy > 0 ? pStart.Y + a : pStart.Y - a;
            }
        }

        public override void setToNormalShape()
        {
            vertex2.X = pEnd.X;
            vertex2.Y = pStart.Y;
            vertex4.X = pStart.X;
            vertex4.Y = pEnd.Y;
            vertex3 = new Point(pEnd.X, pEnd.Y);
        }

        public override void showShape(OpenGL gl)
        {
            fillShape(gl);

            if (shapePts.Count() != 0)
            {
                shapePts.Clear();
            }
            for (int i = 0; i < 3; i++)
            {
                drawLineBresenham(vertices[i], vertices[i + 1], gl);
            }

            drawLineBresenham(vertices[3], vertices[0], gl);
        }

        public override void editShape(List<Point> vertices, List<Point> pts, List<Point> ctrlPts, float thick, Color shapeColor, Color fillColor, bool isFilling)
        {
            base.editShape(vertices, pts, ctrlPts, thick, shapeColor, fillColor, isFilling);
        }

        public override void showEditShape(OpenGL gl)
        {
            showShape(gl);
            drawCtrlPts(gl);
        }

        public override void fillShape(OpenGL gl)
        {
            if (this.isFilling)
            {
                Filler.scanFill(vertices, fillColor, gl);
            }
        }

        public override void setCtrlPts()
        {
            Point topLeft, topMid, topRight, midLeft, midRight, bottomLeft, bottomMid, bottomRight;
            topLeft = new Point(vertices[0].X, vertices[0].Y);
            topMid = new Point((int)Math.Round((vertices[0].X + vertices[2].X) / 2.0), vertices[0].Y);
            topRight = new Point(vertices[2].X, vertices[0].Y);
            midLeft = new Point(vertices[0].X, (int)Math.Round((vertices[0].Y + vertices[2].Y) / 2.0));
            midRight = new Point(vertices[2].X, (int)Math.Round((vertices[0].Y + vertices[2].Y) / 2.0));
            bottomLeft = new Point(vertices[0].X, vertices[2].Y);
            bottomMid = new Point((int)Math.Round((vertices[0].X + vertices[2].X) / 2.0), vertices[2].Y);
            bottomRight = new Point(vertices[2].X, vertices[2].Y);

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
            return new Point((int)Math.Round((vertices[0].X + vertices[2].X) / 2.0), (int)Math.Round((vertices[0].Y + vertices[2].Y) / 2.0));
        }

        public override ShapeType getShapeType()
        {
            return ShapeType.Rectangle;
        }

        public override Shape Clone() {
            // Create a deep copy of the shape
            return new Rectangle(new List<Point>(vertices), pStart, pEnd, thick, shapeColor, fillColor, isFilling, isRegular, copy: true);
        }
 
    }
}
