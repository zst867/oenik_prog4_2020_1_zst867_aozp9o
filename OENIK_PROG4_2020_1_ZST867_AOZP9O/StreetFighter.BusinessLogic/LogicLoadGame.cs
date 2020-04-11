// <copyright file="LogicLoadGame.cs" company="PlaceholderCompany">
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

    public class LogicLoadGame : ILogicLoadGame
    {
        private readonly IRepositoryLoadGame loadGameRepo;

        public void Delete(int id, string filename)
        {
            this.loadGameRepo.Delete(id, filename);
        }

        public List<Player> Read(int id, string filename)
        {
            List<Player> players = new List<Player>();
            XDocument xd = new XDocument(this.loadGameRepo.Read(id, filename));

            return xd.Descendants(id.ToString()).Select(node => new Player()
            {
                Name = node.Element("name")?.Value,
                PositionX = int.Parse(node.Element("posx")?.Value),
                PositionY = int.Parse(node.Element("posy")?.Value),
                Health = int.Parse(node.Element("health")?.Value),
                Stamina = int.Parse(node.Element("stamina")?.Value),
                Score = int.Parse(node.Element("score")?.Value),
                FacinLeft = bool.Parse(node.Element("fleft")?.Value),
                Invulnerable = bool.Parse(node.Element("invulnerable")?.Value),
                Stunned = bool.Parse(node.Element("stunned")?.Value),
            }).ToList();
        }
    }
}
