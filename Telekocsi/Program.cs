using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Telekocsi
{
    class Program
    {
        class Auto
        {
            public string Indulas;
            public string Cel;
            public string Rendszam;
            public string Telefonszam;
            public int Ferohely;
            public int Foglalt;
        }

        class Igeny
        {
            public string Azonosito;
            public string Indulas;
            public string Cel;
            public int Szemelyek;
        }

        private readonly List<Auto> _autok = new List<Auto>();
        private readonly List<Igeny> _igenyek = new List<Igeny>();

        private void LoadAutok()
        {
            using(var f = new StreamReader("autok.csv", Encoding.UTF8))
            {
                // Cimkek beolvasasa
                f.ReadLine();

                // Sorok olvasasa
                while(!f.EndOfStream)
                {
                    string sor = f.ReadLine();
                    var ss = sor.Split(';');
                    _autok.Add(new Auto
                    {
                        Indulas     = ss[0],
                        Cel         = ss[1],
                        Rendszam    = ss[2],
                        Telefonszam = ss[3],
                        Ferohely    = int.Parse(ss[4]),
                        Foglalt     = 0
                    });
                }
            }
        }

        private void LoadIgenyek()
        {
            using(var f = new StreamReader("igenyek.csv", Encoding.UTF8))
            {
                // Cimkek beolvasasa
                f.ReadLine();

                // Sorok olvasasa
                while(!f.EndOfStream)
                {
                    string sor = f.ReadLine();
                    var ss = sor.Split(';');
                    _igenyek.Add(new Igeny
                    {
                        Azonosito = ss[0],
                        Indulas   = ss[1],
                        Cel       = ss[2],
                        Szemelyek = int.Parse(ss[3])
                    });
                }
            }
        }

        private void Load()
        {
            LoadAutok();
            LoadIgenyek();
        }

        public void Run()
        {
            Load();

            // 2. feladat
            Console.WriteLine("2. feladat");
//            HashSet<string> hirdetok = new HashSet<string>();
//            foreach(var a in _autok)
//                hirdetok.Add(a.Telefonszam);
//            Console.WriteLine($"    {hirdetok.Count} autós hirdet fuvart");
            Console.WriteLine($"    {_autok.Select(a => a.Telefonszam).Distinct().Count()} autós hirdet fuvart");

            // 3. feladat
            Console.WriteLine("3. feladat");
//            int ferohelyek = 0;
//            foreach(var a in _autok)
//                if(a.Indulas == "Budapest" && a.Cel == "Miskolc")
//                    ferohelyek += a.Ferohely;
//            Console.WriteLine($"    Összesen {ferohelyek} férőhelyet hirdettek az autósok Budapestről Miskolcra");
            Console.WriteLine($"    Összesen {_autok.Where(a => a.Indulas == "Budapest" && a.Cel == "Miskolc").Sum(a => a.Ferohely)} férőhelyet hirdettek az autósok Budapestről Miskolcra");

            // 4. feladat
            Console.WriteLine("4. feladat");
            // Kulcs = Indulas + "-" + Cel, Ertek = SUM(Ferohel)
            Dictionary<string, int> ferut = new Dictionary<string, int>();
            foreach(var a in _autok)
            {
                string key = a.Indulas + "-" + a.Cel;
                if(!ferut.ContainsKey(key))
                    ferut[key] = a.Ferohely;
                else
                    ferut[key] = ferut[key] + a.Ferohely;                    
            }

            // Max kivétel
            string maxutvonal = "";
            int maxferohely = 0;
            foreach(var f in ferut)
                if(f.Value > maxferohely)
                {
                    maxferohely = f.Value;
                    maxutvonal = f.Key;
                }
            Console.WriteLine($"    A legtöbb férőhelyet ({maxferohely}) a {maxutvonal} útvonalon ajánlották fel a hirdetők");

            // 5. feladat
            Console.WriteLine("5. feladat");
            foreach(var i in _igenyek)
            {
                foreach(var a in _autok)
                    if(i.Indulas == a.Indulas && i.Cel == a.Cel && i.Szemelyek <= a.Ferohely - a.Foglalt)
                    {
                        Console.WriteLine($"    {i.Azonosito} => {a.Telefonszam}");
                        a.Foglalt += i.Szemelyek;
                        break;
                    }
            }

            // 6. feladat
            Console.WriteLine("6. feladat");
            // Foglaltsag torlese
            foreach(var a in _autok)
                a.Foglalt = 0;
            using(var f = File.CreateText("utasuzenetek.txt"))
            {
                foreach(var i in _igenyek)
                {
                    bool talaltam = false;
                    foreach(var a in _autok)
                        if(i.Indulas == a.Indulas && i.Cel == a.Cel && i.Szemelyek <= a.Ferohely - a.Foglalt)
                        {
                            talaltam = true;
                            f.WriteLine($"{i.Azonosito}: Rendszám: {a.Rendszam}, Telefonszám: {a.Telefonszam}");
                            a.Foglalt += i.Szemelyek;
                            break;
                        }
                    if(!talaltam)
                        f.WriteLine($"{i.Azonosito}: Sajnos nem sikerült autót találni");
                }
            }
        }

        static void Main(string[] args)
        {
            new Program().Run();
        }
    }
}
