using Sharprompt;

namespace ConsoleAppConsumoAPIsOtel.Inputs;

public class InputHelper
{
    private const string SIM = "Sim";
    private const string NAO = "Nao";

    public static bool Continue()
    {
        Console.WriteLine();
        var answer = Prompt.Select<string>(options =>
        {
            options.Message = "Realizar um novo teste?";
            options.Items = [SIM, NAO];
        });
        Console.WriteLine();
        return answer == SIM;
    }
}