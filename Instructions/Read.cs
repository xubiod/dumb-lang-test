using System;
using dumb_lang_test.Interfaces;

namespace dumb_lang_test.Instructions;

internal class Read : IBasicInstruction
{
    public void Execute()
    {
        Program.SetMemory((byte)Console.ReadKey().KeyChar);
    }
}