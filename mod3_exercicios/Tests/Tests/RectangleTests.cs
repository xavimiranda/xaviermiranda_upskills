using Exercicios;
using NUnit.Framework;
using System;
using System.Linq;

namespace Tests
{
     public class RectangleTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ValidConstruction()
        {
            Point[] points = { new Point(0, 0), new Point(2, 0), new Point(2, 2), new Point(0, 2)};
            Point[] points2 = { new Point(0, 0), new Point(2, 0), new Point(2, 2), new Point(0, 2)};
            var rect = new Rectangle(points);
            var rect1 = new Rectangle(points2);

            Assert.IsTrue(rect1 == rect);
            Assert.NotNull(rect);
            Assert.AreEqual(2, rect.Height);
            Assert.AreEqual(2, rect.Width);

        }
        [Test]
        public void InvalidConstruction()
        {
            Point[] points = { new Point(0, 0), new Point(3, 0), new Point(2, 2), new Point(0, 2) };
            Assert.Throws<ArgumentException>(() => new Rectangle(points));
            
            Point[] points2 = { new Point(1,1), new Point(1, 1), new Point(1, 7), new Point(1, 7) };
            Assert.Throws<ArgumentException>(() => new Rectangle(points2));

            Assert.Throws<IndexOutOfRangeException>(() => new Rectangle(new Point(0,0), new Point(1, 2)));
        }
        [Test]
        public void SequenceEqualWithValueTypeArrays()
        {
            int[] int1 = { 1, 3, 4, 5 };
            int[] int2 = { 1, 3, 4, 5 };

            Assert.IsTrue(int1.SequenceEqual(int2));
        }
        [Test]
        public void SequenceEqualWithRefereceTypeArrays()
        {
            Point[] pts1 = { new Point(1, 2), new Point(2, 3), new Point(3, 3) };
            Point[] pts2 = { new Point(1, 2), new Point(2, 3), new Point(3, 3) };
            Assert.IsTrue(pts1.SequenceEqual(pts2));
        }
    }
}