using Swed64;

namespace EldenRingTrainer
{
    public class Program
    {
        public static Swed swed = new Swed("start_protected_game");
        public static IntPtr moduleBase;
        public static void Main()
        {
            moduleBase = swed.GetModuleBase("start_protected_game.exe");

            RunesService runesService = new RunesService();
            StatsService statsService = new StatsService();
            HealthService healthService = new HealthService();
            RenderUI renderUI = new RenderUI(runesService, statsService, healthService);
            renderUI.Start();
        }
    }
}