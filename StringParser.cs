using System;
using System.Linq;
using System.Text.RegularExpressions;
using dumb_lang_test.Instructions;
using dumb_lang_test.Instructions.ArgumentRequired;
using dumb_lang_test.Instructions.Pointer;
using dumb_lang_test.Interfaces;
using Group = dumb_lang_test.Instructions.ArgumentRequired.Group;

namespace dumb_lang_test;

internal static class StringParser
{
	private static readonly List<IBasicInstruction> CompletedInstructions = new();
	private static ParserOptions _options;

	public static List<IBasicInstruction> ParseString(string program)
	{
		_options = new ParserOptions();
		CompletedInstructions.Clear();

		if (program.StartsWith("#env"))
		{
			var envOptions = program[..(program.IndexOf("#envstop", StringComparison.Ordinal) + "#envstop".Length)];

			_options.AllowPseudoInstructions = !program.Contains("disallow-pseudo-instructions");
			_options.AllowUserSpecials = !program.Contains("disallow-user-specials");
			_options.StrictParsing = program.Contains("strict-parse");
			_options.DebugState = program.Contains("enable-debug");

			program = program.Replace(envOptions, "");
		}

		if (!_options.AllowUserSpecials)
		{
			program = StrSpecial.Keys.Aggregate(program, (current, key) => current.Replace(key, "noop"));
		}

		if (_options.AllowPseudoInstructions)
		{
			program = PseudoReplacements.Keys.Aggregate(program, (current, key) => current.Replace(key, PseudoReplacements[key]));
		}

		var unrecognized = 0;

		var started = DateTime.Now;

		foreach (var line in program.Split('\n',';'))
		{
			var cleanedLine = Regex.Replace(line, @"[\s\r]+(\/\/.*)", "").Trim();

			var opc = cleanedLine.Split(' ')[0];

			if (StrBasic.TryGetValue(cleanedLine, out var value))
			{
				CompletedInstructions.Add((IBasicInstruction)Activator.CreateInstance(value));
			}
			else if (StrSpecial.TryGetValue(cleanedLine, out var value1))
			{
				CompletedInstructions.Add((IBasicInstruction)Activator.CreateInstance(value1));
			}
			else if (StrNonbasic.TryGetValue(opc, out var value2))
			{
				var newInstr = (IBasicInstruction)Activator.CreateInstance(value2);

				var args = cleanedLine.Split(' ')[1].Split(',');

				if (newInstr != null && newInstr.GetType() == typeof(OnZero) && args.Length == 1)
				{
					((OnZero)newInstr).ExecutedOnSuccess.Add((IBasicInstruction)Activator.CreateInstance(StrBasic[args[0]]));
				}
				else if (newInstr != null && newInstr.GetType() == typeof(InTopHalf) && args.Length == 1)
				{
					((InTopHalf)newInstr).ExecutedOnSuccess.Add((IBasicInstruction)Activator.CreateInstance(StrBasic[args[0]]));
				}
				else if (newInstr != null && newInstr.GetType() == typeof(Addi))
				{
					List<byte> nums = new();

					foreach (var arg in args)
					{
						if (byte.TryParse(arg, out var o))
						{
							nums.Add(o);
						}
					}

					((Addi)newInstr).FillParameters(nums);
				}
				else if (newInstr != null && newInstr.GetType() == typeof(Group) && args.Length is > 1 and < 4)
				{
					foreach (var t in args)
					{
						((Group)newInstr).AddParameter((IBasicInstruction)Activator.CreateInstance(StrBasic[t]));
					}
				}
				else if (newInstr != null && newInstr.GetType() == typeof(JumpOffsetFine) && args.Length == 1)
				{
					if (sbyte.TryParse(args[0], out var o))
					{
						((JumpOffsetFine)newInstr).SetParameter(o);
					}
				}
				else if (newInstr != null && newInstr.GetType() == typeof(JumpOffsetCoarse) && args.Length == 1)
				{
					if (sbyte.TryParse(args[0], out var o))
					{
						((JumpOffsetCoarse)newInstr).SetParameter(o);
					}
				}

				CompletedInstructions.Add(newInstr);
			}
			else
			{
				if (_options.StrictParsing)
				{
					Console.WriteLine("Parse: {0} unrecognized, halting", opc);
					return new List<IBasicInstruction>();
				}

				Console.WriteLine("Parse: {0} unrecognized, ignoring", opc);
				unrecognized++;
			}
		}

		var parseTime = DateTime.Now - started;

		Console.WriteLine("Parse: Complete, {0} instructions loaded, {1} unrecognized skipped, {2} ticks ({3}ms)", CompletedInstructions.Count, unrecognized, parseTime.Ticks, parseTime.TotalMilliseconds);

		return CompletedInstructions;
	}

