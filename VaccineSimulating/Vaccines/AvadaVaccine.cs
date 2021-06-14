using System;
using System.Collections.Generic;
using System.Text;
using Task3.Subjects;

namespace Task3.Vaccines
{
    class AvadaVaccine : IVaccine
    {
        public string Immunity => "ACTAGAACTAGGAGACCA";

        public double DeathRate => 0.2f;

        private Random randomElement = new Random(0);

        public override string ToString()
        {
            return "AvadaVaccine";
        }

        public void Vaccine(Pig pig)
        {
            pig.Alive = false;
            
        }

        public void Vaccine(Dog dog)
        {
            dog.Alive = true;
            dog.Immunity = Immunity;
        }

        public void Vaccine(Cat cat)
        {
            if (randomElement.NextDouble() < DeathRate)
                cat.Alive = false;
            else
            {
                cat.Alive = true;
                cat.Immunity = Immunity.Substring(3, Immunity.Length-3);
            }
        }
    }
}
