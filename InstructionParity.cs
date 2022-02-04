using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dumb_lang_test
{
    class InstructionParity : Interfaces.IBasicInstruction
    {
        public void Execute()
        {
            //byte parity = 0;
            //byte check = Program.GetMemory();

            //while (check != 0)
            //{
            //    parity = (byte)~parity;
            //    check = (byte)(check & (check - 1));
            //}

            Program.SetMemory((byte)((Program.GetMemory() % 2) * 0xFF));
        }
    }
}
