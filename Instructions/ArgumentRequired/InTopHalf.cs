using System;
using dumb_lang_test.Interfaces;

namespace dumb_lang_test.Instructions.ArgumentRequired;

internal class InTopHalf : IInstruction<IBasicInstruction>
{
    public readonly List<IBasicInstruction> ExecutedOnSuccess = new();

    public InTopHalf()
    {
    }

    public InTopHalf(IBasicInstruction toRunOnZero)
    {
        ExecutedOnSuccess.Add(toRunOnZero);
    }

    public void Execute()
    {
        if (Program.MemoryPointer < 0x80)
        {
            ExecutedOnSuccess[0].Execute();
        }
    }

    public void FillParameters(List<IBasicInstruction> parameters = null)
    {
        throw new NotImplementedException();
    }
}