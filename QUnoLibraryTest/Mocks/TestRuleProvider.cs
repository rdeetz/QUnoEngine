// <copyright file="TestRuleProvider.cs" company="Mooville">
//   Copyright © 2018 Roger Deetz. All rights reserved.
// </copyright>

namespace Mooville.QUno.Testing.Mocks
{
    using Mooville.QUno.Model;

    public class TestRuleProvider : IRuleProvider
    {
        public const int DefaultTestHandSize = 5;
        private int testHandSize;
        private bool canPlayResult;
        private bool changedDirectionResult;
        private int playerIncrementResult;
        private int numberOfCardsToAddResult;
        private bool validateWildColorResult;

        public TestRuleProvider()
            : this(TestRuleProvider.DefaultTestHandSize)
        {
        }

        public TestRuleProvider(int testHandSize)
        {
            this.testHandSize = testHandSize;
            this.canPlayResult = true;
            this.changedDirectionResult = false;
            this.playerIncrementResult = 1;
            this.numberOfCardsToAddResult = 0;
            this.validateWildColorResult = true;
        }

        public bool CanPlayResult
        {
            get
            {
                return this.canPlayResult;
            }

            set
            {
                this.canPlayResult = value;
            }
        }

        public bool ChangedDirectionResult
        {
            get
            {
                return this.changedDirectionResult;
            }

            set
            {
                this.changedDirectionResult = value;
            }
        }

        public int PlayerIncrementResult
        {
            get
            {
                return this.playerIncrementResult;
            }

            set
            {
                this.playerIncrementResult = value;
            }
        }

        public int NumberOfCardsToAddResult
        {
            get
            {
                return this.numberOfCardsToAddResult;
            }

            set
            {
                this.numberOfCardsToAddResult = value;
            }
        }

        public bool ValidateWildColorResult
        {
            get
            {
                return this.validateWildColorResult;
            }

            set
            {
                this.validateWildColorResult = value;
            }
        }

        #region IRuleProvider Members

        public int HandSize
        {
            get
            {
                return this.testHandSize;
            }
        }

        public bool CanPlay(Card card, Card currentCard, Color? currentWildColor)
        {
            return this.canPlayResult;
        }

        public bool ChangedDirection(Card card)
        {
            return this.changedDirectionResult;
        }

        public int GetPlayerIncrement(Card card)
        {
            return this.playerIncrementResult;
        }

        public int GetNumberOfCardsToAdd(Card card)
        {
            return this.numberOfCardsToAddResult;
        }

        public bool ValidateWildColor(Card card, Color? wildColor)
        {
            return this.validateWildColorResult;
        }

        #endregion
    }
}
