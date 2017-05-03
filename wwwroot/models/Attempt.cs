using System;

namespace models
{
    public class Attempt
    {  
        public int Id;
        public int TeachingUnitId;
        public int StudentId;

        public bool Success;

        public int StepId;
        public DateTime Date;
    }
}