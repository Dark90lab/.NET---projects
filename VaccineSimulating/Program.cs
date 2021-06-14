//Vaccine Simulator
//Mateusz Grzelak

using System;
using System.Collections.Generic;
using Task3.Subjects;
using Task3.Vaccines;

namespace Task3
{
    class Program
    {
        public class MediaOutlet
        {
            public void Publish(IEnumerator<VirusData> it)
            {
                while(it.MoveNext())
                {
                    Console.WriteLine($"{it.Current.VirusName}, death rate: {it.Current.DeathRate}, infection rate: {it.Current.InfectionRate}");
                    foreach(var gen in it.Current.Genomes)
                    {
                        Console.WriteLine(" "+gen.ToString());
                    }

                }
            }
        }

        public class Tester
        {
            public void Test()
            {
                var vaccines = new List<IVaccine>() { new AvadaVaccine(), new Vaccinator3000(), new ReverseVaccine() };

                foreach (var vaccine in vaccines)
                {
                    Console.WriteLine($"Testing {vaccine}");
                    var subjects = new List<ISubject>();
                    int n = 5;
                    for (int i = 0; i < n; i++)
                    {
                        subjects.Add(new Cat($"{i}"));
                        subjects.Add(new Dog($"{i}"));
                        subjects.Add(new Pig($"{i}"));
                    }

                    foreach (var subject in subjects)
                    {
                        subject.VaccineObject(vaccine);
                        // process of vaccination
                    }

                    var genomeDatabase = Generators.PrepareGenomes();
                    var simpleDatabase = Generators.PrepareSimpleDatabase(genomeDatabase);
                    // iteration over SimpleGenomeDatabase 
                   
                    var simpledbIt = new SimpleDatabaseIterator(simpleDatabase, genomeDatabase);
                    while (simpledbIt.MoveNext())
                    {
                        foreach (var subject in subjects)
                    
                        subject.GetTested(simpledbIt.Current);
                    }
                   
                    int aliveCount = 0;
                    foreach (var subject in subjects)
                    {
                        if (subject.Alive) aliveCount++;
                    }
                    Console.WriteLine($"{aliveCount} alive!");
                }
            }
        }
        public static void Main(string[] args)
        {
            //Part 1 - iterating
            var genomeDatabase = Generators.PrepareGenomes();
            var simpleDatabase = Generators.PrepareSimpleDatabase(genomeDatabase);
            var excellDatabase = Generators.PrepareExcellDatabase(genomeDatabase);
            var overcomplicatedDatabase = Generators.PrepareOvercomplicatedDatabase(genomeDatabase);
            var mediaOutlet = new MediaOutlet();
            var iterator = new SimpleDatabaseIterator(simpleDatabase, genomeDatabase);
            var exlIterator = new ExcellDatabaseIterator(excellDatabase, genomeDatabase);
            var ovIterator = new OvercomplicatedDatabaseIterator(overcomplicatedDatabase, genomeDatabase);

            Console.WriteLine("-------Part 1-------\n\n---Simnple Data Base:---\n");
            mediaOutlet.Publish(iterator);
            Console.WriteLine("\n---Excel Data Base---\n");
            mediaOutlet.Publish(exlIterator);
            Console.WriteLine("\n---Overcomplicated Data Base---\n");
            mediaOutlet.Publish(ovIterator);

            //Part 2 - filtering and mapping
            Console.WriteLine("\n-------Part 2-------\n\n---Excel Data Base filtering---");
            var fIterator = new Filtering(new ExcellDatabaseIterator(excellDatabase, genomeDatabase),new FirstFilter());
            mediaOutlet.Publish(fIterator);
            Console.WriteLine("\n\n---Excel Data Base mapping---");
            var fMapping = new Mapping(new ExcellDatabaseIterator(excellDatabase, genomeDatabase), new FirstMapping());
            mediaOutlet.Publish(fMapping);
            var filteredAndMapped = new Filtering(new Mapping(new ExcellDatabaseIterator(excellDatabase, genomeDatabase), new FirstMapping()), new FirstFilter());
            Console.WriteLine("\n\n---Excel Data Base mapping and filtering---");
            mediaOutlet.Publish(filteredAndMapped);

            //Part 3 - testting animals
            Console.WriteLine("\n-------Part 3,4-------\n\n---Animals testing---");
            var tester = new Tester();
            tester.Test();
        }
    }
}
