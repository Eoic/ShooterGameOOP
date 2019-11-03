using System.Collections.Generic;

namespace Server.Models
{
    public class GunTypes
    {
       private static readonly List<string> Types = new List<string> { "Pistol", "Sniper Rifle", "Shotgun" };

        public static string GetGunType(int id) =>
            id <= Types.Count ? Types[id] : null;
    }
}