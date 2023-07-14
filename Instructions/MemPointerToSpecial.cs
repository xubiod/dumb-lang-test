using dumb_lang_test.Interfaces;

namespace dumb_lang_test.Instructions;

internal class MemPointerToSpecial : ISpecialInstruction
{
    public void Execute()
    {
        Program.SetMemorySpecial((byte)Program.MemoryPointer);
    }
}