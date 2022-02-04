using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dumb_lang_test.Pointer
{
    class PointerInstructionShiftL : Interfaces.IBasicInstruction
    {
        public void Execute()
        {
            Program.MemoryPointer += Program.MemoryPointer == 0 ? Program.MEMORY_SIZE - 1 : -1;
            return;
        }
    }
}
