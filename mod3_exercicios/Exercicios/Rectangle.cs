using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Exercicios
{
    public class Rectangle : Polygon
    {
        public Rectangle(Point p0, Point p1, Point p2, Point p3) : base(4)
        {   
            if (PointsMakeRect(p0, p1, p2, p3))
            {
                Vertices[0] = p0;
                Vertices[1] = p1;
                Vertices[2] = p2;
                Vertices[3] = p3;
            }
            else
                throw new ArgumentException("Check that rectangle has 4 valid points.");
        }
        public Rectangle(params Point[] points) : this(points[0], points[1], points[2], points[3]) { }
        public double Height 
        { 
            get => Vertices[0].DistTo(Vertices[3]);
            private set { } 
        }
        public double Width 
        {
            get => Vertices[0].DistTo(Vertices[1]);
            private set { } 
        }
        public override double GetArea() => Height * Width;
        public override double GetPerimeter() => 2 * Height + 2 * Width;
        public double GetDiagonal() => Math.Sqrt(Math.Pow(Height, 2) + Math.Pow(Width, 2));
        public override void Scale(double scale)
        {
            Height *= scale;
            Width *= scale;
        }
        public override string ToString()
        {
            return $"Dimensions: {Height,6} x {Width,-6}\nArea: {GetArea(),6}\nPerimeter: {GetPerimeter(),6}\nDiagonal: {GetDiagonal(),6}";
        }

        public static bool operator == (Rectangle r1, Rectangle r2) => r1.Vertices.SequenceEqual(r2.Vertices);
        public static bool operator != (Rectangle r1, Rectangle r2) => r1.Vertices.SequenceEqual(r2.Vertices);

        private bool PointsMakeRect(params Point[] points)
        {

            bool AreAligned = points[0].IsHorzAligned(points[1]) && points[0].IsVertAligned(points[3]) && points[2].IsVertAligned(points[1]) && points[2].IsHorzAligned(points[3]);
            bool NotSame = points[0].DistTo(points[1]) != 0 && points[0].DistTo(points[3]) != 0;

            return AreAligned && NotSame;
        }
    }
}
