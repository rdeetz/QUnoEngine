// <copyright file="StandardRuleProviderTests.cs" company="Mooville">
//   Copyright © 2018 Roger Deetz. All rights reserved.
// </copyright>

namespace Mooville.QUno.Model.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mooville.QUno.Model;

    [TestClass]
    public class StandardRuleProviderTests
    {
        [TestMethod]
        public void Constructor_ShouldNotThrow()
        {
            StandardRuleProvider target = new StandardRuleProvider();
            Assert.IsNotNull(target);

            return;
        }

        [TestMethod]
        public void HandSize_ShouldReturnCorrectHandSize()
        {
            StandardRuleProvider target = new StandardRuleProvider();
            Assert.AreEqual(5, target.HandSize); // This is the same as StandardRuleProvider.StandardHandSize;

            return;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CanPlay_GivenNullCard_ShouldThrow()
        {
            StandardRuleProvider target = new StandardRuleProvider();
            bool canPlay = target.CanPlay(null, new Card(Color.Red, Value.Five), null);

            return;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CanPlay_GivenNullCurrentCard_ShouldThrow()
        {
            StandardRuleProvider target = new StandardRuleProvider();
            bool canPlay = target.CanPlay(new Card(Color.Red, Value.Five), null, null);

            return;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CanPlay_GivenWildCurrentCardAndNullCurrentWildColor_ShouldThrow()
        {
            StandardRuleProvider target = new StandardRuleProvider();
            bool canPlay = target.CanPlay(new Card(Color.Red, Value.Five), new Card(Color.Wild, Value.Wild), null);

            return;
        }

        [TestMethod]
        public void CanPlay_GivenWildCard_ShouldReturnTrue()
        {
            StandardRuleProvider target = new StandardRuleProvider();
            bool canPlay = target.CanPlay(new Card(Color.Wild, Value.WildDrawFour), new Card(Color.Red, Value.Five), null);
            Assert.IsTrue(canPlay);

            return;
        }

        [TestMethod]
        public void CanPlay_GivenColorMatch_ShouldReturnTrue()
        {
            StandardRuleProvider target = new StandardRuleProvider();
            bool canPlay = target.CanPlay(new Card(Color.Blue, Value.One), new Card(Color.Blue, Value.Nine), null);
            Assert.IsTrue(canPlay);

            return;
        }

        [TestMethod]
        public void CanPlay_GivenValueMatch_ShouldReturnTrue()
        {
            StandardRuleProvider target = new StandardRuleProvider();
            bool canPlay = target.CanPlay(new Card(Color.Yellow, Value.Two), new Card(Color.Red, Value.Two), null);
            Assert.IsTrue(canPlay);

            return;
        }

        [TestMethod]
        public void CanPlay_GivenColorMatchingCurrentWildColor_ShouldReturnTrue()
        {
            StandardRuleProvider target = new StandardRuleProvider();
            bool canPlay = target.CanPlay(new Card(Color.Green, Value.Three), new Card(Color.Wild, Value.Wild), Color.Green);
            Assert.IsTrue(canPlay);

            return;
        }

        [TestMethod]
        public void CanPlay_GivenColorMismatch_ShouldReturnFalse()
        {
            StandardRuleProvider target = new StandardRuleProvider();
            bool canPlay = target.CanPlay(new Card(Color.Blue, Value.Eight), new Card(Color.Red, Value.Five), null);
            Assert.IsFalse(canPlay);

            return;
        }

        [TestMethod]
        public void CanPlay_GivenValueMismatch_ShouldReturnFalse()
        {
            StandardRuleProvider target = new StandardRuleProvider();
            bool canPlay = target.CanPlay(new Card(Color.Green, Value.Reverse), new Card(Color.Blue, Value.DrawTwo), null);
            Assert.IsFalse(canPlay);

            return;
        }

        [TestMethod]
        public void CanPlay_GivenColorNotMatchingCurrentWildColor_ShouldReturnFalse()
        {
            StandardRuleProvider target = new StandardRuleProvider();
            bool canPlay = target.CanPlay(new Card(Color.Red, Value.Four), new Card(Color.Wild, Value.WildDrawFour), Color.Blue);
            Assert.IsFalse(canPlay);

            return;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ChangedDirection_GivenNull_ShouldThrow()
        {
            StandardRuleProvider target = new StandardRuleProvider();
            target.ChangedDirection(null);

            return;
        }

        [TestMethod]
        public void ChangedDirection_GivenReverse_ShouldReturnTrue()
        {
            StandardRuleProvider target = new StandardRuleProvider();
            bool changedDirection = target.ChangedDirection(new Card(Color.Red, Value.Reverse));
            Assert.IsTrue(changedDirection);

            return;
        }

        [TestMethod]
        public void ChangedDirection_GivenNotReverse_ShouldReturnFalse()
        {
            StandardRuleProvider target = new StandardRuleProvider();
            bool changedDirection = target.ChangedDirection(new Card(Color.Red, Value.DrawTwo));
            Assert.IsFalse(changedDirection);

            return;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetPlayerIncrement_GivenNull_ShouldThrow()
        {
            StandardRuleProvider target = new StandardRuleProvider();
            target.GetPlayerIncrement(null);

            return;
        }

        [TestMethod]
        public void GetPlayerIncrement_GivenSkip_ShouldReturnTwo()
        {
            StandardRuleProvider target = new StandardRuleProvider();
            int playerIncrement = target.GetPlayerIncrement(new Card(Color.Red, Value.Skip));
            Assert.AreEqual(2, playerIncrement);

            return;
        }

        [TestMethod]
        public void GetPlayerIncrement_GivenNotSkip_ShouldReturnOne()
        {
            StandardRuleProvider target = new StandardRuleProvider();
            int playerIncrement = target.GetPlayerIncrement(new Card(Color.Red, Value.Four));
            Assert.AreEqual(1, playerIncrement);

            return;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetNumberOfCardsToAdd_GivenNull_ShouldThrow()
        {
            StandardRuleProvider target = new StandardRuleProvider();
            int numberOfCardsToAdd = target.GetNumberOfCardsToAdd(null);

            return;
        }

        [TestMethod]
        public void GetNumberOfCardsToAdd_GivenRegular_ShouldReturnZero()
        {
            StandardRuleProvider target = new StandardRuleProvider();
            int numberOfCardsToAdd = target.GetNumberOfCardsToAdd(new Card(Color.Red, Value.Seven));
            Assert.AreEqual(0, numberOfCardsToAdd);

            return;
        }

        [TestMethod]
        public void GetNumberOfCardsToAdd_GivenDrawTwo_ShouldReturnTwo()
        {
            StandardRuleProvider target = new StandardRuleProvider();
            int numberOfCardsToAdd = target.GetNumberOfCardsToAdd(new Card(Color.Red, Value.DrawTwo));
            Assert.AreEqual(2, numberOfCardsToAdd);

            return;
        }

        [TestMethod]
        public void GetNumberOfCardsToAdd_GivenWildDrawFour_ShouldReturnFour()
        {
            StandardRuleProvider target = new StandardRuleProvider();
            int numberOfCardsToAdd = target.GetNumberOfCardsToAdd(new Card(Color.Wild, Value.WildDrawFour));
            Assert.AreEqual(4, numberOfCardsToAdd);

            return;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidateWildColor_GivenNullCard_ShouldThrow()
        {
            StandardRuleProvider target = new StandardRuleProvider();
            target.ValidateWildColor(null, null);

            return;
        }

        [TestMethod]
        public void ValidateWildColor_GivenRegularCard_ShouldReturnTrue()
        {
            StandardRuleProvider target = new StandardRuleProvider();
            bool isValid = target.ValidateWildColor(new Card(Color.Red, Value.One), null);
            Assert.IsTrue(isValid);

            return;
        }

        [TestMethod]
        public void ValidateWildColor_GivenWildCardWithNullWildColor_ShouldReturnFalse()
        {
            StandardRuleProvider target = new StandardRuleProvider();
            bool isValid = target.ValidateWildColor(new Card(Color.Wild, Value.Wild), null);
            Assert.IsFalse(isValid);

            return;
        }

        [TestMethod]
        public void ValidateWildColor_GivenWildCardWithWildWildColor_ShouldReturnFalse()
        {
            StandardRuleProvider target = new StandardRuleProvider();
            bool isValid = target.ValidateWildColor(new Card(Color.Wild, Value.Wild), Color.Wild);
            Assert.IsFalse(isValid);

            return;
        }

        [TestMethod]
        public void ValidateWildColor_GivenWildCardWithValidWildColor_ShouldReturnTrue()
        {
            StandardRuleProvider target = new StandardRuleProvider();
            bool isValid = target.ValidateWildColor(new Card(Color.Wild, Value.Wild), Color.Red);
            Assert.IsTrue(isValid);

            return;
        }
    }
}
