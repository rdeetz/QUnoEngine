// <copyright file="CardTests.cs" company="Mooville">
//   Copyright © 2018 Roger Deetz. All rights reserved.
// </copyright>

namespace Mooville.QUno.Model.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mooville.QUno.Model;

    [TestClass]
    public class CardTests
    {
        [TestMethod]
        public void Constructor_ShouldNotThrow()
        {
            Card target = new Card(Color.Wild, Value.WildDrawFour);
            Assert.IsNotNull(target);

            return;
        }

        [TestMethod]
        public void GetColor_ShouldReturnCorrectColor()
        {
            Card target = new Card(Color.Red, Value.Five);
            Assert.AreEqual(Color.Red, target.Color);

            return;
        }

        [TestMethod]
        public void GetValue_ShouldReturnCorrectValue()
        {
            Card target = new Card(Color.Red, Value.Five);
            Assert.AreEqual(Value.Five, target.Value);

            return;
        }

        [TestMethod]
        public void ToString_ShouldReturnCorrectValue()
        {
            Card target = new Card(Color.Red, Value.Five);
            Assert.AreEqual("Red Five", target.ToString());

            return;
        }
    }
}
