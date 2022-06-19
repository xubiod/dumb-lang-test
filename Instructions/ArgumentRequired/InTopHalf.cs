using dumb_lang_test.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dumb_lang_test.Instructions.ArgumentRequired
{
    class InTopHalf : IInstruction<IBasicInstruction>
    {
        public List<IBasicInstruction> ExecutedOnSuccess = new();

        public InTopHalf()
        {
            return;
        }

        public InTopHalf(IBasicInstruction toRunOnZero)
        {
            ExecutedOnSuccess.Add(toRunOnZero);
        }

        public void Execute()
        {
            if (Program.MemoryPointer < 0x80)
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
