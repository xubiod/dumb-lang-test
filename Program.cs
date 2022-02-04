using System;
using System.Collections.Generic;

namespace dumb_lang_test
{
    class Program
    {
        public const int MEMORY_SIZE = 256;

        private static byte[] memory = new byte[MEMORY_SIZE];
        public static int MemoryPointer = 0;
        public static bool skip = false;
        public static bool halt = false;
        public static int instruction_pointer = 0;
        private static int cycles = 0;
        private static readonly int max_cycles = 500000;

        static void Main(string[] args)
        {
            int _;
            for (_ = 0; _ < memory.Length; _++)
            {
                memory[_] = 0x00;
            }

            List<Interfaces.IBasicInstruction> instructions = StringParser.ParseString(@"k;>;whnth @");

            DateTime _start = DateTime.Now;

            for (instruction_pointer = 0; (instruction_pointer < instructions.Count) && cycles < max_cycles && !halt; instruction_pointer++)
            {
                if (!skip) instructions[instruction_pointer].Execute();
                else skip = false;
                cycles++;
            }

            TimeSpan exec_time = DateTime.Now - _start;

            PrintFullMem();
            System.Console.WriteLine("{0:D} out of {1} maximum cycles\nTook {2} ticks ({3}ms)", cycles, max_cycles, exec_time.Ticks, exec_time.TotalMilliseconds);
        }

        public static void SetMemory(byte memory)
        {
            Program.memory[MemoryPointer] = memory;
        }

        public static void SetMemoryRightOf(byte memory)
        {
            Program.memory[MemoryPointer] = memory;
        }

        public static void ShiftMemory(byte shift)
        {
            memory[MemoryPointer] += shift;
        }

        public static byte GetMemory() => memory[MemoryPointer];

        public static byte GetMemoryRightOf() => memory[(MemoryPointer + 1) % MEMORY_SIZE];

        public static byte GetMemoryLeftOf() => memory[MemoryPointer - 1  == -1 ? 255 : MemoryPointer - 1];

        public static void PrintFullMem()
        {
            System.Console.WriteLine("\nMemory readout ({0:D} bytes)", MEMORY_SIZE);

            System.Console.Write("     ");
            for (int k = 0; k < 16; k++)
            {
                System.Console.Write(" x{0:X}", k);
            }
            System.Console.WriteLine();

            for (int i = 0; i < (MEMORY_SIZE / 16); i++)
            {
                System.Console.Write("  {0:X}x ", i);
                for (int j = 0; j < 16; j++)
                {
                    System.Console.Write(" {0,2:X}", (int)memory[(i * 16) + j]);
                }

                System.Console.WriteLine("  {0:X}x", i);
            }
        }

        public static void PrintMemoryPtrDetail()
        {
            Console.Write("[{0:X}] @ mp {1:X} ", memory[MemoryPointer], MemoryPointer);
        }

        public static void AddCycles(int cycles)
        {
            Program.cycles += cycles;
        }
    }
}
