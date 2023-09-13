using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace VizibicikliKolcsonzo
{
    class Kolcsonzes
    {
        public string Nev;
        public string Azon;
        public TimeSpan Elvitel;
        public TimeSpan Visszahozatal;

        public Kolcsonzes(string adatsor)
        {
            string[] data = adatsor.Split(';');
            Nev = data[0];
            Azon = data[1];
            Elvitel = new TimeSpan(int.Parse(data[2]), int.Parse(data[3]), 0);
            Visszahozatal = new TimeSpan(int.Parse(data[4]), int.Parse(data[5]), 0);
        }
    }

    class Program
    {
        static List<Kolcsonzes> kolcsonzesek = new();

        static void Main(string[] args)
        {
            //F4
            try
            {
                StreamReader r = new("kolcsonzesek.txt");

                r.ReadLine();
                while (!r.EndOfStream)
                {
                    kolcsonzesek.Add(new(r.ReadLine()));
                }
                r.Close();
            } catch (IOException err)
            {
                Console.WriteLine(err.Message);
            }
            //F5
            Console.WriteLine($"5. feladat: Napi kölcsönzések száma: {kolcsonzesek.Count}");
            //F6
            Console.Write("6. feladat: Kérek egy nevet: ");
            string nev = Console.ReadLine();
            List<Kolcsonzes> nevkol = new();
            foreach (Kolcsonzes k in kolcsonzesek)
            {
                if (k.Nev == nev) nevkol.Add(k);
            }
            if (nevkol.Count == 0) Console.WriteLine("\tNem volt ilyen nevű kölcsönző!");
            else Console.WriteLine($"\t{nev} kölcsönzései:");
            foreach (Kolcsonzes k in nevkol)
            {
                Console.WriteLine($"\t{k.Elvitel.Hours:00}:{k.Elvitel.Minutes:00}-{k.Visszahozatal.Hours:00}:{k.Visszahozatal.Minutes:00}");
            }
            //F7
            Console.Write("7. feladat: Adjon meg egy időpontot óra:perc alakban: ");
            string idopont = Console.ReadLine();
            string[] d = idopont.Split(':');
            TimeSpan ido = new TimeSpan(int.Parse(d[0]), int.Parse(d[1]), 0);
            Console.WriteLine("\tA vízen lévő járművek:");
            foreach (Kolcsonzes k in kolcsonzesek)
            {
                if (k.Elvitel < ido && k.Visszahozatal > ido)
                    Console.WriteLine($"\t{k.Elvitel.Hours:00}:{k.Elvitel.Minutes:00}-{k.Visszahozatal.Hours:00}:{k.Visszahozatal.Minutes:00} : {k.Nev}");
            }
            //F8
            int bevetel = 0;
            foreach (Kolcsonzes k in kolcsonzesek)
            {
                double elteltido = k.Visszahozatal.TotalMinutes - k.Elvitel.TotalMinutes;
                if (elteltido % 30 != 0)
                {
                    bevetel += Convert.ToInt32(elteltido) / 30 * 2400 + 2400;
                } else
                {
                    bevetel += Convert.ToInt32(elteltido) / 30 * 2400;
                }
            }
            Console.WriteLine($"8. feladat: A napi bevétel: {bevetel} Ft");
            //F9
            try
            {
                StreamWriter w = new("F.txt");
                foreach (Kolcsonzes k in kolcsonzesek)
                {
                    if (k.Azon == "F")
                        w.WriteLine($"{k.Elvitel.Hours:00}:{k.Elvitel.Minutes:00}-{k.Visszahozatal.Hours:00}:{k.Visszahozatal.Minutes:00} : {k.Nev}");
                }
                w.Close();
            } catch (IOException err)
            {
                Console.WriteLine(err.Message);
            }
            //F10
            var statisztika = kolcsonzesek.GroupBy(x => x.Azon).OrderBy(x => x.Key);
            Console.WriteLine("10. feladat: Statisztika");
            foreach (var stat in statisztika)
            {
                Console.WriteLine($"\t{stat.Key} - {stat.Count()}");
            }
        }
    }
}
