﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dumb_lang_test.Instructions
{
    class BumpDown : Interfaces.IBasicInstruction
    {
        public void Execute()
        {
            Program.ShiftMemory(0xFF);
        }
    }
}