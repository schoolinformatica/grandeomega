using System;
using System.Collections.Generic;
using Data;
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