using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Pilotak
{
    class Program
    {
        class Pilotak
        {
            //név;születési_dátum;nemzetiség;rajtszám
            //Lewis Hamilton;1985.01.07;brit;44
            public string Nev;
            public DateTime SzulDatum;
            public string Nemzetiseg;
            public int Rajtszam;
        }
        List<Pilotak> _pilotak = new List<Pilotak>();

        public void Load()
        {
            using(var sr = new StreamReader("pilotak.csv"))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    string sor = sr.ReadLine();
                    string[] ss = sor.Split(";");
                    int rsz;
                    _pilotak.Add(new Pilotak
                    {
                        Nev = ss[0],
                        SzulDatum = DateTime.Parse(ss[1]),
                        Nemzetiseg = ss[2],
                        Rajtszam = int.TryParse(ss[3], out rsz) ? rsz: 0
                    });
                }
            }
        }
        public void Run()
        {
            Load();
            Console.WriteLine($"3. feladat: {_pilotak.Count}");
            Console.WriteLine($"4. feladat: {_pilotak[_pilotak.Count-1].Nev}");
            Console.WriteLine($"5. feladat:");
// _pilotak.Where(x => x.SzulDatum < DateTime.Parse("1901.01.01")).ToList().ForEach(a => Console.WriteLine($"\t{a.Nev} ({a.SzulDatum.ToShortDateString()})"));
            foreach (var p in _pilotak)
            {
                if (p.SzulDatum < DateTime.Parse("1901.01.01"))
                    Console.WriteLine($"{p.Nev}, {p.SzulDatum}");
            }

            Console.WriteLine($"6. feladat: {_pilotak.Where(b => b.Rajtszam > 0).OrderBy(a => a.Rajtszam).First().Nemzetiseg}");

            int minrajtszam = int.MaxValue;
            string nemzet = "";
            for(int i = 0; i < _pilotak.Count; i++)
                if(_pilotak[i].Rajtszam > 0)
                    if(_pilotak[i].Rajtszam < minrajtszam)
                    {
                        minrajtszam = _pilotak[i].Rajtszam;
                        nemzet = _pilotak[i].Nemzetiseg;
                    }
             Console.WriteLine($"6. feladat: {nemzet}");

            Dictionary<int, int> hanyrsz = new Dictionary<int, int>();
            foreach(var p in _pilotak)
                if(p.Rajtszam > 0)
                {
                    if(!hanyrsz.ContainsKey(p.Rajtszam))
                        hanyrsz.Add(p.Rajtszam, 0);
                    hanyrsz[p.Rajtszam] =  hanyrsz[p.Rajtszam] + 1;
            }
            Console.Write($"7. feladat: ");
            bool first = true;
            foreach(var h in hanyrsz)
                if(h.Value > 1)
                {
                    if(first)
                        first = false;
                    else
                        Console.Write(", ");
                    Console.Write($"{h.Key}");
                }
            Console.WriteLine();



            //Console.Write($"7. feladat: ");
//_pilotak.GroupBy(j => j.Rajtszam).Where(g => g.Count() > 1 && g.Key != "").ToList().ForEach(a => Console.Write(a.Key + " "));

        }
        static void Main(string[] args)
        {
            new Program().Run();
        }
    }
}
