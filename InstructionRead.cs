using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dumb_lang_test
{
    class InstructionRead : Interfaces.IBasicInstruction
    {
        public void Execute()
        {
            Program.SetMemory((byte)Console.ReadKey().KeyChar);
        }
    }
}
