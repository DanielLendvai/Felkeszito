using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace KemiaiElemek
{
    class Program
    {
        class KemiaiElem

        {
            public string Ev;
            public string Elem;
            public string Vegyjel;
            public int Rendszam;
            public string Felfedezo;
        }

        private readonly List<KemiaiElem> _elemek =  new List<KemiaiElem>();
        
        private void Load()
        {
            using(var f = new StreamReader("felfedezesek.csv", Encoding.UTF8))
            {
                // Cimkek beolvasasa
                f.ReadLine();

                // Sorok olvasasa
                while(!f.EndOfStream)
                {
                    string sor = f.ReadLine();
                    var ss = sor.Split(';');
                    _elemek.Add(new KemiaiElem
                    {
                        Ev        = ss[0],
                        Elem      = ss[1],
                        Vegyjel   = ss[2],
                        Rendszam  = int.Parse(ss[3]),
                        Felfedezo = ss[4]
                    });
                }
            }
        }

        private bool isalpha(char ch)
        {
            return 'a' <= ch &&  ch <= 'z' || 'A' <= ch &&  ch <= 'Z';
        }

        private bool jovegyjel(string s)
        {
            if(s.Length != 1 && s.Length != 2)
                return false;
            if(!isalpha(s[0]))
                return false;
            if(s.Length == 2 && !isalpha(s[1]))
                return false;
            return true;
        }

        public void Run()
        {
            Load();

            // 3.feladat
            Console.WriteLine($"3. feladat: Elemek száma: {_elemek.Count}");

            // 4.feladat
            Console.WriteLine($"4. feladat: Felfedezések száma az ókorban: {_elemek.Count(e => e.Ev == "Ókor")}");

            // 5.feladat
            Console.Write("5. feladat: Kérek egy vegyjelet: ");
            string vegyjel = Console.ReadLine();
            while(!jovegyjel(vegyjel))
            {
                Console.Write("5. feladat: Kérek egy vegyjelet: ");
                vegyjel = Console.ReadLine();
            }

            // 6.feladat
            Console.WriteLine("6. feladat: Keresés");
            bool talalt = false;
            foreach(var e in _elemek)
            {
                if(vegyjel.Equals(e.Vegyjel, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"    Az elem vegyjele: {e.Vegyjel}");
                    Console.WriteLine($"    Az elem neve: {e.Elem}");
                    Console.WriteLine($"    Az elem renszáma: {e.Rendszam}");
                    Console.WriteLine($"    Felfedezés éve: {e.Ev}");
                    Console.WriteLine($"    Felfedező neve: {e.Felfedezo}");
                    talalt = true;
                    break;
                }
            }
            if(!talalt)
                Console.WriteLine("Nincs ilyen elem az adatforrásban!");
           
            // 7. Feladat
            int hossz = 0;
            for(int  i = 1; i <  _elemek.Count; i++)
                if(_elemek[i - 1].Ev != "Ókor")
                {
                    int e1 = int.Parse(_elemek[i - 1].Ev);
                    int e2 = int.Parse(_elemek[i].Ev);
                    if(e2 - e1 > hossz)
                        hossz = e2 - e1;
                }
            Console.WriteLine($"7. feladat: {hossz} év volt a leghosszabb időszak két elem felfedezése között.");

            // 8.feladat
            Console.WriteLine("8. feladat: Statisztika");
            Dictionary<string, int> stat =  new Dictionary<string, int>();
            foreach(var e in _elemek)
                if(e.Ev != "Ókor")
                    if(stat.ContainsKey(e.Ev))
                        stat[e.Ev] = stat[e.Ev] + 1;
                    else
                        stat[e.Ev]  = 1;
            foreach(var p in  stat)
                if(p.Value > 3)
                Console.WriteLine($"    {p.Key}:  {p.Value} db");
        }

        static void Main(string[] args)
        {
            new Program().Run();
        }
    }
}
