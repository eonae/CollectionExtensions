using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUITemp
{
    public class Person
    {
        private static int nextId = 1;
        private static Random rnd = new Random();

        public readonly int Id;
        public string Name { get; set; }
        private int SecretNumber { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int RandomNumber { get; set; }

        public Person()
        {
            Id = nextId;
            nextId++;
            SecretNumber = rnd.Next(10);
            RandomNumber = rnd.Next(1000000);
        }
    }
}
