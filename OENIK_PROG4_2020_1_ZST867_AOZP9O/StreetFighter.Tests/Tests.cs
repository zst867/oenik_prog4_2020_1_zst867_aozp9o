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
    using System.Xml.Linq;
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

            Assert.AreEqual(m.Player1.CX, 60);
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

            Assert.AreEqual(m.Player1.CX, 140);
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

            Assert.AreEqual(m.Player1.CY, 375);
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

            Assert.AreEqual(m.Player1.CY, 374);
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

            Assert.AreEqual(m.Player2.Health, 100);
            Assert.AreEqual(m.Player1.Stamina, 70);
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

            Assert.AreEqual(m.Player2.Health, 100);
            Assert.AreEqual(m.Player1.Stamina, 50);
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
            Assert.AreEqual(logic.EnoughStamina(m.Player1, 101), false);
        }

        /// <summary>
        /// Test.
        /// </summary>
        [Test]
        public void TestThatHighScoreRepoWorksAndWasCalledOnce()
        {
            GameModel m = new GameModel();
            var xd = new XDocument(new XElement(
                "game",
                new XAttribute("id", 1),
                new XAttribute("name", "name"),
                new XAttribute("hour", DateTime.Now.Hour),
                new XAttribute("minute", DateTime.Now.Minute),
                new XElement(
                "player1",
                new XElement("name", m.Player1.Name),
                new XElement("posx", m.Player1.CX),
                new XElement("posy", m.Player1.CY),
                new XElement("posd", m.Player1.DY),
                new XElement("jump", m.Player1.IsJumping),
                new XElement("health", m.Player1.Health),
                new XElement("stamina", m.Player1.Stamina),
                new XElement("score", m.Player1.Score),
                new XElement("invulnerable", m.Player1.Invulnerable),
                new XElement("stunned", m.Player1.Stunned),
                new XElement("status", m.Player1.State),
                new XElement("timer", m.Player1.Timer),
                new XElement("fleft", m.Player1.FacinLeft)),
                new XElement(
                "player2",
                new XElement("name", m.Player2.Name),
                new XElement("posx", m.Player2.CX),
                new XElement("posy", m.Player2.CY),
                new XElement("posd", m.Player2.DY),
                new XElement("jump", m.Player2.IsJumping),
                new XElement("health", m.Player2.Health),
                new XElement("stamina", m.Player2.Stamina),
                new XElement("score", m.Player2.Score + 2),
                new XElement("invulnerable", m.Player2.Invulnerable),
                new XElement("stunned", m.Player2.Stunned),
                new XElement("status", m.Player2.State),
                new XElement("timer", m.Player2.Timer),
                new XElement("fleft", m.Player2.FacinLeft))));
            Mock<IRepositoryHighScore> mockInstance1 = new Mock<IRepositoryHighScore>();
            mockInstance1.Setup(x => x.GetAll(It.IsAny<string>())).Returns(xd);
            LogicHighScore logic = new LogicHighScore(mockInstance1.Object);
            var i = logic.CalculateHighscore(It.IsAny<string>());
            mockInstance1.Verify(x => x.GetAll(It.IsAny<string>()), Times.Once());
            Assert.That(2, Is.EqualTo(i.Score));
        }

        /// <summary>
        /// Test.
        /// </summary>
        [Test]
        public void TestThatSaveGameRepoWasCalledOnce()
        {
            GameModel m = new GameModel();
            Mock<IRepositorySaveGame> mockinstance = new Mock<IRepositorySaveGame>();
            LogicSaveGame logic = new LogicSaveGame(mockinstance.Object);

            // mockinstance.Setup(x => x.GetIds(It.IsAny<string>())).Returns(It.IsAny<int>());
            // mockinstance.Setup(x => x.Write(It.IsAny<string>(), It.IsAny<string>())).Verifiable();
            logic.Write(string.Empty, m.Player1, m.Player2, string.Empty);
            mockinstance.Verify(x => x.Write(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }

        /// <summary>
        /// Test.
        /// </summary>
        [Test]
        public void TestThatLoadGameReadWasCalledOnce()
        {
            GameModel m = new GameModel();
            var xd = new XDocument(new XElement(
                "game",
                new XAttribute("id", 1),
                new XAttribute("name", "name"),
                new XAttribute("hour", DateTime.Now.Hour),
                new XAttribute("minute", DateTime.Now.Minute),
                new XElement(
                "player1",
                new XElement("name", m.Player1.Name),
                new XElement("posx", m.Player1.CX),
                new XElement("posy", m.Player1.CY),
                new XElement("health", m.Player1.Health),
                new XElement("stamina", m.Player1.Stamina),
                new XElement("score", m.Player1.Score + 1),
                new XElement("invulnerable", m.Player1.Invulnerable),
                new XElement("stunned", m.Player1.Stunned),
                new XElement("fleft", m.Player1.FacinLeft)),
                new XElement(
                "player2",
                new XElement("name", m.Player2.Name),
                new XElement("posx", m.Player2.CX),
                new XElement("posy", m.Player2.CY),
                new XElement("health", m.Player2.Health),
                new XElement("stamina", m.Player2.Stamina),
                new XElement("score", m.Player2.Score + 2),
                new XElement("invulnerable", m.Player2.Invulnerable),
                new XElement("stunned", m.Player2.Stunned),
                new XElement("fleft", m.Player2.FacinLeft))));
            Mock<IRepositoryLoadGame> mockinstance = new Mock<IRepositoryLoadGame>();
            LogicLoadGame logic = new LogicLoadGame(mockinstance.Object);
            mockinstance.Setup(x => x.GetAll(It.IsAny<string>())).Returns(xd);
            logic.Read(It.IsAny<int>(), It.IsAny<string>());
            mockinstance.Verify(x => x.GetAll(It.IsAny<string>()), Times.Once());
        }

        /// <summary>
        /// Test.
        /// </summary>
        [Test]
        public void TestThatLoadGameReadGameWasCalledOnceAndWorks()
        {
            GameModel m = new GameModel();
            var xd = new XDocument(new XElement(
                "game",
                new XAttribute("id", 5),
                new XAttribute("name", "name"),
                new XAttribute("hour", DateTime.Now.Hour),
                new XAttribute("minute", DateTime.Now.Minute),
                new XElement(
                "player1",
                new XElement("name", m.Player1.Name),
                new XElement("posx", m.Player1.CX),
                new XElement("posy", m.Player1.CY),
                new XElement("health", m.Player1.Health),
                new XElement("stamina", m.Player1.Stamina),
                new XElement("score", m.Player1.Score + 1),
                new XElement("invulnerable", m.Player1.Invulnerable),
                new XElement("stunned", m.Player1.Stunned),
                new XElement("fleft", m.Player1.FacinLeft)),
                new XElement(
                "player2",
                new XElement("name", m.Player2.Name),
                new XElement("posx", m.Player2.CX),
                new XElement("posy", m.Player2.CY),
                new XElement("health", m.Player2.Health),
                new XElement("stamina", m.Player2.Stamina),
                new XElement("score", m.Player2.Score + 2),
                new XElement("invulnerable", m.Player2.Invulnerable),
                new XElement("stunned", m.Player2.Stunned),
                new XElement("fleft", m.Player2.FacinLeft))));
            Mock<IRepositoryLoadGame> mockinstance = new Mock<IRepositoryLoadGame>();
            LogicLoadGame logic = new LogicLoadGame(mockinstance.Object);
            mockinstance.Setup(x => x.GetAll(It.IsAny<string>())).Returns(xd);
            var buffer = logic.ReadGame(It.IsAny<string>());
            mockinstance.Verify(x => x.GetAll(It.IsAny<string>()), Times.Once());
            Assert.AreEqual(buffer.FirstOrDefault().Id, 5);
        }
    }
}
