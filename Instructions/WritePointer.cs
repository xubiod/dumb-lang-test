using dumb_lang_test.Interfaces;

namespace dumb_lang_test.Instructions;

internal class WritePointer : IBasicInstruction
{
    public void Execute()
    {
        Program.SetMemory((byte)Program.MemoryPointer);
    }
}