namespace DataTools
{
    /*! \brief Two dimensional vector.
     *
     *  Two dimensional vector having a X and Y dimension.
     */
    
    public class Vector2
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Vector2(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}