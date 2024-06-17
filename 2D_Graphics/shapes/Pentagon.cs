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
    internal class Pentagon : Shape
    {
        Point top, right, rightBot, leftBot, left;

        public Pentagon(List<Point> vertices, Point Start, Point End, float thick, Color shapeColor, Color fillColor, bool isFilling) : base(vertices, Start, End, thick, shapeColor, fillColor, isFilling)
        {
            int dy = pEnd.Y - pStart.Y;
            int dx = pEnd.X - pStart.X;

            Point center;
            if (Math.Abs(dx) > Math.Abs(dy))
            {
                if (dx > 0)
                {
                    center = new Point(pStart.X + Math.Abs(dy / 2), pStart.Y + dy / 2);
                }
                else
                {
                    center = new Point(pStart.X - Math.Abs(dy / 2), pStart.Y + dy / 2);
                }
            }
            else
            {
                if (dy > 0)
                {
                    center = new Point(pStart.X + dx / 2, pStart.Y + Math.Abs(dx / 2));
                }
                else
                {
                    center = new Point(pStart.X + dx / 2, pStart.Y - Math.Abs(dx / 2));
                }
            }

            int radius = Math.Min(Math.Abs(dx), Math.Abs(dy)) / 2;

            double angleOffSet = -Math.PI / 2;

            this.vertices = new List<Point>();
            for (int i = 0; i < 5; i++)
            {
                double angle = angleOffSet + i * 2 * Math.PI / 5;
                int x = (int)(center.X + radius * Math.Cos(angle));
                int y = (int)(center.Y + radius * Math.Sin(angle));
                this.vertices.Add(new Point(x, y));
            }

            top = this.vertices[0];
            right = this.vertices[1];
            rightBot = this.vertices[2];
            leftBot = this.vertices[3];
            left = this.vertices[4];

            setCtrlPts();
        }

        public override void showShape(OpenGL gl)
        {
            fillShape(gl);
            if (shapePts.Count() != 0)
            {
                shapePts.Clear();
            }
            for (int i = 0; i < 4; i++)
            { 
                drawLineBresenham(vertices[i], vertices[i + 1], gl);
            }
            drawLineBresenham(vertices[4], vertices[0], gl);
        }

        public override void editShape(List<Point> vertices, List<Point> points, List<Point> controlPoints, float thick, Color color, Color color_fill, bool isFill)
        {
            base.editShape(vertices, points, controlPoints, thick, color, color_fill, isFill);
        }
        public override void showEditShape(OpenGL gl)
        {
            showShape(gl);
            drawCtrlPts(gl);
        }
        public override void fillShape(OpenGL gl)
        {
            if(isFilling)
            {
                Filler.scanFill(vertices, fillColor, gl);
            }    
        }


        public override void setCtrlPts()
        {
            Point topLeft, topMid, topRight, midLeft, midRight, bottomLeft, bottomMid, bottomRight;
            topLeft = new Point(left.X,  top.Y);
            topMid = new Point(top.X,  top.Y);
            topRight = new Point(right.X,  top.Y);

            midLeft = new Point(left.X, (int)Math.Round((leftBot.Y + top.Y) / 2.0));
            midRight = new Point(right.X, (int)Math.Round((leftBot.Y + top.Y) / 2.0));

            bottomLeft = new Point(left.X, rightBot.Y);
            bottomMid = new Point(top.X, rightBot.Y);
            bottomRight = new Point(right.X, rightBot.Y);

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
            this.vertices.Add(leftBot);
            this.vertices.Add(rightBot);
            this.vertices.Add(right);
            this.vertices.Add(top);
            this.vertices.Add(left);

            Point M = new Point((vertices[0].X + vertices[4].X) / 2, (vertices[0].Y + vertices[4].Y) / 2);
            Point N = new Point((vertices[1].X + vertices[2].X) / 2, (vertices[1].Y + vertices[2].Y) / 2);
            Point P = new Point((M.X + N.X) / 2, (M.Y + N.Y) / 2);

            Point G = new Point((vertices[4].X + 4 * P.X) / 5, (vertices[4].Y + 4 * P.Y) / 5);
            return G;
        }


        public override ShapeType getShapeType()
        {
            return ShapeType.Pentagon;
        }

    }
}
