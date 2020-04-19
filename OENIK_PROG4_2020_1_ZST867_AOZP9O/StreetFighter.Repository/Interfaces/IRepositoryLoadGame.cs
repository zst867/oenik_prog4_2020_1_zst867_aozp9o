// <copyright file="IRepositoryLoadGame.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace StreetFighter.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    /// <summary>
    /// LoadGame interface.
    /// </summary>
    public interface IRepositoryLoadGame
    {
        /// <summary>
        /// Deletes Player.
        /// </summary>
        /// <param name="name">Name of object to delete.</param>
        /// <param name="id">Id of object to delete.</param>
        /// <param name="hour">Time of the saving (hours).</param>
        /// <param name="minute">Time of the saving (minutes).</param>
        /// <param name="filename">Name of save file.</param>
        void Delete(string name, int id, int hour, int minute, string filename);

        /// <summary>
        /// Gets all Player objects.
        /// </summary>
        /// <returns> XML.</returns>
        /// <param name="filename">Name of save file.</param>
        XDocument GetAll(string filename);
    }
}
