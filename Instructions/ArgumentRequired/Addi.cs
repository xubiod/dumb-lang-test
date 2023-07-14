using System.Linq;
using dumb_lang_test.Interfaces;

namespace dumb_lang_test.Instructions.ArgumentRequired;

internal class Addi : IInstruction<byte>
{
    private readonly List<byte> _values = new();

    public Addi()
    {
    }

    public Addi(List<byte> values)
    {
        _values = values;
    }

    public void Execute()
    {
        var result = Program.GetMemory();

        result = _values.Aggregate(result, (current, number) => (byte)(current + number));

        Program.SetMemory(result);
    }

    public void FillParameters(List<byte> parameters = null)
    {
        if (parameters != null) _values.AddRange(parameters);
    }
}