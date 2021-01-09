// <copyright file="TestDeckProvider.cs" company="Mooville">
//   Copyright © 2018 Roger Deetz. All rights reserved.
// </copyright>

namespace Mooville.QUno.Testing.Mocks
{
    using System.Collections.Generic;
    using System.Linq;
    using Mooville.QUno.Model;

    public class TestDeckProvider : IDeckProvider
    {
        public const int DefaultTestDeckSize = 6;
        private int testDeckSize;

        public TestDeckProvider()
            : this(TestDeckProvider.DefaultTestDeckSize)
        {
        }

        public TestDeckProvider(int testDeckSize)
        {
            this.testDeckSize = testDeckSize;
        }

        #region IDeckProvider Members

        public IEnumerable<Card> ProvideCards()
        {
            List<Card> cards = new List<Card>(this.testDeckSize);

            for (int i = 0; i < this.testDeckSize; i++)
            {
                cards.Add(new Card(Color.Red, Value.Five));
            }

            return cards.AsEnumerable<Card>();
        }

        #endregion
    }
}
