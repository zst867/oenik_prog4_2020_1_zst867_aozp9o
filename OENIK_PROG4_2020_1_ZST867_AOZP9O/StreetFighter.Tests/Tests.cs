// <copyright file="Tests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace StreetFighter.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Moq;
    using NUnit.Framework;
    using StreetFighter.BusinessLogic;
    using StreetFighter.Repository;

    /// <summary>
    /// Class for testing.
    /// </summary>
    [TestFixture]
    public class Tests
    {
        /// <summary>
        /// Test.
        /// </summary>
        [Test]
        public void TestThatMoveLeftWorks()
        {
            GameModel m = new GameModel();
            IGameLogic logic = new GameLogic();

            logic.MoveLeft(m.Player1, m.Player2);

            Assert.AreEqual(m.Player1.CX, 70);
        }

        /// <summary>
        /// Test.
        /// </summary>
        [Test]
        public void TestThatMoveRightWorks()
        {
            GameModel m = new GameModel();
            IGameLogic logic = new GameLogic();

            logic.MoveRight(m.Player1, m.Player2, m);

            Assert.AreEqual(m.Player1.CX, 130);
        }

        /// <summary>
        /// Test.
        /// </summary>
        [Test]
        public void TestThatMoveUpWorks()
        {
            GameModel m = new GameModel();
            IGameLogic logic = new GameLogic();

            logic.MoveUp(m.Player1, m.Player2);

            Assert.AreEqual(m.Player1.CY, 350);
        }

        /// <summary>
        /// Test.
        /// </summary>
        [Test]
        public void TestThatMoveDownWorks()
        {
            GameModel m = new GameModel();
            IGameLogic logic = new GameLogic();

            logic.MoveDown(m.Player1, m.Player2);

            Assert.AreEqual(m.Player1.CY, 349);
        }

        /// <summary>
        /// Test.
        /// </summary>
        [Test]
        public void TestThatSlapWorks()
        {
            GameModel m = new GameModel();
            IGameLogic logic = new GameLogic();

            logic.Slap(m.Player1, m.Player2);

            Assert.AreEqual(m.Player2.Health, 9);
            Assert.AreEqual(m.Player1.Stamina, 7);
        }

        /// <summary>
        /// Test.
        /// </summary>
        [Test]
        public void TestThatKickWorks()
        {
            GameModel m = new GameModel();
            IGameLogic logic = new GameLogic();

            logic.Kick(m.Player1, m.Player2);

            Assert.AreEqual(m.Player2.Health, 8);
            Assert.AreEqual(m.Player1.Stamina, 5);
        }

        /// <summary>
        /// Test.
        /// </summary>
        [Test]
        public void TestThatEnoughStaminaWorks()
        {
            GameModel m = new GameModel();
            IGameLogic logic = new GameLogic();

            Assert.AreEqual(logic.EnoughStamina(m.Player1, 1), true);
            Assert.AreEqual(logic.EnoughStamina(m.Player1, 11), false);
        }
    }
}
