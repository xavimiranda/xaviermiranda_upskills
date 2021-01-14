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
        public double X { get; set; }
        public double Y { get; set; }
        public bool IsVertAligned(Point p) => X == p.X;
        public bool IsHorzAligned(Point p) => Y == p.Y;

        public double DistTo(Point p) => Math.Sqrt(Math.Pow(p.X - X, 2) + Math.Pow(p.Y - Y, 2));
        public static Point operator+ (Point p1, Vector v)
        {
            return new Point(p1.X + v.Pos.X, p1.Y + v.Pos.Y);
        }
        public static bool operator == (Point p1, Point p2) => p1.X == p2.X && p1.Y == p2.Y;
        public static bool operator != (Point p1, Point p2) => p1.X != p2.X || p1.Y != p2.Y;
    }
}