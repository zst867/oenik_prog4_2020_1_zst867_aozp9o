// <copyright file="IRepositoryHighScore.cs" company="PlaceholderCompany">
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
    /// HighScore interface.
    /// </summary>
    public interface IRepositoryHighScore
    {
        /// <summary>
        /// Gets all Player objects.
        /// </summary>
        /// <returns> XML.</returns>
        /// <param name="filename">Name of save file.</param>
        XDocument GetAll(string filename);
    }
}
