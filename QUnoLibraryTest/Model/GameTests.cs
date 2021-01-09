// <copyright file="GameTests.cs" company="Mooville">
//   Copyright © 2018 Roger Deetz. All rights reserved.
// </copyright>

namespace Mooville.QUno.Model.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mooville.QUno.Model;
    using Mooville.QUno.Testing.Mocks;

    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void Constructor_ShouldNotThrow()
        {
            Game target = new Game();
            Assert.IsNotNull(target);

            return;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_GivenNullDeck_ShouldThrow()
        {
            Game target = new Game(null, new TestRuleProvider());

            return;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_GivenNullRuleProvider_ShouldThrow()
        {
            Game target = new Game(new Deck(), null);

            return;
        }

        [TestMethod]
        public void AfterConstructor_Players_ShouldNotBeNull()
        {
            Game target = new Game();
            Assert.IsNotNull(target.Players);

            return;
        }

        [TestMethod]
        public void AfterConstructor_PlayersCount_ShouldBeZero()
        {
            Game target = new Game();
            Assert.AreEqual(0, target.Players.Count);

            return;
        }

        [TestMethod]
        public void AfterConstructor_Deck_ShouldNotBeNull()
        {
            Game target = new Game();
            Assert.IsNotNull(target.Deck);

            return;
        }

        [TestMethod]
        public void AfterConstructor_CurrentDirection_ShouldBeClockwise()
        {
            Game target = new Game();
            Assert.AreEqual(Direction.Clockwise, target.CurrentDirection);

            return;
        }

        [TestMethod]
        public void AfterConstructor_CurrentPlayer_ShouldBeNull()
        {
            Game target = new Game();
            Assert.IsNull(target.CurrentPlayer);

            return;
        }

        [TestMethod]
        public void AfterConstructor_IsGameOver_ShouldBeFalse()
        {
            Game target = new Game();
            Assert.IsFalse(target.IsGameOver);

            return;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WithoutAddingPlayers_Deal_ShouldThrow()
        {
            Game target = new Game();
            target.Deal();

            return;
        }

        [TestMethod]
        public void AddingTypicalNumberOfPlayers_Deal_ShouldNotThrow()
        {
            Game target = new Game();
            target.Players.Add(new Player());
            target.Players.Add(new Player());
            target.Players.Add(new Player());
            target.Players.Add(new Player());
            target.Deal();

            return;
        }

        [TestMethod]
        public void AddingPlayer_CurrentPlayer_ShouldNotBeNull()
        {
            Game target = new Game();
            target.Players.Add(new Player());
            target.Players.Add(new Player());
            target.Deal();
            Assert.IsNotNull(target.CurrentPlayer);

            return;
        }

        [TestMethod]
        public void AddingMaxPlayers_Deal_ShouldNotThrow()
        {
            Game target = new Game();
            int maxPlayers = StandardDeckProvider.StandardDeckSize / StandardRuleProvider.StandardHandSize;

            for (int i = 0; i < maxPlayers; i++)
            {
                target.Players.Add(new Player());
            }

            target.Deal();

            return;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddingTooManyPlayers_Deal_ShouldThrow()
        {
            Game target = new Game();
            int maxPlayers = StandardDeckProvider.StandardDeckSize / StandardRuleProvider.StandardHandSize;
            int tooManyPlayers = maxPlayers + 1;

            for (int i = 0; i < tooManyPlayers; i++)
            {
                target.Players.Add(new Player());
            }
            
            target.Deal();

            return;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddingPlayersWithNoCardsLeftOver_Deal_ShouldThrow()
        {
            // This test creates a deck with 20 cards and 
            // adds 4 players, each with the default hand size of 5.
            // This should throw, since after dealing, there 
            // would be no cards left over for the current card.
            const int DeckSize = 20;
            Deck deck = new Deck(new TestDeckProvider(DeckSize));
            Game target = new Game(deck, new TestRuleProvider());
            int players = DeckSize / StandardRuleProvider.StandardHandSize;

            for (int i = 0; i < players; i++)
            {
                target.Players.Add(new Player());
            }

            target.Deal();

            return;
        }

        [TestMethod]
        public void AfterDeal_AllPlayers_ShouldHaveCorrectNumberOfCards()
        {
            Game target = new Game();
            target.Players.Add(new Player());
            target.Players.Add(new Player());
            target.Players.Add(new Player());
            target.Players.Add(new Player());
            target.Deal();

            foreach (Player player in target.Players)
            {
                Assert.AreEqual(StandardRuleProvider.StandardHandSize, player.Hand.Cards.Count);
            }

            return;
        }

        [TestMethod]
        public void AfterDeal_CurrentCard_ShouldNotBeNull()
        {
            Game target = new Game();
            target.Players.Add(new Player());
            target.Players.Add(new Player());
            target.Players.Add(new Player());
            target.Players.Add(new Player());
            target.Deal();

            Assert.IsNotNull(target.Deck.CurrentCard);

            return;
        }

        [TestMethod]
        public void AfterDeal_CurrentCard_ShouldNotBeWild()
        {
            Game target = new Game();
            target.Players.Add(new Player());
            target.Players.Add(new Player());
            target.Players.Add(new Player());
            target.Players.Add(new Player());
            target.Deal();

            Card currentCard = target.Deck.CurrentCard;
            bool isWild = (currentCard.Value == Value.Wild) || (currentCard.Value == Value.WildDrawFour);
            ////bool isWild = (currentCard.Color == Color.Wild); // Can also check this way.

            Assert.IsFalse(isWild);

            return;
        }

        [TestMethod]
        public void AfterDeal_IsGameOver_ShouldBeFalse()
        {
            Game target = new Game();
            target.Players.Add(new Player());
            target.Players.Add(new Player());
            target.Players.Add(new Player());
            target.Players.Add(new Player());
            target.Deal();

            Assert.IsFalse(target.IsGameOver);

            return;
        }

        [TestMethod]
        public void IsGameOver_WithPlayerWithNoCards_ShouldBeTrue()
        {
            Game target = new Game();
            target.Players.Add(new Player());

            Assert.IsTrue(target.IsGameOver);

            return;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CanPlay_GivenNullCard_ShouldThrow()
        {
            Game target = new Game();
            target.CanPlay(null);

            return;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CanPlay_WithNoCurrentCard_ShouldThrow()
        {
            Game target = new Game();
            target.CanPlay(new Card(Color.Red, Value.Five));

            return;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PlayCard_GivenNullCard_ShouldThrow()
        {
            Game target = new Game();
            target.Players.Add(new Player());
            target.Players.Add(new Player());
            target.Deal();
            target.PlayCard(null);

            return;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PlayCard_WithNoPlayers_ShouldThrow()
        {
            Game target = new Game();
            target.PlayCard(new Card(Color.Red, Value.Five));

            return;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PlayCard_WhenCardCannotBePlayed_ShouldThrow()
        {
            TestRuleProvider ruleProvider = new TestRuleProvider();
            ruleProvider.CanPlayResult = false;

            Game target = new Game(new Deck(), ruleProvider);
            target.Players.Add(new Player());
            target.Players.Add(new Player());
            target.Deal();
            target.PlayCard(new Card(Color.Red, Value.Five));

            return;
        }

        [TestMethod]
        public void AfterPlayCard_CurrentPlayer_ShouldBeNextOne()
        {
            Player roger = new Player() { Name = "Roger" };
            Player livy = new Player() { Name = "Livy" };
            Player lucy = new Player() { Name = "Lucy" };

            Game target = new Game(new Deck(), new TestRuleProvider());
            target.Players.Add(roger);
            target.Players.Add(livy);
            target.Players.Add(lucy);
            target.Deal();

            Card card = roger.Hand.Cards[0];
            roger.Hand.Cards.RemoveAt(0);
            target.PlayCard(card);

            Assert.AreEqual("Livy", target.CurrentPlayer.Name);

            return;
        }

        [TestMethod]
        public void AfterPlayCardForAllPlayers_CurrentPlayer_ShouldBeFirstOne()
        {
            Player roger = new Player() { Name = "Roger" };
            Player livy = new Player() { Name = "Livy" };
            Player lucy = new Player() { Name = "Lucy" };
            Player lainey = new Player() { Name = "Lainey" };

            Game target = new Game(new Deck(), new TestRuleProvider());
            target.Players.Add(roger);
            target.Players.Add(livy);
            target.Players.Add(lucy);
            target.Players.Add(lainey);
            target.Deal();

            Card card1 = roger.Hand.Cards[0];
            roger.Hand.Cards.RemoveAt(0);
            target.PlayCard(card1);

            Card card2 = livy.Hand.Cards[0];
            livy.Hand.Cards.RemoveAt(0);
            target.PlayCard(card2);

            Card card3 = lucy.Hand.Cards[0];
            lucy.Hand.Cards.RemoveAt(0);
            target.PlayCard(card3);

            Card card4 = lainey.Hand.Cards[0];
            lainey.Hand.Cards.RemoveAt(0);
            target.PlayCard(card4);

            Assert.AreEqual("Roger", target.CurrentPlayer.Name);

            return;
        }

        [TestMethod]
        public void AfterPlayCardCounterClockwise_CurrentPlayer_ShouldBeNextOne()
        {
            Player roger = new Player() { Name = "Roger" };
            Player livy = new Player() { Name = "Livy" };
            Player lucy = new Player() { Name = "Lucy" };

            TestRuleProvider ruleProvider = new TestRuleProvider();
            ruleProvider.ChangedDirectionResult = true;

            Game target = new Game(new Deck(), ruleProvider);
            target.Players.Add(roger);
            target.Players.Add(livy);
            target.Players.Add(lucy);
            target.Deal();

            Card card = roger.Hand.Cards[0];
            roger.Hand.Cards.RemoveAt(0);
            target.PlayCard(card);

            Assert.AreEqual("Lucy", target.CurrentPlayer.Name);

            return;
        }

        [TestMethod]
        public void AfterPlayCardTwiceCounterClockwise_CurrentPlayer_ShouldBeOriginal()
        {
            Player roger = new Player() { Name = "Roger" };
            Player livy = new Player() { Name = "Livy" };
            Player lucy = new Player() { Name = "Lucy" };

            TestRuleProvider ruleProvider = new TestRuleProvider();
            ruleProvider.ChangedDirectionResult = true;

            Game target = new Game(new Deck(), ruleProvider);
            target.Players.Add(roger);
            target.Players.Add(livy);
            target.Players.Add(lucy);
            target.Deal();

            Card card1 = roger.Hand.Cards[0];
            roger.Hand.Cards.RemoveAt(0);
            target.PlayCard(card1);

            Card card2 = lucy.Hand.Cards[0]; // "Lucy" should be the next player in this reversed case.
            lucy.Hand.Cards.RemoveAt(0);
            target.PlayCard(card2);

            Assert.AreEqual("Roger", target.CurrentPlayer.Name);

            return;
        }

        [TestMethod]
        public void AfterPlayCardWithSkip_CurrentPlayer_ShouldBeCorrectOne()
        {
            Player roger = new Player() { Name = "Roger" };
            Player livy = new Player() { Name = "Livy" };
            Player lucy = new Player() { Name = "Lucy" };

            TestRuleProvider ruleProvider = new TestRuleProvider();
            ruleProvider.PlayerIncrementResult = 2;

            Game target = new Game(new Deck(), ruleProvider);
            target.Players.Add(roger);
            target.Players.Add(livy);
            target.Players.Add(lucy);
            target.Deal();

            Card card = roger.Hand.Cards[0];
            roger.Hand.Cards.RemoveAt(0);
            target.PlayCard(card);

            Assert.AreEqual("Lucy", target.CurrentPlayer.Name);

            return;
        }

        [TestMethod]
        public void AfterPlayCardCounterClockwiseWithSkip_CurrentPlayer_ShouldBeCorrectOne()
        {
            Player roger = new Player() { Name = "Roger" };
            Player livy = new Player() { Name = "Livy" };
            Player lucy = new Player() { Name = "Lucy" };

            TestRuleProvider ruleProvider = new TestRuleProvider();
            ruleProvider.ChangedDirectionResult = true;
            ruleProvider.PlayerIncrementResult = 2;

            Game target = new Game(new Deck(), ruleProvider);
            target.Players.Add(roger);
            target.Players.Add(livy);
            target.Players.Add(lucy);
            target.Deal();

            Card card = roger.Hand.Cards[0];
            roger.Hand.Cards.RemoveAt(0);
            target.PlayCard(card);

            Assert.AreEqual("Livy", target.CurrentPlayer.Name);

            return;
        }

        [TestMethod]
        public void AfterPlayCardWithDrawTwo_Player_ShouldHaveMoreCards()
        {
            Player roger = new Player() { Name = "Roger" };
            Player livy = new Player() { Name = "Livy" };
            Player lucy = new Player() { Name = "Lucy" };

            TestRuleProvider ruleProvider = new TestRuleProvider();
            ruleProvider.NumberOfCardsToAddResult = 2;

            Game target = new Game(new Deck(), ruleProvider);
            target.Players.Add(roger);
            target.Players.Add(livy);
            target.Players.Add(lucy);
            target.Deal();

            int originalHand = livy.Hand.Cards.Count;

            Card card = roger.Hand.Cards[0];
            roger.Hand.Cards.RemoveAt(0);
            target.PlayCard(card);

            int newHand = originalHand + 2; // Increment originalHand by NumberOfCardsToAddResult.

            Assert.AreEqual(newHand, livy.Hand.Cards.Count);

            return;
        }

        [TestMethod]
        public void AfterPlayCardWithDrawTwo_CurrentPlayer_ShouldBeCorrectOne()
        {
            Player roger = new Player() { Name = "Roger" };
            Player livy = new Player() { Name = "Livy" };
            Player lucy = new Player() { Name = "Lucy" };

            TestRuleProvider ruleProvider = new TestRuleProvider();
            ruleProvider.NumberOfCardsToAddResult = 2;

            Game target = new Game(new Deck(), ruleProvider);
            target.Players.Add(roger);
            target.Players.Add(livy);
            target.Players.Add(lucy);
            target.Deal();

            Card card = roger.Hand.Cards[0];
            roger.Hand.Cards.RemoveAt(0);
            target.PlayCard(card);

            Assert.AreEqual("Lucy", target.CurrentPlayer.Name);

            return;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PlayCard_GivenWildCardButNoWildColor_ShouldThrow()
        {
            Game target = new Game();
            target.Players.Add(new Player());
            target.Players.Add(new Player());
            target.Deal();
            target.PlayCard(new Card(Color.Wild, Value.Wild));

            return;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PlayCard_GivenWildCardButWildWildColor_ShouldThrow()
        {
            Game target = new Game();
            target.Players.Add(new Player());
            target.Players.Add(new Player());
            target.Deal();
            target.PlayCard(new Card(Color.Wild, Value.Wild), Color.Wild);

            return;
        }

        [TestMethod]
        public void AfterPlayCard_GivenWildColorCurrentWildColor_ShouldBeCorrect()
        {
            Game target = new Game();
            target.Players.Add(new Player());
            target.Players.Add(new Player());
            target.Deal();
            target.PlayCard(new Card(Color.Wild, Value.Wild), Color.Red);

            Assert.AreEqual(target.Deck.CurrentWildColor, Color.Red);

            return;
        }

        [TestMethod]
        public void AfterPlayCard_GivenRegularCardCurrentWildColor_ShouldBeNull()
        {
            Game target = new Game();
            target.Players.Add(new Player());
            target.Players.Add(new Player());
            target.Deal();
            target.PlayCard(new Card(Color.Wild, Value.Wild), Color.Red);
            target.PlayCard(new Card(Color.Red, Value.One));

            Assert.IsNull(target.Deck.CurrentWildColor);

            return;
        }

        [TestMethod]
        public void Deal_ShouldFireEvent()
        {
            bool fired = false;
            Game target = new Game();
            target.PlayerChanged += new EventHandler((sender, e) => { fired = true; });
            target.Players.Add(new Player());
            target.Players.Add(new Player());
            target.Deal();

            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void PlayCard_ShouldFireEvent()
        {
            var times = 0;
            Game target = new Game();
            target.PlayerChanged += new EventHandler((sender, e) => { times++; });
            target.Players.Add(new Player());
            target.Players.Add(new Player());
            target.Deal();
            target.PlayCard(new Card(Color.Wild, Value.Wild), Color.Red);

            Assert.AreEqual(2, times);
        }

        [TestMethod]
        public void DrawCard_ShouldFireEvent()
        {
            var times = 0;
            Game target = new Game();
            target.PlayerChanged += new EventHandler((sender, e) => { times++; });
            target.Players.Add(new Player());
            target.Players.Add(new Player());
            target.Deal();
            Card card = target.DrawCard();

            Assert.AreEqual(2, times);
        }

        [TestMethod]
        public void DrawCard_ShouldNotReturnNull()
        {
            Game target = new Game();
            target.Players.Add(new Player());
            target.Players.Add(new Player());
            target.Deal();
            Card card = target.DrawCard();

            Assert.IsNotNull(card);

            return;
        }

        [TestMethod]
        public void AfterDrawCard_CurrentPlayer_ShouldBeNextOne()
        {
            Player roger = new Player() { Name = "Roger" };
            Player livy = new Player() { Name = "Livy" };
            Player lucy = new Player() { Name = "Lucy" };

            Game target = new Game();
            target.Players.Add(roger);
            target.Players.Add(livy);
            target.Players.Add(lucy);
            target.Deal();

            Card card = target.DrawCard();

            Assert.AreEqual("Livy", target.CurrentPlayer.Name);

            return;
        }
    }
}
