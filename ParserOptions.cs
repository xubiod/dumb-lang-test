using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dumb_lang_test
{
    class ParserOptions
    {
        public bool AllowUserSpecials = true;
        public bool AllowPseudoInstructions = true;
        public bool DebugState = false;
        public bool StrictParsing = false;
    }
}
