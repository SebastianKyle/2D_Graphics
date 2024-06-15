using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Graphics
{
    internal class Hexagon : Shape
    {
        Point top, upperRight, lowerRight, bot, lowerLeft, upperLeft;

        public Hexagon(List<Point> vertices, Point Start, Point End, float thick, Color shapeColor, Color fillColor, bool isFilling) : base(vertices, Start, End, thick, shapeColor, fillColor, isFilling)
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
            double angleOffset = -Math.PI / 2;

            this.vertices = new List<Point>();
            for (int i = 0; i < 6; i++)
            {
                double angle = angleOffset + i * Math.PI / 3;
                int x = (int)(center.X + radius * Math.Cos(angle));
                int y = (int)(center.Y + radius * Math.Sin(angle));
                this.vertices.Add(new Point(x, y));
            }

            top = this.vertices[0];
            upperRight = this.vertices[1];
            lowerRight = this.vertices[2];
            bot = this.vertices[3];
            lowerLeft = this.vertices[4];
            upperLeft = this.vertices[5];

            setCtrlPts();
        }

        public override void showShape(OpenGL gl)
        {
            fillShape(gl);

            if (shapePts.Count != 0)
            {
                shapePts.Clear();
            }

            for (int i = 0; i < 5; i++)
            {
                drawLineBresenham(vertices[i], vertices[i + 1], gl);
            }
            drawLineBresenham(vertices[5], vertices[0], gl);
        }

        public override void editShape(List<Point> vertices, List<Point> pts, List<Point> ctrlPts, float thick, Color shapeColor, Color fillColor, bool isFilling)
        {
            base.editShape(vertices, pts, ctrlPts, thick, shapeColor, fillColor, isFilling);
            this.vertices = new List<Point>(vertices);
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
                
            }
        }

        public override void setCtrlPts()
        {
            Point topLeft, topMid, topRight, midLeft, midRight, botLeft, botMid, botRight;

            topLeft = new Point(upperLeft.X, top.Y);
            topMid = new Point(top.X, top.Y);
            topRight = new Point(upperRight.X, top.Y);

            midLeft = new Point(upperLeft.X, (upperLeft.Y + lowerLeft.Y) / 2);
            midRight = new Point(upperRight.X, (upperRight.Y + lowerRight.Y) / 2);

            botLeft = new Point(upperLeft.X, bot.Y);
            botMid = new Point(bot.X, bot.Y);
            botRight = new Point(upperRight.X, bot.Y);

            ctrlPts = new List<Point>();
            ctrlPts.Add(topLeft);
            ctrlPts.Add(topMid);
            ctrlPts.Add(topRight);
            ctrlPts.Add(midRight);
            ctrlPts.Add(botRight);
            ctrlPts.Add(botMid);
            ctrlPts.Add(botLeft);
            ctrlPts.Add(midLeft);
        }

        public override Point getCenter()
        {
            return new Point((vertices[0].X + vertices[3].X) / 2, (vertices[0].Y + vertices[3].Y) / 2);
        }

        public override ShapeType getShapeType()
        {
            return ShapeType.Hexagon;
        }
    }
}
