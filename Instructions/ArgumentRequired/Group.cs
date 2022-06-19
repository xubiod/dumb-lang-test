using dumb_lang_test.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dumb_lang_test.Instructions.ArgumentRequired
{
    class Group : IInstruction<IBasicInstruction>
    {
        readonly List<IBasicInstruction> grouped = new();

        public void Execute()
        {
            foreach (IBasicInstruction instruction in grouped)
            {
                instruction.Execute();
                Program.AddCycles(3);
            }
        }

        public void FillParameters(List<IBasicInstruction> parameters = null)
        {
            grouped.AddRange(parameters);
        }

        public void AddParameter(IBasicInstruction parameter)
        {
            grouped.Add(parameter);
        }
    }
}
