using dumb_lang_test.Interfaces;

namespace dumb_lang_test.Instructions;

internal class ReplaceFromSpecial : ISpecialInstruction
{
    public void Execute()
    {
        Program.SetMemory(Program.GetMemorySpecial());
    }
}