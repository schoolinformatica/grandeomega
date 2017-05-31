using System;

namespace models
{
    public class Attempt
    {  
        public int Id;
        public int TeachingUnitId;
        public int StudentId;
        public int StepId;
        public bool Success;
        public DateTime Date;
    }
}