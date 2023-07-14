﻿using dumb_lang_test.Interfaces;

namespace dumb_lang_test.Instructions;

internal class BitWiseXorR : IBasicInstruction
{
    public void Execute()
    {
        Program.SetMemory((byte)(Program.GetMemory() ^ Program.GetMemoryRightOf()));
    }
}