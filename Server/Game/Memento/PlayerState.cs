using System;

namespace ConsoleApp1.Memento
{
    class PlayerState
    {
        private string name;
        private int team;
        private int health;

        public PlayerState() { }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                Console.WriteLine("Name = " + name);
            }
        }
        public int Team
        {
            get { return team; }
            set
            {
                team = value;
                Console.WriteLine("Team = " + team);
            }
        }
        public int Health
        {
            get { return health; }
            set
            {
                health = value;
                Console.WriteLine("Health = " + health);
            }
        }

        public Memento CreateMemento()
        {
            Console.WriteLine();
            Console.WriteLine("Save player object to memory");
            return new Memento(name, team, health);
        }

        public void SetMemento(Memento memento)
        {
            Console.WriteLine();
            Console.WriteLine("Restoring object...");
            Name = memento.Name;
            Team = memento.Team;
            Health = memento.Health;
        }
    }

}
