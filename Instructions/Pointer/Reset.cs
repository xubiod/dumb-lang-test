using dumb_lang_test.Interfaces;

namespace dumb_lang_test.Instructions.Pointer;

internal class Reset : IBasicInstruction
{
    public void Execute()
    {
        Program.MemoryPointer = 0;
    }
}