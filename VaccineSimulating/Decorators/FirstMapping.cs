using System;
using System.Collections.Generic;
using System.Text;

namespace Task3
{
    class FirstMapping : IMaps
    {
        //add +10 to death rate
        public VirusData doMapping(VirusData v)
        {
            return new VirusData(v.VirusName, v.DeathRate + 10, v.InfectionRate, v.Genomes);
        }
    }
}
