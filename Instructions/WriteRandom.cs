using System;
using dumb_lang_test.Interfaces;

namespace dumb_lang_test.Instructions;

internal class WriteRandom : IBasicInstruction
{
    private static readonly Random Rnd = new();

    public void Execute()
    {
        Program.SetMemory((byte)Rnd.Next(256));
    }
}