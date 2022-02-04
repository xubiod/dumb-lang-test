using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dumb_lang_test
{
    class InstructionAddi : Interfaces.IInstruction<byte>
    {
        List<byte> immediates = new();

        public InstructionAddi()
        {
            return;
        }

        public InstructionAddi(List<byte> immediates)
        {
            this.immediates = immediates;
        }

        public void Execute()
        {
            byte result = Program.GetMemory();

            foreach (byte number in immediates)
            {
                result += number;
            }

            Program.SetMemory(result);
        }

        public void FillParameters(List<byte> parameters = null)
        {
            immediates.AddRange(parameters);
        }
    }
}
