using System;

namespace Helpers
{
    public static class Extensions
    {
        public static void Times(this int count, System.Action action)
        {
            for (var i = 0; i < count; i++)
                action();
        }
    }
}