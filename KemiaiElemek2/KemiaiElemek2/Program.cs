using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace KemiaiElemek2
{
    class Program
    {
        class KemiaiElem  //Osztály létrehozása a beolvasott adatoknak
        {
            //Év;Elem;Vegyjel;Rendszám;Felfedező            
            public string Ev;
            public string Elem;
            public string Vegyjel;
            public int Rendszam;
            public string Felfedezo;
        }
        List<KemiaiElem> _elemek = new List<KemiaiElem>(); //A beolvasott adatok listába rendezése

        public void Load() //Load függvényben beolvassuk és szétbontjuk az adatokat.
        {
            using (var f = new StreamReader("felfedezesek.csv", Encoding.UTF8))
            {
                f.ReadLine();
                while (!f.EndOfStream)
                {
                    string sor = f.ReadLine();
                    var ss = sor.Split(";");
                    _elemek.Add(new KemiaiElem
                    {
                        Ev = ss[0],
                        Elem = ss[1],
                        Vegyjel = ss[2],
                        Rendszam = int.Parse(ss[3]),
                        Felfedezo = ss[4],
                    });
                }
            }          
        }   
//5.feladathoz
        public bool jovegyjel(string sChar)
        {
            string s = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return sChar.Length == 1 && s.Contains(sChar[0]) || sChar.Length == 2 && sChar.Contains(sChar[0]) && sChar.Contains(sChar[1]);            
        }
        
        public void Run() //Program működése
        {
            Load();     //Load függvény hívása.
            Console.WriteLine($"3. feladat: Elemek száma: {_elemek.Count}");
//4.feladat
            int okorifelfedezes = 0;
            int felfed = 0;
            foreach(var p in _elemek)
            {
                if (p.Ev == "Ókor")
                {
                    okorifelfedezes++;
                }
                if(p.Ev != "Ókor" && int.Parse(p.Ev) <= 1700)
                {
                    felfed++;
                }
            }
            Console.WriteLine($"4. feladat: Felfedezések száma az Ókorban: {okorifelfedezes}");
            Console.WriteLine($"Extra feladat: 1700 előtt és az Ókor után összesen {felfed} felfedezés történt.");
            Console.WriteLine($"Extra feladat: Összesen {okorifelfedezes + felfed} felfedezés történt 1700 előtt.");
//5.feladat           
            Console.WriteLine($"5. feladat: Kérek egy vegyjelet: ");
            string sChar = Console.ReadLine();
            while (!jovegyjel(sChar))
            {
                Console.WriteLine($"Nem megfelelő formátum! Kérek egy vegyjelet: ");
                sChar = Console.ReadLine();
            }
//6.feladat
            Console.WriteLine($"6. feladat: Keresés");
            bool talalt = false;
            foreach(var p in _elemek)
            {
                if(sChar.Equals(p.Vegyjel, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"Az elem vegyjele: {p.Vegyjel}");
                    Console.WriteLine($"Az elem neve: {p.Elem}");
                    Console.WriteLine($"Az elem rendszáma: {p.Rendszam}");
                    Console.WriteLine($"Az elem felfedezésének éve: {p.Ev}");
                    Console.WriteLine($"A felfedező neve: {p.Felfedezo}");
                    talalt = true;
                    break;
                }
            }
            if (!talalt)
            {
                Console.WriteLine($"Nincs ilyen elem.");
            }
//7. feladat
            int hossz = 0;
            for(int i = 1; i < _elemek.Count; i++) 
                if(_elemek[i-1].Ev != "Ókor")
                {
                    int ev1 = int.Parse(_elemek[i - 1].Ev);
                    int ev2 = int.Parse(_elemek[i].Ev);
                    if(ev2-ev1 > hossz)
                        hossz = ev2 - ev1;
                }
            Console.WriteLine($"7. feladat: {hossz} év volt a leghosszabb időszak két elem felfedezése között.");

//8. feladat
            Console.WriteLine("8. feladat: Statisztika: Melyik években fedeztek fel több mint három elemet?");
            Dictionary<string, int> stat = new Dictionary<string, int>();
            foreach (var e in _elemek)
                if (e.Ev != "Ókor")
                    if (stat.ContainsKey(e.Ev))
                        stat[e.Ev] = stat[e.Ev] + 1;
                    else
                        stat[e.Ev] = 1;
            foreach (var p in stat)
                if (p.Value > 3)
            Console.WriteLine($"    {p.Key}:  {p.Value} db");

//Extra feladat - Melyik években fedeztek fel 2 vagy annál kevesebb elemet?
            Console.WriteLine("8. feladat: Statisztika: Melyik években fedeztek fel kettő vagy annál kevesebb elemet?");
            Dictionary<string, int> stat2 = new Dictionary<string, int>();
            foreach (var f in _elemek)
                if (f.Ev != "Ókor")
                    if (stat2.ContainsKey(f.Ev))
                        stat2[f.Ev] = stat2[f.Ev] + 1;
                    else
                        stat2[f.Ev] = 1;
            foreach(var g in stat2)
                if(g.Value <= 2)
                Console.WriteLine($" {g.Key}: {g.Value} db");

        }
        static void Main(string[] args)
        {
            new Program().Run(); //Run függvény meghívása, program indítása
        }
    }
}
