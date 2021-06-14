using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Task3
{
    //Add mappings
    class Mapping : IEnumerator<VirusData>
    {
        private IEnumerator<VirusData> Iterator { set; get; }
        private IMaps MapType;

        public VirusData Current { set; get; }
        public Mapping(IEnumerator<VirusData> it , IMaps im)
        {
            Iterator = it;
            MapType = im;
        }
        object IEnumerator.Current => Current;

        public void Dispose() { }
        
        public bool MoveNext()
        {
            if (Iterator.MoveNext())
            {
                Current = MapType.doMapping(Iterator.Current);
                return true;
             }
            return false;
        }

        public void Reset()
        {
            Iterator.Reset();
        }
    }
}
