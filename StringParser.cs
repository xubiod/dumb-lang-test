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

                    if (new_instr.GetType() == typeof(Instructions.OnZero) && args.Length > 0)
                    {
                        ((Instructions.OnZero)new_instr).ExecutedOnSuccess.Add((Interfaces.IBasicInstruction)Activator.CreateInstance(str_basic[args[0]]));
                    }
                    else if (new_instr.GetType() == typeof(Instructions.InTopHalf) && args.Length > 0)
                    {
                        ((Instructions.InTopHalf)new_instr).ExecutedOnSuccess.Add((Interfaces.IBasicInstruction)Activator.CreateInstance(str_basic[args[0]]));
                    }
                    else if (new_instr.GetType() == typeof(Instructions.Addi))
                    {
                        List<byte> nums = new();

                        foreach (var arg in args)
                        {
                            if (byte.TryParse(arg, out byte o))
                            {
                                nums.Add(o);
                            }
                        }

                        ((Instructions.Addi)new_instr).FillParameters(nums);
                    }
                    else if (new_instr.GetType() == typeof(Instructions.Group) && args.Length > 1 && args.Length < 4)
                    {
                        for (int _ = 0; _ < args.Length; _++)
                        {
                            ((Instructions.Group)new_instr).AddParameter((Interfaces.IBasicInstruction)Activator.CreateInstance(str_basic[args[_]]));
                        }
                    }

                    completed_instructions.Add(new_instr);
                }
            }

            return completed_instructions;
        }

        static readonly Dictionary<string, Type> str_basic = new()
        {
            {"pkjmp", typeof(Instructions.Pointer.PeekJump) }, // {"", typeof(Pointer.PointerInstructionPeekJump) },
            {"reset", typeof(Instructions.Pointer.Reset) },         {"0", typeof(Instructions.Pointer.Reset) },
            {"shftl", typeof(Instructions.Pointer.ShiftLeft) },     {"<", typeof(Instructions.Pointer.ShiftLeft) },
            {"shftr", typeof(Instructions.Pointer.ShiftRight) },    {">", typeof(Instructions.Pointer.ShiftRight) },

            {"andr", typeof(Instructions.BitWiseAndR) },            {"&", typeof(Instructions.BitWiseAndR) },
            {"compl", typeof(Instructions.BitWiseComplement) },     {"~", typeof(Instructions.BitWiseComplement) },
            {"orr", typeof(Instructions.BitWiseOrR) },              {"|", typeof(Instructions.BitWiseOrR) },
            {"xorr", typeof(Instructions.BitWiseXorR) },            {"^", typeof(Instructions.BitWiseXorR) },
            {"bumpd", typeof(Instructions.BumpDown) },              {"j", typeof(Instructions.BumpDown) },
            {"bumpu", typeof(Instructions.BumpUp) },                {"k", typeof(Instructions.BumpUp) },
            {"cpyfl", typeof(Instructions.CopyFromL) },
            {"noop", typeof(Instructions.Noop) },                   {"-", typeof(Instructions.Noop) },
            {"prity", typeof(Instructions.Parity) },                {"%", typeof(Instructions.Parity) },
            {"randm", typeof(Instructions.WriteRandom) },           {"?", typeof(Instructions.WriteRandom) },
            {"read", typeof(Instructions.Read) },                   {"i", typeof(Instructions.Read) },
            {"rstrt", typeof(Instructions.Restart) },               {"@", typeof(Instructions.Restart) },
            {"skip", typeof(Instructions.Skip) },                   {".", typeof(Instructions.Skip) },
            {"halt", typeof(Instructions.Terminate) },              {"!", typeof(Instructions.Terminate) },
            {"wrptr", typeof(Instructions.WritePointer) },          {"v", typeof(Instructions.WritePointer)}
        };

        static readonly Dictionary<string, Type> str_nonbasic = new()
        {
            {"whenz", typeof(Instructions.OnZero) },                {"z", typeof(Instructions.OnZero) },
            {"whnth", typeof(Instructions.InTopHalf) },             {"t", typeof(Instructions.InTopHalf)} ,
            {"addi", typeof(Instructions.Addi) },
            {"group", typeof(Instructions.Group) },                 {"g", typeof(Instructions.Group) }
        };
    }
}
