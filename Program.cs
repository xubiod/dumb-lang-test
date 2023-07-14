using System;
using System.IO;
using System.Linq;
using dumb_lang_test.Interfaces;

namespace dumb_lang_test;

internal class Program
{
    public const int MemorySize = 256;

    public static int MemoryPointer = 0;
    public static bool Skip;
    public static bool Halt = false;
    public static int InstructionPointer;
    public static readonly int SpecialIndex = 0xFF;

    private static readonly byte[] Memory = new byte[MemorySize];
    private static int _cycles;
    private static readonly int MaxCycles = 500000;
    private static string _program = "";

    private static void Main(string[] args)
    {
        switch (args[0].ToLower())
        {
            case "parse":
            {
                args = args.Skip(1).ToArray();
                _program = string.Join(' ', args);
                break;
            }
            case "file":
            {
                args = args.Skip(1).ToArray();
                var filename = string.Join(' ', args);
                _program = File.ReadAllText(filename);

                break;
            }
        }

        int _;
        for (_ = 0; _ < Memory.Length; _++)
        {
            Memory[_] = 0x00;
        }

        var instructions = StringParser.ParseString(_program);

        var start = DateTime.Now;

        for (InstructionPointer = 0; InstructionPointer < instructions.Count && _cycles < MaxCycles && !Halt; InstructionPointer++)
        {
            if (!Skip) instructions[InstructionPointer].Execute();
            else Skip = false;
            _cycles++;
        }

        var execTime = DateTime.Now - start;

        PrintFullMem();
        Console.WriteLine("{0:D} out of {1} maximum cycles\nTook {2} ticks ({3}ms)", _cycles, MaxCycles, execTime.Ticks, execTime.TotalMilliseconds);
    }

    public static void SetMemory(byte memory)
    {
        Memory[MemoryPointer] = memory;
    }

    public static void SetMemorySpecial(byte memory)
    {
        Memory[SpecialIndex] = memory;
    }

    public static void ShiftMemory(byte shift)
    {
        Memory[MemoryPointer] += shift;
    }

    public static byte GetMemory() => Memory[MemoryPointer];

    public static byte GetMemoryRightOf() => Memory[(MemoryPointer + 1) % MemorySize];

    public static byte GetMemoryLeftOf() => Memory[MemoryPointer - 1  == -1 ? 255 : MemoryPointer - 1];

    public static byte GetMemorySpecial() => Memory[SpecialIndex];

    public static void PrintFullMem()
    {
        Console.WriteLine("Memory readout ({0:D} bytes)", MemorySize);

        var defaultFcolor = Console.ForegroundColor;
        var defaultBcolor = Console.BackgroundColor;

        Console.Write("     ");
        for (var k = 0; k < 16; k++)
        {
            Console.Write(" x{0:X}", k);
        }
        Console.WriteLine();

        for (var i = 0; i < MemorySize / 16; i++)
        {
            Console.Write("  {0:X}x ", i);
            for (var j = 0; j < 16; j++)
            {
                Console.ForegroundColor = i * 16 + j == SpecialIndex ? ConsoleColor.White : i * 16 + j == MemoryPointer ? ConsoleColor.White : defaultFcolor;
                Console.BackgroundColor = i * 16 + j == SpecialIndex ? ConsoleColor.DarkMagenta : i * 16 + j == MemoryPointer ? ConsoleColor.DarkGreen : defaultBcolor;
                Console.Write(" {0,2:X}", (int)Memory[i * 16 + j]);

                Console.ForegroundColor = defaultFcolor;
                Console.BackgroundColor = defaultBcolor;
            }

            Console.WriteLine("  {0:X}x", i);
        }
    }

    public static void PrintMemoryPtrDetail()
    {
        Console.Write("[{0:X}] @ mp {1:X} ", Memory[MemoryPointer], MemoryPointer);
    }

    public static void AddCycles(int cycles)
    {
        _cycles += cycles;
    }
}