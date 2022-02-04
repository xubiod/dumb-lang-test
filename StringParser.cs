using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace dumb_lang_test
{
    class StringParser
    {
        static List<Interfaces.IBasicInstruction> completed_instructions = new();

        public static List<Interfaces.IBasicInstruction> ParseString(string program)
        {
            completed_instructions.Clear();

            string cleaned_line;
            string opc;
            foreach (string line in program.Split('\n',';'))
            {
                cleaned_line = Regex.Replace(line, @"[\s\r]+(\/\/.*)", "").Trim();
                opc = cleaned_line.Split(' ')[0];

                if (str_basic.ContainsKey(cleaned_line))
                {
                    completed_instructions.Add((Interfaces.IBasicInstruction)Activator.CreateInstance(str_basic[cleaned_line]));
                }
                else if (str_nonbasic.ContainsKey(opc))
                {
                    Interfaces.IBasicInstruction new_instr = (Interfaces.IBasicInstruction)Activator.CreateInstance(str_nonbasic[opc]);

                    string[] args = cleaned_line.Split(' ')[1].Split(',');

                    if (new_instr.GetType() == typeof(InstructionBasicOnZero) && args.Length > 0)
                    {
                        ((InstructionBasicOnZero)new_instr).ExecutedOnSuccess.Add((Interfaces.IBasicInstruction)Activator.CreateInstance(str_basic[args[0]]));
                    }
                    else if (new_instr.GetType() == typeof(InstructionOnTopHalf) && args.Length > 0)
                    {
                        ((InstructionOnTopHalf)new_instr).ExecutedOnSuccess.Add((Interfaces.IBasicInstruction)Activator.CreateInstance(str_basic[args[0]]));
                    }
                    else if (new_instr.GetType() == typeof(InstructionAddi))
                    {
                        List<byte> nums = new();

                        foreach (var arg in args)
                        {
                            if (byte.TryParse(arg, out byte o))
                            {
                                nums.Add(o);
                            }
                        }

                        ((InstructionAddi)new_instr).FillParameters(nums);
                    }
                    else if (new_instr.GetType() == typeof(InstructionGroup) && args.Length > 1 && args.Length < 4)
                    {
                        for (int _ = 0; _ < args.Length; _++)
                        {
                            ((InstructionGroup)new_instr).AddParameter((Interfaces.IBasicInstruction)Activator.CreateInstance(str_basic[args[_]]));
                        }
                    }

                    completed_instructions.Add(new_instr);
                }
            }

            return completed_instructions;
        }

        static readonly Dictionary<string, Type> str_basic = new Dictionary<string, Type>()
        {
            {"pkjmp", typeof(Pointer.PointerInstructionPeekJump) },
            {"reset", typeof(Pointer.PointerInstructionReset) },    {"0", typeof(Pointer.PointerInstructionReset) },
            {"shftl", typeof(Pointer.PointerInstructionShiftL) },   {"<", typeof(Pointer.PointerInstructionShiftL) },
            {"shftr", typeof(Pointer.PointerInstructionShiftR) },   {">", typeof(Pointer.PointerInstructionShiftR) },

            {"andr", typeof(InstructionBitWiseAndR) },              {"&", typeof(InstructionBitWiseAndR) },
            {"compl", typeof(InstructionBitWiseComplement) },       {"~", typeof(InstructionBitWiseComplement) },
            {"orr", typeof(InstructionBitWiseOrR) },                {"|", typeof(InstructionBitWiseOrR) },
            {"xorr", typeof(InstructionBitWiseXorR) },              {"^", typeof(InstructionBitWiseXorR) },
            {"bumpd", typeof(InstructionBumpDown) },                {"j", typeof(InstructionBumpDown) },
            {"bumpu", typeof(InstructionBumpUp) },                  {"k", typeof(InstructionBumpUp) },
            {"cpytr", typeof(InstructionCopyToR) },
            {"noop", typeof(InstructionNoop) },                     {"-", typeof(InstructionNoop) },
            {"prity", typeof(InstructionParity) },                  {"%", typeof(InstructionParity) },
            {"randm", typeof(InstructionRandom) },                  {"?", typeof(InstructionRandom) },
            {"read", typeof(InstructionRead) },                     {"i", typeof(InstructionRead) },
            {"rstrt", typeof(InstructionRestart) },                 {"@", typeof(InstructionRestart) },
            {"skip", typeof(InstructionSkip) },                     {".", typeof(InstructionSkip) },
            {"halt", typeof(InstructionTerminate) },                {"!", typeof(InstructionTerminate) },
            {"wrptr", typeof(InstructionWritePointer) }
        };

        static readonly Dictionary<string, Type> str_nonbasic = new Dictionary<string, Type>()
        {
            {"whenz", typeof(InstructionBasicOnZero) },             {"z", typeof(InstructionBasicOnZero) },
            {"whnth", typeof(InstructionOnTopHalf) },
            {"addi", typeof(InstructionAddi) },
            {"group", typeof(InstructionGroup) },                   {"g", typeof(InstructionGroup) }
        };
    }
}
