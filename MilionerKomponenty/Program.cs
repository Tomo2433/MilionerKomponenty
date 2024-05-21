namespace MilionerKomponenty
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            //ApplicationConfiguration.Initialize();
            //Application.Run(new Form1());
            var x = new DummyGenerator();
            x.SetSubject("world war");
            x.SetAnswerCount(4);
            x.Generate();
            while (x.IsGenerating())
            {
                Console.WriteLine(".");
                Thread.Sleep(100);
            }
            var res = x.FetchResponse();
            x.ShowDebug();

            /*  GAMESTATE DEBUG
            var g = new GameState();
            for(int i = 0; i < 5; i++) {
                g.FetchAndUpdate();
                while(g.IsGenerating()) {
                    Console.Write(".");
                    Thread.Sleep(100);
                }
                Console.WriteLine(".");
                Console.WriteLine(g.GetCorrectAnswer());
            }
            */

        }
    }
}