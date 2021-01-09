// <copyright file="TestGameSerializer.cs" company="Mooville">
//   Copyright © 2018 Roger Deetz. All rights reserved.
// </copyright>

namespace Mooville.QUno.Testing.Mocks
{
    using Mooville.QUno.Model;

    public class TestGameSerializer : IGameSerializer
    {
        public TestGameSerializer()
        {
        }

        #region IGameSerializer Members

        public Game Load(string fileName)
        {
            Deck deck = new Deck(new TestDeckProvider());
            Game game = new Game(deck, new TestRuleProvider());

            return game;
        }

        public void Save(Game game, string fileName)
        {
            return;
        }

        #endregion
    }
}
