// <copyright file="WildColorViewModelTests.cs" company="Mooville">
//   Copyright © 2018 Roger Deetz. All rights reserved.
// </copyright>

namespace Mooville.QUno.ViewModel.Tests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mooville.QUno.Model;
    using Mooville.QUno.ViewModel;

    [TestClass]
    public class WildColorViewModelTests
    {
        [TestMethod]
        public void Constructor_ShouldNotThrow()
        {
            WildColorViewModel target = new WildColorViewModel();
            Assert.IsNotNull(target);

            return;
        }

        [TestMethod]
        public void AfterConstructor_Colors_ShouldNotBeNull()
        {
            WildColorViewModel target = new WildColorViewModel();
            Assert.IsNotNull(target.Colors);

            return;
        }

        [TestMethod]
        public void AfterConstructor_SelectedColor_ShouldBeNull()
        {
            WildColorViewModel target = new WildColorViewModel();
            Assert.IsNull(target.SelectedColor);

            return;
        }

        [TestMethod]
        public void AfterConstructor_Colors_ShouldHaveFourItems()
        {
            WildColorViewModel target = new WildColorViewModel();
            Assert.AreEqual(4, target.Colors.Count);

            return;
        }
    }
}
