namespace EldenRingTrainer
{
    public class RunesService
    {
        public int CurrentRuneAmount, AddRuneAmount;

        IntPtr runeAddress;

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
    }
}