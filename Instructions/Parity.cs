using dumb_lang_test.Interfaces;

namespace dumb_lang_test.Instructions;

internal class Parity : IBasicInstruction
{
    public void Execute()
    {
        //byte parity = 0;
        //byte check = Program.GetMemory();

        //while (check != 0)
        //{
        //    parity = (byte)~parity;
        //    check = (byte)(check & (check - 1));
        //}

        Program.SetMemory((byte)(Program.GetMemory() % 2 * 0xFF));
    }
}