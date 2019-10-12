using System.Collections.Generic;

namespace Server.Models
{
    public class GunTypes
    {
       private static List<string> gunTypes = new List<string> { "Pistol", "Sniper Rifle", "Shotgun" };

        public static string getGunType(int id)
        {
            if(id <= gunTypes.Count)
                return gunTypes[id];
            return null;
        }
    }
}