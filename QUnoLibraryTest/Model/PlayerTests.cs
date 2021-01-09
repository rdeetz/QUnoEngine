// <copyright file="PlayerTests.cs" company="Mooville">
//   Copyright © 2018 Roger Deetz. All rights reserved.
// </copyright>

namespace Mooville.QUno.Model.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mooville.QUno.Model;
    using Mooville.QUno.Testing.Mocks;

    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void Constructor_ShouldNotThrow()
        {
            Player target = new Player();
            Assert.IsNotNull(target);

            return;
        }

        [TestMethod]
        public void AfterConstructor_Hand_ShouldNotBeNull()
        {
            Player target = new Player();
            Assert.IsNotNull(target.Hand);

            return;
        }

        [TestMethod]
        public void AfterConstructor_Name_ShouldBeNull()
        {
            Player target = new Player();
            Assert.IsNull(target.Name);

            return;
        }

        [TestMethod]
        public void AfterConstructor_IsHuman_ShouldBeFalse()
        {
            Player target = new Player();
            Assert.IsFalse(target.IsHuman);

            return;
        }

        [TestMethod]
        public void AfterConstructor_ChooseCardToPlay_ShouldReturnNull()
        {
            Player target = new Player();
            Assert.AreEqual(target.ChooseCardToPlay(new Game()), null);

            return;
        }

        [TestMethod]
        public void AfterConstructor_ChooseWildColor_ShouldReturnRed()
        {
            Player target = new Player();
            Assert.AreEqual(target.ChooseWildColor(), Color.Red);

            return;
        }

        [TestMethod]
        public void Name_ShouldAcceptValue()
        {
            Player target = new Player();
            target.Name = "Player1";
            Assert.AreEqual("Player1", target.Name);

            return;
        }

        [TestMethod]
        public void IsHuman_ShouldAcceptValue()
        {
            Player target = new Player();
            target.IsHuman = true;
            Assert.IsTrue(target.IsHuman);

            return;
        }

        [TestMethod]
        public void ChooseCardToPlay_WhenCanPlay_ShouldReturnCard()
        {
            const int DeckSize = 10;
            Deck deck = new Deck(new TestDeckProvider(DeckSize));
            IRuleProvider ruleProvider = new TestRuleProvider() { CanPlayResult = true };
            Game game = new Game(deck, ruleProvider);
            Player target = new Player();
            game.Players.Add(target);
            game.Deal();
            Card card = target.ChooseCardToPlay(game);

            Assert.IsNotNull(card);

            return;
        }

        [TestMethod]
        public void ChooseCardToPlay_WhenCannotPlay_ShouldReturnNull()
        {
            const int DeckSize = 10;
            Deck deck = new Deck(new TestDeckProvider(DeckSize));
            IRuleProvider ruleProvider = new TestRuleProvider() { CanPlayResult = false };
            Game game = new Game(deck, ruleProvider);
            Player target = new Player();
            game.Players.Add(target);
            game.Deal();
            Card card = target.ChooseCardToPlay(game);

            Assert.IsNull(card);

            return;
        }

        [TestMethod]
        public void ChooseWildColor_ShouldReturnMostCommonColor()
        {
            Player target = new Player();
            target.Hand.Cards.Add(new Card(Color.Blue, Value.One));
            target.Hand.Cards.Add(new Card(Color.Blue, Value.Two));
            target.Hand.Cards.Add(new Card(Color.Blue, Value.Three));
            target.Hand.Cards.Add(new Card(Color.Green, Value.One));
            Assert.AreEqual(target.ChooseWildColor(), Color.Blue);

            return;
        }
    }
}