	private static readonly Dictionary<string, Type> StrBasic = new()
	{
		{ "pkjmp", typeof(PeekJump) },     { "$", typeof(PeekJump) },
		{ "reset", typeof(Reset) },        { "0", typeof(Reset) },
		{ "shftl", typeof(ShiftLeft) },    { "<", typeof(ShiftLeft) },
		{ "shftr", typeof(ShiftRight) },   { ">", typeof(ShiftRight) },

		{ "andr",  typeof(BitWiseAndR) },          { "&", typeof(BitWiseAndR) },
		{ "compl", typeof(BitWiseComplement) },    { "~", typeof(BitWiseComplement) },
		{ "orr",   typeof(BitWiseOrR) },           { "|", typeof(BitWiseOrR) },
		{ "xorr",  typeof(BitWiseXorR) },          { "^", typeof(BitWiseXorR) },
		{ "bumpd", typeof(BumpDown) },             { "j", typeof(BumpDown) },
		{ "bumpu", typeof(BumpUp) },               { "k", typeof(BumpUp) },
		{ "cpyfl", typeof(CopyFromL) },			{ "c", typeof(CopyFromL) },
		{ "noop",  typeof(Noop) },                 { "_", typeof(Noop) },
		{ "prity", typeof(Parity) },               { "%", typeof(Parity) },
		{ "randm", typeof(WriteRandom) },          { "?", typeof(WriteRandom) },
		{ "read",  typeof(Read) },                 { "i", typeof(Read) },
		{ "rstrt", typeof(Restart) },              { "@", typeof(Restart) },
		{ "skip",  typeof(Skip) },                 { ".", typeof(Skip) },
		{ "halt",  typeof(Terminate) },            { "!", typeof(Terminate) },
		{ "wrptr", typeof(WritePointer) },         { "v", typeof(WritePointer) }
	};

	private static readonly Dictionary<string, Type> StrSpecial = new()
	{
		{ "rtspl", typeof(ReplaceToSpecial) },
		{ "rfspl", typeof(ReplaceFromSpecial) },
		{ "mpspl", typeof(MemPointerToSpecial) },
		{ "jpspl", typeof(SpecialJump) }
	};

	private static readonly Dictionary<string, Type> StrNonbasic = new()
	{
		{ "whenz", typeof(OnZero) },               { "z", typeof(OnZero) },
		{ "whnth", typeof(InTopHalf) },            { "t", typeof(InTopHalf) },
		{ "addi",  typeof(Addi) },                 { "+", typeof(Addi) },
		{ "group", typeof(Group) },                { "g", typeof(Group) },
		{ "jmpof", typeof(JumpOffsetFine) },
		{ "jmpoc", typeof(JumpOffsetCoarse) }
	};

	private static readonly Dictionary<string, string> PseudoReplacements = new()
	{
		{ "andl", "shftl;rtspl;andr;shftr;cpyfl;shftl;rfspl;shftr" },
		{ "orl", "shftl;rtspl;orr;shftr;cpyfl;shftl;rfspl;shftr" },
		{ "xorl", "shftl;rtspl;xorr;shftr;cpyfl;shftl;rfspl;shftr" },
		{ "swapr", "shftr;rtspl;cpyfl;shftl;rfspl" },
		{ "jpspl", "reset;shftl" }
	};
}