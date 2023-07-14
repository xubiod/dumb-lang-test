using System;
using dumb_lang_test.Interfaces;

namespace dumb_lang_test.Instructions.ArgumentRequired;

internal class OnZero : IInstruction<IBasicInstruction>
{
    public readonly List<IBasicInstruction> ExecutedOnSuccess = new();

    public OnZero()
    {
    }

    public OnZero(IBasicInstruction toRunOnZero)
    {
        ExecutedOnSuccess.Add(toRunOnZero);
    }

    public void Execute()
    {
        if (Program.GetMemory() == 0)
        {
            ExecutedOnSuccess[0].Execute();
        }
    }

    public void FillParameters(List<IBasicInstruction> parameters = null)
    {
        throw new NotImplementedException();
    }
}