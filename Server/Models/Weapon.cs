using Server.Models.Prototype;

namespace Server.Models
{
    public class Weapon : WeaponPrototype, IWeapon
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

        public int getAmmo()
        {
            return Ammo;
        }

        public class Builder
        {
            private static volatile Builder _instance;
            private static readonly object SyncRoot = new object();

            private const int DefaultAmmo = 100;
            private const int DefaultOwner = 0;

            private int Id;
            private string Name { get; set; }
            private int Ammo { get; set; }
            private int OwnedById { get; set; }
            private IRateStrategy RateStrategy { get; set; }
            private string Type { get; set; }
            private Bullet Bullets { get; set; }

            public static Builder GetInstance()
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new Builder();
                    }
                }

                return _instance;
            }

            public Builder()
            {
                Ammo = DefaultAmmo;

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
                var weapon = new Weapon
                {
                    Id = Id++,
                    Name = Name,
                    Ammo = Ammo,
                    OwnedById = OwnedById,
                    Type = Type,
                    Bullets = Bullets
                };

                if(RateStrategy != null) 
                    weapon.Rate = RateStrategy.Rate();

                Dispose();
                return weapon;
            }

            private void Dispose()
            {
                Name = null;
                Ammo = DefaultAmmo;
                OwnedById = DefaultOwner;
                RateStrategy = null;
            }
        }
    }
}