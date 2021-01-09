// <copyright file="StandardDeckProviderTests.cs" company="Mooville">
//   Copyright © 2018 Roger Deetz. All rights reserved.
// </copyright>

namespace Mooville.QUno.Model.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mooville.QUno.Model;

    [TestClass]
    public class StandardDeckProviderTests
    {
        [TestMethod]
        public void Constructor_ShouldNotThrow()
        {
            StandardDeckProvider target = new StandardDeckProvider();
            Assert.IsNotNull(target);

            return;
        }

        [TestMethod]
        public void ProvideCards_ShouldReturnCorrectNumberOfCards()
        {
            StandardDeckProvider target = new StandardDeckProvider();
            IEnumerable<Card> cards = target.ProvideCards();
            int count = cards.Count<Card>();
            Assert.AreEqual(56, count); // This is the same as StandardDeckProvider.StandardDeckSize.

            return;
        }
    }
}
