using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Apa
{

    class Program
    {
        class Szamla
        {
            public string Szlaszam;
            public int Sorszam;
            public DateTime Datum;
            public string Partner;
            public double Osszeg;
            public string Deviza;
            public double DevOsszeg;
        }

        private readonly List<Szamla> _szamlak = new List<Szamla>();

        private void LoadSzamlak()
        {
            _szamlak.Clear();
            using(var f = File.OpenText("szamlak.csv"))
            {
                f.ReadLine();
                while(!f.EndOfStream)
                {
                    string sor = f.ReadLine();
                    string[] mezok = sor.Split(';');
                    _szamlak.Add(new Szamla {
                        Szlaszam = mezok[0],
                        Sorszam = int.Parse(mezok[1]),
                        Datum = DateTime.Parse(mezok[2], CultureInfo.InvariantCulture),
                        Partner = mezok[3],
                        Osszeg = double.Parse(mezok[4], CultureInfo.InvariantCulture),
                        Deviza = mezok[5],
                        DevOsszeg = double.Parse(mezok[6], CultureInfo.InvariantCulture)
                    });
                }
            }
        }

        void Run()
        {
            LoadSzamlak();

            Console.WriteLine("1. Feladat");
            Console.WriteLine($"    A számlák száma: {_szamlak.Count}");

            Console.WriteLine("2. Feladat");
            double maxosszeg = 0;
            string szlaszam = "";
            int sorszam = 0;
            foreach(var sz in _szamlak)
                if(sz.Osszeg > maxosszeg)
                {
                    maxosszeg = sz.Osszeg;
                    szlaszam = sz.Szlaszam;
                    sorszam = sz.Sorszam;
                }
            Console.WriteLine($"    A maximális összegű tétel: SZLASZAM={szlaszam}, SORSZAM={sorszam}, OSSZEG={maxosszeg}");

            Console.WriteLine("3. Feladat");
            double minosszeg = 0;
            bool elso = true;
            foreach(var sz in _szamlak)
                if(elso || sz.Osszeg < minosszeg)
                {
                    elso = false;
                    minosszeg = sz.Osszeg;
                    szlaszam = sz.Szlaszam;
                    sorszam = sz.Sorszam;
                }
            Console.WriteLine($"    A minimális összegű tétel: SZLASZAM={szlaszam}, SORSZAM={sorszam}, OSSZEG={minosszeg}");

            Console.WriteLine("4. Feladat");
            maxosszeg = 0;
            string deviza = "";
            foreach(var sz in _szamlak)
                if(sz.DevOsszeg > maxosszeg)
                {
                    maxosszeg = sz.Osszeg;
                    deviza = sz.Deviza;
                    szlaszam = sz.Szlaszam;
                    sorszam = sz.Sorszam;
                }
            Console.WriteLine($"    A maximális deviza összegű tétel: SZLASZAM={szlaszam}, SORSZAM={sorszam}, DEVIZA={deviza}, DEVOSSZEG={maxosszeg}");
        }

        static void Main(string[] args)
        {
            new Program().Run();
        }
    }
}
