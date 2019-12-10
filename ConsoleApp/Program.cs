using ConsoleApp.Interpretor;
using Server.Game;
using Server.Game.Entities;
using Server.Models.FlyWeight;
using Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static string[] commands = { "!health", "!minimizeHealth" }; 

        static void Main(string[] args)
        {
            //Program program = new Program();

            print();
            run();
        }

        static void applyCommand(string c)
        {
            var teamA = formTeam("teamA");
            var teamB = formTeam("teamB");

            List<IExpression> e1, e2;

            switch (c)
            {
                case "!health":
                    //Console.WriteLine("do !health");

                    e1 = new List<IExpression>();
                    foreach (var p in teamA)
                        e1.Add(new NumberExpression(p.Health));

                    e2 = new List<IExpression>();
                    foreach (var p in teamB)
                        e2.Add(new NumberExpression(p.Health));

                    IExpression healthTeamA = new SumExpression(e1);
                    IExpression healthTeamB = new SumExpression(e2);

                    Console.WriteLine("Total health value of team A : " + healthTeamA.execute());
                    Console.WriteLine("Total health value of team B : " + healthTeamB.execute());

                    Console.WriteLine("---\nEnter next command or exit...");
                    break;
                case "!minimizeHealth":
                    //Console.WriteLine("do !minimizeHealth");

                    var damage = 10;
                    IExpression playerHealth;

                    e1 = new List<IExpression>();
                    foreach (var p in teamA)
                    {
                        e1.Add(new NumberExpression(p.Health));
                        p.TakeDamage(damage);
                        playerHealth = new SubtractExpression(e1.FirstOrDefault(), damage);
                        Console.WriteLine("Team A Player health reduced to : " + playerHealth.execute());
                    }

                    e2 = new List<IExpression>();
                    foreach (var p in teamB)
                    {
                        e2.Add(new NumberExpression(p.Health));
                        p.TakeDamage(damage);
                        playerHealth = new SubtractExpression(e2.FirstOrDefault(), damage);
                        Console.WriteLine("Team B Player health reduced to : " + playerHealth.execute());
                    }

                    Console.WriteLine("---\nEnter next command or exit...");
                    break;
                case "":
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }

        static string readCommand()
        {
            var c = Console.ReadLine();
            if (!commands.Contains(c) && c != "")
                return null;
            return c;
        }

        static void run()
        {
            while (true)
            {
                var c = readCommand();
                if (c != null)
                    applyCommand(c);
                else
                    Console.WriteLine("Invalid command, try again.\n---");
            }
        }

        static void print()
        {
            Console.Write("Available commands: ");
            foreach (var c in commands)
                Console.Write(c + " ");
            Console.WriteLine();
            Console.WriteLine("---\nEnter a command...");
        }

        static List<Player> formTeam(string team)
        {
            var t = new List<Player>();
            
            for(int i = 0; i < Constants.MaxPlayersPerTeam; i++)
                t.Add(PlayerFactory.getPlayer(team));

            return t;
        }
    }
}
