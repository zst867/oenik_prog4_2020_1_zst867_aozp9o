// <copyright file="ILogicHighScore.cs" company="PlaceholderCompany">
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
    /// HighScore logic interface.
    /// </summary>
    public interface ILogicHighScore
    {
        /// <summary>
        /// Calculate Highscore.
        /// </summary>
        /// <returns> Player with highest score.</returns>
        /// <param name="filename">Name of save file.</param>
        Player CalculateHighscore(string filename);
    }
}
