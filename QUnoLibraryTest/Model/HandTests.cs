// <copyright file="HandTests.cs" company="Mooville">
//   Copyright © 2018 Roger Deetz. All rights reserved.
// </copyright>

namespace Mooville.QUno.Model.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mooville.QUno.Model;

    [TestClass]
    public class HandTests
    {
        [TestMethod]
        public void Constructor_ShouldNotThrow()
        {
            Hand target = new Hand();
            Assert.IsNotNull(target);

            return;
        }

        [TestMethod]
        public void AfterConstructor_Cards_ShouldNotBeNull()
        {
            Hand target = new Hand();
            Assert.IsNotNull(target.Cards);

            return;
        }

        [TestMethod]
        public void AfterConstructor_CardsCount_ShouldBeZero()
        {
            Hand target = new Hand();
            Assert.AreEqual(0, target.Cards.Count);

            return;
        }
    }
}
