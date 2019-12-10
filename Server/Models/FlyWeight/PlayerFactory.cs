using Server.Game.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Models.FlyWeight
{
    /// <summary>
    /// Flyweight pattern for Player
    /// </summary>
    public class PlayerFactory : Player
    {
        private static Dictionary<string, Player> dict =
                         new Dictionary<string, Player>();

        public static Player getPlayer(string type)
        {
            Player p = null;

            if (dict.ContainsKey(type))
                p = (Player) dict.Where(t => t.Key == type).Select(v => v.Value).FirstOrDefault();
            else
            {
                switch (type)
                {
                    case "teamA":
                        //Console.WriteLine("teamA Player Created");
                        p = new Player();
                        p.JoinTeam(0);
                        break;
                    case "teamB":
                        //Console.WriteLine("teamB Player Created");
                        p = new Player();
                        p.JoinTeam(1);
                        break;
                    default:
                        Console.WriteLine("Unreachable code.");
                        break;
                }

                // Once created insert it into the Dictionary
                dict.Add(type, p);
            }
            return p;
        }
    }
}