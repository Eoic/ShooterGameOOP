using Server.Models.Prototype;

namespace Server.Models
{
    public class Weapon : WeaponPrototype
    {
        public Weapon()
        {
            Ammo = 100;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Ammo { get; set; }
        public int OwnedById { get; set; }
        public double Rate { get; set; }
        public string Type { get; set; }
        public Bullet Bullets { get; set; }

        public override Weapon Clone()
        {
            return (Weapon)this.MemberwiseClone();
        }

        public void UpdateRate(IRateStrategy s)
        {
            Rate = s.rate();
        }

        public class Builder
        {
            private static volatile Builder instance;
            private static readonly object syncRoot = new object();

            private const int DEFAULT_AMMO = 100;
            private const int DEFAULT_OWNER = 0;

            private int Id;
            private string Name { get; set; }
            private int Ammo { get; set; }
            private int OwnedById { get; set; }
            private IRateStrategy RateStrategy { get; set; }
            private string Type { get; set; }
            private Bullet Bullets { get; set; }

            public static Builder GetInstance()
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Builder();
                    }
                }

                return instance;
            }

            public Builder()
            {
                Ammo = DEFAULT_AMMO;

            }

            public Builder SetName(string name)
            {
                Name = name;
                return this;
            }

            public Builder SetAmmo(int ammo)
            {
                Ammo = ammo;
                return this;
            }

            public Builder SetOwner(int ownerId)
            {
                OwnedById = ownerId;
                return this;
            }

            public Builder SetRate(IRateStrategy s)
            {
                RateStrategy = s;
                return this;
            }

            public Builder SetType(string type)
            {
                Type = type;
                return this;
            }

            public Builder SetBullets(Bullet bullet)
            {
                Bullets = bullet;
                return this;
            }

            public Weapon Build()
            {
                var weapon = new Weapon();
                weapon.Id = Id++;
                weapon.Name = Name;
                weapon.Ammo = Ammo;
                weapon.OwnedById = OwnedById;
                weapon.Type = Type;
                weapon.Bullets = Bullets;
                
                if(RateStrategy != null) 
                    weapon.Rate = RateStrategy.rate();

                Dispose();
                return weapon;
            }

            private void Dispose()
            {
                Name = null;
                Ammo = DEFAULT_AMMO;
                OwnedById = DEFAULT_OWNER;
                RateStrategy = null;
            }
        }
    }
}