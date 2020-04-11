﻿// <copyright file="ILogicLoadGame.cs" company="PlaceholderCompany">
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

    /// <summary>
    /// LoadGame logic interface.
    /// </summary>
    public interface ILogicLoadGame
    {
        /// <summary>
        /// Deletes Player.
        /// </summary>
        /// <param name="id">Id of object to delete.</param>
        /// <param name="filename">Name of save file.</param>
        void Delete(int id, string filename);

        /// <summary>
        /// Reads Players from savefile.
        /// </summary>
        /// <returns>Two Players.</returns>
        /// <param name="id">Id of object to read.</param>
        /// <param name="filename">Name of save file.</param>
        List<Player> Read(int id, string filename);
    }
}
