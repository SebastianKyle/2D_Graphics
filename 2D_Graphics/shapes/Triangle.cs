using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Graphics
{
    internal class Triangle : Shape
    {
        Point topVertex, leftVertex, rightVertex;

        public Triangle(List<Point> vertices, Point Start, Point End, float thick, Color shapeColor, Color fillColor, bool isFilling, Boolean isRegular = false) : base(vertices, Start, End, thick, shapeColor, fillColor, isFilling)
        {
            if (isRegular)
            {
                setToRegularShape();
            }
            else
            {
                setToNormalShape();
            }

            this.vertices = new List<Point>();
            this.vertices.Add(topVertex);
            this.vertices.Add(leftVertex);
            this.vertices.Add(rightVertex);

            setCtrlPts();
        }

        public override void setToRegularShape()
        {
            int dy = pEnd.Y - pStart.Y;
            int dx = pEnd.X - pStart.X;

            double a;
            double h;

            if (Math.Abs(dx) > Math.Abs(dy))
            {
                h = Math.Abs(dy);
                a = 2 * h / Math.Sqrt(3);
            }
            else
            {
                a = Math.Abs(dx);
                h = Math.Sqrt(3) * a / 2;
            }
            
            if (dy < 0)
            {  
                if (dx < 0)
                {
                    topVertex = new Point(Convert.ToInt32(pStart.X - a / 2), Convert.ToInt32(pStart.Y - h));
                    leftVertex = new Point(pStart.X, pStart.Y);
                    rightVertex = new Point(Convert.ToInt32(pStart.X - a), pStart.Y);
                }
                else
                {
                    topVertex = new Point(Convert.ToInt32(pStart.X + a / 2), Convert.ToInt32(pStart.Y - h));
                    leftVertex = new Point(Convert.ToInt32(pStart.X + a), pStart.Y);
                    rightVertex = new Point(pStart.X, pStart.Y);
                }
            }
            else
            {
                if (dx < 0)
                {
                    topVertex = new Point(Convert.ToInt32(pStart.X - a / 2), pStart.Y);
                    leftVertex = new Point(pStart.X, Convert.ToInt32(pStart.Y + h));
                    rightVertex = new Point(Convert.ToInt32(pStart.X - a), Convert.ToInt32(pStart.Y + h));
                }
                else
                {
                    topVertex = new Point(Convert.ToInt32(pStart.X + a / 2), pStart.Y);
                    leftVertex = new Point(Convert.ToInt32(pStart.X + a), Convert.ToInt32(pStart.Y + h));
                    rightVertex = new Point(pStart.X, Convert.ToInt32(pStart.Y + h));
                }
            }

        }

        public override void setToNormalShape()
        {
            int dy = pEnd.Y - pStart.Y;
            int dx = pEnd.X - pStart.X;

            if (dy < 0)
            {
                topVertex = new Point((pEnd.X + pStart.X) / 2, pEnd.Y);
                
                if (dx < 0)
                {
                    leftVertex = new Point(pStart.X, pStart.Y);
                    rightVertex = new Point(pEnd.X, pStart.Y);
                }
                else
                {
                    leftVertex = new Point(pEnd.X, pStart.Y);
                    rightVertex = new Point(pStart.X, pStart.Y);
                }
            }
            else
            {
                topVertex = new Point((pEnd.X + pStart.X) / 2, pStart.Y);

                if (dx < 0)
                {
                    leftVertex = new Point(pStart.X, pEnd.Y);
                    rightVertex = new Point(pEnd.X, pEnd.Y);
                }
                else
                {
                    leftVertex = new Point(pEnd.X, pEnd.Y);
                    rightVertex = new Point(pStart.X, pEnd.Y);
                }
            }
        }

        public override void showShape(OpenGL gl)
        {
            fillShape(gl);

            if (shapePts.Count() != 0)
            {
                shapePts.Clear();
            }

            for (int i = 0; i < 2; i++)
            {
                drawLineBresenham(vertices[i], vertices[i + 1], gl);
            }

            drawLineBresenham(vertices[2], vertices[0], gl);
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
                //fill.ScanFill(vertices, fillColor, gl);
            }
        }

        public override void setCtrlPts()
        {
            Point topLeft, topMid, topRight, midLeft, midRight, bottomLeft, bottomMid, bottomRight;
            topLeft = new Point(leftVertex.X, topVertex.Y);
            topMid = new Point(topVertex.X, topVertex.Y);
            topRight = new Point(rightVertex.X, topVertex.Y);

            midLeft = new Point(leftVertex.X, (int)Math.Round((leftVertex.Y + topVertex.Y) / 2.0));
            midRight = new Point(rightVertex.X, (int)Math.Round((rightVertex.Y + topVertex.Y) / 2.0));

            bottomLeft = new Point(leftVertex.X, leftVertex.Y);
            bottomMid = new Point((int)Math.Round((leftVertex.X + rightVertex.X) / 2.0), leftVertex.Y);
            bottomRight = new Point(rightVertex.X, rightVertex.Y);

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
            return new Point((vertices[0].X + vertices[1].X + vertices[2].X) / 3, (vertices[0].Y + vertices[1].Y + vertices[2].Y) / 3);
        }

        public override ShapeType getShapeType()
        {
            return ShapeType.Triangle;
        }
    }
}
