using System;


namespace ForceField.Game
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            using (FFMain game = new FFMain())
            {
                game.Run();
            }
        }
    }
}

