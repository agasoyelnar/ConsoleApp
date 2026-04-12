namespace CourseSystem.Helpers;

public static class Helper
{
    public static void PrintConsole(ConsoleColor color, string text)
    {
        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ResetColor();
    }
}