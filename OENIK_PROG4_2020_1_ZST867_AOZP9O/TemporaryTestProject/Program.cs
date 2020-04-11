using StreetFighter.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemporaryTestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Player pa = new Player()
            {
                Name = "name",
                PositionX = 12,
                PositionY = 34,
                Health = 10,
                Stamina = 9,
                Score = 4,
                FacinLeft = true,
                Invulnerable = false,
                Stunned = false,
            };

            Player pb = new Player()
            {
                Name = "name",
                PositionX = 12,
                PositionY = 34,
                Health = 10,
                Stamina = 9,
                Score = 4,
                FacinLeft = true,
                Invulnerable = false,
                Stunned = false,
            };

            //LogicSaveGame lsg = new LogicSaveGame();
            //lsg.Write(pa, pb, "test.txt");

            LogicLoadGame lsg = new LogicLoadGame();
            List<Player> players = lsg.Read(4,"test.txt");
            foreach (var item in players)
            {
                Console.WriteLine(item.Score);
            }

            Console.ReadLine();
        }
    }
}
