using System;
using System.Collections.Generic;
using System.IO;

namespace kosar2004
{
    class Program
    {

        class Kosar
        {
           public string hazai;
            public string idegen;
            public int hazaiPont;
            public int idegenPont;
            public string helyszin;
            public DateTime idopont;
        }
        List<Kosar> _kosar = new List<Kosar>();
        
        public void Load()
        {
            using(var f = new StreamReader("eredmenyek.csv"))
            {
                f.ReadLine();
                while (!f.EndOfStream)
                {
                    string sor = f.ReadLine();
                    var ss = sor.Split(";");
                    _kosar.Add(new Kosar
                    {
                        hazai = ss[0],
                        idegen = ss[1],
                        hazaiPont = int.Parse(ss[2]),
                        idegenPont = int.Parse(ss[3]),
                        helyszin = ss[4],
                        idopont = DateTime.Parse(ss[5]),
                    });
                }
            }
        }
        public void Run()
        {
            Load();
//3.feladat            
            int rmhazai = 0;
            int rmidegen = 0;
            foreach(var h in _kosar)
            {
                if(h.hazai == "Real Madrid")
                {
                    rmhazai++;
                }
                if(h.idegen == "Real Madrid")
                {
                    rmidegen++;
                }
            }
            Console.WriteLine($"3. feladat: Real madrid: Hazai: {rmhazai}, Idegen: {rmidegen}");
//4.feladat
            int dontetlen = 0;
            foreach(var k in _kosar)
            {
                if(k.hazaiPont == k.idegenPont)
                {
                    dontetlen++;
                }                
            }
            if(dontetlen > 0)
            {
                Console.WriteLine($"4. feladat: Volt döntetlen? igen");
            }
            else
            {
                Console.WriteLine($"4. feladat: Volt döntetlen? nem");
            }
//5.feladat
            string teljesnev = "";
            foreach(var k in _kosar)
            {
                if (k.hazai.Contains("Barcelona"))
                {
                    teljesnev = k.hazai; 
                }
            }
            Console.WriteLine($"5. feladat: Barcelonai csapat neve: {teljesnev}");
            //6. feladat
            Console.WriteLine($"6. feladat:");
            foreach(var i in _kosar)
            {
                if(i.idopont == new DateTime(2004, 11, 21))
                {
                    Console.WriteLine($"    {i.hazai} - {i.idegen} ({i.hazaiPont}:{i.idegenPont})");
                }
            }

            Dictionary<string, int> szotar = new Dictionary<string, int>();
            foreach(var i in _kosar)
            {
                if (szotar.ContainsKey(i.helyszin))
                {
                    szotar[i.helyszin]++;
                }
                else
                {
                    szotar.Add(i.helyszin, 1);
                }
            }
            //7. feladat
            Console.WriteLine($"7.feladat");
            foreach(var sz in szotar)
            {
                if (sz.Value > 20)
                {
                    Console.WriteLine($"    {sz.Key} : {sz.Value}");
                }
            }
        
        }
        static void Main(string[] args)
        {
            new Program().Run();
        }
    }
}
