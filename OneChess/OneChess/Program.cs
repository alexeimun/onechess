using System;

namespace OneChess
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            Manager man = new Manager();
            Match.LoadOptions();
            using (Game1 game = new Game1())
            {
                game.Window.BeginScreenDeviceChange(true);
                //game.Window.EndScreenDeviceChange(game.Window.ScreenDeviceName);
                if (Stage.starman) man.Show();
                game.Run();
            }
        }
    }
#endif
}

