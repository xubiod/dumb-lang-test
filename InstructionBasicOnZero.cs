using dumb_lang_test.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dumb_lang_test
{
    class InstructionBasicOnZero : Interfaces.IInstruction<Interfaces.IBasicInstruction>
    {
        public List<IBasicInstruction> ExecutedOnSuccess = new();

        public InstructionBasicOnZero()
        {
            return;
        }

        public InstructionBasicOnZero(IBasicInstruction toRunOnZero)
        {
            ExecutedOnSuccess.Add(toRunOnZero);
        }

        public void Execute()
        {
            if (Program.GetMemory() == 0)
            {
                ExecutedOnSuccess[0].Execute();
            }
        }

        public void FillParameters(List<IBasicInstruction> parameters = null)
        {
            throw new NotImplementedException();
        }
    }
}
