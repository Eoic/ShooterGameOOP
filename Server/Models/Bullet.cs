namespace Server.Models
{
    public class Bullet
    {
        public int Id { get; set; }
        public double Damage { get; set; }
        public string GunType { get; set; }

        public Bullet(double damage, string gunType)
        {
            Damage = damage;
            GunType = gunType;
        }
    }
}