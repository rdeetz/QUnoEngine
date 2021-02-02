// <copyright file="StandardGameSerializerTests.cs" company="Mooville">
//   Copyright © 2018 Roger Deetz. All rights reserved.
// </copyright>

namespace Mooville.QUno.Model.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Mooville.QUno.Model;

    [TestClass]
    public class StandardGameSerializerTests
    {
        [TestMethod]
        public void Constructor_ShouldNotThrow()
        {
            StandardGameSerializer target = new StandardGameSerializer();
            Assert.IsNotNull(target);

            return;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Load_GivenNull_ShouldThrow()
        {
            StandardGameSerializer target = new StandardGameSerializer();
            Game game = target.Load(null);

            return;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Load_GivenEmptyString_ShouldThrow()
        {
            StandardGameSerializer target = new StandardGameSerializer();
            Game game = target.Load(String.Empty);

            return;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Save_GivenNullGame_ShouldThrow()
        {
            StandardGameSerializer target = new StandardGameSerializer();
            target.Save(null, "file");

            return;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Save_GivenNullFile_ShouldThrow()
        {
            StandardGameSerializer target = new StandardGameSerializer();
            target.Save(new Game(), null);

            return;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Save_GivenEmptyFile_ShouldThrow()
        {
            StandardGameSerializer target = new StandardGameSerializer();
            target.Save(new Game(), String.Empty);

            return;
        }
    }
}
