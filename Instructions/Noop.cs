using dumb_lang_test.Interfaces;

namespace dumb_lang_test.Instructions;

internal class Noop : IBasicInstruction
{
    public void Execute()
    {
    }
}