// <copyright file="LogicHighScore.cs" company="PlaceholderCompany">
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
    /// Class for calculating highscore.
    /// </summary>
    public class LogicHighScore : ILogicHighScore
    {
        private readonly IRepositoryHighScore highScoreRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogicHighScore"/> class.
        /// </summary>
        public LogicHighScore()
        {
            this.highScoreRepo = new Repository();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogicHighScore"/> class.
        /// </summary>
        /// <param name="repo">
        /// Repository type.
        /// </param>
        public LogicHighScore(IRepositoryHighScore repo)
        {
            this.highScoreRepo = repo;
        }

        /// <summary>
        /// Calculate Highscore.
        /// </summary>
        /// <returns> Player with highest score.</returns>
        /// <param name="filename">Name of save file.</param>
        public Player CalculateHighscore(string filename)
        {
            XDocument xd = new XDocument(this.highScoreRepo.GetAll(filename));

            List<Player> q1 = xd.Descendants("player1")
                .Select(node => new Player()
                {
                    Name = node.Element("name")?.Value,
                    CX = int.Parse(node.Element("posx")?.Value),
                    CY = int.Parse(node.Element("posy")?.Value),
                    Health = int.Parse(node.Element("health")?.Value),
                    Stamina = int.Parse(node.Element("stamina")?.Value),
                    Score = int.Parse(node.Element("score")?.Value),
                    Invulnerable = bool.Parse(node.Element("invulnerable")?.Value),
                    Stunned = bool.Parse(node.Element("stunned")?.Value),
                    FacinLeft = bool.Parse(node.Element("fleft")?.Value),
                }).ToList();

            List<Player> q2 = xd.Descendants("player2")
           .Select(node => new Player()
           {
               Name = node.Element("name")?.Value,
               CX = int.Parse(node.Element("posx")?.Value),
               CY = int.Parse(node.Element("posy")?.Value),
               Health = int.Parse(node.Element("health")?.Value),
               Stamina = int.Parse(node.Element("stamina")?.Value),
               Score = int.Parse(node.Element("score")?.Value),
               Invulnerable = bool.Parse(node.Element("invulnerable")?.Value),
               Stunned = bool.Parse(node.Element("stunned")?.Value),
               FacinLeft = bool.Parse(node.Element("fleft")?.Value),
           }).ToList();

            return q1.Concat(q2).OrderByDescending(x => x.Score).FirstOrDefault();
        }
    }
}
