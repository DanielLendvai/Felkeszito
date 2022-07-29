using System;
using System.Collections.Generic;
using System.IO;

namespace Balkezesek
{
    class Program
    {
        //név;elsõ;utolsó;súly;magasság
        //Jim Abbott;1989-04-08;1999-07-21;200;75
        class balkezesek
        {
            public string Nev;
            public DateTime Elso;
            public DateTime Utolso;
            public int Suly;
            public int Magassag;
        }
        List<balkezesek> _balkezesek = new List<balkezesek>();
        public void Load()
        {
            using(var sr = new StreamReader("balkezesek.csv"))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    string sor = sr.ReadLine();
                    var ss = sor.Split(';');
                    _balkezesek.Add(new balkezesek
                    {
                        Nev = ss[0],
                        Elso = DateTime.Parse(ss[1]),
                        Utolso = DateTime.Parse(ss[2]),
                        Suly = int.Parse(ss[3]),
                        Magassag = int.Parse(ss[4]),
                    });
                }
            }
        }
        public void Run()
        {
            Load();
            Console.WriteLine($"{_balkezesek.Count}");
            string oktoberNev = "";
            double oktoberMagassag = 0;
            foreach(var b in _balkezesek)
            {
                if (b.Utolso >= DateTime.Parse("1999-10-01") && b.Utolso <= DateTime.Parse("1999-10-31"))
                {
                    oktoberNev = b.Nev;
                    oktoberMagassag = b.Magassag * 2.54;
                    Console.WriteLine($"{oktoberNev}, {oktoberMagassag:F1}");
                }
            }
        }
        static void Main(string[] args)
        {
            new Program().Run();
        }
    }
}
