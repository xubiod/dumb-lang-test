using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dumb_lang_test.Instructions
{
    internal class JumpOffsetCoarse : Interfaces.IInstruction<sbyte>
    {
        sbyte line_offset;

        public void Execute()
        {
            Program.instruction_pointer += line_offset * 128;
        }

        public void FillParameters(List<sbyte> parameters = null)
        {
            line_offset = parameters[0];
        }

        public void SetParameter(sbyte parameter)
        {
            line_offset = parameter;
        }
    }
}
