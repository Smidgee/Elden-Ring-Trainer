namespace EldenRingTrainer
{
    public class EnduranceService
    {

        public int Endurance;

        IntPtr EnduranceYellowBar, EnduranceBarSize;
        IntPtr FreezeEnduranceAddress = Program.moduleBase + 0x438AB2;

        public byte[] FreezeEnduranceBytes = { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 };
        public byte[] RestoreEnduranceBytes = { 0x89, 0x87, 0x54, 0x01, 0x00, 0x00 };

        public EnduranceService()
        {
            EnduranceYellowBar = Program.swed.ReadPointer(Program.moduleBase, 0x3D6F8F0);
            EnduranceYellowBar = Program.swed.ReadPointer(EnduranceYellowBar, 0x20) + 0x14E8;

            EnduranceBarSize = Program.swed.ReadPointer(Program.moduleBase, 0x3B16E30);
            EnduranceBarSize = Program.swed.ReadPointer(EnduranceBarSize, 0x00) + 0xB0C;
        }

        public void SetEndurance(int Endurance)
        {
            Program.swed.WriteInt(EnduranceBarSize, Endurance);
            Program.swed.WriteInt(EnduranceYellowBar, Endurance);
        }

        public void GetCurrentEndurance()
        {
            Endurance = Program.swed.ReadInt(EnduranceBarSize);
        }

        public void FreezeEndurance()
        {
            Program.swed.WriteBytes(FreezeEnduranceAddress, FreezeEnduranceBytes);
        }

        public void UnFreezeEndurance()
        {
            Program.swed.WriteBytes(FreezeEnduranceAddress, RestoreEnduranceBytes);
        }
    }
}
