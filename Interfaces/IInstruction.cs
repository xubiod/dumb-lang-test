using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dumb_lang_test.Interfaces
{
    interface IInstruction<T> : IBasicInstruction
    {
        public void FillParameters(List<T> parameters = null);
    }
}
