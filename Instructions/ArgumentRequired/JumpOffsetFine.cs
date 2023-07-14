using dumb_lang_test.Interfaces;

namespace dumb_lang_test.Instructions.ArgumentRequired;

internal class JumpOffsetFine : IInstruction<sbyte>
{
    private sbyte _lineOffset;

    public void Execute()
    {
        Program.InstructionPointer += _lineOffset;
    }

    public void FillParameters(List<sbyte> parameters = null)
    {
        if (parameters != null) _lineOffset = parameters[0];
    }

    public void SetParameter(sbyte parameter)
    {
        _lineOffset = parameter;
    }
}