using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Graphics
{
    internal class Circle : Shape
    {
        protected int radius;
        protected Point center;

        public Circle(List<Point> vertices, Point Start, Point End, float thick, Color shapeColor, Color fillColor, bool isFilling) : base(vertices, Start, End, thick, shapeColor, fillColor, isFilling)
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
            radius = Math.Abs(center.X - pStart.X);  

            vertices = new List<Point>();
            vertices.Add(center);
            vertices.Add(pStart);
            vertices.Add(pEnd);

            calculateShapePts();
            setCtrlPts();
        }

        public override void calculateShapePts()
        {
            
        }
    }
}
