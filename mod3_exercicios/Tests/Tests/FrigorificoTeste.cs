using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Exercicios;

namespace Tests
{
    [TestFixture]
    public class FrigorificoTeste
    {
        [Test]
        public void FrigorificoCombinado()
        {
            var frig = new Frigorifico("AEG", "A230");
            frig.Ligado = true;
            frig.TempFrig = 5;
            frig.TempCong = -3;

            Assert.AreEqual("AEG", frig.Marca);
            Assert.AreEqual("A230", frig.Modelo);
            Assert.AreEqual(5, frig.TempFrig);
            Assert.AreEqual(-3, frig.TempCong);
            Assert.Throws<ArgumentOutOfRangeException>(() => frig.TempFrig = 50);
            Assert.Throws<ArgumentOutOfRangeException>(() => frig.TempFrig = -50);
            Assert.Throws<ArgumentOutOfRangeException>(() => frig.TempCong = -50);
            Assert.Throws<ArgumentOutOfRangeException>(() => frig.TempCong = 50);
        }
        [Test]
        public void FrigorificoNaoCombinado()
        {
            var frig = new Frigorifico("AEG", "B230", false);
            frig.Ligado = true;

            Assert.Throws<ArgumentException>(() => { frig.TempCong = -1; });
            Assert.Throws<ArgumentException>(() => { frig.PortaAbertaCong = true; });
        }
    }
}
