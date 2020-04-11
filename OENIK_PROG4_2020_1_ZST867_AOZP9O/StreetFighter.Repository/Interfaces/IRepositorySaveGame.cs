// <copyright file="IRepositorySaveGame.cs" company="PlaceholderCompany">
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
    /// SaveGame interface.
    /// </summary>
    public interface IRepositorySaveGame
    {
        /// <summary>
        /// Saves Player object to savefile.
        /// </summary>
        /// <param name="a">Player object to save.</param>
        /// <param name="filename">Name of save file.</param>
        void Write(string a, string filename);

        /// <summary>
        /// Can be used to get the number of save ids in savefile.
        /// </summary>
        /// <param name="filename">Name of save file.</param>
        /// <returns>Number of ids.</returns>
        int GetIds(string filename);
    }
}
