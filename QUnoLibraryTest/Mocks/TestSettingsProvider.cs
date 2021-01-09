// <copyright file="TestSettingsProvider.cs" company="Mooville">
//   Copyright © 2018 Roger Deetz. All rights reserved.
// </copyright>

namespace Mooville.QUno.Testing.Mocks
{
    using System;
    using Mooville.QUno.ViewModel;

    public class TestSettingsProvider : ISettingsProvider
    {
        public TestSettingsProvider()
        {
        }

        #region ISettingsProvider Members

        public string DefaultHumanPlayerName
        {
            get
            {
                return @"Default Player";
            }

            set
            {
                return;
            }
        }

        public int DefaultComputerPlayers
        {
            get
            {
                return 7;
            }

            set
            {
                return;
            }
        }

        public bool AutoCheckForUpdates
        {
            get
            {
                return true;
            }

            set
            {
                return;
            }
        }

        public void LoadSettings()
        {
            return;
        }

        public void SaveSettings()
        {
            return;
        }

        #endregion
    }
}
