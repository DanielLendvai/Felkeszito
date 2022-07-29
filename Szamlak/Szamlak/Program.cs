using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Szamlak
{
    class Program
    {
        //SZLASZAM;SORSZAM;DATUM;PARTNER;OSSZEG;DEVIZA;DEVOSSZEG
        class Szamlak
        {
            public string Szlaszam;
            public int Sorszam;
            public DateTime Datum;
            public string Partner;
            public double Osszeg;
            public string Deviza;
            public double Devosszeg;
        }
        //PARTNER;NEV
        class Partnerek
        {
            public string Id;
            public string Nev;
        }

        List<Szamlak> _szamlak = new List<Szamlak>();
        List<Partnerek> _partnerek = new List<Partnerek>();

        public void Load()
        {
            using (var sr = new StreamReader("szamlak.csv"))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                {   
                    string sor = sr.ReadLine();
                    var ss = sor.Split(';');
                    _szamlak.Add(new Szamlak
                    {
                        Szlaszam = ss[0],
                        Sorszam = int.Parse(ss[1]),
                        Datum = DateTime.Parse(ss[2], CultureInfo.InvariantCulture),
                        Partner = ss[3],
                        Osszeg = double.Parse(ss[4], CultureInfo.InvariantCulture),
                        Deviza = ss[5],
                        Devosszeg = double.Parse(ss[6], CultureInfo.InvariantCulture)
                    });
                }
            }
            using (var s = new StreamReader("partnerek.csv"))
            {
                s.ReadLine();
                while (!s.EndOfStream)
                {
                    string sor = s.ReadLine();
                    var ss = sor.Split(";");
                    _partnerek.Add(new Partnerek
                    {
                        Id =ss[0],
                        Nev = ss[1],
                    });
                }
            }
        }
        public void Run()
        {
            Load();

            //1.feladat
            Console.WriteLine($"Számlák száma: {_szamlak.Count}");

            //2.feladat
            string szlaszam = "";
            double maxosszeg = 0;
            int sorszam = 0;
            string datum = "";
            foreach (var p in _szamlak)
            {
                if (p.Osszeg > maxosszeg)
                {
                    maxosszeg = p.Osszeg;
                    szlaszam = p.Szlaszam;
                    sorszam = p.Sorszam;
                    datum = p.Datum.ToString();
                }
            }
            Console.WriteLine($"A legnagyobb összegű számla sorszáma: {sorszam}, számlaszáma: {szlaszam}, dátuma: {datum} és összege: {maxosszeg}");

            //3.feladat
            double minosszeg = 0;
            bool elso = true;
            foreach (var p in _szamlak)
            {
                if (elso || p.Osszeg < minosszeg)
                {
                    elso = false;
                    minosszeg = p.Osszeg;
                    szlaszam = p.Szlaszam;
                    sorszam = p.Sorszam;
                    datum = p.Datum.ToString();
                }
            }
            Console.WriteLine($"A legkisebb összegű számla sorszáma: {sorszam}, számlaszáma: {szlaszam}, dátuma: {datum} és összege: {minosszeg}");

            //4. feladat
            string deviza = "";
            double maxosszeg2 = 0;
            double devosszeg = 0;
            foreach (var sz in _szamlak)
            {
                if (maxosszeg2 < sz.Devosszeg)
                {
                    deviza = sz.Deviza;
                    maxosszeg2 = sz.Osszeg;
                    szlaszam = sz.Szlaszam;
                    sorszam = sz.Sorszam;
                    devosszeg = sz.Devosszeg;
                }
            }
            Console.WriteLine($"A legnagyobb deviza összegű számla devizája: {deviza}, deviza összege: {devosszeg} összege: {maxosszeg2}, számlaszáma: {szlaszam}, sorszáma: {sorszam}");

            //5. feladat
            double maxSzamla = 0;
            string maxPartner = "";
            foreach (var sz in _szamlak)
            {
                if (sz.Osszeg > maxSzamla)
                {
                    maxSzamla = sz.Osszeg;
                    maxPartner = sz.Partner;
                }
            }
            Console.WriteLine($"A legnagyobb összegű ({maxSzamla}) számlát, {maxPartner} kapta.");

            //6. feladat
            double maxDevOsszeg = 0;
            string maxDeviza = "";
            string maxDevPartner = "";
            foreach (var sz in _szamlak)
            {
                if (sz.Devosszeg > maxDevOsszeg)
                {
                    maxDevOsszeg = sz.Devosszeg;
                    maxDeviza = sz.Deviza;
                    maxDevPartner = sz.Partner;
                }
            }
            Console.WriteLine($"A legnagyobb deviza ({maxDeviza}) összegű ({maxDevOsszeg}) számlát {maxDevPartner} kapta.");

            //7. feladat
            Console.WriteLine($"{_partnerek.Count} Partner van összesen.");

            //8. feladat
            string maxpartner = "";
            maxosszeg = 0;
            foreach (var p in _szamlak)
            {
                if (p.Osszeg > maxosszeg)
                {
                    maxosszeg = p.Osszeg;
                    maxpartner = p.Partner;
                }
            }
            Console.WriteLine($"Legnagyobb összegű számlát ({maxosszeg}), {maxpartner} kapta");
            string partnernev = PartnerNeve(maxpartner);
            Console.WriteLine($"Legnagyobb összegű számlát ({maxosszeg}), {partnernev} kapta");

            Dictionary<string, double> partnerosszeg = new Dictionary<string, double>();
            foreach(var sz in _szamlak)
            {
                if (!partnerosszeg.ContainsKey(sz.Partner))
                {
                    partnerosszeg.Add(sz.Partner, sz.Osszeg);
                }
                else 
                { 
                    partnerosszeg[sz.Partner] = partnerosszeg[sz.Partner] + sz.Osszeg; 
                }
            }
            foreach(var s in partnerosszeg)
            {
                Console.WriteLine($"{s.Key}: {s.Value}");
            }
            //10. feladat
            maxosszeg = 0;
            maxpartner = "";
            string maxdeviza = "";
            foreach (var p in _szamlak)
            {
                if (p.Devosszeg > maxosszeg)
                {
                    maxosszeg = p.Devosszeg;
                    maxpartner = p.Partner;
                    maxdeviza = p.Deviza;
                }
            }
             Console.WriteLine($"Legnagyobb deviza ({maxdeviza}) összegű számlát ({maxosszeg}), {PartnerNeve(maxpartner)} kapta.");

            Dictionary<DateTime, double> minimumosszeg = new Dictionary<DateTime, double>();
            foreach(var s in _szamlak)
            {
                if (!minimumosszeg.ContainsKey(s.Datum))
                {
                    minimumosszeg.Add(s.Datum, s.Osszeg);
                }
                else
                {
                    minimumosszeg[s.Datum] = minimumosszeg[s.Datum] + s.Osszeg;
                }
                //foreach(var m in minimumosszeg)
                //{
                //   Console.WriteLine($"{m.Key}; {m.Value}");
                //}               
            }
            double minOsszeg = double.MaxValue;
            DateTime mindatum = DateTime.Now;
            foreach(var m in minimumosszeg)
            {
                if(m.Value < minosszeg)
                {
                    minosszeg = m.Value;
                    mindatum = m.Key;
                }
            }

            double maxOsszeg = double.MinValue;
            DateTime maxdatum = DateTime.Now;
            foreach(var m in minimumosszeg)
            {
                if(m.Value > maxOsszeg)
                {
                    maxOsszeg = m.Value;
                    maxdatum = m.Key;
                }
            }
            Console.WriteLine($"{maxdatum}, {maxOsszeg}");
            Console.WriteLine($"{mindatum}, {minosszeg}");

            int[] kartyak = new int[20];
            for(int i = 0; i < kartyak.Length; i++)
            {
                kartyak[i] = i;
            }
            Random r = new Random();            
            for(int i = 0; i < 100; i++)
            {
                int i1 = r.Next(20);
                int i2 = r.Next(20);
                int x = kartyak[i1];
                kartyak[i1] = kartyak[i2];
                kartyak[i2] = x;
            }
            foreach(var i in kartyak)
            {
               // Console.WriteLine($"{i}");
            }

            
        }

        private string PartnerNeve(string maxpartner)
        {            
            foreach (var p in _partnerek)
            {
                if (p.Id == maxpartner)
                {
                    return p.Nev;                       
                }
            }
            return "Nem találom";
        }
        static void Main(string[] args)
        {
            new Program().Run();
        }
    }
}
