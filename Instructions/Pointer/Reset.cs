using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dumb_lang_test.Instructions.Pointer
{
    class Reset : Interfaces.IBasicInstruction
    {
        public void Execute()
        {
            Program.MemoryPointer = 0;
            return;
        }
    }
}
