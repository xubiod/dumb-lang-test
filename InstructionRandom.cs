using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dumb_lang_test
{
    class InstructionRandom : Interfaces.IBasicInstruction
    {
        static readonly Random rnd = new();

        public void Execute()
        {
            Program.SetMemory((byte)rnd.Next(256));
        }
    }
}
