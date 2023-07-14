using dumb_lang_test.Interfaces;

namespace dumb_lang_test.Instructions;

internal class Skip : IBasicInstruction
{
    public void Execute()
    {
        Program.Skip = true;
    }
}