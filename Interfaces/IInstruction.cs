namespace dumb_lang_test.Interfaces;

internal interface IInstruction<T> : IBasicInstruction
{
    public void FillParameters(List<T> parameters = null);
}