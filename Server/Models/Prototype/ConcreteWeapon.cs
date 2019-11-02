namespace Server.Models.Prototype
{
    class ConcreteWeapon : WeaponPrototype
    {
       private Weapon weapon;

        public ConcreteWeapon(int bulletCount)
        {
            if (weapon == null)
            {
                this.weapon = Weapon.Builder.GetInstance()
                    .setAmmo(bulletCount)
                    .build();
            }
        }

        public override Weapon Clone()
        {
            return this.MemberwiseClone() as Weapon;
        }
    }
}