#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace LostSoul
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                using (var game = new LostSoulGame())
                    game.Run();
            }
            catch (Exception e)
            {
                string stackfile = System.IO.Path.GetTempPath() + "lostsoul_" + Guid.NewGuid().ToString();
                System.IO.StreamWriter file = new System.IO.StreamWriter(stackfile);
                WriteExceptionsUntilNoMoreInner(e, file);
                file.Close();
                throw e;
            }
        }

        private static void WriteExceptionsUntilNoMoreInner(Exception e, System.IO.StreamWriter file)
        {
            while (e != null)
            {
                file.WriteLine(e.Message);
                file.WriteLine(e.StackTrace);
                file.WriteLine("");
                e = e.InnerException;
            }
        }
    }
#endif
}
