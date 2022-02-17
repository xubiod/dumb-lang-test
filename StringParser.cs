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
        public static ParserOptions options;

        public static List<Interfaces.IBasicInstruction> ParseString(string program)
        {
            options = new ParserOptions();
            completed_instructions.Clear();

            if (program.StartsWith("#env"))
            {
                string envOptions = program.Substring(0, program.IndexOf("#envstop") + "#envstop".Length);

                options.AllowPseudoInstructions = !program.Contains("disallow-pseudo-instructions");
                options.AllowUserSpecials = !program.Contains("disallow-user-specials");
                options.StrictParsing = program.Contains("strict-parse");
                options.DebugState = program.Contains("enable-debug");

                program = program.Replace(envOptions, "");
            }

            if (!options.AllowUserSpecials)
            {
                foreach (string key in str_special.Keys)
                {
                    program = program.Replace(key, "noop");
                }
            }

            if (options.AllowPseudoInstructions)
            {
                foreach (string key in pseudo_replacements.Keys)
                {
                    program = program.Replace(key, pseudo_replacements[key]);
                }
            }

            string cleaned_line;
            string opc;
            int unrecognized = 0;

            DateTime _started = DateTime.Now;

            foreach (string line in program.Split('\n',';'))
            {
                cleaned_line = Regex.Replace(line, @"[\s\r]+(\/\/.*)", "").Trim();

                opc = cleaned_line.Split(' ')[0];

                if (str_basic.ContainsKey(cleaned_line))
                {
                    completed_instructions.Add((Interfaces.IBasicInstruction)Activator.CreateInstance(str_basic[cleaned_line]));
                }
                else if (str_special.ContainsKey(cleaned_line))
                {
                    completed_instructions.Add((Interfaces.IBasicInstruction)Activator.CreateInstance(str_special[cleaned_line]));
                }
                else if (str_nonbasic.ContainsKey(opc))
                {
                    Interfaces.IBasicInstruction new_instr = (Interfaces.IBasicInstruction)Activator.CreateInstance(str_nonbasic[opc]);

                    string[] args = cleaned_line.Split(' ')[1].Split(',');

                    if (new_instr.GetType() == typeof(Instructions.OnZero) && args.Length == 1)
                    {
                        ((Instructions.OnZero)new_instr).ExecutedOnSuccess.Add((Interfaces.IBasicInstruction)Activator.CreateInstance(str_basic[args[0]]));
                    }
                    else if (new_instr.GetType() == typeof(Instructions.InTopHalf) && args.Length == 1)
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
                    else if (new_instr.GetType() == typeof(Instructions.JumpOffsetFine) && args.Length == 1)
                    {
                        if (sbyte.TryParse(args[0], out sbyte o))
                        {
                            ((Instructions.JumpOffsetFine)new_instr).SetParameter(o);
                        }
                    }
                    else if (new_instr.GetType() == typeof(Instructions.JumpOffsetCoarse) && args.Length == 1)
                    {
                        if (sbyte.TryParse(args[0], out sbyte o))
                        {
                            ((Instructions.JumpOffsetCoarse)new_instr).SetParameter(o);
                        }
                    }

                    completed_instructions.Add(new_instr);
                }
                else
                {
                    if (options.StrictParsing)
                    {
                        System.Console.WriteLine("Parse: {0} unrecognized, halting", opc);
                        return new List<Interfaces.IBasicInstruction>();
                    }
                    else
                    {
                        System.Console.WriteLine("Parse: {0} unrecognized, ignoring", opc);
                        unrecognized++;
                    }
                }
            }

            TimeSpan parse_time = DateTime.Now - _started;

            System.Console.WriteLine("Parse: Complete, {0} instructions loaded, {1} unrecognized skipped, {2} ticks ({3}ms)", completed_instructions.Count, unrecognized, parse_time.Ticks, parse_time.TotalMilliseconds);

            return completed_instructions;
        }

        static readonly Dictionary<string, Type> str_basic = new()
        {
            { "pkjmp", typeof(Instructions.Pointer.PeekJump) },      { "$", typeof(Instructions.Pointer.PeekJump) },
            { "reset", typeof(Instructions.Pointer.Reset) },         { "0", typeof(Instructions.Pointer.Reset) },
            { "shftl", typeof(Instructions.Pointer.ShiftLeft) },     { "<", typeof(Instructions.Pointer.ShiftLeft) },
            { "shftr", typeof(Instructions.Pointer.ShiftRight) },    { ">", typeof(Instructions.Pointer.ShiftRight) },

            { "andr", typeof(Instructions.BitWiseAndR) },            { "&", typeof(Instructions.BitWiseAndR) },
            { "compl", typeof(Instructions.BitWiseComplement) },     { "~", typeof(Instructions.BitWiseComplement) },
            { "orr", typeof(Instructions.BitWiseOrR) },              { "|", typeof(Instructions.BitWiseOrR) },
            { "xorr", typeof(Instructions.BitWiseXorR) },            { "^", typeof(Instructions.BitWiseXorR) },
            { "bumpd", typeof(Instructions.BumpDown) },              { "j", typeof(Instructions.BumpDown) },
            { "bumpu", typeof(Instructions.BumpUp) },                { "k", typeof(Instructions.BumpUp) },
            { "cpyfl", typeof(Instructions.CopyFromL) },
            { "noop", typeof(Instructions.Noop) },                   { "-", typeof(Instructions.Noop) },
            { "prity", typeof(Instructions.Parity) },                { "%", typeof(Instructions.Parity) },
            { "randm", typeof(Instructions.WriteRandom) },           { "?", typeof(Instructions.WriteRandom) },
            { "read", typeof(Instructions.Read) },                   { "i", typeof(Instructions.Read) },
            { "rstrt", typeof(Instructions.Restart) },               { "@", typeof(Instructions.Restart) },
            { "skip", typeof(Instructions.Skip) },                   { ".", typeof(Instructions.Skip) },
            { "halt", typeof(Instructions.Terminate) },              { "!", typeof(Instructions.Terminate) },
            { "wrptr", typeof(Instructions.WritePointer) },          { "v", typeof(Instructions.WritePointer) }
        };

        static readonly Dictionary<string, Type> str_special = new()
        {
            { "rtspl", typeof(Instructions.ReplaceToSpecial) },
            { "rfspl", typeof(Instructions.ReplaceFromSpecial) },
            { "mpspl", typeof(Instructions.MemPointerToSpecial) },
            { "jpspl", typeof(Instructions.SpecialJump) }
        };

        static readonly Dictionary<string, Type> str_nonbasic = new()
        {
            { "whenz", typeof(Instructions.OnZero) },                { "z", typeof(Instructions.OnZero) },
            { "whnth", typeof(Instructions.InTopHalf) },             { "t", typeof(Instructions.InTopHalf) },
            { "addi", typeof(Instructions.Addi) },
            { "group", typeof(Instructions.Group) },                 { "g", typeof(Instructions.Group) },
            { "jmpof", typeof(Instructions.JumpOffsetFine) },
            { "jmpoc", typeof(Instructions.JumpOffsetCoarse) }
        };

        static readonly Dictionary<string, string> pseudo_replacements = new()
        {
            { "andl", "shftl;rtspl;andr;shftr;cpyfl;shftl;rfspl;shftr" },
            { "orl", "shftl;rtspl;orr;shftr;cpyfl;shftl;rfspl;shftr" },
            { "xorl", "shftl;rtspl;xorr;shftr;cpyfl;shftl;rfspl;shftr" },
            { "swapr", "shftr;rtspl;cpyfl;shftl;rfspl" },
            { "jpspl", "reset;shftl" }
        };
    }
}
