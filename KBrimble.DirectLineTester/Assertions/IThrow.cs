namespace KBrimble.DirectLineTester.Assertions
{
    internal interface IThrow<out T>
    {
        T CreateEx(string testedProperty, string regex);
    }
}
