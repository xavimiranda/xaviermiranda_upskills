using System;
using System.Collections.Generic;
using System.Text;

namespace Exercicios
{
    class Rectangle
    {
        public Rectangle(double height, double width)
        {
            Height = height;
            Width = width;
        }
        public double Height { get; private set; }
        public double Width { get; private set; }
        public double GetArea() => Height * Width;
        public double GetPerimeter() => 2 * Height + 2 * Width;
        public double GetDiagonal() => Math.Sqrt(Math.Pow(Height, 2) + Math.Pow(Width, 2));
        public Rectangle Scale(double scale) => new Rectangle(Height * scale, Width * scale);
        public override string ToString()
        {
            return $"Dimensions: {Height,6} x {Width,-6}\nArea: {GetArea(),6}\nPerimeter: {GetPerimeter(),6}\nDiagonal: {GetDiagonal(),6}";
        }
    }
}
