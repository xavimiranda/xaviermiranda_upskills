using System;

namespace Exercicios
{
    public class Point
    {
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
        public Point(double x) : this(x, x) { }
        public double X { get; set; }
        public double Y { get; set; }
        public double Norm() => this.DistTo(0, 0);
        public bool HasSmallerNorm(Point p2) => this.Norm() < p2.Norm();public static Point operator +(Point p1, Vector v)
        {
            return new Point(p1.X + v.Pos.X, p1.Y + v.Pos.Y);
        }
        public override string ToString() => $"{{X: {X}, Y: {Y}}}";
        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
                return false;
            else
            {
                Point p2 = obj as Point;
                if (this.X == p2.X && this.Y == p2.Y)
                    return true;
                else
                    return false;
            }

        }
        public static bool operator ==(Point p1, Point p2) => p1.X == p2.X && p1.Y == p2.Y;
        public static bool operator !=(Point p1, Point p2) => p1.X != p2.X || p1.Y != p2.Y;
        public bool IsVertAligned(Point p2) => X == p2.X;
        public bool IsHorzAligned(Point p2) => Y == p2.Y;
        public double DistTo(Point p2) => Math.Sqrt(Math.Pow(X - p2.X, 2) + Math.Pow(Y - p2.Y, 2));
        public double DistTo(double x, double y) => this.DistTo(new Point(x, y));

    }
}