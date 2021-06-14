using System;
using System.Collections.Generic;
using System.Text;
using Task3.Subjects;

namespace Task3.Vaccines
{
    class ReverseVaccine : IVaccine
    {
        public string Immunity => "ACTGAGACAT";

        public double DeathRate => 0.05f;
        private int Counter=0;

        private Random randomElement = new Random(0);
        public override string ToString()
        {
            return "ReverseVaccine";
        }

        public void Vaccine(Pig pig)
        {
            Counter++;
            char[] charArray = Immunity.ToCharArray();
            string extra = new string(charArray);
            pig.Immunity += Immunity + extra;

            if (randomElement.NextDouble() < DeathRate * Counter)
                pig.Alive = false;
        }

        public void Vaccine(Dog dog)
        {
            char[] charArray = Immunity.ToCharArray();
            Array.Reverse(charArray);
            dog.Immunity = new string(charArray);
            Counter++;
            dog.Alive = true;
        }

        public void Vaccine(Cat cat)
        {
            Counter++;
            cat.Alive = false;
        }
    }
}
