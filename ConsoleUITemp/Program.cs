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
                sw.Start();
                list.ConsolePrint("People");
                long list1 = sw.ElapsedMilliseconds;
                Console.WriteLine("first");
                sw.Restart();
                list.ConsolePrint2("People");
                long list2 = sw.ElapsedMilliseconds;
                Console.WriteLine("second");
                sw.Restart();
                table.ConsolePrint();
                long table1 = sw.ElapsedMilliseconds;
                Console.WriteLine("third");
                sw.Restart();
                table.ConsolePrint2();
                long table2 = sw.ElapsedMilliseconds;
                Console.WriteLine("fourth");
                sw.Stop();

                Console.WriteLine("List (reflection style) {0}", list1);
                Console.WriteLine("List (toDataTable style) {0}", list2);
                Console.WriteLine("Table (reflection style) {0}", table1);
                Console.WriteLine("Table (toDataTable style) {0}", table2);
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
