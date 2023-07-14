using dumb_lang_test.Interfaces;

namespace dumb_lang_test.Instructions;

internal class BumpUp : IBasicInstruction
{
    public void Execute()
    {
        Program.ShiftMemory(1);
    }
}