namespace Server.Models
{
    public class Weapon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Ammo { get; set; }
        public int OwnedById { get; set; }
        public double rate { get; set; }

        public void updateRate(IRateStrategy s)
        {
            rate = s.rate();
        }

        public class Builder
        {
            private static volatile Builder instance;
            private static object syncRoot = new object();

            private const int DEFAULT_AMMO = 100;
            private const int DEFAULT_OWNER = 0;

            private int Id;
            private string Name { get; set; }
            private int Ammo { get; set; }
            private int OwnedById { get; set; }
            private IRateStrategy RateStrategy { get; set; }

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

            public Builder setName(string name)
            {
                Name = name;
                return this;
            }

            public Builder setAmmo(int ammo)
            {
                Ammo = ammo;
                return this;
            }

            public Builder setOwner(int ownerId)
            {
                OwnedById = ownerId;
                return this;
            }

            public Builder setRate(IRateStrategy s)
            {
                RateStrategy = s;
                return this;
            }

            public Weapon build()
            {
                var weapon = new Weapon();
                weapon.Id = Id++;
                weapon.Name = Name;
                weapon.Ammo = Ammo;
                weapon.OwnedById = OwnedById;
                if(RateStrategy != null) weapon.rate = RateStrategy.rate();

                dispose();
                return weapon;
            }

            private void dispose()
            {
                Name = null;
                Ammo = DEFAULT_AMMO;
                OwnedById = DEFAULT_OWNER;
                RateStrategy = null;
            }
        }
    }
}