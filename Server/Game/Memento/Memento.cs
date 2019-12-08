namespace ConsoleApp1.Memento
{
    class Memento
    {
        public Memento(string name, int team, int health)
        {
            Name = name;
            Team = team;
            Health = health;
        }

        public string Name { get; set; }
        public int Team { get; set; }
        public int Health { get; set; }
    }
}
