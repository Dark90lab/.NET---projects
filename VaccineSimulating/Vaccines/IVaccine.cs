using System;
using System.Collections.Generic;
using System.Text;
using Task3.Subjects;

namespace Task3.Vaccines
{
    //Visitior pattern
    interface IVaccine
    {
        public string Immunity { get; }
        public double DeathRate { get; }
        void Vaccine(Pig pig);
        void Vaccine(Dog dog);
        void Vaccine(Cat cat);
    }
}
