using System;
using System.Collections.Generic;
using System.Text;
using Task3.Subjects;

namespace Task3.Vaccines
{
    class Vaccinator3000 : IVaccine
    {
        public string Immunity => "ACTG";
        public double DeathRate => 0.1f;

        private Random randomElement = new Random(0);
        public override string ToString()
        {
            return "Vaccinator3000";
        }

        public void Vaccine(Pig pig)
        {
            for (int i = 0; i < 15; i++)
            {
                int k = randomElement.Next() % 4;
                pig.Immunity += Immunity[k];
            }
            if (randomElement.NextDouble() < DeathRate*3)
                pig.Alive = false;
        }

        public void Vaccine(Dog dog)
        {
            for(int i=0;i<3000;i++)
            {
                int k = randomElement.Next() % 4;
                dog.Immunity += Immunity[k];
            }
            if (randomElement.NextDouble() < DeathRate)
                dog.Alive = false;
        }

        public void Vaccine(Cat cat)
        {
            for (int i = 0; i < 300; i++)
            {
                int k = randomElement.Next() % 4;
                cat.Immunity += Immunity[k];
            }
            if (randomElement.NextDouble() < DeathRate)
                cat.Alive = false;
        }
    }
}
