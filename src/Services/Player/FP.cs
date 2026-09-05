namespace EldenRingTrainer
{
    public class FPService
    {
        public int FP;

        public IntPtr FPYellowBarAddress, FPBarSizeAddress;

        public IntPtr FreezeFPAddress = Program.moduleBase + 0x4389AF;

        public byte[] FreezeFPBytes = { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 };
        public byte[] UnFreezeFPBytes = { 0x89, 0x87, 0x48, 0x01, 0x00, 0x00 };

        public FPService()
        {
            FPYellowBarAddress = Program.swed.ReadPointer(Program.moduleBase, 0x3D6F8F0);
            FPYellowBarAddress = Program.swed.ReadPointer(FPYellowBarAddress, 0x20) + 0x12E8;

            FPBarSizeAddress = Program.swed.ReadPointer(Program.moduleBase, 0x3B16E30);
            FPBarSizeAddress = Program.swed.ReadPointer(FPBarSizeAddress, 0x00) + 0xB00;
        }

        public void SetFP(int FP)
        {
            Program.swed.WriteInt(FPBarSizeAddress, FP);
            Program.swed.WriteInt(FPYellowBarAddress, FP);
        }

        public void GetCurrentFP()
        {
            FP = Program.swed.ReadInt(FPBarSizeAddress);
        }

        public void FreezeFP()
        {
            Program.swed.WriteBytes(FreezeFPAddress, FreezeFPBytes);
        }

        public void UnFreezeFP()
        {
            Program.swed.WriteBytes(FreezeFPAddress, UnFreezeFPBytes);
        }
    }
}
