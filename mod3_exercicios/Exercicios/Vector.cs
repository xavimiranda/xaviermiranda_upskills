namespace Exercicios
{
    public class Vector
    {
        public Vector(Point p)
        {
            Pos = p;
        }
        public Vector(double x, double y)
        {
            Pos = new Point(x, y);
        }
        public Point Pos { get; set; }
        public double Mag { get; set; }

    }
}