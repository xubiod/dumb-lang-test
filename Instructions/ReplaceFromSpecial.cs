using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dumb_lang_test.Instructions
{
    internal class ReplaceFromSpecial : Interfaces.ISpecialInstruction
    {
        public void Execute()
        {
            Program.SetMemory(Program.GetMemorySpecial());
        }
    }
}
