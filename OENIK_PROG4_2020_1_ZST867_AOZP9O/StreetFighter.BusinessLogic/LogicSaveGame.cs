// <copyright file="LogicSaveGame.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace StreetFighter.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using StreetFighter.Repository;

    /// <summary>
    /// SaveGame logic interface.
    /// </summary>
    public class LogicSaveGame : ILogicSaveGame
    {
        private readonly IRepositorySaveGame saveGameRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogicSaveGame"/> class.
        /// </summary>
        public LogicSaveGame()
        {
            this.saveGameRepo = new Repository();
        }
        /// <summary>
        /// Saves Player object to savefile.
        /// </summary>
        /// <param name="a">First Player object to save.</param>
        /// <param name="b">Second Player object to save.</param>
        /// <param name="filename">Name of save file.</param>
        public void Write(Player a, Player b, string filename)
        {
            int ids = this.saveGameRepo.GetIds(filename);
            Console.WriteLine(ids);
            var xd = new XElement(
                "game",
                new XAttribute("id", ids + 1),
                new XElement(
                "player1",
                new XElement("name", a.Name),
                new XElement("posx", a.PositionX),
                new XElement("posy", a.PositionY),
                new XElement("health", a.Health),
                new XElement("stamina", a.Stamina),
                new XElement("score", a.Score),
                new XElement("invulnerable", a.Invulnerable),
                new XElement("stunned", a.Stunned),
                new XElement("fleft", a.FacinLeft)),
                new XElement(
                "player2",
                new XElement("name", b.Name),
                new XElement("posx", b.PositionX),
                new XElement("posy", b.PositionY),
                new XElement("health", b.Health),
                new XElement("stamina", b.Stamina),
                new XElement("score", b.Score),
                new XElement("invulnerable", b.Invulnerable),
                new XElement("stunned", b.Stunned),
                new XElement("fleft", b.FacinLeft)));
            this.saveGameRepo.Write(filename, xd.ToString() + "</saved_games>");
        }
    }
}
