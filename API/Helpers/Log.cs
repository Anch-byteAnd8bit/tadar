using System;

namespace Helpers
{
    public static class Log
    {
        public static void Write(string text)
        {
            Console.WriteLine(DateTime.Now.ToString("T") + ":\t" + text);
        }

        public static void Write(Exception ex)
        {
            Write(ex.Message + " StackTrace: " + ex.StackTrace);
        }

        /*public static void Write(Exception ex)
        {

        }*/
    }
}
