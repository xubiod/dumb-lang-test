using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dumb_lang_test.Instructions.Pointer
{
    class ShiftRight : Interfaces.IBasicInstruction
    {
        public void Execute()
        {
            Program.MemoryPointer = (Program.MemoryPointer + 1) % Program.MEMORY_SIZE;
            return;
        }
    }
}
