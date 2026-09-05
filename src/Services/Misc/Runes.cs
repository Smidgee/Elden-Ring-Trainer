namespace EldenRingTrainer
{
    public class RunesService
    {
        public int CurrentRuneAmount, AddRuneAmount;

        IntPtr runeAddress;
        IntPtr FreezeRuneAddress = Program.moduleBase + 0x25E11A;

        public byte[] FreezeRuneBytes = { 0x90, 0x90, 0x90 };
        public byte[] UnFreezeRuneBytes = { 0x89, 0x41, 0x6C };

        public RunesService()
        {
            runeAddress = Program.swed.ReadPointer(Program.moduleBase, 0x3D61F98);
            runeAddress = Program.swed.ReadPointer(runeAddress, 0x08) + 0x6C;
        }

        public void AddRune(int RuneAmount)
        {
            CurrentRuneAmount = GetCurrentRuneAmount();
            Program.swed.WriteInt(runeAddress, CurrentRuneAmount + RuneAmount);
        }

        public int GetCurrentRuneAmount()
        {
            return Program.swed.ReadInt(runeAddress);
        }

        public void FreezeRune()
        {
            Program.swed.WriteBytes(FreezeRuneAddress, FreezeRuneBytes);
        }

        public void UnFreezeRune()
        {
            Program.swed.WriteBytes(FreezeRuneAddress, UnFreezeRuneBytes);
        }
    }
}