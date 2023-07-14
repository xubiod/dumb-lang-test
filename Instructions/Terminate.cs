using dumb_lang_test.Interfaces;

namespace dumb_lang_test.Instructions;

internal class Terminate : IBasicInstruction
{
    public void Execute()
    {
        Program.Halt = true;
    }
}