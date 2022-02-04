using dumb_lang_test.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dumb_lang_test.Instructions
{
    class Group : Interfaces.IInstruction<Interfaces.IBasicInstruction>
    {
        readonly List<Interfaces.IBasicInstruction> grouped = new();

        public void Execute()
        {
            foreach (Interfaces.IBasicInstruction instruction in grouped)
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
