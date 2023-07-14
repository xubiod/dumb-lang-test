using dumb_lang_test.Interfaces;

namespace dumb_lang_test.Instructions.Pointer;

internal class ShiftLeft : IBasicInstruction
{
    public void Execute()
    {
        Program.MemoryPointer += Program.MemoryPointer == 0 ? Program.MemorySize - 1 : -1;
    }
}