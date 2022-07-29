using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Jackie
{
    class Program
    {
        class Verseny
        {
            public int Year;
            public int Races;
            public int Wins;
            public int Podiums;
            public int Poles;
            public int Fastests;
       
        }
        private readonly List<Verseny> _verseny = new List<Verseny>();
        

        public void LoadVerseny()
        {
            using (var f = new StreamReader("jackie.txt", Encoding.UTF8))
            {
                f.ReadLine();
                while (!f.EndOfStream)
                {
                    string sorok = f.ReadLine();
                    var ss = sorok.Split("\t");
                    _verseny.Add(new Verseny { 
                        Year        = int.Parse(ss[0]),
                        Races       = int.Parse(ss[1]),
                        Wins        = int.Parse(ss[2]),
                        Podiums     = int.Parse(ss[3]),
                        Poles       = int.Parse(ss[4]),
                        Fastests    = int.Parse(ss[5])
                    });;
                }
            }                                                                           
        }

        private void Load()
        {
            LoadVerseny();
        }

        private void Run()
        {
            Load();

            Console.WriteLine("3.feladat");
            Console.WriteLine($"    {_verseny.Count}");

            Console.WriteLine("4.feladat");
            Console.WriteLine($"    {_verseny.OrderByDescending(v=>v.Races).First().Year}");

            int maxyear = 0, maxraces = 0;
/*
 
 year   races   maxraces maxyear
                0       0
1973	18      18      1973
1972	11
1971	26      26      1971
1970	20
1969	19
1968	12
1967	27      27       1967
1966	26
1965	18
1964	14

 */
            foreach(var p in _verseny)
            {
                if(p.Races > maxraces)
                {
                    maxraces = p.Races;
                    maxyear = p.Year;
                }
            }
            Console.WriteLine("4.feladat");
            Console.WriteLine($"      {maxyear}");

/*
year	races	wins	podiums	poles	fastests evtized  evtizedek
                                                          {}              
1973	18	6	9	4	1                        1970     {}          => { 1970: 6 }
1972	11	4	5	2	4                        1970     { 1970: 6 } => { 1970: 10 }
1971	26	8	15	11	5
1970	20	3	8	7	3
1969	19	9	13	4	8
1968	12	4	5	0	3
1967	27	3	9	4	2
1966	26	6	7	3	6
1965	18	2	9	1	1
1964	14	8	11	5	8
 */
            Dictionary<int, int> evtizedek = new Dictionary<int, int>();
            foreach(var p in _verseny)
            {
                int kulcs = p.Year/10 * 10;
                if(!evtizedek.ContainsKey(kulcs))
                    evtizedek[kulcs] = 0;
                evtizedek[kulcs] = evtizedek[kulcs] + p.Wins;
            }
            
        }

        static void Main(string[] args)
        {
            new Program().Run();
        }
    }
}
