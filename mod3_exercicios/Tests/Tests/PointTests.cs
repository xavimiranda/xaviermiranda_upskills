using Exercicios;
using NUnit.Framework;
using System;

namespace Tests
{
    public class PointTests
    {

        [Test]
        public void ValidConstruction()
        {
            double x = 2.3d;
            double y = 4.2d;
            Point p = new Point(x, y);

            Assert.AreEqual(x, p.X);
            Assert.AreEqual(y, p.Y);
        }
        [Test]
        public void PointAddVectors()
        {
            var rand = new Random();
            double vx = rand.Next();
            double vy = rand.Next();
            var v = new Vector(vx, vy);
            var p = new Point(0, 0);

            p += v;

            Assert.AreEqual(vx, p.X);
            Assert.AreEqual(vy, p.Y);
        }
        [Test]
        public void EqualityTest()
        {
            var p1 = new Point(0, 0);
            var p2 = new Point(0, 0);

            Assert.IsTrue(p1 == p2);
        }
        [Test]
        public void InequalityTest()
        {
            var p1 = new Point(2, 2);
            var p2 = new Point(1, 2);

            Assert.IsTrue(p1 != p2);
        }
    }
}