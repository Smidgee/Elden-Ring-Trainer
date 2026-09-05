namespace EldenRingTrainer
{
    public class HealthService
    {
        public int Health, MaxHealth;

        public IntPtr AddHPAddress, HPYellowBarAddress, HPBarSizeAddress;

        public IntPtr NoDamageAddress = Program.moduleBase + 0x4374A2;

        public byte[] NoDamageBytes = { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 };
        public byte[] DamageBytes = { 0x89, 0x81, 0x38, 0x01, 0x00, 0x00 };

        public HealthService()
        {
            AddHPAddress = Program.swed.ReadPointer(Program.moduleBase, 0x3B16E30);
            AddHPAddress = Program.swed.ReadPointer(AddHPAddress, 0x00) + 0xAE8;

            HPYellowBarAddress = Program.swed.ReadPointer(Program.moduleBase, 0x3D6F8F0);
            HPYellowBarAddress = Program.swed.ReadPointer(HPYellowBarAddress, 0x20) + 0xD10;

            HPBarSizeAddress = Program.swed.ReadPointer(Program.moduleBase, 0x3B16E30);
            HPBarSizeAddress = Program.swed.ReadPointer(HPBarSizeAddress, 0x00) + 0xAF4;
        }


        public void SetHP(int Health)
        {
            Program.swed.WriteInt(AddHPAddress, Health);
            Program.swed.WriteInt(HPYellowBarAddress, Health);
            Program.swed.WriteInt(HPBarSizeAddress, Health);
        }

        public void GetCurrentHP()
        {
            Health = Program.swed.ReadInt(HPYellowBarAddress);
        }

        public void NoDamage()
        {
            Program.swed.WriteBytes(NoDamageAddress, NoDamageBytes);
        }

        public void RestoreDamage()
        {
            Program.swed.WriteBytes(NoDamageAddress, DamageBytes);
        }
    }
}
