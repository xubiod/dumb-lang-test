using dumb_lang_test.Interfaces;

namespace dumb_lang_test.Instructions.ArgumentRequired;

internal class Group : IInstruction<IBasicInstruction>
{
    private readonly List<IBasicInstruction> _grouped = new();

    public void Execute()
    {
        foreach (var instruction in _grouped)
        {
            instruction.Execute();
            Program.AddCycles(3);
        }
    }

    public void FillParameters(List<IBasicInstruction> parameters = null)
    {
        if (parameters != null) _grouped.AddRange(parameters);
    }

    public void AddParameter(IBasicInstruction parameter)
    {
        _grouped.Add(parameter);
    }
}