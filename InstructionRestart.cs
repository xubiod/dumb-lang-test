using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dumb_lang_test
{
    class InstructionRestart : Interfaces.IBasicInstruction
    {
        public void Execute()
        {
            Program.instruction_pointer = -1;
        }
    }
}
