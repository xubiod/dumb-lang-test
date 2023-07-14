using dumb_lang_test.Interfaces;

namespace dumb_lang_test.Instructions;

internal class CopyFromL : IBasicInstruction
{
    public void Execute()
    {
        Program.SetMemory(Program.GetMemoryLeftOf());
    }
}