using dumb_lang_test.Interfaces;

namespace dumb_lang_test.Instructions;

internal class ReplaceToSpecial : ISpecialInstruction
{
    public void Execute()
    {
        Program.SetMemorySpecial(Program.GetMemory());
    }
}