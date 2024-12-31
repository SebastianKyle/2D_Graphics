using _2D_Graphics.fillers;
using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Graphics.shapes
{
    internal class Polygon : Shape
    {
        public Polygon(List<Point> vertices, Point Start, Point End, float thick, Color shapeColor, Color fillColor, bool isFilling, bool isRegular = false, Boolean copy = false) : base(vertices, Start, End, thick, shapeColor, fillColor, isFilling, isRegular, copy: copy)
        {
            preserveRatio = false;
            this.vertices = new List<Point>(vertices);

            setCtrlPts();
        }

        public override void showShape(OpenGL gl)
        {
            fillShape(gl);

            if (shapePts.Count != 0)
            {
                shapePts.Clear();
            }

            int nVer = this.vertices.Count;
            if (nVer == 0)
            {
                return;
            }

            for (int i = 0; i < nVer - 1; i++)
            {
                drawLineBresenham(vertices[i], vertices[i + 1], gl);
            }
            drawLineBresenham(vertices[nVer - 1], vertices[0], gl);
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
            if (isFilling)
            {
                Filler.scanFill(vertices, fillColor, gl);
            }
        }

        public override void setCtrlPts()
        {
            int nVer = vertices.Count;
            ctrlPts = new List<Point>();

            for (int i = 0; i < nVer; i++)
            {
                Point vertex = new Point(vertices[i].X, vertices[i].Y);
                ctrlPts.Add(vertex);
            }
        }

        public override Point getCenter()
        {
            int ymin = int.MaxValue, ymax = int.MinValue;
            int xmin = int.MaxValue, xmax = int.MinValue;

            for (int i = 0; i < vertices.Count(); i++)
            {
                if (xmin > vertices[i].X)
                    xmin = vertices[i].X;
                if (xmax < vertices[i].X)
                    xmax = vertices[i].X;
                if (ymin > vertices[i].Y)
                    ymin = vertices[i].Y;
                if (ymax < vertices[i].Y)
                    ymax = vertices[i].Y;
            }

            return new Point((int)Math.Round((xmin + xmax) / 2.0), (int)Math.Round((ymin + ymax) / 2.0));
        }

        public override ShapeType getShapeType()
        {
            return ShapeType.Polygon;
        }

        public override Shape Clone() {
            // Create a deep copy of the shape
            return new Polygon(new List<Point>(vertices), pStart, pEnd, thick, shapeColor, fillColor, isFilling, copy: true);
        }
    }
}
