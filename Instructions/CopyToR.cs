using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dumb_lang_test.Instructions
{
    class CopyToR : Interfaces.IBasicInstruction
    {
        public void Execute()
        {
            Program.SetMemoryRightOf(Program.GetMemory());
        }
    }
}
