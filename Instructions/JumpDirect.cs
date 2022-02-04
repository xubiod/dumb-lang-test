using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dumb_lang_test.Instructions
{
    internal class JumpDirect : Interfaces.IInstruction<uint>
    {
        uint line;

        public void Execute()
        {
            Program.instruction_pointer = ((int)line) - 1;
        }

        public void FillParameters(List<uint> parameters = null)
        {
            line = parameters[0];
        }

        public void SetParameter(uint parameter)
        {
            line = parameter;
        }
    }
}
