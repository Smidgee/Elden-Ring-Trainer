namespace EldenRingTrainer
{
    public class StatsService
    {
        public int Vigor;
        public int Mind;
        public int Endurance;
        public int Strength;
        public int Dexterity;
        public int Intelligence;
        public int Faith;
        public int Arcane;
        public int Weight;

        public IntPtr VigorStatAddress;
        public IntPtr MindStatAddress;
        public IntPtr EnduranceStatAddress;
        public IntPtr StrengthStatAddress;
        public IntPtr DexterityStatAddress;
        public IntPtr IntelligenceStatAddress;
        public IntPtr FaithStatAddress;
        public IntPtr ArcaneStatAddress;
        public IntPtr WeightStateVisualAddress;
        public IntPtr WeightRealStateAddress;
        public IntPtr WeightVisualObstructionAddress = Program.moduleBase + 0x7CC661;
        public IntPtr WeightRealObstructionAddress = Program.moduleBase + 0x661576;

        public byte[] NewWeightVisualStateBytes = { 0x90, 0x90, 0x90 };
        public byte[] NewWeightRealStateBytes = { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 };

        public StatsService()
        {
            VigorStatAddress = Program.swed.ReadPointer(Program.moduleBase, 0x3D6C4B8);
            VigorStatAddress = Program.swed.ReadPointer(VigorStatAddress, 0x148) + 0x26EC;

            MindStatAddress = VigorStatAddress + 0x04;
            EnduranceStatAddress = MindStatAddress + 0x04;
            StrengthStatAddress = EnduranceStatAddress + 0x04;
            DexterityStatAddress = StrengthStatAddress + 0x04;
            IntelligenceStatAddress = DexterityStatAddress + 0x04;
            FaithStatAddress = IntelligenceStatAddress + 0x04;
            ArcaneStatAddress = FaithStatAddress + 0x04;

            WeightStateVisualAddress = Program.swed.ReadPointer(Program.moduleBase, 0x3B46F58) + 0x3C;
            WeightRealStateAddress = Program.swed.ReadPointer(Program.moduleBase, 0x03B16E30);
            WeightRealStateAddress = Program.swed.ReadPointer(WeightRealStateAddress, 0x00);
            WeightRealStateAddress = Program.swed.ReadPointer(WeightRealStateAddress, 0x58) + 0x18C;

        }

        public void SetWeight(int Level)
        {
            Program.swed.WriteInt(WeightStateVisualAddress, Level + 1);
            Program.swed.WriteInt(WeightRealStateAddress, Level + 1);
        }

        public void FixWeightObscruction()
        {
            Program.swed.WriteBytes(WeightVisualObstructionAddress, NewWeightVisualStateBytes);
            Program.swed.WriteBytes(WeightRealObstructionAddress, NewWeightRealStateBytes);
        }

        public void SetStats(int Vigor, int Mind, int Endurance, int Strength, int Dexterity, int Intelligence, int Faith, int Arcane)
        {
            if (Vigor != 0)
            {
                Program.swed.WriteInt(VigorStatAddress, Vigor);
            }
            if (Mind != 0)
            {
                Program.swed.WriteInt(MindStatAddress, Mind);
            }
            if (Endurance != 0)
            {
                Program.swed.WriteInt(EnduranceStatAddress, Endurance);
            }
            if (Strength != 0)
            {
                Program.swed.WriteInt(StrengthStatAddress, Strength);
            }
            if (Dexterity != 0)
            {
                Program.swed.WriteInt(DexterityStatAddress, Dexterity);
            }
            if (Intelligence != 0)
            {
                Program.swed.WriteInt(IntelligenceStatAddress, Intelligence);
            }
            if (Faith != 0)
            {
                Program.swed.WriteInt(FaithStatAddress, Faith);
            }
            if (Arcane != 0)
            {
                Program.swed.WriteInt(ArcaneStatAddress, Arcane);
            }
        }
    }
}
