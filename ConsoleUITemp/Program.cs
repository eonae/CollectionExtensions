using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Eonae.CollectionExtensions;

namespace ConsoleUITemp
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new List<Person>();

            var list = new List<Person>();
            for(int i = 0; i < 5000; i++)
            {
                list.Add(new Person() { Name = "Sam", Description = "Just some man", Phone = "+79645792790", Email = "eonae@mail.ru" });
                list.Add(new Person() { Name = "Eddy", Description = "Corsairs fan", Phone = "+79263726687", Email = "tionil@list.ru" });
                list.Add(new Person() { Name = "Alexis", Description = "Nice guy", Phone = "+75691640280", Email = "aslanov@chess-iq.ru" });
            }
            var table = list.ToDataTable("People");

            var sw = new Stopwatch();

            try
            {
                Console.WriteLine(table.CreateStringTable());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.ReadKey();
            }


        }
    }
}
