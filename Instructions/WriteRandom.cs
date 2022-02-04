using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dumb_lang_test.Instructions
{
    class WriteRandom : Interfaces.IBasicInstruction
    {
        static readonly Random rnd = new();

        public void Execute()
        {
            Program.SetMemory((byte)rnd.Next(256));
        }
    }
}
