using System;
using System.Collections.Generic;
using System.Text;

namespace Task3
{
    //Choose viruses for which Death Rate > 15
    class FirstFilter : IFilters
    {
        public bool FilterValue(VirusData v)
        {
            if (v.DeathRate > 15)
                return true;
            else
                return false;
        }
    }
}
