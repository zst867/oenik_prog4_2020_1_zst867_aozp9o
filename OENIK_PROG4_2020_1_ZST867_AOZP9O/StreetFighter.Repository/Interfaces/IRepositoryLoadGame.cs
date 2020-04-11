﻿// <copyright file="IRepositoryLoadGame.cs" company="PlaceholderCompany">
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
        /// <param name="id">Id of object to delete.</param>
        /// <param name="filename">Name of save file.</param>
        void Delete(int id, string filename);

        /// <summary>
        /// Reads Player.
        /// </summary>
        /// <returns>XML.</returns>
        /// <param name="id">Id of object to read.</param>
        /// <param name="filename">Name of save file.</param>
        XDocument Read(int id, string filename);
    }
}
