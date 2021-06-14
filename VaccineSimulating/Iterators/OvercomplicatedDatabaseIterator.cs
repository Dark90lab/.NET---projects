using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Task3
{
    class OvercomplicatedDatabaseIterator : IEnumerator<VirusData>
    {
        public VirusData Current { set; get; }
        private SimpleGenomeDatabase GenomeData { set; get; }
        private List<INode> Nodes { get; } = new List<INode>();
        private  int count =0;
        public OvercomplicatedDatabaseIterator(OvercomplicatedDatabase ovDB, SimpleGenomeDatabase simpleDB)
        {
            SearchTree(ovDB.Root);
            GenomeData = simpleDB;
        }

        void SearchTree(INode root)
        {
            Nodes.Add(root);
            foreach (var c in root.Children)
                SearchTree(c);
        }

        object IEnumerator.Current => Current;

        public void Dispose() { }


        public bool MoveNext()
        {
            if (count < Nodes.Count)
            {
                List<GenomeData> Genomes = new List<GenomeData>();
                foreach (var n in GenomeData.genomeDatas)
                    foreach (var k in n.Tags)
                        if (k == Nodes[count].GenomeTag)
                            Genomes.Add(n);
                Current = new VirusData(Nodes[count].VirusName,Nodes[count].DeathRate,Nodes[count].InfectionRate, Genomes);
                count++;
                return true;
            }
            else
                return false;
        }

        public void Reset()
        {
            count=0;
        }
    }
}
