// <copyright file="DeckTests.cs" company="Mooville">
//   Copyright © 2018 Roger Deetz. All rights reserved.
// </copyright>

namespace Mooville.QUno.Model.Tests
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mooville.QUno.Model;
    using Mooville.QUno.Testing.Mocks;

    [TestClass]
    public class DeckTests
    {
        [TestMethod]
        public void DefaultConstructor_ShouldNotThrow()
        {
            Deck target = new Deck();
            Assert.IsNotNull(target);

            return;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_GivenNullDeckProvider_ShouldThrow()
        {
            Deck target = new Deck(null);

            return;
        }

        [TestMethod]
        public void AfterConstructor_CurrentWildColor_ShouldBeNull()
        {
            Deck target = new Deck();
            Assert.IsNull(target.CurrentWildColor);

            return;
        }

        [TestMethod]
        public void AfterConstructor_CurrentCard_ShouldBeNull()
        {
            Deck target = new Deck();
            Assert.IsNull(target.CurrentCard);

            return;
        }

        [TestMethod]
        public void CurrentWildColor_GivenValue_ShouldNotThrow()
        {
            Deck target = new Deck();
            target.CurrentWildColor = Color.Blue;
            Assert.AreEqual(target.CurrentWildColor, Color.Blue);

            return;
        }

        [TestMethod]
        public void CurrentWildColor_GivenNull_ShouldNotThrow()
        {
            Deck target = new Deck();
            target.CurrentWildColor = Color.Blue; // Set it to something other than null at first.
            target.CurrentWildColor = null;
            Assert.IsNull(target.CurrentWildColor);

            return;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CurrentWildColor_GivenWild_ShouldThrow()
        {
            Deck target = new Deck();
            target.CurrentWildColor = Color.Wild;

            return;
        }

        [TestMethod]
        public void AfterConstructor_DrawPile_ShouldHaveCorrectNumberOfCards()
        {
            Deck target = new Deck(new TestDeckProvider());
            Assert.AreEqual(target.DrawPile.Count, TestDeckProvider.DefaultTestDeckSize);

            return;
        }

        [TestMethod]
        public void AfterConstructor_DiscardPile_ShouldHaveCorrectNumberOfCards()
        {
            Deck target = new Deck(new TestDeckProvider());
            Assert.AreEqual(target.DiscardPile.Count, 0);

            return;
        }

        [TestMethod]
        public void Draw_GivenZeroDeck_ShouldReturnNull()
        {
            Deck target = new Deck(new TestDeckProvider(0));
            Card card = target.Draw();
            Assert.IsNull(card);

            return;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Play_GivenNullCard_ShouldThrow()
        {
            Deck target = new Deck();
            target.Play(null);

            return;
        }

        [TestMethod]
        public void AfterPlay_CurrentCard_ShouldNotBeNull()
        {
            Deck target = new Deck();
            Card card = target.Draw();
            target.Play(card);
            Assert.IsNotNull(target.CurrentCard);
            
            return;
        }

        [TestMethod]
        public void AfterPlayAll_DrawPile_ShouldHaveCorrectNumberOfCards()
        {
            Deck target = new Deck(new TestDeckProvider());

            for (int i = 0; i < TestDeckProvider.DefaultTestDeckSize; i++)
            {
                Card card = target.Draw();
                target.Play(card);                
            }

            Assert.AreEqual(target.DrawPile.Count, 0);

            return;
        }

        [TestMethod]
        public void AfterPlayAll_Draw_ShouldNotReturnNull()
        {
            Deck target = new Deck(new TestDeckProvider());

            for (int i = 0; i < TestDeckProvider.DefaultTestDeckSize; i++)
            {
                Card card = target.Draw();
                target.Play(card);
            }

            Card final = target.Draw(); // This will trigger the reshuffle.
            Assert.IsNotNull(final);

            return;
        }

        [TestMethod]
        public void AfterPlayAll_CurrentCard_ShouldNotBeNull()
        {
            Deck target = new Deck(new TestDeckProvider());

            for (int i = 0; i < TestDeckProvider.DefaultTestDeckSize; i++)
            {
                Card card = target.Draw();
                target.Play(card);
            }

            Card current = target.CurrentCard;
            Assert.IsNotNull(current);

            return;
        }

        [TestMethod]
        public void AfterPlayAll_CurrentCard_ShouldBeSameAsBefore()
        {
            Deck target = new Deck(new TestDeckProvider());

            for (int i = 0; i < TestDeckProvider.DefaultTestDeckSize; i++)
            {
                Card card = target.Draw();
                target.Play(card);
            }

            Card previousCurrent = target.CurrentCard;
            Card final = target.Draw(); // This will trigger the reshuffle.
            Card newCurrent = target.CurrentCard;

            Assert.AreEqual(previousCurrent.Color, newCurrent.Color);
            Assert.AreEqual(previousCurrent.Value, newCurrent.Value);

            return;
        }

        [TestMethod]
        public void Shuffle_ShouldNotThrow()
        {
            Deck target = new Deck();
            target.Shuffle();

            return;
        }

        [TestMethod]
        public void GetCardsOffPile_ShouldGetCorrectNumberOfCards()
        {
            Stack<Card> pile = this.CreateTestStack(7);
            Card[] cards = Deck.GetCardsOffPile(ref pile);
            Assert.AreEqual(7, cards.Length);

            return;
        }

        [TestMethod]
        public void GetCardsOffPile_ShouldRemoveAllCardsFromPile()
        {
            Stack<Card> pile = this.CreateTestStack(7);
            Card[] cards = Deck.GetCardsOffPile(ref pile);
            Assert.AreEqual(0, pile.Count);

            return;
        }

        [TestMethod]
        public void PutCardsOnPile_ShouldPutCorrectNumberOfCards()
        {
            Card[] cards = this.CreateTestArray(11);
            Stack<Card> pile = this.CreateTestStack(0);
            Deck.PutCardsOnPile(cards, ref pile);
            Assert.AreEqual(11, pile.Count);

            return;
        }

        [TestMethod]
        public void AfterShuffle_DrawPile_ShouldHaveCorrectNumberOfCards()
        {
            Deck target = new Deck(new TestDeckProvider());
            target.Shuffle();
            Assert.AreEqual(target.DrawPile.Count, TestDeckProvider.DefaultTestDeckSize);

            return;
        }

        [TestMethod]
        public void ShuffleCards_ShouldKeepCorrectNumberOfCards()
        {
            Card[] cards = this.CreateTestArray(11);
            Deck.ShuffleCards(ref cards);
            Assert.AreEqual(11, cards.Length);

            return;
        }

        private Stack<Card> CreateTestStack(int number)
        {
            Stack<Card> pile = new Stack<Card>(number);

            for (int i = 0; i < number; i++)
            {
                pile.Push(new Card(Color.Yellow, Value.Skip));
            }

            return pile;
        }

        private Card[] CreateTestArray(int number)
        {
            Card[] cards = new Card[number];

            for (int i = 0; i < number; i++)
            {
                cards[i] = new Card(Color.Green, Value.Reverse);
            }

            return cards;
        }
    }
}
