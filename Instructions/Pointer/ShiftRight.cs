using dumb_lang_test.Interfaces;

namespace dumb_lang_test.Instructions.Pointer;

internal class ShiftRight : IBasicInstruction
{
    public void Execute()
    {
        Program.MemoryPointer = (Program.MemoryPointer + 1) % Program.MemorySize;
    }
}