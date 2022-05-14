using System;

namespace Helpers
{
    public static class Msg
    {
        public static void Write(string text)
        {
            Console.WriteLine(Environment.OSVersion.Platform.ToString());
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
