
namespace DataTools.utils
{
    public static class Utils
    {
        public static void Times(this int count, System.Action action)
        {
            for (var i = 0; i < count; i++)
                action();
        }
    }
}