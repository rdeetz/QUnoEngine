// <copyright file="EmptyDeckProviderTests.cs" company="Mooville">
//   Copyright © 2018 Roger Deetz. All rights reserved.
// </copyright>

namespace Mooville.QUno.Model.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mooville.QUno.Model;

    [TestClass]
    public class EmptyDeckProviderTests
    {
        [TestMethod]
        public void Constructor_ShouldNotThrow()
        {
            EmptyDeckProvider target = new EmptyDeckProvider();
            Assert.IsNotNull(target);

            return;
        }

        [TestMethod]
        public void ProvideCards_ShouldReturnCorrectNumberOfCards()
        {
            EmptyDeckProvider target = new EmptyDeckProvider();
            IEnumerable<Card> cards = target.ProvideCards();
            int count = cards.Count<Card>();
            Assert.AreEqual(0, count);

            return;
        }
    }
}
