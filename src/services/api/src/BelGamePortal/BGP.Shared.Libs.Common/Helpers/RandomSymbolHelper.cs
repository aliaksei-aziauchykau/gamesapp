namespace BGP.Shared.Libs.Common.Helpers;


public class RandomSymbolHelper
{
    private static readonly Random _random = new();

    public static string GetRandomSymbols(int length = 8)
    {
        var bank = "abcdefghijklmnopqrstuvwxyz";

        var result = string.Empty;

        for (int i = 0; i < length; i++)
        {
            var position = _random.Next(26);
            var symbol = bank.ElementAt(position).ToString();
            //var symbolToAdd = _random.Next(100) % 2 == 0 ? symbol.ToUpper() : symbol.ToLower();
            result = result + symbol;
        }

        return result;
    }
}
