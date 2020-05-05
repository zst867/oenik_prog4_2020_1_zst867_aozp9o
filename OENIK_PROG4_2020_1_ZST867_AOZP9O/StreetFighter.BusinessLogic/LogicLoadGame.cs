// <copyright file="LogicLoadGame.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace StreetFighter.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Linq;
    using StreetFighter.Repository;

    /// <summary>
    /// Class for handling save file.
    /// </summary>
    public class LogicLoadGame : ILogicLoadGame
    {
        private readonly IRepositoryLoadGame loadGameRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogicLoadGame"/> class.
        /// </summary>
        public LogicLoadGame()
        {
            this.loadGameRepo = new Repository();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogicLoadGame"/> class.
        /// </summary>
        /// <param name="repo">
        /// Repository type.
        /// </param>
        public LogicLoadGame(IRepositoryLoadGame repo)
        {
            this.loadGameRepo = repo;
        }

        /// <summary>
        /// Deletes Player.
        /// </summary>
        /// <param name="id">Id of object to delete.</param>
        /// <param name="name">Name of object to delete.</param>
        /// <param name="hour">Time of the saving (hours).</param>
        /// <param name="minute">Time of the saving (minutes).</param>
        /// <param name="filename">Name of save file.</param>
        public void Delete(int id, string name, int hour, int minute, string filename)
        {
            this.loadGameRepo.Delete(name, id, hour, minute, filename);
        }

        /// <summary>
        /// Loads game object from savefile.
        /// </summary>
        /// <param name="id">Id of saved game to load.</param>
        /// <param name="filename">Name of save file.</param>
        /// <returns>Game object (which is 2 Players object).</returns>
        public List<Player> Read(int id, string filename)
        {
            List<Player> players = new List<Player>();
            XDocument xd = new XDocument(this.loadGameRepo.GetAll(filename));

            players.Add(xd.Descendants("player1")
                .Where(n => int.Parse(n.Parent.Attribute("id").Value) == id)
                .Select(node => new Player()
                {
                Name = node.Element("name")?.Value,
                CX = int.Parse(node.Element("posx")?.Value),
                CY = int.Parse(node.Element("posy")?.Value),
                DY = int.Parse(node.Element("posd")?.Value),
                IsJumping = bool.Parse(node.Element("jump")?.Value),
                Health = int.Parse(node.Element("health")?.Value),
                Stamina = int.Parse(node.Element("stamina")?.Value),
                Score = int.Parse(node.Element("score")?.Value),
                Invulnerable = bool.Parse(node.Element("invulnerable")?.Value),
                Stunned = bool.Parse(node.Element("stunned")?.Value),
                State = (PlayerStatus)Enum.Parse(typeof(PlayerStatus), node.Element("status")?.Value, true),
                Timer = int.Parse(node.Element("timer")?.Value),
                FacinLeft = bool.Parse(node.Element("fleft")?.Value),
                Geometry = bool.Parse(node.Element("fleft")?.Value) ? Player.FacingLeftBaseGeometry : Player.FacingRightBaseGeometry,
                }).FirstOrDefault());

            players.Add(xd.Descendants("player2")
                .Where(n => int.Parse(n.Parent.Attribute("id").Value) == id)
                .Select(node => new Player()
                {
                    Name = node.Element("name")?.Value,
                    CX = int.Parse(node.Element("posx")?.Value),
                    CY = int.Parse(node.Element("posy")?.Value),
                    DY = int.Parse(node.Element("posd")?.Value),
                    IsJumping = bool.Parse(node.Element("jump")?.Value),
                    Health = int.Parse(node.Element("health")?.Value),
                    Stamina = int.Parse(node.Element("stamina")?.Value),
                    Score = int.Parse(node.Element("score")?.Value),
                    Invulnerable = bool.Parse(node.Element("invulnerable")?.Value),
                    Stunned = bool.Parse(node.Element("stunned")?.Value),
                    State = (PlayerStatus)Enum.Parse(typeof(PlayerStatus), node.Element("status")?.Value, true),
                    Timer = int.Parse(node.Element("timer")?.Value),
                    FacinLeft = bool.Parse(node.Element("fleft")?.Value),
                    Geometry = bool.Parse(node.Element("fleft")?.Value) ? Player.FacingLeftBaseGeometry : Player.FacingRightBaseGeometry,
                }).FirstOrDefault());

            return players;
        }

        /// <summary>
        /// Reads Game object from savefile.
        /// </summary>
        /// <returns>Two Players.</returns>
        /// <param name="filename">Name of save file.</param>
        public List<SavedGame> ReadGame(string filename)
        {
            XDocument xd = new XDocument(this.loadGameRepo.GetAll(filename));

            List<SavedGame> games = xd.Descendants("player1")
                .Select(node => new SavedGame()
                {
                    Id = int.Parse(node.Parent.Attribute("id")?.Value),
                    Name = node.Parent.Attribute("name")?.Value,
                    Hour = int.Parse(node.Parent.Attribute("hour")?.Value),
                    Minute = int.Parse(node.Parent.Attribute("minute")?.Value),
                }).ToList();
            return games;
        }
    }
}
