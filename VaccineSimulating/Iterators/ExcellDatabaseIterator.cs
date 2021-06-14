using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Task3
{
    class ExcellDatabaseIterator : IEnumerator<VirusData>
    {
        public VirusData Current { set; get; }
        private int count = 0;
        private List<string> names;
        private List<string> deathRate;
        private List<string> infectionRate;
        private List<string> geneID;
        private SimpleGenomeDatabase GenomeData { set; get; }
        public ExcellDatabaseIterator(ExcellDatabase exlDB, SimpleGenomeDatabase genomeDB)
        {
            names = exlDB.Names.Split(";").ToList();
            deathRate = exlDB.DeathRates.Split(";").ToList();
            infectionRate = exlDB.InfectionRates.Split(";").ToList();
            geneID = exlDB.GenomeIds.Split(";").ToList();
            GenomeData = genomeDB;
        }



        object IEnumerator.Current => Current;

        public void Dispose() { }


        public bool MoveNext()
        {
            if (count < names.Count)
            {

                Current = new VirusData(names[count], Convert.ToDouble(deathRate[count]), Convert.ToDouble(infectionRate[count]), GenomeData.genomeDatas.FindAll(Genome => Guid.Parse(geneID[count]) == Genome.Id));
                count++;
                return true;
            }
            else
                return false;
        }

        public void Reset()
        {
            count = 0;
        }
    }
}

