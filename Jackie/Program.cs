using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Jackie
{
    class Program
    {
        class Versenyek
        {
            public int Year;
            public int Races;
            public int Wins;
            public int Podiums;
            public int Poles;
            public int Fastests;


            public Versenyek() {

            }
            public Versenyek(string adatsor)
            {
                string[] data = adatsor.Split('\t');
                Year = int.Parse(data[0]);
                Races = int.Parse(data[1]);
                Wins = int.Parse(data[2]);
                Podiums = int.Parse(data[3]);
                Poles = int.Parse(data[4]);
                Fastests = int.Parse(data[5]);
            }
        }

        static List<Versenyek> statisztika = new();

        static void Main(string[] args)
        {
            //Feladat 2
            try
            {
                StreamReader r = new("jackie.txt");
                r.ReadLine();
                while (!r.EndOfStream)
                {
                    statisztika.Add(new(r.ReadLine()));
                }
                r.Close();
            } catch (IOException err)
            {
                Console.WriteLine(err.Message);
            }

            //Feladat 3
            Console.WriteLine($"3. feladat: {statisztika.Count}");

            //Feladat 4
            Versenyek legtobbVerseny = statisztika[0];
            foreach (Versenyek v in statisztika)
            {
                if (v.Races > legtobbVerseny.Races) legtobbVerseny = v;
            }
            Console.WriteLine($"4. feladat: {legtobbVerseny.Year}");

            //Feladat 5
            Console.WriteLine("5. feladat:");
            var versenyek = statisztika.GroupBy(x => x.Year.ToString().Substring(2, 1)).ToList();
            foreach (var v in versenyek)
            {
                Console.WriteLine($"\t{v.Key}0-es évek: {v.Sum(x => x.Wins)} megnyert verseny");
            }

            //Feladat 6
            try
            {
                StreamWriter w = new("jackie.html");
                w.WriteLine("<html>\n<head></head>\n<style>td { border: 1px solid black; }</style>\n<body>\n<h1>Jackie Stewart</h1>\n<table>");

                foreach (Versenyek v in statisztika)
                {
                    w.WriteLine($"<tr><td>{v.Year}</td><td>{v.Races}</td><td>{v.Wins}</td></tr>");
                }
                w.WriteLine("</table>\n</body></html>");
                w.Close();
            } catch (IOException err)
            {
                Console.WriteLine(err.Message);
            }
            Console.WriteLine("6. feladat: jackie.html");
        }
    }
}
