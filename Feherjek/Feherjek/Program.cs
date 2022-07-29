using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Feherjek
{
    class Program
    {
        class feherjek
        {
            //Nev;Kategoria;Energia_kj;Energia_kcal;Feherje_g;Zsir_g;Szenhidrat_g
            //Albert keksz; Édesipari termékek;1855;443;8,7;12;75,1
            public string Nev;
            public string Kategoria;
            public int Energia_kj;
            public int Energia_kcal;
            public double Feherje;
            public double Zsir;
            public double Szenhidrat;

        }
        List<feherjek> _feherjek = new List<feherjek>();
        
        public void Load()
        {
            using(var sr = new StreamReader("feherjek.txt"))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    string sor = sr.ReadLine();
                    string[] ss = sor.Split(";");
                    _feherjek.Add(new feherjek
                    {
                        Nev = ss[0],
                        Kategoria = ss[1],
                        Energia_kj = int.Parse(ss[2]),
                        Energia_kcal = int.Parse(ss[3]),
                        Feherje = double.Parse(ss[4]),
                        Zsir = double.Parse(ss[4]),
                        Szenhidrat = double.Parse(ss[4]),
                    });
                }
            }
        }
        public void Run()
        {
            Load();

            Console.WriteLine($"3. feladat: Élelmiszerek száma: {_feherjek.Count}");
/*            feherjek max = _feherjek[0];
            for(int i = 1; i < _feherjek.Count; i++)
                if(_feherjek[i].Feherje > max.Feherje)
                {
                    max = _feherjek[i];
                }
            Console.WriteLine($"A legnagyobb fehérjetartalom {max.Feherje}, {max.Nev}, {max.Kategoria}");
*/
            double minfeherje = 0;
            string nev = "";
            string kategoria = "";
            foreach(var f in _feherjek)
            {
                if(f.Feherje > minfeherje)
                {
                    minfeherje = f.Feherje;
                    nev = f.Nev;
                    kategoria = f.Kategoria;
                }
            }
           Console.WriteLine($"4. feladat: \r\nA legnagyobb fehérjetartalmú élelmiszer neve: {nev} \r\nkategóriája: {kategoria} \r\nfehérjetartalma: {minfeherje}");

            int db = 0;
            double osszeg = 0;
            foreach(var f in _feherjek)
            {
                if( string.Compare(f.Kategoria, "Gabonafélék") == 0)
                {
                    db++;
                    osszeg +=f.Feherje;
                }
            }
            Console.WriteLine($"5. feladat: Gabonafélék átlagos fehérjetartalma: {osszeg/db}");

            Console.Write($"6. feladat: Kérek egy karakterláncot: ");
            string be = Console.ReadLine();
            bool talalt = false;
            foreach(var s in _feherjek)
            {
                if(s.Nev.ToLower().Contains(be.ToLower()) == true)
                {
                    Console.WriteLine($"Név: {s.Nev} + Kategória: {s.Kategoria} + Fehérjetartalom: {s.Feherje}");
                    talalt = true;
                }                
            }
            if (!talalt)
            {
                Console.WriteLine($"Nincs egyezés!");
            }

            Dictionary<string, int> statisztika = new Dictionary<string, int>();
            foreach(var f in _feherjek) //végigszalad a sorokon.
            {
                if (statisztika.ContainsKey(f.Kategoria)) //ha már van benne ugyanolyan Kategória (kulcs = f.Kategoria)
                {
                    statisztika[f.Kategoria]++;           //Abban a kategóriában lévő elemek számát megn
                }
                else                                      //Ha a szótárban még nincs olyan kategória 
                {
                    statisztika.Add(f.Kategoria, 1);      //Hozzáad egy új kulcsot (f.Kategoria).
                }
            }
                Console.WriteLine($"7. feladat: Statisztika");
                foreach (var s in statisztika)                  //Végigszalad a dictionaryn
                   if(s.Value < 10)                             //Ha a kategóriákban lévő elemek száma < 10
                   Console.WriteLine($"{s.Key} - {s.Value}");   //Kiírja a kulcs - érték párokat.                                 

            //8.Feladat
            FileStream fs = new FileStream("gabonafelek.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine($"Név, Fehérje");
            foreach(var f in _feherjek)
            {
                if( string.Compare(f.Kategoria, "Gabonafélék") == 0)
                {
                    sw.WriteLine($"{f.Nev}; {f.Feherje}");                    
                }
            }
            sw.Close();
            fs.Close();
                
        }
        static void Main(string[] args)
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("hu-HU");
            new Program().Run();
        }
    }
}
