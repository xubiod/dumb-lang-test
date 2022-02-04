using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dumb_lang_test.Instructions
{
    internal class JumpOffset : Interfaces.IInstruction<byte>
    {
        byte line_offset;

        public void Execute()
        {
            bool signed = (line_offset & 0x80) == 0x80;
            
            if (signed) Program.instruction_pointer -= line_offset;
            else Program.instruction_pointer += line_offset;
        }

        public void FillParameters(List<byte> parameters = null)
        {
            line_offset = parameters[0];
        }

        public void SetParameter(byte parameter)
        {
            line_offset = parameter;
        }
    }
}
