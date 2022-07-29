using System;
using System.Collections.Generic;
using System.IO;

namespace Operátorok
{
    class Program
    {
        class Operatorok
        {
            public int elso;
            public string operat;
            public int masodik;
        }
        
        List<Operatorok> _operatorok = new List<Operatorok>();
        public void Load()
        {
            using(var sr = new StreamReader("kifejezesek.txt")) 
            {                
                while (!sr.EndOfStream)
                {
                    string sor = sr.ReadLine();
                    var ss = sor.Split(" ");
                    _operatorok.Add(new Operatorok
                    {
                        elso = int.Parse(ss[0]),
                        operat = ss[1],
                        masodik = int.Parse(ss[2]),
                    }); 
                }
            
            }
        }
        public void Run()
        {
            Load(); 
            Console.WriteLine($"2. feladat: {_operatorok.Count}");

            int mod = 0;
            foreach(var o in _operatorok)
            {
                if(o.operat == "mod")
                {
                    mod++;
                }
            }
            Console.WriteLine($"3. feladat: Kifejezések maradékos osztással: {mod}");
//4. feladat
            foreach(var r in _operatorok)
            {
                if(r.elso % 10 == 0 && r.masodik % 10 == 0)
                {
                    Console.WriteLine($"4. feladat: Van ilyen kifejezés!");
                    break;
                }
            }
            //5. feladat
            Console.WriteLine($"5.feladat: Statisztika");
            Dictionary<string, int> szotar = new Dictionary<string, int>();
            foreach(var i in _operatorok)
            {
                if (szotar.ContainsKey(i.operat))
                {
                    szotar[i.operat]++;
                }
                else
                {
                    szotar.Add(i.operat, 1);
                }
            }
            foreach(var sz in szotar)
            {
                if(sz.Key == "mod")
                {
                    Console.WriteLine($"{sz.Key}        -> {sz.Value}");
                }
                if (sz.Key == "/")
                {
                    Console.WriteLine($"{sz.Key}        -> {sz.Value}");
                }
                if (sz.Key == "div")
                {
                    Console.WriteLine($"{sz.Key}        -> {sz.Value}");
                }
                if (sz.Key == "-")
                {
                    Console.WriteLine($"{sz.Key}        -> {sz.Value}");
                }
                if (sz.Key == "*")
                {
                    Console.WriteLine($"{sz.Key}        -> {sz.Value}");
                }
                if (sz.Key == "+")
                {
                    Console.WriteLine($"{sz.Key}        -> {sz.Value}");
                }
            }
            //6.feladat        
            string beolvasottKif = "";
            while (!beolvasottKif.Equals("vége"))
            {
                Console.WriteLine("7.feladat: Kérek egy kifejezést (pl.: 1 + 1): ");
                beolvasottKif = Console.ReadLine();
                if (F06(beolvasottKif).Equals("vége"))
                {
                    beolvasottKif = F06(beolvasottKif);
                }
                else
                {
                    Console.WriteLine(F06(beolvasottKif) + "\n");
                }
            }
            
        }
        private static String F06(string beolvasottKif)
        {
            string adat = "";
            string visszater = "";
            if (beolvasottKif.Equals("vége"))
            {
                return "vége";
            }
            string[] darab = beolvasottKif.Split(' ');
            adat = darab[0] + " " + darab[1] + " " + darab[2];
            try
            {
                switch (darab[1])
                {
                    case "mod":
                        visszater = adat + " = " + (int.Parse(darab[0]) % int.Parse(darab[2]));
                        break;
                    case "div":
                        visszater = adat + " = " + (int.Parse(darab[0]) / int.Parse(darab[2]));
                        break;
                    case "/":
                        visszater = adat + " = " + (Double.Parse(darab[0]) / Double.Parse(darab[2]));
                        break;
                    case "-":
                        visszater = adat + " = " + (int.Parse(darab[0]) - int.Parse(darab[2]));
                        break;
                    case "*":
                        visszater = adat + " = " + (int.Parse(darab[0]) * int.Parse(darab[2]));
                        break;
                    case "+":
                        visszater = adat + " = " + (int.Parse(darab[0]) + int.Parse(darab[2]));
                        break;
                    default:
                        return ($"{adat} = Hibás operátor!");
                }
            }
            catch (Exception)
            {
                return ($"{adat} = Egyéb hiba!");
            }
            return visszater;
        }
       
        
        static void Main(string[] args)
        {
            new Program().Run();
        }
    }
}
