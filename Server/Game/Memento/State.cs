using System;

namespace ConsoleApp1.Memento
{
    class State
    {
        private string name;
        private int team;
        private int health;

        public State() { }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
            }
        }
        public int Team
        {
            get { return team; }
            set
            {
                team = value;
            }
        }
        public int Health
        {
            get { return health; }
            set
            {
                health = value;
            }
        }

        public Memento CreateMemento()
        {
            return new Memento(name, team, health);
        }

        public void SetMemento(Memento memento)
        {
            Name = memento.Name;
            Team = memento.Team;
            Health = memento.Health;
        }
    }

}
