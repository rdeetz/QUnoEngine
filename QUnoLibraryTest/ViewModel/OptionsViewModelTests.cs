// <copyright file="OptionsViewModelTests.cs" company="Mooville">
//   Copyright © 2018 Roger Deetz. All rights reserved.
// </copyright>

namespace Mooville.QUno.ViewModel.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mooville.QUno.Testing.Mocks;
    using Mooville.QUno.ViewModel;

    [TestClass]
    public class OptionsViewModelTests
    {
        [TestMethod]
        public void Constructor_ShouldNotThrow()
        {
            OptionsViewModel target = new OptionsViewModel(new TestSettingsProvider());
            Assert.IsNotNull(target);

            return;
        }

        [TestMethod]
        public void AfterLoadSettings_AllProperties_ShouldBeSet()
        {
            OptionsViewModel target = new OptionsViewModel(new TestSettingsProvider());
            target.LoadSettings();
            Assert.AreEqual(@"Default Player", target.DefaultHumanPlayerName);
            Assert.AreEqual(7, target.DefaultComputerPlayers);
            Assert.IsTrue(target.AutoCheckForUpdates);

            return;
        }

        [TestMethod]
        public void SaveSettings_ShouldNotThrow()
        {
            OptionsViewModel target = new OptionsViewModel(new TestSettingsProvider());
            target.SaveSettings();

            return;
        }
    }
}
