// <copyright file="ILogicSaveGame.cs" company="PlaceholderCompany">
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
    /// SaveGame logic interface.
    /// </summary>
    public interface ILogicSaveGame
    {
        /// <summary>
        /// Saves Player object to savefile.
        /// </summary>
        /// <param name="a">First Player object to save.</param>
        /// <param name="b">Second Player object to save.</param>
        /// <param name="filename">Name of save file.</param>
        void Write(Player a, Player b, string filename);
    }
}
