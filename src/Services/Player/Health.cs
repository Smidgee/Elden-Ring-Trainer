namespace EldenRingTrainer
{
    public class HealthService
    {
        public int Health;

        public IntPtr AddHPAddress, YellowBarAddress, BarSizeAddress;

        public HealthService()
        {
            AddHPAddress = Program.swed.ReadPointer(Program.moduleBase, 0x3B16E30);
            AddHPAddress = Program.swed.ReadPointer(AddHPAddress, 0x00) + 0xAE8;

            YellowBarAddress = Program.swed.ReadPointer(Program.moduleBase, 0x3D6F8F0);
            YellowBarAddress = Program.swed.ReadPointer(YellowBarAddress, 0x20) + 0xD10;

            BarSizeAddress = Program.swed.ReadPointer(Program.moduleBase, 0x3B16E30);
            BarSizeAddress = Program.swed.ReadPointer(BarSizeAddress, 0x00) + 0xAF4;
        }


        public void SetHP(int Health)
        {
            Program.swed.WriteInt(AddHPAddress, Health);
            Program.swed.WriteInt(YellowBarAddress, Health);
            Program.swed.WriteInt(BarSizeAddress, Health);
        }

    }
}
