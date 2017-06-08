using System;
using System.Collections.Generic;
using DataTools.classification;

namespace startup
{
    public static class OnStartUp
    {
        public static void Init()
        {
            Students.parseData();

        }

        
    }
}