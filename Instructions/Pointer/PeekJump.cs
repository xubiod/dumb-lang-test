using dumb_lang_test.Interfaces;

namespace dumb_lang_test.Instructions.Pointer;

internal class PeekJump : IBasicInstruction
{
    public void Execute()
    {
        Program.MemoryPointer = Program.GetMemory();
    }
}