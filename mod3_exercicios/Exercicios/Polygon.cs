using System;
using System.Collections.Generic;
using System.Text;

namespace Exercicios
{
    public abstract class Polygon : IPolygon
    {
        public Polygon(int num)
        {
            Vertices = new Point[num];
        }
        public Point[] Vertices { get; set; }
        public Point Pos { get; set; }
        public abstract double GetArea();
        public abstract double GetPerimeter();
        public abstract void Scale(double scaleFactor);
        public void Move(Vector v) => Pos += v;
        public override abstract string ToString();
    }
}
