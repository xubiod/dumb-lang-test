using dumb_lang_test.Interfaces;

namespace dumb_lang_test.Instructions;

internal class Restart : IBasicInstruction
{
    public void Execute()
    {
        Program.InstructionPointer = -1;
    }
}