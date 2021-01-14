namespace Exercicios
{
    public interface IPolygon
    {
        Point Pos { get; set; }
        double GetArea();
        double GetPerimeter();
        void Scale(double scaleFactor);
        void Move(Vector v);
    }
}