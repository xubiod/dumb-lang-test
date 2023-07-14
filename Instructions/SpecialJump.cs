using dumb_lang_test.Interfaces;

namespace dumb_lang_test.Instructions;

internal class SpecialJump : ISpecialInstruction
{
    public void Execute()
    {
        Program.MemoryPointer = Program.SpecialIndex;
    }
}