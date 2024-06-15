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
        Point vertex2, vertex4;

        public Rectangle(List<Point> vertices, Point Start, Point End, float thick, Color shapeColor, Color fillColor, Boolean isFilling)
            : base(vertices, Start, End, thick, shapeColor, fillColor, isFilling)
        {
            vertex2.X = End.X;
            vertex2.Y = Start.Y;
            vertex4.X = Start.X;
            vertex4.Y = End.Y;

            this.vertices = new List<Point>();
            this.vertices.Add(pStart);
            this.vertices.Add(vertex2);
            this.vertices.Add(pEnd);
            this.vertices.Add(vertex4);

            setCtrlPts();
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
                //fill.ScanFill(vertices, fillColor, gl);
            }
        }

        public override void setCtrlPts()
        {
            Point topLeft, topMid, topRight, midLeft, midRight, bottomLeft, bottomMid, bottomRight;
            topLeft = new Point(pStart.X, pStart.Y);
            topMid = new Point((int)Math.Round((pStart.X + pEnd.X) / 2.0), pStart.Y);
            topRight = new Point(pEnd.X, pStart.Y);
            midLeft = new Point(pStart.X, (int)Math.Round((pStart.Y + pEnd.Y) / 2.0));
            midRight = new Point(pEnd.X, (int)Math.Round((pStart.Y + pEnd.Y) / 2.0));
            bottomLeft = new Point(pStart.X, pEnd.Y);
            bottomMid = new Point((int)Math.Round((pStart.X + pEnd.X) / 2.0), pEnd.Y);
            bottomRight = new Point(pEnd.X, pEnd.Y);

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
 
    }
}
