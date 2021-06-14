using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Task3
{
    class SimpleDatabaseIterator : IEnumerator<VirusData>
    {
        private SimpleDatabase Genomes { set; get; }
        private SimpleGenomeDatabase GenomeData { set; get; }
        private int count = 0;

        public VirusData Current { set; get; }

        object IEnumerator.Current => Current;

        public SimpleDatabaseIterator(SimpleDatabase simpleDB, SimpleGenomeDatabase genomeDB)
        {
            Genomes = simpleDB;
            GenomeData = genomeDB;
        }
        

      
        public void Dispose() { }

        public bool MoveNext()
        {
            if (count < Genomes.Rows.Count)
            {   
                Current = new VirusData(Genomes.Rows[count].VirusName, Genomes.Rows[count].DeathRate, Genomes.Rows[count].InfectionRate,GenomeData.genomeDatas.FindAll(Genome=>Genomes.Rows[count].GenomeId==Genome.Id));
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
