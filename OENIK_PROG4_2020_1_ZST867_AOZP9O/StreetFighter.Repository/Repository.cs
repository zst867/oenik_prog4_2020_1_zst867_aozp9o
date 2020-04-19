// <copyright file="Repository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace StreetFighter.Repository
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    /// <summary>
    /// General repository class.
    /// </summary>
    public class Repository :
        IRepositoryHighScore,
        IRepositoryLoadGame,
        IRepositorySaveGame
    {
        /// <summary>
        /// Get all Player objects.
        /// </summary>
        /// <returns> XML.</returns>
        /// <param name="filename">Name of save file.</param>
        public XDocument GetAll(string filename)
        {
            var xd = XDocument.Load(filename);
            return xd;
        }

        /// <summary>
        /// Deletes one Player.
        /// </summary>
        /// <param name="name">Name of object to delete.</param>
        /// <param name="id">Id of object to delete.</param>
        /// <param name="hour">Time of the saving (hours).</param>
        /// <param name="minute">Time of the saving (minutes).</param>
        /// <param name="filename">Name of save file.</param>
        public void Delete(string name, int id, int hour, int minute, string filename)
        {
            XDocument xd = new XDocument(this.GetAll(filename));
            const string quote = "\"";

            var q1 = from t in xd.Elements("saved_games").Elements("game")
                     where int.Parse(t.Attribute("id").Value) == id
                     select t;

            foreach (XElement itemElement in q1)
            {
                itemElement.Value = string.Empty;
            }

            xd.Save("test.txt");
            string text = File.ReadAllText(filename);
            StreamWriter sw0 = new StreamWriter(filename, false);
            sw0.Write(text.Replace("<game id=" + quote + id + quote + " name=" + quote + name + quote + " hour=" + quote + hour + quote + " minute=" + quote + minute + quote + "></game>", string.Empty));
            sw0.Close();
            XDocument xd2 = new XDocument(this.GetAll(filename));
            var q2 = from t in xd2.Elements("saved_games").Elements("game")
                     where int.Parse(t.Attribute("id").Value) > id
                     select t;

            foreach (XElement itemElement in q2)
            {
                itemElement.Attribute("id").Value = (int.Parse(itemElement.Attribute("id").Value) - 1).ToString();
            }

            xd2.Save("test.txt");
        }

        /// <summary>
        /// Writes Player to save file.
        /// </summary>
        /// <param name="filename">Name of save file.</param>
        /// <param name="a">Players to write to savefile.</param>
        public void Write(string filename, string a)
        {
            string text = File.ReadAllText(filename);
            StreamWriter sw0 = new StreamWriter(filename, false);
            sw0.Write(text.Replace("</saved_games>", string.Empty));
            sw0.Close();
            StreamWriter sw1 = new StreamWriter(filename, true);
            sw1.Write(a);
            sw1.Close();
        }

        /// <summary>
        /// Can be used to get the number of save ids in savefile.
        /// </summary>
        /// <param name="filename">Name of save file.</param>
        /// <returns>Number of ids.</returns>
        public int GetIds(string filename)
        {
            XDocument xd = this.GetAll(filename);
            if (xd.Element("saved_games").HasElements)
            {
                int counter = xd
                    .Descendants("game")
                    .Count();
                return counter;
            }

            return 0;
        }
    }
}