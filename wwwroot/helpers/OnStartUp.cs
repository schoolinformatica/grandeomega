using System;

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