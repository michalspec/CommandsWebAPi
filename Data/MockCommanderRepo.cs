using ApiFirstYTry.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiFirstYTry.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public IEnumerable<Command> GetAppCommands()
        {
            var commands = new List<Command>
            {
                new Command {Id=2, Platform="Kiss my cook", Line="Garden", HowTo="Simple" },
                new Command {Id=3, Platform="Dig my root", HowTo="shock&depressed", Line="Whatsup" }
            };

            return commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command { Id = id, HowTo = "Boil an egg", Line = "Boil water", Platform = "Kettle and pan" };
        }
    }
}
