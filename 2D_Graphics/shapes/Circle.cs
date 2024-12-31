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

        public Circle(List<Point> vertices, Point Start, Point End, float thick, Color shapeColor, Color fillColor, bool isFilling, Boolean copy = false) : base(vertices, Start, End, thick, shapeColor, fillColor, isFilling, copy)
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

            if (copy)
            {
                this.vertices = new List<Point>(vertices);
                radius = Math.Abs(this.vertices[0].X - this.vertices[1].X);
                center = this.vertices[0];
                pStart = this.vertices[1];
                pEnd = this.vertices[2];
            }

            calculateShapePts();
            setCtrlPts();
        }

        public override void calculateShapePts()
        {
            
        }

        public override Shape Clone() {
            // Create a deep copy of the shape
            return new Circle(new List<Point>(vertices), pStart, pEnd, thick, shapeColor, fillColor, isFilling, copy: true);
        }
    }
}
