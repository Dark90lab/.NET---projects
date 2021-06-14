using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Task3
{
    //Decorator
    //Filtering DB
    class Filtering : IEnumerator<VirusData>
    {
        private IEnumerator<VirusData> Iterator { set; get; }
        IFilters filterType;
        
   
        public VirusData Current { set; get; }
        public Filtering(IEnumerator<VirusData> it, IFilters f)
        {
            Iterator = it;
            Current = it.Current;
            filterType = f;
        }
        object IEnumerator.Current => Current;

        public void Dispose() {}
     

        public bool MoveNext()
        {
            while (Iterator.MoveNext())
            {
                if (filterType.FilterValue(Iterator.Current))
                {
                    Current = Iterator.Current;
                    return true;
                }
            }
            return false;
        }

        public void Reset()
        {
            Iterator.Reset();
        }
    }
}
